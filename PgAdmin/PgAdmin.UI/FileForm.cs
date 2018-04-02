using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PgAdmin.UI
{
    public partial class FileForm : Form
    {
        public FileForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder  = Environment.SpecialFolder.Desktop;
            folderBrowserDialog1.Description = "Select File Path";

            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        public string jsonFileStr { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("Please select a file path.");
                return;
            }
            if (!Directory.Exists(txtPath.Text))
            {
                MessageBox.Show("Please select a file path.");
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please input a file name.");
                return;
            }
            string fileName = "";
            fileName = txtName.Text.ToLower();
            if(fileName.LastIndexOf(".json")<0)
            {
                fileName += ".json";
            }
            fileName = txtPath.Text +"\\"+ fileName;
            FileStream fs = File.Create(fileName);
            Encoding encode = Encoding.UTF8;
            if(comboBox1.SelectedIndex==1)
            {
                encode = Encoding.Unicode;
            }
            StreamWriter streamWriter = new StreamWriter(fs, encode);

            streamWriter.Write(jsonFileStr);
            streamWriter.Flush();
            streamWriter.Close();
            fs.Close();
            MessageBox.Show("Save the file successfully.");
            this.Close();
        }
    }
}
