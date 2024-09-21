using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.IdentityModel.Tokens;
using PhoneNumbers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using FluentEmail.Core.Models;

namespace SoKHCNVTAPI.Helpers;

public class EmailHelper
{

    public string FromEmail { set; get; } = "neolock18@gmail.com";

    public EmailHelper()
    {
        SmtpClient smtp = new SmtpClient
        {
            //smtp Server address
            Host = "in-v3.mailjet.com",
            Port = 587,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            // Enter here that you are sending smtp User name and password for the server 
            Credentials = new NetworkCredential("7a6d4404a7c1b3ae22b05c3fc6372b48", "cc7f0253d768d0d1ec9e4af960bd074d"),
            //EnableSsl = true
        };

        Email.DefaultSender = new SmtpSender(smtp);
        Email.DefaultRenderer = new RazorRenderer();
    }
    public bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SendEmail(string email, string CC, string subject, string body)
    {
        var sendReponse = await Email
            .From(FromEmail)
            .To(email)
            .CC(CC)
            .Subject(subject)
            .Body(body)
            .SendAsync();

        return sendReponse.Successful;
    }

    public async Task<bool> SendEmailAction(string email, string cc, string subject)
    {
        var template = "Xin chào, @Model.Fullname You are totally @Model.WorkflowStepName.";
        //var tem = $"{Directory.GetCurrentDirectory()}/Templates/Email/workflow_step_action.cshtml";
        var sendReponse = await Email
            .From(FromEmail)
            .To(email)
            .CC(cc)
            .Subject(subject)
            .UsingTemplate(template, new { Fullname = "CÃ´ng Nguyá»…n", WorkflowStepName = "SKHCN" })
            .SendAsync();

        return sendReponse.Successful;
    }

    public async Task<bool> SendEmailTemplate(string template, dynamic model, string email, string subject, string attachedFile)
    {
        var tem = $"{Directory.GetCurrentDirectory()}/Templates/Email/{template}";
        //var tem = $"{Directory.GetCurrentDirectory()}/Templates/Email/workflow_step_action.cshtml";

        if (attachedFile != null && !attachedFile.IsNullOrEmpty())
        {
            var stream = File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), attachedFile)}");
            var attachment = new FluentEmail.Core.Models.Attachment
            {
                Data = stream,
                Filename = "Test.txt",
                ContentType = "text/plain"
            };

            var sendReponse = await Email
           .From(FromEmail)
           .To(email)
           .Subject(subject)
           .Attach(attachment)
           .UsingTemplateFromFile(tem, model)
           .SendAsync();
            return sendReponse.Successful;
        } else
        {
            var sendReponse = await Email
            .From(FromEmail)
            .To(email)
            .Subject(subject)
            .UsingTemplateFromFile(tem, model)
            .SendAsync();
            return sendReponse.Successful;
        }
    }
}