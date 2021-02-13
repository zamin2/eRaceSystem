using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace eRaceProject.Admin.Security
{
  
        public static class Settings
        {
            public static string AdminRole => ConfigurationManager.AppSettings["adminRole"];

            public static string DirectorRole
            { get { return ConfigurationManager.AppSettings["DirectorRole"]; } }
        public static string CoordinatorRole
        { get { return ConfigurationManager.AppSettings["CoordinatorRole"]; } }
        public static string InvestigatorRole
        { get { return ConfigurationManager.AppSettings["InvestigatorRole"]; } }
            public static string SeniorMechanicRole
        { get { return ConfigurationManager.AppSettings["SeniorMechanicRole"]; } }
        public static string MechanicRole
        { get { return ConfigurationManager.AppSettings["mechanicRole"]; } }
        public static string TrackServiceRole
        { get { return ConfigurationManager.AppSettings["trackServiceRole"]; } }
        public static string FoodServiceRole
        { get { return ConfigurationManager.AppSettings["foodServiceRole"]; } }
        public static string ShopRole
        { get { return ConfigurationManager.AppSettings["shopRole"]; } }
        public static string ClerkRole
        { get { return ConfigurationManager.AppSettings["clerkRole"]; } }
        public static string OfficeManagerRole
        { get { return ConfigurationManager.AppSettings["officeManagerRole"]; } }
   

    }
    
}