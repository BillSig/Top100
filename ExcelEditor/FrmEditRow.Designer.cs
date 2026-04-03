namespace ExcelEditor
{
    partial class FrmEditRow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPosition = new Label();
            txtPosition = new TextBox();
            txtBandName = new TextBox();
            lblBandName = new Label();
            txtSongTitle = new TextBox();
            lblSongTitle = new Label();
            txtVideoLink = new TextBox();
            lblVideoLink = new Label();
            chkIsViewed = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(22, 12);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(61, 20);
            lblPosition.TabIndex = 0;
            lblPosition.Text = "Position";
            // 
            // txtPosition
            // 
            txtPosition.AcceptsTab = true;
            txtPosition.Enabled = false;
            txtPosition.Location = new Point(89, 12);
            txtPosition.Name = "txtPosition";
            txtPosition.Size = new Size(54, 27);
            txtPosition.TabIndex = 1;
            txtPosition.TextAlign = HorizontalAlignment.Right;
            // 
            // txtBandName
            // 
            txtBandName.AcceptsTab = true;
            txtBandName.Location = new Point(276, 12);
            txtBandName.Name = "txtBandName";
            txtBandName.Size = new Size(407, 27);
            txtBandName.TabIndex = 3;
            // 
            // lblBandName
            // 
            lblBandName.AutoSize = true;
            lblBandName.Location = new Point(158, 12);
            lblBandName.Name = "lblBandName";
            lblBandName.Size = new Size(87, 20);
            lblBandName.TabIndex = 2;
            lblBandName.Text = "Band Name";
            // 
            // txtSongTitle
            // 
            txtSongTitle.AcceptsTab = true;
            txtSongTitle.Location = new Point(276, 54);
            txtSongTitle.Name = "txtSongTitle";
            txtSongTitle.Size = new Size(407, 27);
            txtSongTitle.TabIndex = 5;
            // 
            // lblSongTitle
            // 
            lblSongTitle.AutoSize = true;
            lblSongTitle.Location = new Point(158, 57);
            lblSongTitle.Name = "lblSongTitle";
            lblSongTitle.Size = new Size(76, 20);
            lblSongTitle.TabIndex = 4;
            lblSongTitle.Text = "Song Title";
            // 
            // txtVideoLink
            // 
            txtVideoLink.AcceptsTab = true;
            txtVideoLink.Location = new Point(276, 101);
            txtVideoLink.Name = "txtVideoLink";
            txtVideoLink.Size = new Size(407, 27);
            txtVideoLink.TabIndex = 7;
            // 
            // lblVideoLink
            // 
            lblVideoLink.AutoSize = true;
            lblVideoLink.Location = new Point(158, 104);
            lblVideoLink.Name = "lblVideoLink";
            lblVideoLink.Size = new Size(78, 20);
            lblVideoLink.TabIndex = 6;
            lblVideoLink.Text = "Video Link";
            // 
            // chkIsViewed
            // 
            chkIsViewed.AutoSize = true;
            chkIsViewed.Location = new Point(22, 102);
            chkIsViewed.Name = "chkIsViewed";
            chkIsViewed.Size = new Size(136, 24);
            chkIsViewed.TabIndex = 8;
            chkIsViewed.Text = "Is Video Present";
            chkIsViewed.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(489, 144);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(589, 144);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmEditRow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 181);
            ControlBox = false;
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(chkIsViewed);
            Controls.Add(txtVideoLink);
            Controls.Add(lblVideoLink);
            Controls.Add(txtSongTitle);
            Controls.Add(lblSongTitle);
            Controls.Add(txtBandName);
            Controls.Add(lblBandName);
            Controls.Add(txtPosition);
            Controls.Add(lblPosition);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmEditRow";
            Text = "FrmEditRow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPosition;
        private TextBox txtPosition;
        private TextBox txtBandName;
        private Label lblBandName;
        private TextBox txtSongTitle;
        private Label lblSongTitle;
        private TextBox txtVideoLink;
        private Label lblVideoLink;
        private CheckBox chkIsViewed;
        private Button btnOK;
        private Button btnCancel;
    }
}