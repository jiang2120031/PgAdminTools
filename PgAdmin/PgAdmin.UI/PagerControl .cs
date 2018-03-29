using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PgAdmin.UI
{
    public partial class PagerControl : UserControl
    {
        public PagerControl()
        {
            InitializeComponent();
        }


        #region 分页字段和属性  

        private int pageIndex = 1;
        /// <summary>  
        /// 当前页数  
        /// </summary>  
        public virtual int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int pageSize = 100;
        /// <summary>  
        /// 每页记录数  
        /// </summary>  
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int recordCount = 0;
        /// <summary>  
        /// 总记录数  
        /// </summary>  
        public virtual int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        private int pageCount = 0;
        /// <summary>  
        /// 总页数  
        /// </summary>  
        public int PageCount
        {
            get
            {
                if (pageSize != 0)
                {
                    pageCount = GetPageCount();
                }
                return pageCount;
            }
        }

        bool isTextChanged = false;

        #endregion

        #region 页码变化时触发事件  

        public event EventHandler OnPageChanged;

        #endregion

        #region 分页及相关事件功能实现  

        /// <summary>  
        /// 设窗体控件全部可用  
        /// </summary>  
        private void SetFormCtrEnabled()
        {
            gotoBtn.Enabled = true;
        }

        /// <summary>  
        /// 计算总页数  
        /// </summary>  
        /// <returns></returns>  
        private int GetPageCount()
        {
            if (PageSize == 0)
            {
                return 0;
            }
            int pageCount = RecordCount / PageSize;
            if (RecordCount % PageSize == 0)
            {
                pageCount = RecordCount / PageSize;
            }
            else
            {
                pageCount = RecordCount / PageSize + 1;
            }
            return pageCount;
        }
        /// <summary>  
        /// 用于客户端调用  
        /// </summary>  
        public void DrawControl(int count)
        {
            recordCount = count;
            DrawControl(false);
        }
        /// <summary>  
        /// 根据不同的条件，改变页面控件的呈现状态  
        /// </summary>  
        private void DrawControl(bool callEvent)
        {

            currentPageLabel.Text = PageIndex.ToString();
            totalCountLabel.Text = PageCount.ToString();
            pageSizeText.Text = PageSize.ToString();

            if (callEvent && OnPageChanged != null)
            {
                OnPageChanged(this, null);//当前分页数字改变时，触发委托事件  
            }
            SetFormCtrEnabled();
            if (PageCount == 1)//有且仅有一页时  
            {
                gotoBtn.Enabled = false;
            }

        }

        #endregion
        #region 相关控件事件  

        //首页按钮  
        private void linkFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            DrawControl(true);
        }

        #endregion
       
        
        private void gotoBtn_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (int.TryParse(pageNumText.Text.Trim(), out num) && num > 0)
            {
                PageIndex = num;
                DrawControl(true);
            }
        }

        /// <summary>  
        /// 跳转页数限制  
        /// </summary>  
        private void pageNumText_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            if (int.TryParse(pageNumText.Text.Trim(), out num) && num > 0)
            {   //TryParse 函数，将字符串转换成等效的整数，返回bool型，判断是否转换成功。  
                //输入除数字以外的字符是转换不成功的  

                if (num > PageCount)   //输入数量大于最大页数时，文本框自动显示最大页数  
                {
                    pageNumText.Text = PageCount.ToString();
                }
            }
        }

        private void pageNumText_KeyPress(object sender, KeyPressEventArgs e)
        {
            gotoBtn_Click(null, null);
        }

        private void pageSizeText_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            //输入不符合规范时，默认设置为100  
            if (!int.TryParse(pageSizeText.Text.Trim(), out num) || num <= 0)
            {
                num = 100;
                pageSizeText.Text = "100";
            }
            else
            {
                isTextChanged = true;

            }
            pageSize = num;
        }

        /// <summary>  
        /// 光标离开 每页设置文本框时，显示到首页  
        private void pageSizeText_Leave(object sender, EventArgs e)
        {
            if (isTextChanged)
            {
                isTextChanged = false;
                linkFirst_Click(null, null);
            }
        }
    }
}

