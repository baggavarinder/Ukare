using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Ukare.Service.User;
using Core.Users;
using Ukare.Service.Dashboard;
using Core.Dashboard;
using Ukare.Service.Message;
using Core.Messages;

namespace Ukare
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IDashboardService, DashboardService>();
            container.RegisterType<IDashboardManager, DashboardManager>();
            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IMessageManager, MessageManager>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}