using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Threading;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace DAnalytics.UTIL
{
    public delegate void ProcessCompleted(WebkitFile _File);

    public class WebkitFile
    {
        public AutoResetEvent Handler = new AutoResetEvent(false);

        public event ProcessCompleted OnProcessCompleted;

        public string HtmlFilePath { get; set; }
        public string PDFFilePath { get; set; }
        bool _IsSuccess = false;
        public Semaphore WaitHandler;
        public bool IsSuccess
        {
            get { return _IsSuccess; }
        }
        public bool IsRootFile { get; set; }

        public void Convert2PDF()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Convert.ToString(ConfigurationManager.AppSettings["WKPDF"]);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.Arguments = "\"" + HtmlFilePath + "\" \"" + PDFFilePath + "\" -O Portrait -s Letter";
            Process p = new Process();
            p.StartInfo = psi;
            try
            {
                p.Start();
                p.WaitForExit();
                _IsSuccess = true;
            }
            catch
            {
                _IsSuccess = false;
            }
            finally
            {
                if (WaitHandler != null)
                    WaitHandler.Release();
                Handler.Set();

                if (OnProcessCompleted != null)
                    OnProcessCompleted(this);
            }
        }
    }

    public class Webkit
    {
        List<WebkitFile> _Files = new List<WebkitFile>();

        int _TotalHtmlFiles = 0, _ProcessedHtnlFiles = 0;

        object _locker = new object();

        public List<WebkitFile> OutputFiles
        {
            get { return _Files; }
        }

        WebkitFile _RootFile = null;

        public WebkitFile RootFile
        {
            get { return _RootFile; }
        }

        public string DailyReportFilePath { get; set; }

        public void ConvertHtmlToPDF(List<string> HtmlFilesPath)
        {
            if (HtmlFilesPath != null && HtmlFilesPath.Count > 0)
            {
                _TotalHtmlFiles = HtmlFilesPath.Count;

                //_RootFile = new WebkitFile
                //{
                //    HtmlFilePath = "http://localhost/DAnalytics.Web/reports/" + Path.GetFileName(HtmlFilesPath[0]),
                //    PDFFilePath = Path.Combine(Path.GetDirectoryName(HtmlFilesPath[0]), Path.GetFileNameWithoutExtension(HtmlFilesPath[0]) + ".pdf"),
                //    IsRootFile = true
                //};
                //_RootFile.OnProcessCompleted += new ProcessCompleted(_File_OnProcessCompleted);
                //_RootFile.Convert2PDF();

                Semaphore _sem = new Semaphore(30, 50);

                for (int iCount = 0; iCount < HtmlFilesPath.Count; iCount++)
                {
                    _sem.WaitOne();

                    WebkitFile _File = new WebkitFile
                    {
                        HtmlFilePath = "http://localhost/DAnalytics.Web/reports/" + Path.GetFileName(HtmlFilesPath[iCount]),
                        PDFFilePath = Path.Combine(Path.GetDirectoryName(HtmlFilesPath[iCount]), Path.GetFileNameWithoutExtension(HtmlFilesPath[iCount]) + ".pdf"),
                        WaitHandler = _sem
                    };
                    _File.OnProcessCompleted += new ProcessCompleted(_File_OnProcessCompleted);
                    Thread _trd = new Thread(new ThreadStart(delegate()
                    {
                        _File.Convert2PDF();
                    }));
                    _trd.Start();
                }

                while (_TotalHtmlFiles != _ProcessedHtnlFiles)
                {
                    Thread.Sleep(5000);
                }

                DailyReportFilePath = Path.Combine(Path.GetDirectoryName(HtmlFilesPath[0]), "DR_" + DateTime.Now.ToString("ddMMHHmmss") + ".pdf");

                Document document = new Document();
                PdfCopy writer = new PdfCopy(document, new FileStream(DailyReportFilePath, FileMode.Create, FileAccess.Write));
                document.Open();

                for (int iCount = 0; iCount < HtmlFilesPath.Count; iCount++)
                {
                    PdfReader reader = new PdfReader(Path.Combine(Path.GetDirectoryName(HtmlFilesPath[iCount]), Path.GetFileNameWithoutExtension(HtmlFilesPath[iCount]) + ".pdf"));
                    reader.ConsolidateNamedDestinations();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage pdfpage = writer.GetImportedPage(reader, i);
                        writer.AddPage(pdfpage);
                    }
                    PRAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        writer.CopyAcroForm(reader);
                    }
                    reader.Close();
                }
                writer.Close();
                document.Close();
            }

            

        }

        

        void _File_OnProcessCompleted(WebkitFile _File)
        {
            _ProcessedHtnlFiles += 1;

            //lock (_locker)
            //{
            //    if (!_File.IsRootFile)
            //    {
            //        Document document = new Document();
            //        PdfCopy writer = new PdfCopy(document, new FileStream(_RootFile.PDFFilePath, FileMode.Append, FileAccess.Write));
            //        //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(_RootFile.PDFFilePath, FileMode.Append, FileAccess.Write));
            //        document.Open();

            //        PdfReader reader = new PdfReader(_File.PDFFilePath);
            //        reader.ConsolidateNamedDestinations();

            //        for (int i = 1; i <= reader.NumberOfPages; i++)
            //        {
            //            PdfImportedPage pdfpage = writer.GetImportedPage(reader, i);
            //            writer.AddPage(pdfpage);
            //        }
            //        PRAcroForm form = reader.AcroForm;
            //        if (form != null)
            //        {
            //            writer.CopyAcroForm(reader);
            //        }
            //        reader.Close();
            //        writer.Close();
            //        document.Close();
            //    }
            //}
        }
    }

    public class PDFUtil
    {
        public string CombinePDF(List<string> _Files)
        {
            string _CombinedPDFPath = string.Empty;
            if (_Files.Count > 0)
            {
                _CombinedPDFPath = Path.Combine(Path.GetDirectoryName(_Files[0]), "DR_" + DateTime.Now.ToString("ddMMHHmmss") + ".pdf");

                Document document = new Document();
                PdfCopy writer = new PdfCopy(document, new FileStream(_CombinedPDFPath, FileMode.Create, FileAccess.Write));
                document.Open();

                for (int iCount = 0; iCount < _Files.Count; iCount++)
                {
                    PdfReader reader = new PdfReader(_Files[iCount]);
                    reader.ConsolidateNamedDestinations();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage pdfpage = writer.GetImportedPage(reader, i);
                        writer.AddPage(pdfpage);
                    }
                    PRAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        writer.CopyAcroForm(reader);
                    }
                    reader.Close();
                }
                writer.Close();
                document.Close();
            }
            return _CombinedPDFPath;
        }
    }
}
