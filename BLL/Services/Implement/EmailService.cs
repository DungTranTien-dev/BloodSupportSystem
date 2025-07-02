using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using BLL.Services.Interface;
using Common.Settings;
using DAL.UnitOfWork;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using DAL.Models;

namespace BLL.Services.Implement
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IOptions<EmailSettings> emailSettings, IUnitOfWork unitOfWork)
        {
            _emailSettings = emailSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, false);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendEmailBloodRequestAsync(BloodRequest request, string email, string paymentUrl)
        {
            string subject = $"[Blood Payment] Request #{request.BloodRequestId.ToString().Substring(0, 8)} - Payment Required";

            string body = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='UTF-8' />
<meta name='viewport' content='width=device-width, initial-scale=1.0'/>
<title>Blood Request Payment</title>
<style>
    body {{
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f4f6f8;
        margin: 0; padding: 0;
        color: #333;
    }}
    .container {{
        max-width: 600px;
        margin: 30px auto;
        background: #fff;
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        padding: 30px;
    }}
    h2 {{
        color: #dc3545;
        margin-bottom: 20px;
    }}
    table {{
        border-collapse: collapse;
        width: 100%;
        margin-top: 20px;
    }}
    th, td {{
        padding: 10px;
        border: 1px solid #ccc;
        text-align: left;
    }}
    th {{
        background-color: #f2f2f2;
    }}
    .highlight {{
        font-weight: bold;
        color: #007bff;
    }}
    .btn {{
        display: inline-block;
        margin-top: 25px;
        padding: 12px 20px;
        background-color: #28a745;
        color: #fff;
        text-decoration: none;
        border-radius: 5px;
        font-size: 16px;
    }}
    .footer {{
        margin-top: 40px;
        text-align: center;
        font-size: 14px;
        color: #666;
    }}
</style>
</head>
<body>
    <div class='container'>
        <h2>Dear {request.HospitalName},</h2>
        <p>We have received your blood request with the following details:</p>

        <table>
            <tr><th>Patient Name</th><td>{request.PatientName}</td></tr>
            <tr><th>Blood Group</th><td>{request.BloodGroup}</td></tr>
            <tr><th>Component</th><td>{request.ComponentType}</td></tr>
            <tr><th>Volume (ml)</th><td>{request.VolumeInML}</td></tr>
            <tr><th>Reason</th><td>{request.Reason}</td></tr>
            <tr><th>Request Date</th><td>{request.RequestedDate:dd/MM/yyyy}</td></tr>
            <tr><th>Status</th><td class='highlight'>{request.Status}</td></tr>
        </table>

        <p>Please complete the payment to process your request:</p>

        <a href='{paymentUrl}' class='btn' target='_blank'>🔗 Pay Now</a>

        <p style='margin-top: 30px;'>If you have any questions, please contact our support team.</p>

        <div class='footer'>
            <p>Blood Donation System © 2025</p>
        </div>
    </div>
</body>
</html>
";

            await SendEmailAsync(email, subject, body);
        }



     



    }
}
