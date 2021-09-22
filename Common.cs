using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PMS.SchedulerAPI
{
    public static class Common
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        
        public static bool SendEmail(string userName,string from, string to, string subject, string msg, string htmlString = null,string password=null)
        {
            try
            {
                string para = string.Empty;
                StringBuilder strBody = new StringBuilder();
                if (!string.IsNullOrEmpty(htmlString))
                {
                    strBody.Append("<a href='http://localhost:4200/login/userlogin' target='_self' class='link'>Click here To change password</a>");
                }
                else
                {
                    strBody.Append("<h3>" + "Hi " + userName + "</h3>" + "<p style='font-size:20px;'>" +msg + password + " </p>");

                }
                strBody.Append("<br/><br/><br/><p style='font-size:20px;'> Thanks<br/>CT General Hospitals"  + " </p>");
                htmlString += "<br/>";
                htmlString += strBody;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("sendtestmail15@gmail.com", "Sendemail@15");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return false;
        }


        public static bool SendDefaultPasswordEmail(string from, string subject, string msg, string email, string password)
        {

            try
            {
                StringBuilder strBody = new StringBuilder();
                //strBody.Append("<a href=''>Click here To change password</a>");

                string htmlString = "<html><body><form><h2>" + msg + "</h2> <p style='color: blue; font - size:46px;'> " + email + ", " + password + " </p></form></body></html>";
                htmlString += "<br/>";
                htmlString += strBody;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(email));
                message.Subject = subject; //Email subject
                message.IsBodyHtml = true; //to make message body as html
                message.Body = htmlString; //msg with html body
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("sendtestmail15@gmail.com", "Sendemail@15");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return false;
        }

        public static string GeneratePassword()
        {
            int lengthchar = 6;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < lengthchar--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            int length = 1;
            const string validchar = "$@$!%*?&";
            while (0 < length--)
            {
                res.Append(validchar[rnd.Next(validchar.Length)]);
            }

            int lengthnum = 1;
            const string validnum = "1234567890";
            while (0 < lengthnum--)
            {
                res.Append(validnum[rnd.Next(validnum.Length)]);
            }
            string password = res.ToString();
            return password;
        }
    }
}
