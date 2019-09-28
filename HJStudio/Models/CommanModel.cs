using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HJStudio.Models
{
    public class CommanModel
    {
        public static LogedUserDetails LoginUserDetails
        {
            get
            {

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie != null)
                    {
                        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        string data = authTicket.UserData;
                        // data is empty !!!
                        return JsonConvert.DeserializeObject<LogedUserDetails>(data);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("/Login/Index");
                        return null;
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/Login/Index");
                    return null;
                }
            }
        }

        public static string GetEnumDisplayName<T>(T value) where T : struct
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }
    }
    public enum UserType
    {

        [Display(Name = "Admin User")]
        Admin,
        [Display(Name = "User")]
        User,

    }
    public enum wagestype
    {
        Monthly,
        Daily
    }

    public enum Enquirytype
    {
        waiting,
        Pending,
        Confirmed
    }

    public class CalendarModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string description { get; set; }
        public string className { get; set; }
        public string url { get; set; }
    }
}