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
            btnMoveUp = new Button();
            btnMoveDown = new Button();
            btnDiscard = new Button();
            btnSave = new Button();
            txtFileName = new TextBox();
            btnOpen = new Button();
            pnlMain = new Panel();
            grdMain = new DataGridView();
            openFileDialog1 = new OpenFileDialog();
            pnlTop.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdMain).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(btnMoveUp);
            pnlTop.Controls.Add(btnMoveDown);
            pnlTop.Controls.Add(btnDiscard);
            pnlTop.Controls.Add(btnSave);
            pnlTop.Controls.Add(txtFileName);
            pnlTop.Controls.Add(btnOpen);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1242, 96);
            pnlTop.TabIndex = 0;
            // 
            // btnMoveUp
            // 
            btnMoveUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnMoveUp.Location = new Point(1045, 56);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(94, 29);
            btnMoveUp.TabIndex = 5;
            btnMoveUp.Text = "↑";
            btnMoveUp.UseVisualStyleBackColor = true;
            btnMoveUp.Click += btnMoveUp_Click;
            // 
            // btnMoveDown
            // 
            btnMoveDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnMoveDown.Location = new Point(945, 56);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(94, 29);
            btnMoveDown.TabIndex = 4;
            btnMoveDown.Text = "↓";
            btnMoveDown.UseVisualStyleBackColor = true;
            btnMoveDown.Click += btnMoveDown_Click;
            // 
            // btnDiscard
            // 
            btnDiscard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDiscard.Location = new Point(1148, 11);
            btnDiscard.Name = "btnDiscard";
            btnDiscard.Size = new Size(94, 29);
            btnDiscard.TabIndex = 3;
            btnDiscard.Text = "Discard Changes";
            btnDiscard.UseVisualStyleBackColor = true;
            btnDiscard.Click += btnDiscard_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(1045, 11);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save file";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(12, 12);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(533, 27);
            txtFileName.TabIndex = 1;
            // 
            // btnOpen
            // 
            btnOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOpen.Location = new Point(945, 11);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(94, 29);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "Load Excel file";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(grdMain);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 96);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1242, 509);
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
            grdMain.Size = new Size(1242, 509);
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
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grdMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Button btnOpen;
        private Panel pnlMain;
        private DataGridView grdMain;
        private OpenFileDialog openFileDialog1;
        private TextBox txtFileName;
        private Button btnSave;
        private Button btnDiscard;
        private Button btnMoveUp;
        private Button btnMoveDown;
    }
}
