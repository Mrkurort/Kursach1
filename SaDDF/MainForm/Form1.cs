using MainForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MainForm
{
    public partial class SaDDF : Form
    {

        private DuplicateFinder _finder;
        private DuplicateManager _manager;
        private List<DuplicateGroup> _duplicates;
        private CancellationTokenSource _cancellationTokenSource;


        public SaDDF()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            _finder = new DuplicateFinder(new List<string>());
            _manager = new DuplicateManager();
            _duplicates = new List<DuplicateGroup>();

            // Настройка контролов
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Visible = false;
            lblStatus.Text = "Готов к работе";

            // Настройка DataGridView
            dgvDuplicates.AutoGenerateColumns = false;
            dgvDuplicates.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Настройка стратегий удаления
            cmbDeleteStrategy.Items.AddRange(new[] {
                "Сохранить самые старые файлы",
                "Сохранить самые новые файлы",
                "Сохранить первые найденные"
            });
            cmbDeleteStrategy.SelectedIndex = 1;
        }

        private void btnAddPath_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку для поиска дубликатов";
                folderDialog.ShowNewFolderButton = false;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    var path = folderDialog.SelectedPath;
                    if (!lstSearchPaths.Items.Contains(path))
                    {
                        lstSearchPaths.Items.Add(path);
                    }
                }
            }
        }

        private void btnRemovePath_Click(object sender, EventArgs e)
        {
            if (lstSearchPaths.SelectedIndex != -1)
            {
                lstSearchPaths.Items.RemoveAt(lstSearchPaths.SelectedIndex);
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (lstSearchPaths.Items.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну папку для поиска.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartScanning();
        }

        private async void StartScanning()
        {
            try
            {
                SetControlsState(false);
                _cancellationTokenSource = new CancellationTokenSource();

                var searchPaths = lstSearchPaths.Items.Cast<string>().ToList();
                var excludedPaths = txtExcludedPaths.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim()).ToList();
                var extensions = txtExtensions.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim().ToLower()).ToList();

                _finder = new DuplicateFinder(
                    searchPaths: searchPaths,
                    excludedPaths: excludedPaths,
                    allowedExtensions: extensions,
                    includeSubdirectories: chkSubdirectories.Checked,
                    minFileSize: (long)numMinSize.Value * 1024,
                    maxFileSize: (long)numMaxSize.Value * 1024 * 1024
                );

                lblStatus.Text = "Поиск дубликатов...";
                panelProgress.Visible = true;
                progressBar.Visible = true;
                progressBar.Value = 0;

                var progress = new Progress<int>(value =>
                {
                    progressBar.Value = value;
                    lblProgress.Text = $"{value}%";
                });

                _duplicates = await _finder.FindDuplicatesAsync(progress, _cancellationTokenSource.Token);

                DisplayResults();
                lblStatus.Text = $"Найдено {_duplicates.Count} групп дубликатов";

            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Поиск отменен";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске дубликатов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Ошибка";
            }
            finally
            {
                SetControlsState(true);
                progressBar.Visible = false;
                panelProgress.Visible = true;
            }
        }

        private void DisplayResults()
        {
            dgvDuplicates.DataSource = null;

            if (_duplicates.Any())
            {
                var summary = _duplicates.Select(g => new
                {
                    Size = g.Size,
                    FormattedSize = FormatFileSize(g.Size),
                    FileCount = g.Files.Count,
                    TotalDuplicates = g.Files.Count - 1,
                    WasteSize = g.Size * (g.Files.Count - 1),
                    FormattedWaste = FormatFileSize(g.Size * (g.Files.Count - 1)),
                    FirstFile = g.Files.First().Path
                }).ToList();

                dgvDuplicates.DataSource = summary;

                // Общая статистика
                var totalDuplicates = _duplicates.Sum(g => g.Files.Count - 1);
                var totalWaste = _duplicates.Sum(g => g.Size * (g.Files.Count - 1));

                lblSummary.Text = $"Найдено: {_duplicates.Count} групп, {totalDuplicates} дубликатов, " +
                                $"лишний объем: {FormatFileSize(totalWaste)}";

                btnDelete.Enabled = true;
            }
            else
            {
                lblSummary.Text = "Дубликаты не найдены";
                btnDelete.Enabled = false;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_duplicates.Any())
                return;

            var strategy = cmbDeleteStrategy.SelectedIndex switch
            {
                0 => DuplicateManager.DeleteStrategy.KeepOldest,
                1 => DuplicateManager.DeleteStrategy.KeepNewest,
                _ => DuplicateManager.DeleteStrategy.KeepFirstFound
            };

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить дубликаты?\n" +
                $"Будет удалено {_duplicates.Sum(g => g.Files.Count - 1)} файлов.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await DeleteDuplicates(strategy);
                lstFileDetails.Items.Clear();
            }
        }

        private async Task DeleteDuplicates(DuplicateManager.DeleteStrategy strategy)
        {
            try
            {
                SetControlsState(false);
                progressBar.Visible = true;
                progressBar.Value = 0;
                lblStatus.Text = "Удаление дубликатов...";

                var progress = new Progress<int>(value =>
                {
                    progressBar.Value = value;
                    lblProgress.Text = $"{value}%";
                });

                var deletedFiles = await _manager.DeleteDuplicatesAsync(
                    _duplicates, strategy, progress: progress);

                MessageBox.Show($"Удалено {deletedFiles.Count} файлов.\n" +
                              $"Освобождено {FormatFileSize(deletedFiles.Sum(f => new FileInfo(f).Length))} места.",
                              "Удаление завершено",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);

                // Обновляем список после удаления
                await RefreshAfterDeletion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsState(true);
                progressBar.Visible = false;
                lblStatus.Text = "Готов";
            }
        }

        private async Task RefreshAfterDeletion()
        {
            // Повторно сканируем чтобы обновить список
            var searchPaths = lstSearchPaths.Items.Cast<string>().ToList();
            var extensions = txtExtensions.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim().ToLower()).ToList();

            _finder = new DuplicateFinder(
                searchPaths: searchPaths,
                allowedExtensions: extensions,
                includeSubdirectories: chkSubdirectories.Checked
            );

            var progress = new Progress<int>(value => progressBar.Value = value);
            _duplicates = await _finder.FindDuplicatesAsync(progress);

            DisplayResults();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private void SetControlsState(bool enabled)
        {
            btnScan.Enabled = enabled;
            btnAddPath.Enabled = enabled;
            btnRemovePath.Enabled = enabled;
            btnDelete.Enabled = enabled && _duplicates.Any();
            btnCancel.Enabled = !enabled;

            lstSearchPaths.Enabled = enabled;
            txtExtensions.Enabled = enabled;
            txtExcludedPaths.Enabled = enabled;
            chkSubdirectories.Enabled = enabled;
            numMinSize.Enabled = enabled;
            numMaxSize.Enabled = enabled;
            cmbDeleteStrategy.Enabled = enabled;
        }

        private void dgvDuplicates_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDuplicates.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDuplicates.SelectedRows[0];
                if (selectedRow.DataBoundItem != null)
                {
                    var selectedGroup = _duplicates[selectedRow.Index];
                    DisplayGroupDetails(selectedGroup);
                }
            }
        }

        private void DisplayGroupDetails(DuplicateGroup group)
        {
            lstFileDetails.Items.Clear();

            foreach (var file in group.Files.OrderBy(f => f.Path))
            {
                var item = new ListViewItem(new[] {
                    file.Path,
                    file.Modified.ToString("yyyy-MM-dd HH:mm"),
                    FormatFileSize(file.Size)
                });
                lstFileDetails.Items.Add(item);
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Устанавливаем значения по умолчанию
            txtExtensions.Text = ".jpg,.jpeg,.png,.gif,.bmp,.pdf,.doc,.docx,.txt";
            numMinSize.Value = 1;
            numMaxSize.Value = 100;
        }
    }
}