namespace PgAdmin.UI
{
    partial class PagerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pageSizeText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.currentPageLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.totalCountLabel = new System.Windows.Forms.Label();
            this.pageNumText = new System.Windows.Forms.TextBox();
            this.gotoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "  Page Size:";
            // 
            // pageSizeText
            // 
            this.pageSizeText.Dock = System.Windows.Forms.DockStyle.Left;
            this.pageSizeText.Location = new System.Drawing.Point(64, 0);
            this.pageSizeText.Name = "pageSizeText";
            this.pageSizeText.Size = new System.Drawing.Size(61, 20);
            this.pageSizeText.TabIndex = 1;
            this.pageSizeText.TextChanged += new System.EventHandler(this.pageSizeText_TextChanged);
            this.pageSizeText.Leave += new System.EventHandler(this.pageSizeText_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(125, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "   Current Page:";
            // 
            // currentPageLabel
            // 
            this.currentPageLabel.AutoSize = true;
            this.currentPageLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.currentPageLabel.Location = new System.Drawing.Point(206, 0);
            this.currentPageLabel.Name = "currentPageLabel";
            this.currentPageLabel.Size = new System.Drawing.Size(13, 13);
            this.currentPageLabel.TabIndex = 3;
            this.currentPageLabel.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(219, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "/";
            // 
            // totalCountLabel
            // 
            this.totalCountLabel.AutoSize = true;
            this.totalCountLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.totalCountLabel.Location = new System.Drawing.Point(231, 0);
            this.totalCountLabel.Name = "totalCountLabel";
            this.totalCountLabel.Size = new System.Drawing.Size(43, 13);
            this.totalCountLabel.TabIndex = 8;
            this.totalCountLabel.Text = "1          ";
            // 
            // pageNumText
            // 
            this.pageNumText.Dock = System.Windows.Forms.DockStyle.Left;
            this.pageNumText.Location = new System.Drawing.Point(274, 0);
            this.pageNumText.Name = "pageNumText";
            this.pageNumText.Size = new System.Drawing.Size(94, 20);
            this.pageNumText.TabIndex = 9;
            this.pageNumText.TextChanged += new System.EventHandler(this.pageNumText_TextChanged);
            this.pageNumText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pageNumText_KeyPress);
            // 
            // gotoBtn
            // 
            this.gotoBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.gotoBtn.Location = new System.Drawing.Point(368, 0);
            this.gotoBtn.Name = "gotoBtn";
            this.gotoBtn.Size = new System.Drawing.Size(56, 52);
            this.gotoBtn.TabIndex = 10;
            this.gotoBtn.Text = "GoTo";
            this.gotoBtn.UseVisualStyleBackColor = true;
            this.gotoBtn.Click += new System.EventHandler(this.gotoBtn_Click);
            // 
            // PagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gotoBtn);
            this.Controls.Add(this.pageNumText);
            this.Controls.Add(this.totalCountLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currentPageLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pageSizeText);
            this.Controls.Add(this.label1);
            this.Name = "PagerControl";
            this.Size = new System.Drawing.Size(429, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pageSizeText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label currentPageLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label totalCountLabel;
        private System.Windows.Forms.TextBox pageNumText;
        private System.Windows.Forms.Button gotoBtn;
    }
}
