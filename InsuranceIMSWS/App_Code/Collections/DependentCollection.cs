using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DependentCollection
/// </summary>
public class DependentCollection : List<DependentDetails>
{
    public void AddDependent(DependentCollection dependentCollection)
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            for (int dependent = 1; dependent <= dependentCollection.Count; dependent++)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("[Updater].[usp_SaveDependentDetails]", sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@cocNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].COCNumber));
                        sqlCommand.Parameters.AddWithValue("@relationship", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].DependentRelationship));
                        sqlCommand.Parameters.AddWithValue("@name", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].DependentName));
                        sqlCommand.Parameters.AddWithValue("@birth", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(dependentCollection[dependent - 1].DependentBirth)));
                        sqlCommand.Parameters.AddWithValue("@mobileNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].DependentCellphone));
                        sqlCommand.Parameters.AddWithValue("@emailAdd", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].DependentEmail));
                        sqlCommand.Parameters.AddWithValue("@natureOfWork", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].DependentNatureOfWork));
                        sqlCommand.Parameters.AddWithValue("@employer", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(dependentCollection[dependent - 1].DependentEmployer));

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Add Dependent";
            auditTrail.ActionDetails = "InsuranceWS: Dependent details added.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("COC Generated and Saved. Error in Saving Dependent Details.");
        }
    }
}