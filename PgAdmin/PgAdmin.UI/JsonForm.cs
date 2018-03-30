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
            tb.SelectionFont = new Font("宋体", 14, (FontStyle.Bold));
            tb.SelectionBackColor = c;//Color.Blue;


            tb.Select(i+s.Length, 0);
            tb.SelectionFont = new Font("宋体", 12, (FontStyle.Regular));
            //tb.SelectionColor = Color.Black;
            tb.SelectionBackColor = Color.Transparent;
        }

        private void ColorKeyWord(System.Windows.Forms.RichTextBox tb, string word, string keyword)
        {
            if (word.Contains(keyword) || word == keyword)
            {
                var index = tb.Text.IndexOf(word) + word.Length;
                MySelect(tb, index, keyword, Color.Black);
            }

        }


        private void ColorDefault(System.Windows.Forms.RichTextBox tb)
        {
            tb.Font = new Font("宋体", 12, (FontStyle.Regular));
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
                
                //string[] words = jsonTextBox.Text.Split(new char[] { ' ', '.', '\n', '(', ')', '}', '{', '"', '[', ']' });
                //for (int i = 0; i < words.Length; i++)
                //{
                //    ColorKeyWord(jsonTextBox, words[i], searchText.Text);
                   
                //}

            }

        }
    }
}