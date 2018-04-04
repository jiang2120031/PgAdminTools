namespace PgAdmin.UI
{
    partial class MenuTreeForm
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
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode17});
            this.searchPanel = new System.Windows.Forms.Panel();
            this.databaseBox = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.searchPanel.Controls.Add(this.databaseBox);
            this.searchPanel.Controls.Add(this.label);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.ForeColor = System.Drawing.SystemColors.Control;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(334, 36);
            this.searchPanel.TabIndex = 1;
            // 
            // databaseBox
            // 
            this.databaseBox.Location = new System.Drawing.Point(107, 6);
            this.databaseBox.Name = "databaseBox";
            this.databaseBox.Size = new System.Drawing.Size(130, 20);
            this.databaseBox.TabIndex = 1;
            this.databaseBox.TextChanged += new System.EventHandler(this.databaseBox_TextChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(3, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(98, 13);
            this.label.TabIndex = 0;
            this.label.Text = "DataBaseName:";
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.ForeColor = System.Drawing.SystemColors.Control;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 36);
            this.treeView.Name = "treeView";
            treeNode13.Name = "Node4";
            treeNode13.Text = "Node4";
            treeNode14.Name = "Node5";
            treeNode14.Text = "Node5";
            treeNode15.Name = "Node2";
            treeNode15.Text = "Node2";
            treeNode16.Name = "Node6";
            treeNode16.Text = "Node6";
            treeNode17.Name = "Node3";
            treeNode17.Text = "Node3";
            treeNode18.Name = "Node0";
            treeNode18.Text = "Node0";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode18});
            this.treeView.Size = new System.Drawing.Size(334, 498);
            this.treeView.TabIndex = 2;
            this.treeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
            // 
            // MenuTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 534);
            this.ControlBox = false;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.searchPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MenuTreeForm";
            this.ShowIcon = false;
            this.Text = "DataBase";
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox databaseBox;
        private System.Windows.Forms.Label label;
        public System.Windows.Forms.TreeView treeView;
    }
}