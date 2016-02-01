using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Configuration;

namespace Chef.Emails
{
    public class EmailSender
    {
        static bool initialized = false;
        private static Dictionary<EEmailType, EmailTemplate> _templates = new Dictionary<EEmailType, EmailTemplate>();

        static EmailSender()
        {
            if (initialized) return;
            initialized = true;

            // load emails.xml
            string path = SiteContext.Current.Context.Server.MapPath(Config.EMAIL_TEMPLATE_PATH);

            path = Path.GetFullPath(path);
            XElement doc = XElement.Load(path);

            foreach (var ele in doc.Elements("email"))
            {
                if (ele.NodeType != System.Xml.XmlNodeType.Comment)
                {
                    _templates.Add(
                        (EEmailType) Enum.Parse( typeof(EEmailType), ele.Attribute("emailType").Value, ignoreCase: true ),
                        new EmailTemplate {
                            Subject = ele.Element("subject").Value,
                            HtmlBody = ele.Element("htmlbody").Value,
                    });
                }
            }
        }

        /// <summary>
        /// EmailMe happens between 2 users.
        /// </summary>
        /// <remarks>
        /// https://github.com/sendgrid/sendgrid-csharp/blob/master/SendGrid/Example/Program.cs
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="message"></param>
        public static void SendEmailMe(EmailUser sender, EmailUser recipient, string message) 
        {
            // init
            EmailTemplate template = _templates[EEmailType.EmailMe];
            var vals = new EmailMeValues(sender, recipient, template, message);

            // prep message
            var grid = new SendGridMessage();
            grid.From = new MailAddress(vals.SenderEmail);
            grid.AddTo(vals.RecipientEmail); // one recipient
            grid.Subject = vals.GetSubject();
            grid.Html = vals.GetHtmlBody();

            // send
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["SendGrid_Username"], ConfigurationManager.AppSettings["SendGrid_Password"]);
            var transportWeb = new Web(credentials);
            transportWeb.DeliverAsync(grid);
        }
    }
}