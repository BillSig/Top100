namespace ExcelEditor
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblFileName = new Label();
            pnlFilter = new Panel();
            btnClear = new Button();
            btnFilter = new Button();
            btnMoveUp = new Button();
            btnMoveDown = new Button();
            btnDiscard = new Button();
            btnSave = new Button();
            btnOpen = new Button();
            chkIsViewed = new CheckBox();
            txtSong = new TextBox();
            lblSong = new Label();
            txtBand = new TextBox();
            lblBand = new Label();
            txtFileName = new TextBox();
            pnlMain = new Panel();
            grdMain = new DataGridView();
            openFileDialog1 = new OpenFileDialog();
            pnlTop.SuspendLayout();
            pnlFilter.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdMain).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblFileName);
            pnlTop.Controls.Add(pnlFilter);
            pnlTop.Controls.Add(txtFileName);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1242, 154);
            pnlTop.TabIndex = 0;
            // 
            // lblFileName
            // 
            lblFileName.AutoSize = true;
            lblFileName.Location = new Point(22, 15);
            lblFileName.Name = "lblFileName";
            lblFileName.Size = new Size(76, 20);
            lblFileName.TabIndex = 7;
            lblFileName.Text = "Filename: ";
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(btnClear);
            pnlFilter.Controls.Add(btnFilter);
            pnlFilter.Controls.Add(btnMoveUp);
            pnlFilter.Controls.Add(btnMoveDown);
            pnlFilter.Controls.Add(btnDiscard);
            pnlFilter.Controls.Add(btnSave);
            pnlFilter.Controls.Add(btnOpen);
            pnlFilter.Controls.Add(chkIsViewed);
            pnlFilter.Controls.Add(txtSong);
            pnlFilter.Controls.Add(lblSong);
            pnlFilter.Controls.Add(txtBand);
            pnlFilter.Controls.Add(lblBand);
            pnlFilter.Dock = DockStyle.Bottom;
            pnlFilter.Location = new Point(0, 43);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1242, 111);
            pnlFilter.TabIndex = 6;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top;
            btnClear.Location = new Point(498, 42);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(139, 29);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Top;
            btnFilter.Location = new Point(498, 7);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(139, 29);
            btnFilter.TabIndex = 11;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // btnMoveUp
            // 
            btnMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMoveUp.Location = new Point(991, 42);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(94, 29);
            btnMoveUp.TabIndex = 10;
            btnMoveUp.Text = "↑";
            btnMoveUp.UseVisualStyleBackColor = true;
            btnMoveUp.Click += btnMoveUp_Click;
            // 
            // btnMoveDown
            // 
            btnMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMoveDown.Location = new Point(891, 42);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(94, 29);
            btnMoveDown.TabIndex = 9;
            btnMoveDown.Text = "↓";
            btnMoveDown.UseVisualStyleBackColor = true;
            btnMoveDown.Click += btnMoveDown_Click;
            // 
            // btnDiscard
            // 
            btnDiscard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDiscard.Location = new Point(1091, 7);
            btnDiscard.Name = "btnDiscard";
            btnDiscard.Size = new Size(139, 29);
            btnDiscard.TabIndex = 8;
            btnDiscard.Text = "Discard Changes";
            btnDiscard.UseVisualStyleBackColor = true;
            btnDiscard.Click += btnDiscard_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(991, 7);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save file";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnOpen
            // 
            btnOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOpen.Location = new Point(891, 7);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(94, 29);
            btnOpen.TabIndex = 6;
            btnOpen.Text = "Load Excel file";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // chkIsViewed
            // 
            chkIsViewed.AutoSize = true;
            chkIsViewed.Checked = true;
            chkIsViewed.CheckState = CheckState.Indeterminate;
            chkIsViewed.Location = new Point(22, 81);
            chkIsViewed.Name = "chkIsViewed";
            chkIsViewed.Size = new Size(233, 24);
            chkIsViewed.TabIndex = 4;
            chkIsViewed.Text = "Video is viewed / downoladed";
            chkIsViewed.ThreeState = true;
            chkIsViewed.UseVisualStyleBackColor = true;
            // 
            // txtSong
            // 
            txtSong.Location = new Point(104, 43);
            txtSong.Name = "txtSong";
            txtSong.PlaceholderText = "Search for song titles...";
            txtSong.Size = new Size(388, 27);
            txtSong.TabIndex = 3;
            // 
            // lblSong
            // 
            lblSong.AutoSize = true;
            lblSong.Location = new Point(22, 46);
            lblSong.Name = "lblSong";
            lblSong.Size = new Size(43, 20);
            lblSong.TabIndex = 2;
            lblSong.Text = "Song";
            // 
            // txtBand
            // 
            txtBand.Location = new Point(104, 8);
            txtBand.Name = "txtBand";
            txtBand.PlaceholderText = "Search for band / Singer names";
            txtBand.Size = new Size(388, 27);
            txtBand.TabIndex = 1;
            // 
            // lblBand
            // 
            lblBand.AutoSize = true;
            lblBand.Location = new Point(22, 11);
            lblBand.Name = "lblBand";
            lblBand.Size = new Size(46, 20);
            lblBand.TabIndex = 0;
            lblBand.Text = "Band:";
            // 
            // txtFileName
            // 
            txtFileName.Enabled = false;
            txtFileName.Location = new Point(104, 12);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(533, 27);
            txtFileName.TabIndex = 1;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(grdMain);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 154);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1242, 451);
            pnlMain.TabIndex = 1;
            // 
            // grdMain
            // 
            grdMain.AllowUserToAddRows = false;
            grdMain.AllowUserToDeleteRows = false;
            grdMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdMain.Dock = DockStyle.Fill;
            grdMain.Location = new Point(0, 0);
            grdMain.Name = "grdMain";
            grdMain.ReadOnly = true;
            grdMain.RowHeadersWidth = 51;
            grdMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdMain.Size = new Size(1242, 451);
            grdMain.TabIndex = 0;
            grdMain.CellClick += grdMain_CellClick;
            grdMain.CellDoubleClick += Grid_CellDoubleClick;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "dlgOpenFile";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1242, 605);
            Controls.Add(pnlMain);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Excel Editor";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grdMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Panel pnlMain;
        private DataGridView grdMain;
        private OpenFileDialog openFileDialog1;
        private TextBox txtFileName;
        private Panel pnlFilter;
        private CheckBox chkIsViewed;
        private TextBox txtSong;
        private Label lblSong;
        private TextBox txtBand;
        private Label lblBand;
        private Button btnMoveUp;
        private Button btnMoveDown;
        private Button btnDiscard;
        private Button btnSave;
        private Button btnOpen;
        private Label lblFileName;
        private Button btnFilter;
        private Button btnClear;
    }
}
