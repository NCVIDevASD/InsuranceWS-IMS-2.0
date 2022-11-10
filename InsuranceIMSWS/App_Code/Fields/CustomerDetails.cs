using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CustomerDetails
/// </summary>
public class CustomerDetails
{
    #region Constructor
    public CustomerDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _insuranceCustomerNo;
    public string InsuranceCustomerNo
    {
        get { return _insuranceCustomerNo; }
        set { _insuranceCustomerNo = value; }
    }

    private string _iClickCustomerNo;
    public string IClickCustomerNo
    {
        get { return _iClickCustomerNo; }
        set { _iClickCustomerNo = value; }
    }

    private string _otherCustomerNo;
    public string OtherCustomerNo
    {
        get { return _otherCustomerNo; }
        set { _otherCustomerNo = value; }
    }

    private bool _customerExists;
    internal bool CustomerExists
    {
        get { return _customerExists; }
        set { _customerExists = value; }
    }

    private string _insuredClass;
    public string InsuredClass
    {
        get { return _insuredClass; }
        set { _insuredClass = value; }
    }

    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _middleName;
    public string MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _suffix;
    public string Suffix
    {
        get { return _suffix; }
        set { _suffix = value; }
    }

    private string _birthdate;
    public string Birthdate
    {
        set { _birthdate = value; }
        get { return _birthdate; }
    }

    private DateTime _dateOfBirth;
    internal DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value; }
    }

    private int _age;
    internal int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    private string _placeOfBirth;
    public string PlaceOfBirth
    {
        get { return _placeOfBirth; }
        set { _placeOfBirth = value; }
    }

    private string _gender;
    public string Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    private string _tinID;
    public string TinID
    {
        get { return _tinID; }
        set { _tinID = value; }
    }

    private string _licenseNumber;
    public string LicenseNumber
    {
        get { return _licenseNumber; }
        set { _licenseNumber = value; }
    }

    private string _validIDPresented;
    public string ValidIDPresented
    {
        get { return _validIDPresented; }
        set { _validIDPresented = value; }
    }

    private string _validIDNumber;
    public string ValidIDNumber
    {
        get { return _validIDNumber; }
        set { _validIDNumber = value; }
    }

    private string _mobileNumber;
    public string MobileNumber
    {
        get { return _mobileNumber; }
        set { _mobileNumber = value; }
    }

    private string _landline;
    public string Landline
    {
        get { return _landline; }
        set { _landline = value; }
    }

    private string _emailAddress;
    public string EmailAddress
    {
        get { return _emailAddress; }
        set { _emailAddress = value; }
    }

    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    private string _city;
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    private string _province;
    public string Province
    {
        get { return _province; }
        set { _province = value; }
    }

    private string _zipCode;
    public string ZipCode
    {
        get { return _zipCode; }
        set { _zipCode = value; }
    }

    private string _nationality;
    public string Nationality
    {
        get { return _nationality; }
        set { _nationality = value; }
    }

    private string _civilStatus;
    public string CivilStatus
    {
        get { return _civilStatus; }
        set { _civilStatus = value; }
    }

    private string _sourceOfFunds;
    public string SourceOfFunds
    {
        get { return _sourceOfFunds; }
        set { _sourceOfFunds = value; }
    }

    private string _natureOfWork;
    public string NatureOfWork
    {
        get { return _natureOfWork; }
        set { _natureOfWork = value; }
    }
    #endregion

    private string _signaturePath;
    public string SignaturePath
    {
        set { _signaturePath = value; }
        get { return _signaturePath; }
    }

    private string _userId;
    internal string UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }

    #region Public Methods
    public void GetCustomerNo()
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Updater].[usp_InsertCustomerOrGetCustomerNo]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@iclickCustomerNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.IClickCustomerNo));
                    sqlCommand.Parameters.AddWithValue("@otherCustomerNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.OtherCustomerNo));
                    sqlCommand.Parameters.AddWithValue("@firstName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.FirstName));
                    sqlCommand.Parameters.AddWithValue("@middleName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.MiddleName));
                    sqlCommand.Parameters.AddWithValue("@lastName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.LastName));
                    sqlCommand.Parameters.AddWithValue("@suffix", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Suffix));
                    sqlCommand.Parameters.AddWithValue("@dateOfBirth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(this.DateOfBirth));
                    sqlCommand.Parameters.AddWithValue("@placeOfBirth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.PlaceOfBirth));
                    sqlCommand.Parameters.AddWithValue("@gender", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Gender));
                    sqlCommand.Parameters.AddWithValue("@tinId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.TinID));
                    sqlCommand.Parameters.AddWithValue("@validIDPresented", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.ValidIDPresented));
                    sqlCommand.Parameters.AddWithValue("@validIDNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.ValidIDNumber));
                    sqlCommand.Parameters.AddWithValue("@mobileNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.MobileNumber));
                    sqlCommand.Parameters.AddWithValue("@landline", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Landline));
                    sqlCommand.Parameters.AddWithValue("@emailAddress", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.EmailAddress));
                    sqlCommand.Parameters.AddWithValue("@address", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Address));
                    sqlCommand.Parameters.AddWithValue("@city", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.City));
                    sqlCommand.Parameters.AddWithValue("@province", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Province));
                    sqlCommand.Parameters.AddWithValue("@zipCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.ZipCode));
                    sqlCommand.Parameters.AddWithValue("@nationality", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Nationality));
                    sqlCommand.Parameters.AddWithValue("@civilStatus", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.CivilStatus));
                    sqlCommand.Parameters.AddWithValue("@sourceOfFunds", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.SourceOfFunds));
                    sqlCommand.Parameters.AddWithValue("@natureOfWork", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.NatureOfWork));
                    sqlCommand.Parameters.AddWithValue("@userId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.UserId));
                    /* added by jhay */
                    sqlCommand.Parameters.AddWithValue("@signaturePath", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.SignaturePath));
                    /* end */

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            this.InsuranceCustomerNo = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_InsuranceCustomerNo"]);
                            this.IClickCustomerNo = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_iClickCustomerNo"]);
                            this.OtherCustomerNo = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_OtherCustomerNo"]);
                            if (rd.NextResult())
                            {
                                while (rd.Read())
                                {
                                    this.CustomerExists = RDFramework.Utility.Conversion.SafeReadDatabaseValue<bool>(rd["CustomerExists"]);
                                }
                            }
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Get Insurance CustomerNo";
            auditTrail.ActionDetails = "InsuranceIMSWS: insurance customer number pulled/generated.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("Error in Creation of New Customer and/or Getting Customer Number.");
        }
    }

    public static bool IsCustomerEligible(GeneralDetails generalDetails, CustomerDetails customerDetails)
    {
        bool returnValue = false;
        string birthday = customerDetails.DateOfBirth.ToString("MM/dd/yyyy");
        string formattedBirthday = birthday.Substring(6, 4) + birthday.Substring(0, 2) + birthday.Substring(3, 2);

        int formattedDateToday = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
        int formattedBirthdate = int.Parse(formattedBirthday);
        customerDetails.Age = (formattedDateToday - formattedBirthdate) / 10000;

        if (RDFramework.Utility.Validation.IsNumeric(customerDetails.Age.ToString()) && customerDetails.Age >= generalDetails.MinimumAge && customerDetails.Age <= generalDetails.MaximumAge)
        {
            returnValue = true;
        }

        return returnValue;
    }

    #endregion
}