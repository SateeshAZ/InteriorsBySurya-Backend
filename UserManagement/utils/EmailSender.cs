using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;

namespace UserManagement.utils
{
    public class EmailSender
    {
        public async Task<bool> SendEmailAsync(string emailAddress, string body)
        {
            string connectionString = Environment.GetEnvironmentVariable("EmailConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("EmailConnectionString is not set in the environment variables.");
                return false;
            }

            try
            {
                // Initialize the EmailClient
                var emailClient = new EmailClient(connectionString);

                // Create the email message
                var emailMessage = new EmailMessage(
                    senderAddress: "DoNotReply@af22f809-ab1d-4b4b-a1ee-439498dc5711.azurecomm.net",
                    content: new EmailContent("Dear Sateesh")
                    {
                        PlainText = "Dear User.",
                        Html = $@"
                    <html>
                        <body>
                            <b> Dear User</b>
                            <p> Greetings from Survey Forms! </p>
                            {body}
                        </body>
                    </html>"
                    },
                    recipients: new EmailRecipients(
                        new List<EmailAddress> { new EmailAddress(emailAddress) }
                    )
                );

                // Send the email
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                    WaitUntil.Completed,
                    emailMessage
                );

                // Log success and return response
                Console.WriteLine($"Email sent successfully. MessageId: {emailSendOperation.Id}");
                return true;
            }
            catch (Exception ex)
            {
                // Log error and return failure response
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }
    }
}
