using System.Net;
using System.Net.Mail;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class SendEmailRepository : GenericRepository<SendEmail>, ISendEmailRepository
    {
        #region constructeur
        public SendEmailRepository(SecelPartnerDataContext context)
            : base(context) { }
        #endregion

        #region methodes
        /// <summary>
        /// nous avons principalement besoin de lemail de celui qui va recevoir le message , le sujet et le massage en lui meme
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public void EmailSend(SendEmail sendMail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sendMail.FromEmail);
            mail.To.Add(sendMail.ToEmail);

            mail.Subject = "SecelPartner - " + sendMail.Subject;
            //les emails en copie
            //mail.CC.Add("");
            //mail.Bcc.Add("");

            mail.IsBodyHtml = true;

            string content = "Name : " + sendMail.Name;
            content += "<br/> Email :" + sendMail.FromEmail;
            content += "<br/> Message :" + sendMail.Message;

            mail.Body = content;

            //create SMTP inatant

            //on passe ladresse email de notre server de messagerie et le numero du prt sur le quel envoyer
            //(smtp.gmail.com pour le serveur de gmail) et le service SMTP smtp-relay.gmail.com ecoute sur les ports suivants : 25, 465 ou 587
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            //create network credential
            NetworkCredential networkCredential = new NetworkCredential(
                "secelpartner001@gmail.com",
                "bjrftyrwtxbobhvs"
            );

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = networkCredential;
            smtpClient.Port = 25; //si on pas precise le port plus haut
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }
        #endregion
    }
}
