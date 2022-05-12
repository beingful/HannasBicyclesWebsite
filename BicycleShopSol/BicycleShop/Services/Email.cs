using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using BicycleShop.Models;

namespace BicycleShop.Services
{
    public class Email
    {
        private readonly ILogger<Email> _logger;
        private readonly IConfiguration _configuration;

        public Email(ILogger<Email> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void SendEmailCode(string email, string code, string subject)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                message.From.Add(new MailboxAddress("Hanna's shop", _configuration["VisibleEmail"]));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                message.Body = new TextPart
                {
                    Text = code
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(_configuration["MyEmail"], _configuration["MyAccPass"]);
                    client.Send(message);
                    client.Disconnect(true);

                    _logger.LogInformation("Messege is delivered successfuly.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }
        }

        public void SendEmailGoodIsBought(Receipt receipt)
        {
            string goods = "";

            foreach (var line in receipt.Cart.Lines)
            {
                goods += line.Bicycle.BicycleName + " ";
            }

            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Hanna's shop", _configuration["VisibleEmail"]));
                message.To.Add(new MailboxAddress(receipt.Buyer.Email));
                message.Subject = "Successful purchase!";
                message.Body = new TextPart()
                {
                    Text = $"Dear customer, {receipt.Buyer.Name} {receipt.Buyer.Lastname}. " +
                    $"You just bought bicycle(s) {goods}. " +
                    $"Thank you for choosing Hanna's Shop."
                };
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(_configuration["MyEmail"], _configuration["MyAccPass"]);
                    client.Send(message);
                    client.Disconnect(true);

                    _logger.LogInformation("Messege is delivered successfuly.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }
        }
    }
}
