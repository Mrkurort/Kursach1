using System.Windows.Forms;

namespace MainForm
{
    partial class SaDDF
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaDDF));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.numMaxSize = new System.Windows.Forms.NumericUpDown();
            this.numMinSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExcludedPaths = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExtensions = new System.Windows.Forms.TextBox();
            this.lblExtensions = new System.Windows.Forms.Label();
            this.chkSubdirectories = new System.Windows.Forms.CheckBox();
            this.grpPaths = new System.Windows.Forms.GroupBox();
            this.btnRemovePath = new System.Windows.Forms.Button();
            this.btnAddPath = new System.Windows.Forms.Button();
            this.lstSearchPaths = new System.Windows.Forms.ListBox();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvDuplicates = new System.Windows.Forms.DataGridView();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFormattedSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalDuplicates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWasteSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFirstFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lstFileDetails = new System.Windows.Forms.ListView();
            this.colFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSizeDetails = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelProgress = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelControls = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            this.cmbDeleteStrategy = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSize)).BeginInit();
            this.grpPaths.SuspendLayout();
            this.tabResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicates)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSearch);
            this.tabControl.Controls.Add(this.tabResults);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(758, 395);
            this.tabControl.TabIndex = 0;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.grpSettings);
            this.tabSearch.Controls.Add(this.grpPaths);
            this.tabSearch.Controls.Add(this.btnScan);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(9);
            this.tabSearch.Size = new System.Drawing.Size(750, 369);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Настройки поиска";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // grpSettings
            // 
            this.grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSettings.Controls.Add(this.numMaxSize);
            this.grpSettings.Controls.Add(this.numMinSize);
            this.grpSettings.Controls.Add(this.label3);
            this.grpSettings.Controls.Add(this.label2);
            this.grpSettings.Controls.Add(this.txtExcludedPaths);
            this.grpSettings.Controls.Add(this.label1);
            this.grpSettings.Controls.Add(this.txtExtensions);
            this.grpSettings.Controls.Add(this.lblExtensions);
            this.grpSettings.Controls.Add(this.chkSubdirectories);
            this.grpSettings.Location = new System.Drawing.Point(11, 173);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(729, 156);
            this.grpSettings.TabIndex = 1;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Дополнительные настройки";
            // 
            // numMaxSize
            // 
            this.numMaxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxSize.Location = new System.Drawing.Point(600, 87);
            this.numMaxSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxSize.Name = "numMaxSize";
            this.numMaxSize.Size = new System.Drawing.Size(103, 20);
            this.numMaxSize.TabIndex = 8;
            this.numMaxSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numMinSize
            // 
            this.numMinSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMinSize.Location = new System.Drawing.Point(600, 61);
            this.numMinSize.Name = "numMinSize";
            this.numMinSize.Size = new System.Drawing.Size(103, 20);
            this.numMinSize.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(497, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Макс. размер (MB):";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(497, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Мин. размер (KB):";
            // 
            // txtExcludedPaths
            // 
            this.txtExcludedPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcludedPaths.Location = new System.Drawing.Point(129, 87);
            this.txtExcludedPaths.Name = "txtExcludedPaths";
            this.txtExcludedPaths.Size = new System.Drawing.Size(343, 20);
            this.txtExcludedPaths.TabIndex = 4;
            this.txtExcludedPaths.Text = "C:\\Windows; C:\\Program Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Исключить папки (*):";
            // 
            // txtExtensions
            // 
            this.txtExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtensions.Location = new System.Drawing.Point(129, 61);
            this.txtExtensions.Name = "txtExtensions";
            this.txtExtensions.Size = new System.Drawing.Size(343, 20);
            this.txtExtensions.TabIndex = 2;
            this.txtExtensions.Text = ".jpg,.jpeg,.png,.gif,.bmp,.pdf,.doc,.docx,.txt";
            // 
            // lblExtensions
            // 
            this.lblExtensions.AutoSize = true;
            this.lblExtensions.Location = new System.Drawing.Point(17, 63);
            this.lblExtensions.Name = "lblExtensions";
            this.lblExtensions.Size = new System.Drawing.Size(114, 13);
            this.lblExtensions.TabIndex = 1;
            this.lblExtensions.Text = "Расширения файлов:";
            // 
            // chkSubdirectories
            // 
            this.chkSubdirectories.AutoSize = true;
            this.chkSubdirectories.Checked = true;
            this.chkSubdirectories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSubdirectories.Location = new System.Drawing.Point(17, 26);
            this.chkSubdirectories.Name = "chkSubdirectories";
            this.chkSubdirectories.Size = new System.Drawing.Size(150, 17);
            this.chkSubdirectories.TabIndex = 0;
            this.chkSubdirectories.Text = "Включая поддиректории";
            this.chkSubdirectories.UseVisualStyleBackColor = true;
            // 
            // grpPaths
            // 
            this.grpPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaths.Controls.Add(this.btnRemovePath);
            this.grpPaths.Controls.Add(this.btnAddPath);
            this.grpPaths.Controls.Add(this.lstSearchPaths);
            this.grpPaths.Location = new System.Drawing.Point(11, 11);
            this.grpPaths.Name = "grpPaths";
            this.grpPaths.Size = new System.Drawing.Size(727, 156);
            this.grpPaths.TabIndex = 0;
            this.grpPaths.TabStop = false;
            this.grpPaths.Text = "Папки для поиска";
            // 
            // btnRemovePath
            // 
            this.btnRemovePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemovePath.Location = new System.Drawing.Point(644, 128);
            this.btnRemovePath.Name = "btnRemovePath";
            this.btnRemovePath.Size = new System.Drawing.Size(78, 22);
            this.btnRemovePath.TabIndex = 2;
            this.btnRemovePath.Text = "Удалить";
            this.btnRemovePath.UseVisualStyleBackColor = true;
            this.btnRemovePath.Click += new System.EventHandler(this.btnRemovePath_Click);
            // 
            // btnAddPath
            // 
            this.btnAddPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPath.Location = new System.Drawing.Point(559, 128);
            this.btnAddPath.Name = "btnAddPath";
            this.btnAddPath.Size = new System.Drawing.Size(84, 22);
            this.btnAddPath.TabIndex = 1;
            this.btnAddPath.Text = "Добавить";
            this.btnAddPath.UseVisualStyleBackColor = true;
            this.btnAddPath.Click += new System.EventHandler(this.btnAddPath_Click);
            // 
            // lstSearchPaths
            // 
            this.lstSearchPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSearchPaths.FormattingEnabled = true;
            this.lstSearchPaths.Location = new System.Drawing.Point(5, 19);
            this.lstSearchPaths.Name = "lstSearchPaths";
            this.lstSearchPaths.Size = new System.Drawing.Size(717, 108);
            this.lstSearchPaths.TabIndex = 0;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.splitContainer);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults.Size = new System.Drawing.Size(750, 369);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Результаты";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvDuplicates);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpDetails);
            this.splitContainer.Size = new System.Drawing.Size(744, 363);
            this.splitContainer.SplitterDistance = 169;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 0;
            // 
            // dgvDuplicates
            // 
            this.dgvDuplicates.AllowUserToAddRows = false;
            this.dgvDuplicates.AllowUserToDeleteRows = false;
            this.dgvDuplicates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDuplicates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuplicates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSize,
            this.colFormattedSize,
            this.colFileCount,
            this.colTotalDuplicates,
            this.colWasteSize,
            this.colFirstFile});
            this.dgvDuplicates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDuplicates.Location = new System.Drawing.Point(0, 0);
            this.dgvDuplicates.Name = "dgvDuplicates";
            this.dgvDuplicates.ReadOnly = true;
            this.dgvDuplicates.RowHeadersVisible = false;
            this.dgvDuplicates.RowTemplate.Height = 25;
            this.dgvDuplicates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDuplicates.Size = new System.Drawing.Size(744, 169);
            this.dgvDuplicates.TabIndex = 0;
            this.dgvDuplicates.SelectionChanged += new System.EventHandler(this.dgvDuplicates_SelectionChanged);
            // 
            // colSize
            // 
            this.colSize.DataPropertyName = "Size";
            dataGridViewCellStyle1.Format = "N0";
            this.colSize.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSize.HeaderText = "Размер (байт)";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            this.colSize.Visible = false;
            // 
            // colFormattedSize
            // 
            this.colFormattedSize.DataPropertyName = "FormattedSize";
            this.colFormattedSize.HeaderText = "Размер";
            this.colFormattedSize.Name = "colFormattedSize";
            this.colFormattedSize.ReadOnly = true;
            // 
            // colFileCount
            // 
            this.colFileCount.DataPropertyName = "FileCount";
            this.colFileCount.HeaderText = "Файлов в группе";
            this.colFileCount.Name = "colFileCount";
            this.colFileCount.ReadOnly = true;
            // 
            // colTotalDuplicates
            // 
            this.colTotalDuplicates.DataPropertyName = "TotalDuplicates";
            this.colTotalDuplicates.HeaderText = "Дубликатов";
            this.colTotalDuplicates.Name = "colTotalDuplicates";
            this.colTotalDuplicates.ReadOnly = true;
            // 
            // colWasteSize
            // 
            this.colWasteSize.DataPropertyName = "FormattedWaste";
            this.colWasteSize.HeaderText = "Лишний объем";
            this.colWasteSize.Name = "colWasteSize";
            this.colWasteSize.ReadOnly = true;
            // 
            // colFirstFile
            // 
            this.colFirstFile.DataPropertyName = "FirstFile";
            this.colFirstFile.HeaderText = "Первый файл";
            this.colFirstFile.Name = "colFirstFile";
            this.colFirstFile.ReadOnly = true;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.lstFileDetails);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Location = new System.Drawing.Point(0, 0);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(744, 191);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Детали группы";
            // 
            // lstFileDetails
            // 
            this.lstFileDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFilePath,
            this.colModified,
            this.colSizeDetails});
            this.lstFileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFileDetails.FullRowSelect = true;
            this.lstFileDetails.GridLines = true;
            this.lstFileDetails.HideSelection = false;
            this.lstFileDetails.Location = new System.Drawing.Point(3, 16);
            this.lstFileDetails.Name = "lstFileDetails";
            this.lstFileDetails.Size = new System.Drawing.Size(738, 172);
            this.lstFileDetails.TabIndex = 0;
            this.lstFileDetails.UseCompatibleStateImageBehavior = false;
            this.lstFileDetails.View = System.Windows.Forms.View.Details;
            // 
            // colFilePath
            // 
            this.colFilePath.Text = "Путь к файлу";
            this.colFilePath.Width = 500;
            // 
            // colModified
            // 
            this.colModified.Text = "Изменен";
            this.colModified.Width = 120;
            // 
            // colSizeDetails
            // 
            this.colSizeDetails.Text = "Размер";
            this.colSizeDetails.Width = 100;
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.btnCancel);
            this.panelProgress.Controls.Add(this.lblProgress);
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgress.Location = new System.Drawing.Point(0, 395);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(758, 48);
            this.panelProgress.TabIndex = 1;
            this.panelProgress.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(665, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 20);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(13, 16);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(59, 13);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Прогресс:";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(73, 13);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(585, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.lblSummary);
            this.panelControls.Controls.Add(this.cmbDeleteStrategy);
            this.panelControls.Controls.Add(this.btnDelete);
            this.panelControls.Controls.Add(this.lblStatus);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 443);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(758, 43);
            this.panelControls.TabIndex = 2;
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(12, 20);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(83, 13);
            this.lblSummary.TabIndex = 4;
            this.lblSummary.Text = "Готов к работе";
            // 
            // cmbDeleteStrategy
            // 
            this.cmbDeleteStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDeleteStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeleteStrategy.Enabled = false;
            this.cmbDeleteStrategy.FormattingEnabled = true;
            this.cmbDeleteStrategy.Location = new System.Drawing.Point(323, 13);
            this.cmbDeleteStrategy.Name = "cmbDeleteStrategy";
            this.cmbDeleteStrategy.Size = new System.Drawing.Size(269, 21);
            this.cmbDeleteStrategy.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(597, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 27);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(278, 335);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(203, 27);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "Сканировать";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(36, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Готов";
            // 
            // SaDDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 486);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelControls);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(774, 525);
            this.Name = "SaDDF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaDDF - Поиск и удаление дубликатов файлов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSize)).EndInit();
            this.grpPaths.ResumeLayout(false);
            this.tabResults.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicates)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl;
        private TabPage tabSearch;
        private TabPage tabResults;
        private GroupBox grpPaths;
        private ListBox lstSearchPaths;
        private Button btnRemovePath;
        private Button btnAddPath;
        private GroupBox grpSettings;
        private CheckBox chkSubdirectories;
        private TextBox txtExtensions;
        private Label lblExtensions;
        private TextBox txtExcludedPaths;
        private Label label1;
        private NumericUpDown numMaxSize;
        private NumericUpDown numMinSize;
        private Label label3;
        private Label label2;
        private SplitContainer splitContainer;
        private DataGridView dgvDuplicates;
        private GroupBox grpDetails;
        private ListView lstFileDetails;
        private ColumnHeader colFilePath;
        private ColumnHeader colModified;
        private ColumnHeader colSizeDetails;
        private Panel panelProgress;
        private ProgressBar progressBar;
        private Label lblProgress;
        private Button btnCancel;
        private Panel panelControls;
        private Button btnScan;
        private Label lblStatus;
        private Button btnDelete;
        private ComboBox cmbDeleteStrategy;
        private Label lblSummary;
        private DataGridViewTextBoxColumn colSize;
        private DataGridViewTextBoxColumn colFormattedSize;
        private DataGridViewTextBoxColumn colFileCount;
        private DataGridViewTextBoxColumn colTotalDuplicates;
        private DataGridViewTextBoxColumn colWasteSize;
        private DataGridViewTextBoxColumn colFirstFile;
    }
}

