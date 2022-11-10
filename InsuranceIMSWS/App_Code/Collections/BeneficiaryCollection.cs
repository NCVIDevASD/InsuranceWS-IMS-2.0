using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for BeneficiaryCollection
/// </summary>
public class BeneficiaryCollection : List<BeneficiaryDetails>
{
    private string _userId;
    internal string UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }

    #region beneficiary
    public void AddBeneficiary(BeneficiaryCollection beneficiaryCollection, string dateTimeFormat, InsuranceTransactionCollection cocDetails)
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            for (int totalNoOfCOCs = 1; totalNoOfCOCs <= cocDetails.Count; totalNoOfCOCs++)
            {
                for (int beneficiary = 1; beneficiary <= beneficiaryCollection.Count; beneficiary++)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("[Updater].[usp_InsertBeneficiaryDetails]", sqlConnection))
                        {
                            sqlConnection.Open();
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@cocNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(cocDetails[totalNoOfCOCs - 1].COCNumber));
                            sqlCommand.Parameters.AddWithValue("@beneficiaryName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(beneficiaryCollection[beneficiary - 1].BeneficiaryName));
                            sqlCommand.Parameters.AddWithValue("@beneficiaryRelationship", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(beneficiaryCollection[beneficiary - 1].BeneficiaryRelationship));
                            sqlCommand.Parameters.AddWithValue("@beneficiaryContactNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(beneficiaryCollection[beneficiary - 1].BeneficiaryContactNo));
                            sqlCommand.Parameters.AddWithValue("@userId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(beneficiaryCollection.UserId));

                            if (beneficiaryCollection[beneficiary - 1].BeneficiaryBirthday != null)
                            {
                                var beneficiaryBirthday = DateTime.ParseExact(beneficiaryCollection[beneficiary - 1].BeneficiaryBirthday, dateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None);
                                sqlCommand.Parameters.AddWithValue("@beneficiaryBirthday", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(beneficiaryBirthday));
                            }

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Add Beneficiary";
            auditTrail.ActionDetails = "InsuranceIMSWS: beneficiary details per COC added.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("COC Generated and Saved. Error in Saving Beneficiary Details.");
        }
    }
    #endregion
}