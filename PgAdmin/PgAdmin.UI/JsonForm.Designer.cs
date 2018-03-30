namespace PgAdmin.UI
{
    partial class JsonForm
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
            this.JsonPanel = new System.Windows.Forms.Panel();
            this.jsonTextBox = new System.Windows.Forms.RichTextBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchText = new System.Windows.Forms.TextBox();
            this.JsonPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // JsonPanel
            // 
            this.JsonPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.JsonPanel.Controls.Add(this.jsonTextBox);
            this.JsonPanel.Controls.Add(this.searchPanel);
            this.JsonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JsonPanel.ForeColor = System.Drawing.SystemColors.Control;
            this.JsonPanel.Location = new System.Drawing.Point(0, 0);
            this.JsonPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.JsonPanel.Name = "JsonPanel";
            this.JsonPanel.Size = new System.Drawing.Size(884, 662);
            this.JsonPanel.TabIndex = 0;
            // 
            // jsonTextBox
            // 
            this.jsonTextBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.jsonTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jsonTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.jsonTextBox.Location = new System.Drawing.Point(0, 64);
            this.jsonTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jsonTextBox.Name = "jsonTextBox";
            this.jsonTextBox.Size = new System.Drawing.Size(884, 598);
            this.jsonTextBox.TabIndex = 3;
            this.jsonTextBox.Text = "";
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchText);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(884, 64);
            this.searchPanel.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Location = new System.Drawing.Point(381, 20);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(79, 26);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchText
            // 
            this.searchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchText.Location = new System.Drawing.Point(20, 20);
            this.searchText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(350, 26);
            this.searchText.TabIndex = 0;
            // 
            // JsonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.JsonPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "JsonForm";
            this.JsonPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel JsonPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.RichTextBox jsonTextBox;
    }
}