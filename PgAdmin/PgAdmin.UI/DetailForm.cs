using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NLog;
using System.IO;
using PgAdmin.Services;

namespace PgAdmin.UI
{
    public partial class DetailForm : Form
    {
        public DetailForm()
        {
            InitializeComponent();
        }

        private ILogger logger = LogManager.GetLogger("DetailForm");
        JObject jobject;
        public JObject JsonData
        {
            set
            {
                jobject = value;
                jsonTextBox.Text = value.ToString();
            }

        }

        public string Title
        {
            set
            {
                this.Text = value;
            }
        }
        public static void MySelect(System.Windows.Forms.RichTextBox tb, int i, string s, Color c)
        {
            tb.Select(i, s.Length);
            tb.SelectionFont = new Font("Microsoft Sans Serif", 14, (FontStyle.Bold));
            tb.SelectionBackColor = c;


            tb.Select(i + s.Length, 0);
            tb.SelectionFont = new Font("Microsoft Sans Serif", 12, (FontStyle.Regular));
            tb.SelectionBackColor = tb.BackColor;
        }

        private void ColorDefault(System.Windows.Forms.RichTextBox tb)
        {
            tb.Select(0, tb.Text.Length);
            tb.SelectionFont = new Font("Microsoft Sans Serif", 12, (FontStyle.Regular));
            tb.SelectionBackColor = tb.BackColor;

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(searchText.Text))
                {
                    ColorDefault(jsonTextBox);
                    Regex r = new Regex(searchText.Text);
                    Match m = r.Match(jsonTextBox.Text);
                    var gc = m.Groups;

                    MatchCollection Matches = Regex.Matches(jsonTextBox.Text, searchText.Text);
                    foreach (Match Match in Matches)
                    {
                        MySelect(jsonTextBox, Match.Index, searchText.Text, Color.CadetBlue);

                    }
                    jsonTextBox.Select(Matches[0].Index, searchText.Text.Length);
                    jsonTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "SearchButton_ClickError:" + ex.Message);
            }

        }
        

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(jobject.ToString(), true);
            MessageBox.Show("Copy to clipboard successfully.");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //FileForm fileForm = new FileForm();
            //fileForm.jsonFileStr = jobject.ToString();
            //fileForm.ShowDialog();
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.Filter = ".json|*.json|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SaveFileName = saveFileDialog1.FileName;
                FileStream fs = File.Create(SaveFileName);
                Encoding encode = Encoding.UTF8;
                StreamWriter streamWriter = new StreamWriter(fs, encode);

                streamWriter.Write(jobject.ToString());
                streamWriter.Flush();
                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Save the file successfully.");
            }

        }

        public string DataName { get; set; }
        public string TableName { get; set; }
        public string DbId { get; set; }
        public TableListForm pForm { get; set; }

        private void btnSaveDb_Click(object sender, EventArgs e)
        {
            JObject saveObj = null;
            try
            {
                saveObj = (JObject)JsonConvert.DeserializeObject(jsonTextBox.Text);
                
            }
            catch(Exception)
            {
                MessageBox.Show("json format error.");
                return;
            }
            ClientQueryService postgresHelper = new ClientQueryService();

            int i= postgresHelper.UpdateInfoById(DbId, DataName, TableName, saveObj);
            if(i>0)
            {
                pForm.refreshDataTable();
                MessageBox.Show("Save data to database successfully.");
            }
        }
    }
}