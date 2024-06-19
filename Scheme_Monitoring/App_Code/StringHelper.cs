using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public static class StringHelper
{
    public static bool IsNumeric(this string Str)
    {
        double d;
        return double.TryParse(Str, out d);
    }
    public static bool IsInt(this string Str)
    {
        Int32 d;
        return Int32.TryParse(Str, out d);
    }
    public static bool IsDateTime(this string Str)
    {
        DateTime d;
        return DateTime.TryParse(Str, out d);
    }
    public static int ToInt(this string Str)
    {
        try
        {
            return Convert.ToInt32(Str);
        }
        catch
        {
            return 0;
        }

    }
    public static decimal ToDecimal(this string Str)
    {
        try
        {
            return Convert.ToDecimal(Str);
        }
        catch
        {
            return 0;
        }

    }
    public static double ToDouble(this string Str)
    {
        try
        {
            return Convert.ToDouble(Str);
        }
        catch
        {
            return 0;
        }

    }
    public static DateTime ToDate(this string Str)
    {
        try
        {
            if (Convert.ToDateTime(Str).ToString("dd/MMM/yyyy") == "01-Jan-1900")
                return Convert.ToDateTime("1900-01-01");
            else if (Convert.ToDateTime(Str).ToString("dd/MMM/yyyy") == "01-Jan-2000")
                return Convert.ToDateTime("1900-01-01");
            else if (Convert.ToDateTime(Str).ToString("dd/MMM/yyyy") == "01/Jan/1900")
                return Convert.ToDateTime("1900-01-01");
            else
                return Convert.ToDateTime(Str);

        }
        catch
        {
            return Convert.ToDateTime("1900-01-01");
        }

    }
}
