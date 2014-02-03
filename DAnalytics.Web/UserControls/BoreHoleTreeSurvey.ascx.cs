using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAnalytics.UTIL;
using System.Data;
using System.Text;
namespace DAnalytics.Web.UserControls
{
    public partial class BoreHoleTreeSurvey : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FillData()
        {
            ddlArea.DataSource = BL.Report.DailyReport.GetArea();
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "AreaID";
            ddlArea.DataBind();

            ddlYear.DataSource = BL.Report.DailyReport.GetSurveyYear();
            ddlYear.DataTextField = "SurveyYear";
            ddlYear.DataValueField = "SurveyYear";
            ddlYear.DataBind();
        }

        public void Bind()
        {
            List<MO.Borehole> _lst = BL.Report.DailyReport.GetBorehole(ddlYear.SelectedItem.Value.ConvertToInt32(), ddlArea.SelectedItem.Value.ConvertToInt32());

            if (_lst != null)
            {
                foreach (MO.Borehole _obj in _lst)
                {
                    TreeNode _SurveyNode = tvBoreHole.FindNode(_obj.SurveyID.ToString());

                    if (_SurveyNode == null)
                    {
                        _SurveyNode = new TreeNode { Text = _obj.SurveyName, Value = _obj.SurveyID.ToString(), ToolTip = "Survey" };
                        tvBoreHole.Nodes.Add(_SurveyNode);
                    }

                    TreeNode _AreaNode = tvBoreHole.FindNode(_obj.SurveyID.ToString() + "/" + _obj.AreaID.ToString());

                    if (_AreaNode == null)
                    {
                        _AreaNode = new TreeNode { Text = _obj.AreaName, Value = _obj.AreaID.ToString(), ToolTip = "Area" };
                        _SurveyNode.ChildNodes.Add(_AreaNode);
                    }

                    TreeNode _LoopNode = tvBoreHole.FindNode(_obj.SurveyID.ToString() + "/" + _obj.AreaID.ToString() + "/" + _obj.LoopID.ToString());

                    if (_LoopNode == null)
                    {
                        _LoopNode = new TreeNode { Text = _obj.LoopName, Value = _obj.LoopID.ToString(), ToolTip = "Loop" };
                        _AreaNode.ChildNodes.Add(_LoopNode);
                    }

                    _LoopNode.ChildNodes.Add(new TreeNode { Text = _obj.BoreHoleName, Value = _obj.BoreHoleID.ToString(), ToolTip = "Borehole" });
                }
                tvBoreHole.CollapseAll();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Bind();
        }

        DataTable dt;
        

        void GetSelections()
        {
            dt = new DataTable();
            dt.Columns.Add("BoreHoleID", typeof(int));

            foreach (TreeNode _node in tvBoreHole.Nodes)
            {
                if (_node.Checked)
                {
                    switch (_node.ToolTip)
                    {

                    }
                }
            }
        }

        void GetChildSelections(TreeNode _node, bool IsRootChecked)
        {

        }
    }
}