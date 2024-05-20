using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TalkyTalk
{
    public class GlobalVariable
    {
        public static string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    }
}