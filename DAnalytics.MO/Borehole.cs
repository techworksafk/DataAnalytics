
using System.Data;
using System;
namespace DAnalytics.MO
{
    public class Borehole
    {
        public int BoreHoleID { get; set; }
        public string BoreHoleName { get; set; }
        public string Depth { get; set; }
        public string AreaName { get; set; }
        public string BoreholeType { get; set; }
        public int LoopID { get; set; }
        public string LoopName { get; set; }
        public int AreaID { get; set; }

        public string SurveyName { get; set; }
        public int SurveyID { get; set; }
    }

    public class GenerateReportArgs
    {
        public DataTable BoreHoleTable { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool DoAutoPick { get; set; }
        public bool DisplayInternalAlerts { get; set; }
        public string ContractNo { get; set; }
        public string PreparedName { get; set; }
        public string PreparedDesig { get; set; }
    }
}
