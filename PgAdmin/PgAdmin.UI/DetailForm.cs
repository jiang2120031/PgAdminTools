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
            FileForm fileForm = new FileForm();
            fileForm.jsonFileStr = jobject.ToString();
            fileForm.ShowDialog();
        }
    }
}