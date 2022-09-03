using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogLibrary
/// </summary>
public class LogLibrary
{
    public static string Logging(string Status, string Function, string UserName, string Message)
    {
        if (Status == "S")
        {
            Status = "START";
        }
        else if (Status == "E")
        {
            Status = "END";
        }
        return Status + " - " + Function + " \n\t\t Username: " + UserName + " \n\t\t Message: " + Message;
    }

    public static string Error(string Function, string UserName, string Message)
    {
        return Function + " \n\t\t Username: " + UserName + " \n\t\t Message: " + Message;
    }
}