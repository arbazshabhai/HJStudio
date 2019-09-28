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
        public static List<CalendarModel> GetCalanderFollowup()
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    var LIST = context.FunctionDetails.ToList().Select(x => new CalendarModel
                    {
                        id = x.Id,
                        title = x.FunctionName,
                        start = x.FunctionDate != null ? x.FunctionDate.Value.ToString("yyyy-MM-dd hh:mm") : "",
                        end = x.FunctionDate != null ? x.FunctionDate.Value.ToString("yyyy-MM-dd hh:mm") : "",
                        description = x.FunctionDescription,
                        className = "m-fc-event--solid-info m-fc-event--light",
                        url = "javascript:addfollowup(" + x.Id + ");"

                    }).ToList();
                    return LIST;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}