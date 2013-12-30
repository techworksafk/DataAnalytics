using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAnalytics.UserControls
{
    /// <summary>
    /// Page changed event args
    /// </summary>
    public class GridPagerChangedEventArgs
    {
        int _CurrentPage = 1;

        public int CurrentPage { get { return _CurrentPage; } }

        public GridPagerChangedEventArgs(int PageNumber)
        {
            _CurrentPage = PageNumber;
        }
    }

    /// <summary>
    /// Drop down list event args
    /// </summary>
    public class DropDownChangeArgs
    {
        int selectedSize = 10;

        public int SelectedSize { get { return selectedSize; } }

        public DropDownChangeArgs(int size)
        {
            selectedSize = size;
        }
    }

    public delegate void GridPagerChangedEventHandler(object sender, GridPagerChangedEventArgs e);

    public delegate void DropDownChangeEventHandler(object sender, DropDownChangeArgs e);

    /// <summary>
    /// Custom Grid pager class
    /// </summary>
    public partial class GridPager : System.Web.UI.UserControl
    {
        public event GridPagerChangedEventHandler GridPagerChanged;

        public event DropDownChangeEventHandler DropDownChanged;

        public int PageSize
        {
            get
            {
                return Convert.ToInt32(ViewState[this.ID + "PageSize"]);
            }
            set
            {
                ViewState[this.ID + "PageSize"] = value;
            }
        }       

        public int PageNum
        {
            get
            {
                return Convert.ToInt32(ViewState[this.ID + "PageNum"]);
            }
            set
            {
                ViewState[this.ID + "PageNum"] = value;
            }
        }       

        public int TotalPages
        {
            get
            {
                return Convert.ToInt32(ViewState[this.ID + "TotalPages"]);
            }
            set
            {
                ViewState[this.ID + "TotalPages"] = value;
            }
        }

        public int TotalRows
        {
            get
            {
                return Convert.ToInt32(ViewState[this.ID + "TotalRows"]);
            }
            set
            {
                ViewState[this.ID + "TotalRows"] = value;
            }
        }

        public string ControlToBind { get; set; }

        /// <summary>
        /// Handles the Load event of the user control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtPageNumber.Attributes.Add("onkeypress", "javascript:return ChangeText(this,event,'" + this.hidPageNumber.ClientID + "','" + this.btnChangePage.ClientID + "')");            
        }

        GridView _BindedGrid;
        public GridView BindedGrid { get { return _BindedGrid; } }

        private void FindControl(System.Web.UI.Control ctrl)
        {

            foreach (System.Web.UI.Control objControl in ctrl.Controls)
            {
                if (objControl is System.Web.UI.WebControls.GridView)
                {
                    if (objControl.ID == this.ControlToBind)
                    {
                        _BindedGrid = (objControl as GridView);
                        _BindedGrid.DataBound += new EventHandler(GridView_DataBound);
                        break;
                    }
                }
                if (objControl.HasControls())
                {
                    this.FindControl(objControl);
                }
            }
        }

        private int ToInt32(string val)
        {
            int retVal = 1;
            try
            {
                retVal = Convert.ToInt32(val);
            }
            catch
            {
                retVal = 1;
            }
            return retVal;
        }

        /// <summary>
        /// Handles the row databound event of the gridview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GridView_DataBound(object sender, EventArgs e)
        {
            if ((sender as GridView).Rows.Count == 0)
                this.Visible = false;
        }

        /// <summary>
        /// Bind the controls in the grid view paging user control.
        /// </summary>
        public void Bind()
        {
            FindControl(this.Page);

            this.spnPageNumber.InnerText = this.PageNum.ToString();

            this.txtPageNumber.Text = this.PageNum.ToString();

            this.spnTotalPages.InnerText = this.TotalPages.ToString();

            if (this.TotalPages > ToInt32(rngPageNumber.MinimumValue)) { rngPageNumber.MaximumValue = this.TotalPages.ToString(); }
            else { rngPageNumber.MaximumValue = rngPageNumber.MinimumValue; }

            this.lnkNext.Enabled = true;

            this.lnkLast.Enabled = true;

            this.lnkPrevious.Enabled = true;

            this.lnkFirst.Enabled = true;

            if (this.TotalPages == this.PageNum || this.TotalPages == 1)
            {
                this.lnkNext.Enabled = false;
                this.lnkLast.Enabled = false;
            }

            if (this.PageNum == 1)
            {
                this.lnkPrevious.Enabled = false;
                this.lnkFirst.Enabled = false;
            }

            if (this.BindedGrid.Rows.Count == 0) { this.Visible = false; }
            else { this.Visible = true; }

            if (this.PageNum > this.TotalPages && this.TotalPages != 0)
            {
                this.PageNum = this.PageNum - 1;

                GridPagerChangedEventArgs gpArgs = new GridPagerChangedEventArgs(this.PageNum);

                if (this.GridPagerChanged != null) { this.GridPagerChanged(this, gpArgs); }
            }

            BindPageSizeDropDownList();

            drpSize.SelectedIndex = drpSize.Items.IndexOf(drpSize.Items.FindByValue(this.PageSize.ToString()));
        }

        /// <summary>
        /// Method to bind page size dropdown.
        /// </summary>
        void BindPageSizeDropDownList()
        {
            drpSize.Items.Clear();

            if (this.TotalRows > 0)
            {
                int pSize = this.TotalRows / 10;

                if ((this.TotalPages % 10) != 0) { pSize += 1; }

                if (pSize > 0)

                    for (int i = 1; i <= pSize; i++)
                    {
                        int size = i * 10;
                        ListItem lst = new ListItem(size.ToString(), size.ToString());
                        drpSize.Items.Add(lst);
                    }
            }
            else
            {
                drpSize.Visible = false;
            }
        }         

        /// <summary>
        /// Size dropdown change handler.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        protected void drpSize_SelectedIndexChanged(object s, EventArgs e)
        {
            DropDownChangeArgs drpArgs = new DropDownChangeArgs(Convert.ToInt32(drpSize.SelectedValue));

            if (this.DropDownChanged != null) { this.DropDownChanged(this, drpArgs); }
        }

        /// <summary>
        /// handles to get the first page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            this.PageNum = 1;

            this.Bind();

            GridPagerChangedEventArgs gpArgs = new GridPagerChangedEventArgs(this.PageNum);

            if (this.GridPagerChanged != null) { this.GridPagerChanged(this, gpArgs); }
        }

        /// <summary>
        /// Handles to get the previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            this.PageNum = this.PageNum - 1;

            this.Bind();

            GridPagerChangedEventArgs gpArgs = new GridPagerChangedEventArgs(this.PageNum);

            if (this.GridPagerChanged != null) { this.GridPagerChanged(this, gpArgs); }
        }

        /// <summary>
        /// Handles the event to get next page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            this.PageNum = (this.PageNum + 1);

            this.Bind();

            GridPagerChangedEventArgs gpArgs = new GridPagerChangedEventArgs(this.PageNum);

            if (this.GridPagerChanged != null) { this.GridPagerChanged(this, gpArgs); }
        }

        /// <summary>
        /// handles the event to get last page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            this.PageNum = this.TotalPages;

            Bind();

            GridPagerChangedEventArgs gpArgs = new GridPagerChangedEventArgs(this.PageNum);

            if (this.GridPagerChanged != null) { this.GridPagerChanged(this, gpArgs); }
        }               

        /// <summary>
        /// Page change event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Change(object sender, EventArgs e)
        {
            this.PageNum = this.ToInt32(hidPageNumber.Value);

            Bind();

            GridPagerChangedEventArgs gpArgs = new GridPagerChangedEventArgs(this.PageNum);

            if (this.GridPagerChanged != null) { this.GridPagerChanged(this, gpArgs); }
        }       
    }
}
