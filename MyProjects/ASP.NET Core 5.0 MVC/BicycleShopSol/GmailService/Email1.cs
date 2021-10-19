//using System;
//using MailKit.Net.Smtp;
//using Microsoft.Extensions.Logging;
//using MimeKit;

//namespace GmailService
//{
//    public class Email1
//    {
//        readonly ILogger<Email1> logger;
//        public Email1(ILogger<Email1> logger)
//        {
//            this.logger = logger;
//        }
//        public void SendEmailGoodIsBought(string customerName, string customerLastname, string goodName, 
//                                                        int goodQuantity, int total, string email)
//        {
//            try
//            {
//                MimeMessage message = new MimeMessage();
//                message.From.Add(new MailboxAddress("Hanna's shop", "cassiopeia.anna@gmail.com"));
//                message.To.Add(new MailboxAddress(email));
//                message.Subject = "Successful purchase!";
//                message.Body = new TextPart()
//                {
//                    Text = $"Dear customer, {customerName} {customerLastname}. You just bought {goodQuantity} bicycle(s) {goodName}. " +
//                $"Your purchase costs {total} UAH. Thank you for choosing Hanna's Shop."
//                };
//                using (SmtpClient client = new SmtpClient())
//                {
//                    client.Connect("smtp.gmail.com", 465, true);
//                    client.Authenticate("hanna.kasai.1994@gmail.com", "annacassio2603");
//                    client.Send(message);
//                    client.Disconnect(true);
//                    logger.LogInformation("Messege is delivered successfuly.");
//                }
//            }
//            catch (Exception e)
//            {
//                logger.LogError(e.GetBaseException().Message);
//            }
//        }
//    }
//}
