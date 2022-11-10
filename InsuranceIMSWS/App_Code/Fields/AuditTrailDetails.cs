﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Summary description for AuditTrailDetails
/// </summary>
public class AuditTrailDetails
{
    #region Constructors
    public AuditTrailDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    //private string _ipAddress;
    //public string IPAddress
    //{
    //    get { return _ipAddress; }
    //    set { _ipAddress = value; }
    //}

    private string _actionTaken;
    public string ActionTaken
    {
        get { return _actionTaken; }
        set { _actionTaken = value; }
    }

    private string _actionDetails;
    internal string ActionDetails
    {
        get { return _actionDetails; }
        set { _actionDetails = value; }
    }
    #endregion

    #region Public Methods
    public void InsertAuditTrailEntry()
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Security].[usp_InsertInsuranceWSAuditTrailEntry]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ipAddress", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(IPAddress));
                    sqlCommand.Parameters.AddWithValue("@actionDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Now));
                    sqlCommand.Parameters.AddWithValue("@actionTaken", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.ActionTaken));
                    sqlCommand.Parameters.AddWithValue("@actionDetails", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.ActionDetails));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static string IPAddress
    {
        get
        {
            var host = Dns.GetHostEntry(System.Environment.MachineName);
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    string ipString = ip.ToString();
                    string[] splitIP = ipString.Trim().Split(new char[] { '.' });
                    if ((splitIP[0] != null && splitIP[0] != "127") && (splitIP[0] != null && splitIP[0] != "169"))
                    {
                        return ip.ToString();
                    }
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system.");
        }
    }
    #endregion
}