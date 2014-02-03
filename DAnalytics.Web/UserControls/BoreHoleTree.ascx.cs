using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using DAnalytics.UTIL;
namespace DAnalytics.Web.UserControls
{
    public delegate void GenerateReport(DataTable dt, DateTime? From, DateTime? To);

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
                OnGenerateReport(dt, FromDate, ToDate);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divTreeDiv.Style["display"] = "block";
            divSelectionDiv.Style["display"] = "none";
        }


        DataTable dt;

        StringBuilder _area = new StringBuilder();
        StringBuilder _loop = new StringBuilder();
        StringBuilder _borehole = new StringBuilder();

        StringBuilder _SelectionTable = new StringBuilder();

        public string SelectionTable
        {
            get { return _SelectionTable.ToString(); }
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
            }
            foreach (TreeNode _node in tvBoreHole.Nodes)
            {
                if (_node.Checked)
                    GetNodeProp(_node);

                GetChildSelections(_node, _node.Checked);
            }

            _SelectionTable.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\">");
            if (_area.ToString().Length > 0)
                _SelectionTable.Append("<tr><td width=\"10%\">Area<td><td width=\"90%\">").Append(_area.ToString()).Append("</td></tr>");
            if (_loop.ToString().Length > 0)
                _SelectionTable.Append("<tr><td width=\"10%\">Loop<td><td width=\"90%\">").Append(_loop.ToString()).Append("</td></tr>");
            if (_borehole.ToString().Length > 0)
                _SelectionTable.Append("<tr><td width=\"10%\">Borehole<td><td width=\"90%\">").Append(_borehole.ToString()).Append("</td></tr>");
            _SelectionTable.Append("</table>");
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
                case "area":
                    if (_area.ToString().Length > 0) _area.Append(",").Append(_node.Text); else _area.Append(_node.Text);
                    break;
                case "loop":
                    if (_loop.ToString().Length > 0) _loop.Append(",").Append(_node.Text); else _loop.Append(_node.Text);
                    break;
                case "borehole":
                    if (_borehole.ToString().Length > 0) _borehole.Append(",").Append(_node.Text); else _borehole.Append(_node.Text);
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