using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HJStudio.Data;
using HJStudio.Models;

namespace HJStudio.Service
{
    public class CommanService
    {
        public static List<ClientModel> clientIDList()
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return context.ClientMasters.Select(x => new ClientModel { ClientID = x.ClientID, Name = x.Name } ).ToList();
                    
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<EmployeeModel> EmployeeNameList()
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    return context.EmployeeMasters.Select(x => new EmployeeModel { EmployeeID=x.EmployeeID, Name = x.Name }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<CalendarModel> GetCalanderFollowup()
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    //var LIST = context.FunctionDetails.ToList().Select(x => new CalendarModel
                    //{
                    //    id = x.Id,
                    //    title = x.FunctionName,
                    //    start = x.FunctionDate != null ? x.FunctionDate.Value.ToString("yyyy-MM-dd hh:mm") : "",
                    //    end = x.FunctionDate != null ? x.FunctionDate.Value.ToString("yyyy-MM-dd hh:mm") : "",
                    //    description = x.FunctionDescription,
                    //    className = "m-fc-event--solid-info m-fc-event--light",
                    //    url = "javascript:addfollowup(" + x.Id + ");"

                    //}).ToList();
                    //return LIST;

                    return (from E in context.EnquiryFollowUps
                     join F in context.FunctionDetails on E.EnquiryId equals F.Id
                     group E by E.EnquiryId into G
                     select G).ToList().Select(G => new CalendarModel
                     {
                         id = G.Key ?? 0,
                         title = G.OrderByDescending(x => x.EnquiryFollowupId).Select(x => x.FunctionDetail.FunctionName).FirstOrDefault(),
                         start = G.OrderByDescending(x => x.EnquiryFollowupId).Select(x => x.NextFolowupDate == null ? "" : x.NextFolowupDate.Value.ToString("yyyy-MM-ddThh:mm:ss")).FirstOrDefault(),//"2019-08-01T11:15:00",
                         end = G.OrderByDescending(x => x.EnquiryFollowupId).Select(x => x.NextFolowupDate == null ? "" : x.NextFolowupDate.Value.ToString("yyyy-MM-ddThh:mm:ss")).FirstOrDefault(),//"2019-08-01T11:15:00",
                         description = G.OrderByDescending(x => x.EnquiryFollowupId).Select(x => x.Remarks).FirstOrDefault() ?? "",
                         className = "m-fc-event--primary",
                         url = "javascript:addfollowup(" + G.Key + ");"
                     }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<JsTree3Node> LoginUserMenu
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session["UserMenu"] != null)
                    {
                        return HttpContext.Current.Session["UserMenu"] as List<JsTree3Node>;
                    }
                    else
                    {
                        int uId = 1;
                        var Menu = MenuService.GetUserMenu(uId);
                        HttpContext.Current.Session["UserMenu"] = Menu;
                        return Menu;
                    }
                }
                catch (Exception)
                {
                    return new List<JsTree3Node>();
                }

            }
        }
    }
}