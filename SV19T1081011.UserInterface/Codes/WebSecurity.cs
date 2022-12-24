using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SV19T1081011.UserInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class WebPrincipal : IPrincipal
    {
        private readonly IIdentity identity;
        private readonly WebUserData userData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="userData"></param>
        public WebPrincipal(IIdentity identity, WebUserData userData)
        {
            this.identity = identity;
            this.userData = userData;
        }
        /// <summary>
        /// 
        /// </summary>
        public IIdentity Identity => this.identity;
        /// <summary>
        /// 
        /// </summary>
        public WebUserData UserData => this.userData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            role = role.ToLower();
            //return role.Equals(this.userData.GroupName.ToLower());

            string[] roles = this.UserData.GroupName.Split(';');
            return roles.Contains(role); 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class WebPrincipalExtension
    {
        /// <summary>
        /// Lấy thông tin liên quan đến phiên đăng nhập của tài khoản
        /// </summary>
        /// <param name="userPrincipal"></param>
        /// <returns></returns>
        public static WebUserData GetUserData(this IPrincipal userPrincipal)
        {
            try
            {
                if (!userPrincipal.Identity.IsAuthenticated)
                    return null;

                WebPrincipal principal = userPrincipal as WebPrincipal;
                if (principal == null)
                {
                    return null;
                }
                else
                {
                    return principal.UserData;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Định nghĩa các Role của tài khoản
    /// </summary>
    public class WebAccountRoles
    {
        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        public const string ADMINISTRATOR = "administrator";
        /// <summary>
        /// Cộng tác viên
        /// </summary>
        public const string MODERATOR = "moderator";
        /// <summary>
        /// Thành viên thông thường
        /// </summary>
        public const string MEMBER = "member";
    }

    /// <summary>
    /// Lưu thông tin đăng nhập của tài khoản
    /// </summary>
    public class WebUserData
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public string ClientIP { get; set; }
        public string SessionId { get; set; }
        /// <summary>
        /// Chuyển dữ liệu về chuỗi để ghi vào cookie
        /// </summary>
        /// <returns></returns>
        public string ToCookieString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// Chuyển chuỗi Cookie về thành đối tượng kiểu WebUserData
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static WebUserData FromCookie(string cookie)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<WebUserData>(cookie);
        }
    }
}