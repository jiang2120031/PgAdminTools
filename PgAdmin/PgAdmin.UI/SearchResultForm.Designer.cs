namespace PgAdmin.UI
{
    partial class SearchResultForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Id = new System.Windows.Forms.TextBox();
            this.label_Id = new System.Windows.Forms.Label();
            this.label_LastModified = new System.Windows.Forms.Label();
            this.textBox_LastModified = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_Data = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_LastModified);
            this.panel1.Controls.Add(this.textBox_LastModified);
            this.panel1.Controls.Add(this.label_Id);
            this.panel1.Controls.Add(this.textBox_Id);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 79);
            this.panel1.TabIndex = 0;
            // 
            // textBox_Id
            // 
            this.textBox_Id.Location = new System.Drawing.Point(100, 20);
            this.textBox_Id.Name = "textBox_Id";
            this.textBox_Id.Size = new System.Drawing.Size(476, 20);
            this.textBox_Id.TabIndex = 0;
            // 
            // label_Id
            // 
            this.label_Id.AutoSize = true;
            this.label_Id.Location = new System.Drawing.Point(64, 23);
            this.label_Id.Name = "label_Id";
            this.label_Id.Size = new System.Drawing.Size(30, 13);
            this.label_Id.TabIndex = 1;
            this.label_Id.Text = "ID：";
            // 
            // label_LastModified
            // 
            this.label_LastModified.AutoSize = true;
            this.label_LastModified.Location = new System.Drawing.Point(15, 49);
            this.label_LastModified.Name = "label_LastModified";
            this.label_LastModified.Size = new System.Drawing.Size(79, 13);
            this.label_LastModified.TabIndex = 3;
            this.label_LastModified.Text = "LastModified：";
            // 
            // textBox_LastModified
            // 
            this.textBox_LastModified.Location = new System.Drawing.Point(100, 46);
            this.textBox_LastModified.Name = "textBox_LastModified";
            this.textBox_LastModified.Size = new System.Drawing.Size(476, 20);
            this.textBox_LastModified.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_Data);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(907, 707);
            this.panel2.TabIndex = 1;
            // 
            // textBox_Data
            // 
            this.textBox_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Data.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Data.Location = new System.Drawing.Point(0, 0);
            this.textBox_Data.Multiline = true;
            this.textBox_Data.Name = "textBox_Data";
            this.textBox_Data.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Data.Size = new System.Drawing.Size(907, 707);
            this.textBox_Data.TabIndex = 0;
            // 
            // SearchResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 786);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SearchResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchResultForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Id;
        private System.Windows.Forms.TextBox textBox_Id;
        private System.Windows.Forms.Label label_LastModified;
        private System.Windows.Forms.TextBox textBox_LastModified;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_Data;
    }
}