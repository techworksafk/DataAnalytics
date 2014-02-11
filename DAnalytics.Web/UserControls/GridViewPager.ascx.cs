using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAnalytics.UTIL;
namespace DAnalytics.Web.UserControls
{
    public delegate void GridViewPagerChange();

    public partial class GridViewPager : System.Web.UI.UserControl
    {

        public event GridViewPagerChange OnGridViewPagerChange;

        public int CurrentPageNumber
        {
            get
            {
                if (hdnPageNumber.Value.ConvertToInt32() < 1)
                    hdnPageNumber.Value = "1";
                return hdnPageNumber.Value.ConvertToInt32();
            }
        }

        public int TotalPages
        {
            get
            {
                if (hdnTotalPages.Value.ConvertToInt32() < 1)
                    hdnTotalPages.Value = "1";
                return hdnTotalPages.Value.ConvertToInt32();
            }
            set
            {
                hdnTotalPages.Value = value < 1 ? "1" : value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Reset();
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            hdnPageNumber.Value = "1";
            ChangePage();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            hdnPageNumber.Value = (CurrentPageNumber - 1).ToString();
            ChangePage();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            hdnPageNumber.Value = (CurrentPageNumber + 1).ToString();
            ChangePage();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            hdnPageNumber.Value = TotalPages.ToString();
            ChangePage();
        }

        protected void btnChangePage_Click(object sender, EventArgs e)
        {
            ChangePage();
        }

        void ChangePage()
        {
            if (CurrentPageNumber == 1)
            {
                lnkFirst.Enabled = false;
                lnkPrevious.Enabled = false;
                lnkLast.Enabled = true;
                lnkNext.Enabled = true;
            }
            else if (CurrentPageNumber == TotalPages)
            {
                lnkFirst.Enabled = true;
                lnkPrevious.Enabled = true;
                lnkLast.Enabled = false;
                lnkNext.Enabled = false;
            }
            else
            {
                lnkFirst.Enabled = true;
                lnkPrevious.Enabled = true;
                lnkLast.Enabled = true;
                lnkNext.Enabled = true;
            }

            txtPageNumber.Text = CurrentPageNumber.ToString();
            lblTotalPages.Text = TotalPages.ToString();

            if (OnGridViewPagerChange != null)
                OnGridViewPagerChange();
        }


        void Reset()
        {
            if (CurrentPageNumber == 1)
            {
                lnkFirst.Enabled = false;
                lnkPrevious.Enabled = false;
                lnkLast.Enabled = true;
                lnkNext.Enabled = true;
            }
            else if (CurrentPageNumber == TotalPages)
            {
                lnkFirst.Enabled = true;
                lnkPrevious.Enabled = true;
                lnkLast.Enabled = false;
                lnkNext.Enabled = false;
            }
            else
            {
                lnkFirst.Enabled = true;
                lnkPrevious.Enabled = true;
                lnkLast.Enabled = true;
                lnkNext.Enabled = true;
            }

            txtPageNumber.Text = CurrentPageNumber.ToString();
            lblTotalPages.Text = TotalPages.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            txtPageNumber.Text = CurrentPageNumber.ToString();
            lblTotalPages.Text = TotalPages.ToString();
            base.OnPreRender(e);
        }
    }
}