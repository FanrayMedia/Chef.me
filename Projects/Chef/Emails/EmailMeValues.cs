using System.Net;

namespace Chef.Emails
{
    public class EmailMeValues
    {
        string _senderUserName;
        string _senderFirstName;
        string _senderLastName;
        string _senderEmail;
        string _senderBgImgUrl;
        string _recipientUserName;
        string _recipientFirstName;
        string _recipientLastName;
        string _recipientEmail;
        string _message;
        EmailTemplate _template;

        /// <summary>
        /// 
        /// </summary>
        public EmailMeValues(EmailUser sender,
                             EmailUser recipient, 
                             EmailTemplate template, 
                             string message)
        {
            _senderUserName = sender.UserName;
            _senderFirstName = sender.FirstName;
            _senderLastName = sender.LastName;
            _senderEmail = sender.Email;
            _senderBgImgUrl = sender.BgImgUrl;

            _recipientUserName = recipient.UserName;
            _recipientFirstName = recipient.FirstName;
            _recipientLastName = recipient.LastName;
            _recipientEmail = recipient.Email;
            _message = message;
            _template = template;
        }

        /// <summary>
        /// Sender's email e.g. "John Smith <john@contoso.com>"
        /// </summary>
        public string SenderEmail 
        {
            get
            {
                return string.Format("{0} {1} <{2}>",
                    _senderFirstName,_senderLastName,_senderEmail);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecipientEmail 
        {
            get
            {
                return string.Format("{0} {1} <{2}>",
                    _recipientFirstName, _recipientLastName, _recipientEmail);
            }
        }

        /// <summary>
        /// Returns the subject of the email.
        /// </summary>
        public string GetSubject()
        {
            string subject = _template.Subject;
            subject = subject.Replace("{s.firstName}", _senderFirstName);
            subject = subject.Replace("{s.lastName}", _senderLastName);
            return subject;
        }

        /// <summary>
        /// Returns the body of the email.
        /// </summary>
        public string GetHtmlBody()
        {
            string body = _template.HtmlBody;
            body = body.Replace("{r.userName}", _recipientUserName);
            body = body.Replace("{r.firstName}", _recipientFirstName);

            body = body.Replace("{s.userName}", _senderUserName);
            body = body.Replace("{s.firstName}", _senderFirstName);
            body = body.Replace("{s.lastName}", _senderLastName);
            body = body.Replace("{s.email}", _senderEmail);
            body = body.Replace("{s.bgImgUrl}", _senderBgImgUrl);
            body = body.Replace("{s.message}", WebUtility.HtmlEncode(_message));
            return body;
        }
    }
}
