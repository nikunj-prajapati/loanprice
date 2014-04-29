using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

public static class WebUtility
{
    public static string GetErrorColor()
    {
        return ConfigurationManager.AppSettings["ErrorColor"];
    }
}
