using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAnalytics.Web.UserControls
{
    public partial class BoreHoleTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Bind()
        {
            List<MO.Borehole> _lst = BL.Report.DailyReport.GetBorehole(string.Empty);

            if (_lst != null)
            {
                foreach (MO.Borehole _obj in _lst)
                {
                    TreeNode _AreaNode = tvBoreHole.FindNode(_obj.AreaID.ToString());

                    if (_AreaNode == null)
                    {
                        _AreaNode = new TreeNode { Text = _obj.AreaName, Value = _obj.AreaID.ToString() };
                        tvBoreHole.Nodes.Add(_AreaNode);
                    }
                    TreeNode _LoopNode = tvBoreHole.FindNode(_obj.AreaID.ToString() + "/" + _obj.LoopID.ToString());

                    if (_LoopNode == null)
                    {
                        _LoopNode = new TreeNode { Text = _obj.LoopName, Value = _obj.LoopID.ToString() };
                        _AreaNode.ChildNodes.Add(_LoopNode);
                    }

                    _LoopNode.ChildNodes.Add(new TreeNode { Text = _obj.BoreHoleName, Value = _obj.BoreHoleID.ToString() });
                }
                tvBoreHole.CollapseAll();
            }
        }
    }

}