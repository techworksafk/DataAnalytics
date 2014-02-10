using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using DAnalytics.UTIL;
using DAnalytics.MO;
namespace DAnalytics.Web.UserControls
{
    public delegate void GenerateReport(GenerateReportArgs args);

    public partial class BoreHoleTree : System.Web.UI.UserControl
    {
        public event GenerateReport OnGenerateReport;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        CultureInfo _enGB = new CultureInfo("en-GB");

        bool GenerateDataTable = false;

        public void Bind()
        {
            txtDtFrom.Text = UTIL.DAnalHelper.FirstDayOfMonth(System.DateTime.Now).ToString("dd/MM/yyyy");
            txtDtTo.Text = UTIL.DAnalHelper.LastDayOfMonth(System.DateTime.Now).ToString("dd/MM/yyyy");

            divTreeDiv.Style["display"] = "block";
            divSelectionDiv.Style["display"] = "none";

            List<MO.Borehole> _lst = BL.Report.DailyReport.GetBorehole(string.Empty);

            if (_lst != null)
            {
                foreach (MO.Borehole _obj in _lst)
                {
                    TreeNode _AreaNode = tvBoreHole.FindNode(_obj.AreaID.ToString());

                    if (_AreaNode == null)
                    {
                        _AreaNode = new TreeNode { Text = _obj.AreaName, Value = _obj.AreaID.ToString(), ToolTip = "Area" };
                        tvBoreHole.Nodes.Add(_AreaNode);
                    }
                    TreeNode _LoopNode = tvBoreHole.FindNode(_obj.AreaID.ToString() + "/" + _obj.LoopID.ToString());

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
            divTreeDiv.Style["display"] = "none";
            divSelectionDiv.Style["display"] = "block";
            GetSelections();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataTable = true;
            GetSelections();

            if (OnGenerateReport != null)
            {
                GenerateReportArgs _args = new GenerateReportArgs();
                _args.FromDate = FromDate;
                _args.ToDate = ToDate;
                _args.BoreHoleTable = dt;
                _args.DoAutoPick = false;

                _args.ContractNo = txtContractNo.Text.Trim();
                _args.PreparedName = txtPrepareName.Text.Trim();
                _args.PreparedDesig = txtPrepareDesig.Text.Trim();

                OnGenerateReport(_args);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divTreeDiv.Style["display"] = "block";
            divSelectionDiv.Style["display"] = "none";
        }


        DataTable dt;

        int _CurrentLoopID = 0, _CurrentAreaID = 0;
        List<StringBuilder> _Selections = new List<StringBuilder>();
        StringBuilder _CurrentSelection;

        StringBuilder _SelectionTable = new StringBuilder();

        public string SelectionTable
        {
            get
            {
                _SelectionTable.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\">");
                foreach (StringBuilder _sb in _Selections)
                {
                    _SelectionTable.Append("<tr><td width=\"100%\">").Append(_sb.ToString()).Append("</td></tr>");
                }
                _SelectionTable.Append("</table>");
                return _SelectionTable.ToString();
            }
        }

        DateTime? _FromDate = null, _ToDate = null;
        public DateTime? FromDate
        {

            get
            {
                if (!string.IsNullOrEmpty(txtDtFrom.Text))
                    _FromDate = Convert.ToDateTime(txtDtFrom.Text, _enGB);

                return _FromDate;
            }
        }

        public DateTime? ToDate
        {
            get
            {
                if (!string.IsNullOrEmpty(txtDtTo.Text))
                    _ToDate = Convert.ToDateTime(txtDtTo.Text, _enGB);
                return _ToDate;
            }
        }
        void GetSelections()
        {
            if (GenerateDataTable)
            {
                dt = new DataTable();
                dt.Columns.Add("BoreHoleID", typeof(int));
                dt.Columns.Add("SurveyID", typeof(string));
            }
            foreach (TreeNode _node in tvBoreHole.Nodes)
            {
                if (_node.Checked)
                    GetNodeProp(_node);

                GetChildSelections(_node, _node.Checked);
            }

        }


        void GetChildSelections(TreeNode _node, bool IsRootChecked)
        {
            foreach (TreeNode _cnode in _node.ChildNodes)
            {
                if (_cnode.Checked || IsRootChecked)
                    GetNodeProp(_cnode);
                GetChildSelections(_cnode, _cnode.Checked || IsRootChecked);
            }
        }

        void GetNodeProp(TreeNode _node)
        {
            switch (_node.ToolTip.ToLower())
            {
                case "borehole":

                    if (
                       _CurrentLoopID != _node.Parent.Value.ConvertToInt32() ||
                       _CurrentAreaID != _node.Parent.Parent.Value.ConvertToInt32())
                    {
                        _CurrentLoopID = _node.Parent.Value.ConvertToInt32();
                        _CurrentAreaID = _node.Parent.Parent.Value.ConvertToInt32();

                        if (_CurrentSelection != null)
                        {
                            if (_CurrentSelection.ToString().EndsWith(","))
                                _CurrentSelection.Remove(_CurrentSelection.ToString().Length - 1, 1);
                            _CurrentSelection.Append(" ]");
                            _Selections.Add(_CurrentSelection);
                        }

                        _CurrentSelection = new StringBuilder();
                        _CurrentSelection.Append(_node.Parent.Parent.Text)
                            .Append("->").Append(_node.Parent.Text).Append(" [");
                    }

                    _CurrentSelection.Append(_node.Text).Append(",");


                    if (GenerateDataTable)
                    {
                        DataRow dr = dt.NewRow();
                        dr["BoreHoleID"] = _node.Value.ConvertToInt32();
                        dt.Rows.Add(dr);
                    }
                    break;
            }
        }
    }

}