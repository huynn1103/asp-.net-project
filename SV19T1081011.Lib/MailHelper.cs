using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SV19T1081011.Lib
{
    /// <summary>
    /// Lớp cung cấp chức năng gởi mail
    /// </summary>
    public partial class MailHeper
    {
        /// <summary>
        /// Gửi mail
        /// </summary>
        /// <param name="data"></param>
        public static void SendMail(SendMailData data)
        {
            using (MailMessage mailMessage = new MailMessage(data.EmailFrom, data.EmailTo))
            {
                SmtpClient mailClient = new SmtpClient(data.MailServer, data.Port);
                mailClient.Timeout = 105000;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Credentials = new NetworkCredential(data.MailUser, data.MailPassword);
                mailClient.EnableSsl = data.EnableSSL;

                mailMessage.IsBodyHtml = data.IsBodyHtml;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Subject = data.Subject;
                mailMessage.Body = data.Body;

                mailClient.Send(mailMessage);
            }
        }
        /// <summary>
        /// Gửi mail
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task SendMailAsync(SendMailData data)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage(data.EmailFrom, data.EmailTo))
                {
                    SmtpClient mailClient = new SmtpClient(data.MailServer, data.Port);
                    mailClient.Timeout = 105000;
                    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mailClient.Credentials = new NetworkCredential(data.MailUser, data.MailPassword);
                    mailClient.EnableSsl = data.EnableSSL;

                    mailMessage.IsBodyHtml = data.IsBodyHtml;
                    mailMessage.SubjectEncoding = Encoding.UTF8;
                    mailMessage.Subject = data.Subject;
                    mailMessage.Body = data.Body;

                    await mailClient.SendMailAsync(mailMessage);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Địa chỉ mail hợp lệ?
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string Email)
        {
            return Regex.IsMatch(Email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
    }

    /// <summary>
    /// Dữ liệu dùng để gởi mail
    /// </summary>
    public class SendMailData
    {
        /// <summary>
        /// 
        /// </summary>
        public string MailServer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableSSL { get; set; }
        /// <summary>
        /// Tài khoản dùng để đăng nhập Email người gởi (địa chỉ mail)
        /// </summary>
        public string MailUser { get; set; }
        /// <summary>
        /// Mật khẩu tài khoản email người gởi
        /// </summary>
        public string MailPassword { get; set; }
        /// <summary>
        /// Tên người gởi mail
        /// </summary>
        public string EmailFrom { get; set; }
        /// <summary>
        /// Địa chỉ email người nhận
        /// </summary>
        public string EmailTo { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Nội dung mail
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Có dùng HTML trong nội dung mail không?
        /// </summary>
        public bool IsBodyHtml { get; set; }
    }
}
