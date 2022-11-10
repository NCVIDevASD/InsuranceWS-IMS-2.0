using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Settings
/// </summary>
public class Settings
{
	public Settings()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string MicroInsuranceConnectionString
    {
        get
        {
            return RDFramework.Utility.Configuration.GetConnectionString("MicroInsuranceConnectionString");
        }
    }

    public static string MicroInsuranceConnectionStringReader
    {
        get
        {
            return RDFramework.Utility.Configuration.GetConnectionString("MicroInsurance_Reader");
        }
    }

    public static string MicroInsuranceConnectionStringWriter
    {
        get
        {
            return RDFramework.Utility.Configuration.GetConnectionString("MicroInsurance_Writer");
        }
    }

    public static string EventSource
    {
        get
        {
            return RDFramework.Utility.Configuration.GetAppSetting("EventSource");
        }
    }

    public static string GenericServerMessage
    {
        get { return "Server has encountered an error. Event ID {0}"; }
    }

    public static string TokenKey
    {
        get
        {
            return RDFramework.Utility.Configuration.GetAppSetting("TokenKey");
        }
    }

    public static string InvalidToken
    {
        get
        {
            return RDFramework.Utility.Configuration.GetAppSetting("InvalidToken");
        }
    }
}