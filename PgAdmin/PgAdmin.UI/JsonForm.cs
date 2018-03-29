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
        public static void MySelect(System.Windows.Forms.RichTextBox tb, int i, string s, Color c, bool font)
        {
            tb.Select(i - s.Length, s.Length);
            tb.SelectionColor = c;

            if (font)
            {
                tb.SelectionFont = new Font("宋体", 12, (FontStyle.Bold));
                tb.SelectionBackColor = Color.Red;
            }

            //以下是把光标放到原来位置，并把光标后输入的文字重置
            tb.Select(i, 0);
            tb.SelectionFont = new Font("宋体", 12, (FontStyle.Regular));
            tb.SelectionColor = Color.Black;
            tb.SelectionBackColor = Color.Transparent;
        }

        private void ColorKeyWord(System.Windows.Forms.RichTextBox tb, string word, string keyword) //判断字符串是否为关键字  
        {
            if (word.Contains(keyword))
            {
                var index = tb.Text.IndexOf(word) + word.Length;
                MySelect(tb, index, keyword, Color.Blue, true);
            }

        }


        private void ColorDefault(System.Windows.Forms.RichTextBox tb)
        {
            tb.Font = new Font("宋体", 12, (FontStyle.Regular));

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(searchText.Text))
            {
                ColorDefault(jsonTextBox);
                string[] words = jsonTextBox.Text.Split(new char[] { ' ', '.', '\n', '(', ')', '}', '{', '"', '[', ']' });
                for (int i = 0; i < words.Length; i++)//一个字符段一个字符段来判断  
                {
                    ColorKeyWord(jsonTextBox, words[i], searchText.Text);
                }

            }

        }
    }
}