using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;

namespace IdentityRazor.Mail
{
	public class SendMailService : IEmailSender
	{
		private readonly MailSettings mailSettings;
		private readonly ILogger<SendMailService> logger;
		public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
		{
			mailSettings = _mailSettings.Value;
			logger = _logger;
			logger.LogInformation("Create SendMailService");
		}

		public async Task SendMail(MailContent mailContent)
		{
			var email = new MimeMessage();
			email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
			email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
			email.To.Add(MailboxAddress.Parse(mailContent.To));
			email.Subject = mailContent.Subject;


			var builder = new BodyBuilder();
			builder.HtmlBody = mailContent.Body;
			email.Body = builder.ToMessageBody();

			// use SmtpClient by MailKit
			using var smtp = new SmtpClient();

			try
			{
				smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
				smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
				await smtp.SendAsync(email);
			}
			catch (Exception ex)
			{
				logger.LogError("Error when sending email to " + mailContent.To);
				logger.LogError(ex.Message);
			}

			smtp.Disconnect(true);

			logger.LogInformation("Send mail to " + mailContent.To);
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			await SendMail(new MailContent()
			{
				To = email,
				Subject = subject,
				Body = htmlMessage
			});
		}
	}
}