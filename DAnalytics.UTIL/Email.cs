using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace DAnalytics.UTIL
{
    /// <summary>
    /// Utility Layer class for sending and receiving emails
    /// </summary>
    public class Email
    {

        public string Subject { get; set; }

        public string Body { get; set; }

        List<string> _Attachments = new List<string>();

        public List<string> Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }

        string _From = ConfigurationManager.AppSettings["FromEmail"];
        public string From
        {
            get
            {
                return _From;
            }
            set { _From = value; }
        }

        public string ToEmail { get; set; }

        string _FromName = ConfigurationManager.AppSettings["FromName"];
        public string FromName
        {
            get
            {
                return _FromName;
            }
            set { _FromName = value; }
        }

        public string ToName { get; set; }

        public string SMTPServer { get { return ConfigurationManager.AppSettings["SMTPServer"]; } }

        public int SMTPPort { get { return Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]); } }

        public string SMTPUserName { get { return ConfigurationManager.AppSettings["SMTPUserName"]; } }

        public string SMTPPassword { get { return ConfigurationManager.AppSettings["SMTPPassword"]; } }

        public MailAddress FromAddress { get { return new MailAddress(From, FromName); } }

        public MailAddress ToAddress { get { return new MailAddress(ToEmail, ToName); } }


        public static bool SendMail(Email Mail)
        {
            bool _SendMail = false;
            try
            {
                MailMessage msg = new MailMessage(Mail.FromAddress, Mail.ToAddress)
                {
                    Body = Mail.Body,
                    Subject = Mail.Subject,
                    Sender = Mail.FromAddress,
                    IsBodyHtml = true
                };

                foreach (string strFile in Mail.Attachments)
                {
                    Attachment att = new Attachment(strFile);
                    msg.Attachments.Add(att);
                }
                SmtpClient smtp = new SmtpClient(Mail.SMTPServer, Mail.SMTPPort);

                //smtp.Credentials = new NetworkCredential(Mail.SMTPUserName, Mail.SMTPPassword);
                smtp.Credentials = new NetworkCredential(Mail.SMTPUserName, "");
                smtp.Send(msg);
                _SendMail = true;
            }
            catch (SmtpException se)
            {
                DAnalHelper.LogError(se.Message, "Email");
                _SendMail = false;
            }
            catch (Exception ex)
            {
                DAnalHelper.LogError(ex.Message, "Email");
                _SendMail = false;
            }
            return _SendMail;
        }
    }
}
