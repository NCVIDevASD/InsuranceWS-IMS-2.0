using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for GeneralDetails
/// </summary>
public class GeneralDetails
{
    #region Constructors
    public GeneralDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _userId;
    public string UserId
    {
        set { _userId = value; }
        get { return _userId; }
    }

    private long _integrationId;
    public long IntegrationId
    {
        set { _integrationId = value; }
        get { return _integrationId; }
    }

    private string _platformAPI;
    public string PlatformAPI
    {
        set { _platformAPI = value; }
        get { return _platformAPI; }
    }

    private int _numberOfCOCs;
    public int NumberOfCOCs
    {
        get { return _numberOfCOCs; }
        set { _numberOfCOCs = value; }
    }

    private decimal _partnerSellingPrice;
    internal decimal PartnerSellingPrice
    {
        get { return _partnerSellingPrice; }
        set { _partnerSellingPrice = value; }
    }

    private bool _isPaid;
    public bool IsPaid
    {
        get { return _isPaid; }
        set { _isPaid = value; }
    }

    private string _referenceNo;
    public string ReferenceNo
    {
        set { _referenceNo = value; }
        get { return _referenceNo; }
    }

    private string _formattedReferenceNo;
    internal string FormattedReferenceNo
    {
        get { return _formattedReferenceNo; }
        set { _formattedReferenceNo = value; }
    }

    private string _referenceCode;
    public string ReferenceCode
    {
        set { _referenceCode = value; }
        get { return _referenceCode; }
    }

    private string _dateTimeFormat;
    public string DateTimeFormat
    {
        set { _dateTimeFormat = value; }
        get { return _dateTimeFormat; }
    }

    #region integration ids
    private long _categoryId;
    internal long CategoryId
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    private long _productId;
    internal long ProductId
    {
        get { return _productId; }
        set { _productId = value; }
    }

    private long _providerId;
    internal long ProviderId
    {
        get { return _providerId; }
        set { _providerId = value; }
    }

    //private string _providerCode;
    //internal string ProviderCode
    //{
    //    get { return _providerCode; }
    //    set { _providerCode = value; }
    //}

    private long _partnerId;
    internal long PartnerId
    {
        get { return _partnerId; }
        set { _partnerId = value; }
    }

    private long _platformId;
    internal long PlatformId
    {
        get { return _platformId; }
        set { _platformId = value; }
    }
    #endregion

    #region multiple number of COCs
    //private int[] _numberOfCOCsPerCoverage;
    //public int[] NumberOfCOCsPerCoverage
    //{
    //    get { return _numberOfCOCsPerCoverage; }
    //    set { _numberOfCOCsPerCoverage = value; }
    //}
    #endregion
    #endregion

    #region Additional Fields/Properties for iClick
    private string _branchCode;
    public string BranchCode
    {
        set { _branchCode = value; }
        get { return _branchCode; }
    }

    private int _serviceId;
    public int ServiceId
    {
        set { _serviceId = value; }
        get { return _serviceId; }
    }

    private int _transactionSourceId;
    public int TransactionSourceId
    {
        set { _transactionSourceId = value; }
        get { return _transactionSourceId; }
    }

    private int _transactionTypeId;
    public int TransactionTypeId
    {
        set { _transactionTypeId = value; }
        get { return _transactionTypeId; }
    }

    private bool _forRenewal;
    public bool ForRenewal
    {
        set { _forRenewal = value; }
        get { return _forRenewal; }
    }

    private string _sourceCOC;
    public string SourceCOC
    {
        set { _sourceCOC = value; }
        get { return _sourceCOC; }
    }

    private int _cocStatusId;
    public int COCStatusId
    {
        set { _cocStatusId = value; }
        get { return _cocStatusId; }
    }
    #endregion

    #region Additional Fields/Properties for IMS 2.0
    private string _agent;
    public string Agent
    {
        set { _agent = value; }
        get { return _agent; }
    }

    private string _subagent;
    public string Subagent
    {
        set { _subagent = value; }
        get { return _subagent; }
    }

    private string _policyBookingStatusId;
    public string PolicyBookingStatusId
    {
        set { _policyBookingStatusId = value; }
        get { return _policyBookingStatusId; }
    }

    private string _distributionChannel;
    public string DistributionChannel
    {
        set { _distributionChannel = value; }
        get { return _distributionChannel; }
    }

    private string _bookingApprover;
    public string BookingApprover
    {
        set { _bookingApprover = value; }
        get { return _bookingApprover; }
    }
    #endregion

