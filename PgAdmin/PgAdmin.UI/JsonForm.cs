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

namespace PgAdmin.UI
{
    public partial class JsonForm : Form
    {
        public JsonForm()
        {
            InitializeComponent();
        }


        public JObject JsonData
        {
            set
            {
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
            tb.Select(i , s.Length);
            //tb.SelectionColor = c;
            tb.SelectionFont = new Font("Microsoft Sans Serif", 14, (FontStyle.Bold));
            tb.SelectionBackColor = c;//Color.Blue;


            tb.Select(i+s.Length, 0);
            tb.SelectionFont = new Font("Microsoft Sans Serif", 12, (FontStyle.Regular));
            //tb.SelectionColor = Color.Black;
            tb.SelectionBackColor = Color.Transparent;
        }
        
        private void ColorDefault(System.Windows.Forms.RichTextBox tb)
        {
            tb.Font = new Font("Microsoft Sans Serif", 12, (FontStyle.Regular));
            tb.SelectionBackColor = Color.Transparent;
        }

        private void searchButton_Click(object sender, EventArgs e)
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
    }
}