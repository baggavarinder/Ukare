using Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        UKareEntities db = new UKareEntities();
        public DashboardCore GetDashboardDetail()
        {

            int count = db.Users.Count();

            DashboardCore DashboardsData = new DashboardCore();
            DashboardsData.UserCount = count;

            DashboardsData.DailyVisitCount = db.SiteVisits.Where(p => EntityFunctions.TruncateTime(p.LastVisitedDate) == EntityFunctions.TruncateTime(DateTime.Now)).Sum(p => p.VisitCount);
            DashboardsData.Messages = db.Messages.Where(p => p.MessageTypeId == 1).OrderByDescending(p => p.CreatedDate).Select(p => new MessageCore
            {
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                MessageTypeId = p.MessageTypeId,
            }).ToList();


            return DashboardsData;
        }

        public static bool SaveVisitorInfo()
        {
            UKareEntities db = new UKareEntities();
            string CurrentUserIp = GetUser_IP();
                //HttpContext.Current.Request.UserHostAddress;

            SiteVisit vister = db.SiteVisits.Where(p => p.IPaddress == CurrentUserIp && EntityFunctions.TruncateTime(p.LastVisitedDate) == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

            if (vister != null)
            {
                vister.VisitCount = vister.VisitCount + 1;
                vister.LastVisitedDate = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                vister = new SiteVisit();
                vister.IPaddress = CurrentUserIp;
                vister.LastVisitedDate = DateTime.Now;
                vister.VisitCount = 1;
                db.SiteVisits.Add(vister);
                db.SaveChanges();
            }

            CurrentUserIp = string.Empty;
            vister = null;
            db.Dispose();
            return true;
        }

        public static string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }

    }
}
