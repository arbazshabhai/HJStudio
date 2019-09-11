using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HJStudio.Models
{
    public class CommanModel
    {
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
        [Display(Name = "Super Admin")]
        Super,
        [Display(Name = "Admin User")]
        Admin,
        [Display(Name = "User")]
        User,

    }
}