    #region Product Details
    private string _productName;
    internal string ProductName
    {
        get { return _productName; }
        set { _productName = value; }
    }

    private string _productCode;
    internal string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }

    private string _categoryCode;
    internal string CategoryCode
    {
        get { return _categoryCode; }
        set { _categoryCode = value; }
    }

    private string _effectiveDateBasis;
    internal string EffectiveDateBasis
    {
        get { return _effectiveDateBasis; }
        set { _effectiveDateBasis = value; }
    }

    private int _minimumAge;
    internal int MinimumAge
    {
        get { return _minimumAge; }
        set { _minimumAge = value; }
    }

    private int _maximumAge;
    internal int MaximumAge
    {
        get { return _maximumAge; }
        set { _maximumAge = value; }
    }

    private int _maxCOCPerDuration;
    internal int MaxCOCPerDuration
    {
        get { return _maxCOCPerDuration; }
        set { _maxCOCPerDuration = value; }
    }

    private int _maxCOCPerYear;
    internal int MaxCOCPerYear
    {
        get { return _maxCOCPerYear; }
        set { _maxCOCPerYear = value; }
    }

    private int _cocLimit;
    internal int COCLimit
    {
        get { return _cocLimit; }
        set { _cocLimit = value; }
    }

    private int _coverageDurationInDays;
    internal int CoverageDurationInDays
    {
        get { return _coverageDurationInDays; }
        set { _coverageDurationInDays = value; }
    }

    private int _coverageDurationInMonths;
    internal int CoverageDurationInMonths
    {
        get { return _coverageDurationInMonths; }
        set { _coverageDurationInMonths = value; }
    }

    private string _limitCode;
    internal string LimitCode
    {
        get { return _limitCode; }
        set { _limitCode = value; }
    }

    private int _freeLook;
    internal int FreeLook
    {
        get { return _freeLook; }
        set { _freeLook = value; }
    }
    #endregion

    #region Public Methods
    #region get product details
    public void GetProductDetails()
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringReader))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_GetProductDetails]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@productId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(this.ProductId));

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            this.ProductName = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_ProductName"]);
                            this.ProductCode = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_ProductCode"]);
                            this.CategoryCode = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_CategoryCode"]);
                            this.EffectiveDateBasis = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_COCEffectiveDateBasis"]);
                            this.MinimumAge = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_MinimumAge"]);
                            this.MaximumAge = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_MaximumAge"]);
                            this.MaxCOCPerDuration = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_MaxCOCPerDuration"]);
                            this.MaxCOCPerYear = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_MaxCOCPerYear"]);
                            this.COCLimit = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_COCLimit"]);
                            this.CoverageDurationInDays = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_CoverageDurationInDays"]);
                            this.CoverageDurationInMonths = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_CoverageDurationInMonths"]);
                            this.LimitCode = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_LimitCode"]);
                            this.FreeLook = RDFramework.Utility.Conversion.SafeReadDatabaseValue<int>(rd["fld_FreeLook"]);
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Get Product Details";
            auditTrail.ActionDetails = "InsuranceIMSWS: product details pulled.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region get integration ids
    public void GetIntegrationIds(IntegrationDetails integrationDetails)
    {
        AuditTrailDetails auditTrail = new AuditTrailDetails();

        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringReader))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_GetIntegrationIds]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@categoryCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(integrationDetails.CategoryCode));
                    sqlCommand.Parameters.AddWithValue("@productCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(integrationDetails.ProductCode));
                    sqlCommand.Parameters.AddWithValue("@providerCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(integrationDetails.ProviderCode));
                    sqlCommand.Parameters.AddWithValue("@partnerCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(integrationDetails.PartnerCode));
                    sqlCommand.Parameters.AddWithValue("@platformName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(integrationDetails.Platform));

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            this.CategoryId = RDFramework.Utility.Conversion.SafeReadDatabaseValue<long>(rd["fld_CategoryId"]);
                            this.ProductId = RDFramework.Utility.Conversion.SafeReadDatabaseValue<long>(rd["fld_ProductId"]);
                            this.ProviderId = RDFramework.Utility.Conversion.SafeReadDatabaseValue<long>(rd["fld_ProviderId"]);
                            this.PartnerId = RDFramework.Utility.Conversion.SafeReadDatabaseValue<long>(rd["fld_PartnerId"]);
                            this.PlatformId = RDFramework.Utility.Conversion.SafeReadDatabaseValue<long>(rd["fld_PlatformId"]);
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Get Integration IDs";
            auditTrail.ActionDetails = "InsuranceIMSWS: got integration IDs";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #endregion
}