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
        public static void TextMessage(string address,string name,out string pin,out string password)
        {
            pin = Password.PinCodeGen();
            SmtpClient client = new SmtpClient();
            //client.Port = 587;
            client.Host = "smtp.mail.ru";
            client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("your mail", "password");
            MailMessage mail;
            password = Password.NewPassword();
            
                try
                {
                    mail = new MailMessage("sahakyan-m15@mail.ru", address);
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
