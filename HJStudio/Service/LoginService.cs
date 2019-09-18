using HJStudio.Data;
using HJStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HJStudio.Service
{
    public class LoginService
    {
        public static LogedUserDetails CheckLogin(LoginModel model)
        {
            try
            {
                using (HJStudioEntities db = new HJStudioEntities())
                {
                    return new LogedUserDetails {
                        EmployeeID = 1,
                        Name = "Kishan",
                        EmailId = "K@gmail.com",
                    };
                    return db.EmployeeMasters.Where(x => x.EmailId == model.EmailId && x.Password.Equals(model.UserPassword)).Select(x => new LogedUserDetails
                    {
                        EmployeeID = x.EmployeeID,
                        Name = x.Name,
                        EmailId = x.EmailId,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}