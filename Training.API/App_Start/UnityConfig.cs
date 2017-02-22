using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.WebApi;
using Training.DAL.Interfaces.Interfaces;
using Training.DAL.Services;
using Training.Identity.Services;
using TrainingTake2.Services;

namespace Training.API.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        public static UnityContainer RegisterTypes()
        {
            var container = new UnityContainer();
            var context = new UserContext();
            container.RegisterType<IAuthRepository, AuthRepository>();
            container.RegisterType<IQueueService, QueueService>();
            container.RegisterInstance<IUserRepository>(new UserRepository(context));
            return container;
        }
    }
}
