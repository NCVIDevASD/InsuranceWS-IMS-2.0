using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PetDetails
/// </summary>
public class PetDetails
{
    #region Constructor
    public PetDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _cocNumber;
    private string _petName;
    private string _petBreed;
    private string _petColor;
    private string _petGender;
    private string _petBirth;
    private int _petAgeYear;
    private int _petAgeMonth;
    private string _petPedigree;
    private string _petRFID;
    private string _petFunction;
    private string _petYearlyTreatment;
    private string _petHistory;
    private string _petHistoryDetails;
    private string _petVitamins;
    private string _petVitaminsDetails;

    public string COCNumber
    {
        get { return _cocNumber; }
        set { _cocNumber = value; }
    }

    public string PetName
    {
        get { return _petName; }
        set { _petName = value; }
    }

    public string PetBreed
    {
        get { return _petBreed; }
        set { _petBreed = value; }
    }

    public string PetColor
    {
        get { return _petColor; }
        set { _petColor = value; }
    }

    public string PetGender
    {
        get { return _petGender; }
        set { _petGender = value; }
    }

    public string PetBirth
    {
        get { return _petBirth; }
        set { _petBirth = value; }
    }

    public int PetAgeYear
    {
        get { return _petAgeYear; }
        set { _petAgeYear = value; }
    }

    public int PetAgeMonth
    {
        get { return _petAgeMonth; }
        set { _petAgeMonth = value; }
    }

    public string PetPedigree
    {
        get { return _petPedigree; }
        set { _petPedigree = value; }
    }

    public string PetRFID
    {
        get { return _petRFID; }
        set { _petRFID = value; }
    }

    public string PetFunction
    {
        get { return _petFunction; }
        set { _petFunction = value; }
    }

    public string PetYearlyTreatment
    {
        get { return _petYearlyTreatment; }
        set { _petYearlyTreatment = value; }
    }

    public string PetHistory
    {
        get { return _petHistory; }
        set { _petHistory = value; }
    }

    public string PetHistoryDetails
    {
        get { return _petHistoryDetails; }
        set { _petHistoryDetails = value; }
    }

    public string PetVitamins
    {
        get { return _petVitamins; }
        set { _petVitamins = value; }
    }

    public string PetVitaminsDetails
    {
        get { return _petVitaminsDetails; }
        set { _petVitaminsDetails = value; }
    }

    #endregion

    #region Public Methods
    public void SavePetDetails(PetDetails petDetails)
    {
        try
        {
            AuditTrailDetails auditTrail = new AuditTrailDetails();

            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Updater].[usp_SavePetDetails]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@cocNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.COCNumber));
                    sqlCommand.Parameters.AddWithValue("@petName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetName));
                    sqlCommand.Parameters.AddWithValue("@petBreed", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetBreed));
                    sqlCommand.Parameters.AddWithValue("@petColor", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetColor));
                    sqlCommand.Parameters.AddWithValue("@petGender", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetGender));
                    if (petDetails.PetBirth != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@petBirth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(petDetails.PetBirth)));
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@petBirth", null);
                    }
                    sqlCommand.Parameters.AddWithValue("@petAgeYear", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(petDetails.PetAgeYear));
                    sqlCommand.Parameters.AddWithValue("@petAgeMonth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(petDetails.PetAgeMonth));
                    sqlCommand.Parameters.AddWithValue("@pedigreeCertNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetPedigree));
                    sqlCommand.Parameters.AddWithValue("@RFIDNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetRFID));
                    sqlCommand.Parameters.AddWithValue("@petFunction", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetFunction));
                    sqlCommand.Parameters.AddWithValue("@petYearlyTreatment", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetYearlyTreatment));
                    sqlCommand.Parameters.AddWithValue("@petHistory", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetHistory));
                    sqlCommand.Parameters.AddWithValue("@petHistoryDetails", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetHistoryDetails));
                    sqlCommand.Parameters.AddWithValue("@petVitamins", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetVitamins));
                    sqlCommand.Parameters.AddWithValue("@petVitaminsDetails", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.PetVitaminsDetails));
                    sqlCommand.ExecuteNonQuery();
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Save Pet Details";
            auditTrail.ActionDetails = "InsuranceIMSWS: Pet Details saved.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("Error in Saving Pet Details.");
        }
    }

    //public void ValidatePet(PetDetails petDetails)
    //{
    //    try
    //    {
    //        AuditTrailDetails auditTrail = new AuditTrailDetails();

    //        using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
    //        {
    //            using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_ValidateDependent]", sqlConnection))
    //            {
    //                sqlConnection.Open();
    //                sqlCommand.CommandType = CommandType.StoredProcedure;
    //                #region details
    //                sqlCommand.Parameters.AddWithValue("@productCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(petDetails.ProductCode));
    //                sqlCommand.Parameters.AddWithValue("@limit", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(1));
    //                sqlCommand.Parameters.AddWithValue("@birthdate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(petDetails.PetBirth)));
    //                sqlCommand.Parameters.AddWithValue("@ageYear", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(petDetails.PetAgeYear));
    //                sqlCommand.Parameters.AddWithValue("@ageMonth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(petDetails.PetAgeMonth));
    //                #endregion

    //                using (SqlDataReader rd = sqlCommand.ExecuteReader())
    //                {
    //                    while (rd.Read())
    //                    {
    //                        petDetails.IsValid = RDFramework.Utility.Conversion.SafeReadDatabaseValue<bool>(rd["ValidationStatus"]);
    //                    }
    //                }
    //            }
    //        }

    //        #region Audit Trail
    //        //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
    //        auditTrail.ActionTaken = "Validate Pet Age";
    //        auditTrail.ActionDetails = "InsuranceWS: Pet Age validated.";
    //        auditTrail.InsertAuditTrailEntry();
    //        #endregion
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.WriteLine(ex.Message);
    //        throw new Exception("Error in Validating Pet Age");
    //    }
    //}

    #endregion
}