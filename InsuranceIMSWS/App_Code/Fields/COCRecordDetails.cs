using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for COCRecordDetails
/// </summary>
public class COCRecordDetails
{
    #region Constructor
    public COCRecordDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private bool _canPurchase;
    public bool CanPurchase
    {
        get { return _canPurchase; }
        set { _canPurchase = value; }
    }

    private int _availableCOCs;
    public int AvailableCOCs
    {
        get { return _availableCOCs; }
        set { _availableCOCs = value; }
    }

    private int _activeCOCs;
    public int ActiveCOCs
    {
        get { return _activeCOCs; }
        set { _activeCOCs = value; }
    }
    #endregion

    #region Public Methods
    public void CheckCustomerRecords(GeneralDetails generalDetails, CustomerDetails customerDetails)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringReader))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_CheckCustomerRecords]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@count", generalDetails.NumberOfCOCs);
                    sqlCommand.Parameters.AddWithValue("@productId", generalDetails.ProductId);
                    sqlCommand.Parameters.AddWithValue("@partnerId", generalDetails.PartnerId);
                    sqlCommand.Parameters.AddWithValue("@limitCode", generalDetails.LimitCode);
                    sqlCommand.Parameters.AddWithValue("@cocLimit", generalDetails.COCLimit);
                    sqlCommand.Parameters.AddWithValue("@insuranceCustomerNo", customerDetails.InsuranceCustomerNo);

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
        catch
        {
            throw;
        }
    }
    #endregion
}