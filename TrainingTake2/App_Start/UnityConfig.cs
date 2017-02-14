using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.WebApi;
using Training.Identity.Services;

namespace TrainingTake2.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {

        public static UnityContainer RegisterTypes()
        {
            var container = new UnityContainer();

            container.RegisterType<IAuthRepository, AuthRepository>();
            return container;
        }
    }
}
