using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Cee.Tools
{
    public class EmailHelper
    {
        public static string CmsPwd = "kk76xr05";
        public static string MD5Code = "Kudongyi2014@Email";

        public static string findCode = "KudongyiRegist2014";

        public static string findPwdUrl = "http://s.kudongyi.com/ForgetPassword/SetPwd";

        public static string findUserPwd = "http://account.kudongyi.com/ForgetPassword/SetPwd";

        public static string activeUrl = "http://account.kudongyi.com/Email";

        public static string activeSubject = "请完成酷动易邮箱激活";

        public static string findPwdSubject = "找回密码-酷动易";

        public static string findPwdContent = @"尊敬的用户：您好！<br/>
您的登录密码已被重置，请点击以下连接重新设置您的登录密码<br/>
{0},感谢您选择酷动易，如有任何问题请拨打028-83222052 联系我们。";

        public static string activeContent = @"尊敬的用户：您好！<br/>
您已成功注册酷动易账号，请牢记以下信息。<br/>
您的账号：{0}<br/>
初始密码：{1}（请于第一次登录系统后及时更改以保证您的账户安全）<br/>
请点击链接验证密保邮箱{2}<br/>
（该邮箱如未验证您将无法找回密码，所以请立即验证）<br/>
感谢您选择酷动易，如有任何问题请拨打028-83222052 联系我们。";

        public static string UpdateContent = @"尊敬的用户：您好！<br/>
您的密保邮箱已更改，请点击链接验证密保邮箱{0}<br/>
（该邮箱如未验证您将无法找回密码，所以请立即验证）<br/>
感谢您选择酷动易，如有任何问题请拨打028-83222052 联系我们。";
        
        public static string OrderPassContent = @"尊敬的用户：您好！<br/>
您编号为{0}的订单已通过审核， 您的授权使用时间将于{1}开始至{2}结束。在此期间您可以随时登录使用我们提供的所有免费模板和抽奖游戏。<br/>
感谢您选择酷动易，如有任何问题请拨打028-83222052 联系我们。";

        public static string OrderFailContent = @"尊敬的用户：很抱歉！<br/>
您编号为{0}的订单由于截至到{1}都未收到付款，所以未通过审核。<br/>
感谢您选择酷动易，如有任何问题请拨打028-83222052 联系我们。";

        public static string TestAccountContent = @"尊敬的用户：您好！<br/>
您已成功分配酷动易测试账号，请牢记以下信息。<br/>
您的账号：{0}<br/>
账号密码：{1}<br/>
感谢您选择酷动易，如有任何问题请拨打028-83222052 联系我们。";
        
        public bool SendMailUseGmail(string toMailAddress,string subject,string strContent)
        {
            string sendMail = "message@kudongyi.com";
            string serverMailPwd = "weme1314";
            string toMail = toMailAddress;
            MailAddress from = new MailAddress(sendMail);
            MailAddress to = new MailAddress(toMail);
            MailMessage mail = new MailMessage(from, to);
            mail.From = new MailAddress("message@kudongyi.com", "酷动易", Encoding.GetEncoding("utf-8"));

            //switch (type)
            //{
            //    //注册
            //    case 1:
            //        //mail.Subject = activeSubject;
            //        //mail.Body = string.Format(activeContent, "", "", "<a href=" + activeUrl + parameUrl + " target=_blank>点击激活邮箱</a>");
            //        break;
            //    //忘记密码
            //    case 2:
            //        //mail.Subject = findPwdSubject;
            //        //mail.Body = findPwdContent + "<a href=" + findUserPwd + parameUrl + " target=_blank>点击重设密码</a>";
            //        break;
            //}

            mail.Subject = subject;
            mail.Body = strContent;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            mail.Sender = new MailAddress(sendMail);
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.ym.163.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(sendMail, serverMailPwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                return false;
            }
        }

        public bool SendMailSaleGmail(string toMailAddress, string parameUrl)
        {
            string sendMail = "message@kudongyi.com";
            string serverMailPwd = "weme1314";
            string toMail = toMailAddress;
            MailAddress from = new MailAddress(sendMail);
            MailAddress to = new MailAddress(toMail);
            MailMessage mail = new MailMessage(from, to);
            mail.From = new MailAddress("message@kudongyi.com", "酷动易", Encoding.GetEncoding("utf-8"));

            mail.Subject = findPwdSubject;
            mail.Body = findPwdContent + "<a href=" + findPwdUrl + parameUrl + " target=_blank>点击找回密码</a>";

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            mail.Sender = new MailAddress(sendMail);
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.ym.163.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(sendMail, serverMailPwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                return false;
            }
        }
    }
}