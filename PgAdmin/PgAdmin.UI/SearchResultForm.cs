using PgAdmin.Services.Model;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace PgAdmin.UI
{
    public partial class SearchResultForm : Form
    {
        public SearchResultForm()
        {
            InitializeComponent();
        }
        public SearchResultForm(GetInfoByIdModel getInfoByIdModel)
        {
            InitializeComponent();
            this.textBox_Id.Text = getInfoByIdModel.Id;
            this.textBox_LastModified.Text = getInfoByIdModel.MtLastModified.ToString();
            this.textBox_Data.Text =JsonSerializer(getInfoByIdModel.Data);
        }

        private string JsonSerializer(string JsonData)
        {
            //格式化json字符串
            if (!string.IsNullOrEmpty(JsonData))
            {
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(JsonData);
                JsonTextReader jtr = new JsonTextReader(tr);
                var obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
                else
                {
                    return "NO DATA";
                }
            }          
            else
            {
                return "NO DATA";
            }
        }

    }
}
