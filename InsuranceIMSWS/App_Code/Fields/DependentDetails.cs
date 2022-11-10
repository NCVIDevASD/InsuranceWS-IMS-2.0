using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DependentDetails
/// </summary>
public class DependentDetails
{
    #region Constructor
    public DependentDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _cocNumber;
    private string _dependentName;
    private string _dependentRelationship;
    private string _dependentBirth;
    private string _dependentCellphone;
    private string _dependentEmail;
    private string _dependentNatureOfWork;
    private string _dependentEmployer;

    private string _relationship;
    private string _productCode;
    private string _birthdate;
    private string _description;
    private int __dependentLimit;
    private int __dependentCOCLimit;
    private int _minAge;
    private int _maxAge;
    private bool _isValid;
    private bool _canPurchase;
    private int _availableCOCs;
    private int _activeCOCs;

    public string COCNumber
    {
        get { return _cocNumber; }
        set { _cocNumber = value; }
    }
    public string DependentName
    {
        get { return _dependentName; }
        set { _dependentName = value; }
    }
    public string DependentRelationship
    {
        get { return _dependentRelationship; }
        set { _dependentRelationship = value; }
    }
    public string DependentBirth
    {
        get { return _dependentBirth; }
        set { _dependentBirth = value; }
    }
    public string DependentCellphone
    {
        get { return _dependentCellphone; }
        set { _dependentCellphone = value; }
    }
    public string DependentEmail
    {
        get { return _dependentEmail; }
        set { _dependentEmail = value; }
    }
    public string DependentNatureOfWork
    {
        get { return _dependentNatureOfWork; }
        set { _dependentNatureOfWork = value; }
    }
    public string DependentEmployer
    {
        get { return _dependentEmployer; }
        set { _dependentEmployer = value; }
    }

    public string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }
    public string Relationship
    {
        get { return _relationship; }
        set { _relationship = value; }
    }
    public string Birthdate
    {
        get { return _birthdate; }
        set { _birthdate = value; }
    }
    internal string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    internal int DependentLimit
    {
        get { return __dependentLimit; }
        set { __dependentLimit = value; }
    }
    internal int DependentCOCLimit
    {
        get { return __dependentCOCLimit; }
        set { __dependentCOCLimit = value; }
    }
    internal int MinAge
    {
        get { return _minAge; }
        set { _minAge = value; }
    }
    internal int MaxAge
    {
        get { return _maxAge; }
        set { _maxAge = value; }
    }
    internal bool IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }

    public bool CanPurchase
    {
        get { return _canPurchase; }
        set { _canPurchase = value; }
    }

    public int AvailableCOCs
    {
        get { return _availableCOCs; }
        set { _availableCOCs = value; }
    }

    public int ActiveCOCs
    {
        get { return _activeCOCs; }
        set { _activeCOCs = value; }
    }

    #endregion

    #region Public Methods
    public void GetDependentDetails()
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringReader))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_GetDependentDetails]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@productCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.ProductCode));
                    sqlCommand.Parameters.AddWithValue("@description", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(this.Relationship));

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            this.ProductCode = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_ProductCode"]);
                            this.Description = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_DependentDescription"]);
                            this.DependentLimit = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_DependentLimit"]);
                            this.DependentCOCLimit = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_DependentCOCLimit"]);
                            this.MinAge = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_MinAge"]);
                            this.MaxAge = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_MaxAge"]);
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Get Dependent Details";
            auditTrail.ActionDetails = "InsuranceWS: Dependent Details Pulled.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void ValidateDependent(DependentDetails dependentDetails, PetDetails petDetails, DependentCollection dependentCollection)
    {
        DateTime date;
        string formattedBirthday;
        int formattedDateToday;
        int formattedBirthdate;
        double age;
        double min = 0.0;

        if (dependentDetails.Description == "Pet")
        {
            if (petDetails.PetBirth != null)
            {
                //formattedBirthday = petDetails.PetBirth.Substring(6, 4) + petDetails.PetBirth.Substring(0, 2) + petDetails.PetBirth.Substring(3, 2);

                formattedDateToday = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                formattedBirthdate = int.Parse(petDetails.PetBirth);
                age = ((double)formattedDateToday - formattedBirthdate) / 10000;
                min = (double)dependentDetails.MinAge / 12;
            }
            else
            {
                age = petDetails.PetAgeYear + ((double)petDetails.PetAgeMonth / 12);
                min = (double)dependentDetails.MinAge / 12;
            }
        }
        else
        {
            date = Convert.ToDateTime(dependentDetails.Birthdate);
            formattedBirthday = date.ToString("yyyyMMdd");

            formattedDateToday = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            formattedBirthdate = int.Parse(formattedBirthday);
            age = ((double)formattedDateToday - formattedBirthdate) / 10000;
            min = (double)dependentDetails.MinAge;
        }

        if (age >= min && age <= (double)dependentDetails.MaxAge)
        {
            dependentDetails.IsValid = true;
        }
        else
        {
            dependentDetails.IsValid = false;
        }

    }

    public void ValidateDependentLimit(DependentDetails dependentDetails, DependentCollection dependentCollection)
    {
        int count = 0;

        for (int i = 0; i < dependentCollection.Count; i++)
        {
            if (dependentDetails.Relationship == dependentCollection[i].DependentRelationship)
            {
                count = count + 1;
            }
        }

        if (dependentDetails.DependentLimit >= count)
        {
            dependentDetails.IsValid = true;
        }
        else
        {
            dependentDetails.IsValid = false;
        }

    }

    public void ValidateDependentCOCLimit(DependentDetails dependentDetails, PetDetails petDetails, GeneralDetails generalDetails)
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringReader))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_CheckDependentRecords]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@count", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(generalDetails.NumberOfCOCs));
                    sqlCommand.Parameters.AddWithValue("@cocLimit", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(dependentDetails.DependentCOCLimit));
                    sqlCommand.Parameters.AddWithValue("@productCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentDetails.ProductCode));
                    sqlCommand.Parameters.AddWithValue("@decription", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentDetails.Relationship));
                    if (dependentDetails.Relationship == "Pet")
                    {
                        sqlCommand.Parameters.AddWithValue("@name", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetName));
                        sqlCommand.Parameters.AddWithValue("@birth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(petDetails.PetBirth)));
                        sqlCommand.Parameters.AddWithValue("@breed", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetBreed));
                        sqlCommand.Parameters.AddWithValue("@ageMonth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(petDetails.PetAgeMonth));
                        sqlCommand.Parameters.AddWithValue("@ageYear", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(petDetails.PetAgeYear));
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@name", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentDetails.DependentName));
                        sqlCommand.Parameters.AddWithValue("@birth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(dependentDetails.DependentBirth)));
                    }

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            this.CanPurchase = RDFramework.Utility.Conversion.SafeReadDatabaseValue<bool>(rd["ValidationStatus"]);
                            this.AvailableCOCs = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["AvailableCOCs"]);
                            this.ActiveCOCs = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["ActiveCOCs"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
}