using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Following
{
    public class SendMail
    {
        public static void TextMessage(string address, string name, out string pin, string password)
        {
            string[] mails = address.Split('@');
            pin = Password.PinCodeGen();

            string sender;
            MailMessage mail;


            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = mails[mails.Length - 1] == "mail.ru" ? "smtp.mail.ru" : "smtp.gmail.com";
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                switch (mails[mails.Length - 1])
                {
                    case "mail.ru":
                        client.Credentials = new System.Net.NetworkCredential("your email address", "password");
                        sender = "your email address";
                        break;
                    case "gmail.com": client.Credentials = new System.Net.NetworkCredential("your gmail address", "password");
                        sender = "your gmail address";
                        break;
                    case "yandex.ru": client.Credentials = new System.Net.NetworkCredential("your yandex address", "password");
                        sender = "your yandes address";
                        break;
                    default: sender = "";break;
                }
               
                mail = new MailMessage(sender, address);
                mail.Subject = "Following";

                mail.Body = $"Hi Dear {name}! \nYou are followed in most read news of BlogNews\nThis is your PIN code for verify your account: {pin}\nYour password :{password}";

                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{address} email was wrong ");
            }

        }
    }
}
