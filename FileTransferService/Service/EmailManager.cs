using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferService.Service
{
    public class EmailManager
    {
        public MailMessage mail;

        public SmtpClient emailClient;

        /// <summary>
        /// List of email addresses for Carbon Copy.
        /// </summary>
        public IEnumerable<string> CC
        {
            set
            {
                while (this.mail.CC.Count != 0) { this.mail.CC.RemoveAt(0); }
                foreach (string emailaddress in value)
                {
                    this.mail.CC.Add(new MailAddress(emailaddress));
                }
            }
        }

        public IEnumerable<string> BCC
        {
            set
            {
                while (this.mail.Bcc.Count != 0) { this.mail.Bcc.RemoveAt(0); }
                foreach (string emailaddress in value)
                {
                    this.mail.Bcc.Add(new MailAddress(emailaddress));
                }
            }
        }

        public EmailManager(string[] to, string subject, string content, byte[] fileData, string attachmentFileName)
        {
            emailClient = new SmtpClient();
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            this.mail = new MailMessage();
            // mail.From = new MailAddress(from, senderDisplayName);
            //  mail.Sender = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = content;

            for (int i = 0; i < to.Length; i++)
            {
                if (!String.IsNullOrEmpty(to[i]))
                {
                    mail.To.Add(new MailAddress(to[i]));
                }
            }

            if (fileData.Length > 0)
            {
                mail.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(fileData), attachmentFileName, MediaTypeNames.Application.Octet));
            }
            mail.IsBodyHtml = true;

        }

      

        public void Send(bool async = false)
        {
            try
            {
                if (async)
                {
                    this.emailClient.SendAsync(this.mail, null);
                }
                else
                {
                    this.emailClient.Send(this.mail);
                }
            }
            catch 
            {
                throw;
            }
        }
    }

    public class EmailBody
    {
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IEnumerable<String> CC { get; set; }
        public IEnumerable<String> BCC { get; set; }
        public IEnumerable<String> To { get; set; }
    }
}
