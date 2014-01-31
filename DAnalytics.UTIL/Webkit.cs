using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Threading;

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

        int _TotalHtmlFiles = 0,_ProcessedHtnlFiles = 0;

        public List<WebkitFile> OutputFiles
        {
            get { return _Files; }
        }

        public void ConvertHtmlToPDF(List<string> HtmlFilesPath)
        {
            if (HtmlFilesPath != null && HtmlFilesPath.Count > 0)
            {
                _TotalHtmlFiles = HtmlFilesPath.Count;

                List<AutoResetEvent> waitHandler = new List<AutoResetEvent>();

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
                    waitHandler.Add(_File.Handler);
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

                ZipFile
            }
        }

        void _File_OnProcessCompleted(WebkitFile _File)
        {
            _ProcessedHtnlFiles += 1;
        }
    }
}
