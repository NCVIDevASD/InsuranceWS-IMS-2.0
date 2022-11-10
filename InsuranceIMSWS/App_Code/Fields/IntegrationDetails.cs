using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IntegrationDetails
/// </summary>
public class IntegrationDetails
{
    #region Constructors
    public IntegrationDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _categoryCode;
    public string CategoryCode
    {
        get { return _categoryCode; }
        set { _categoryCode = value; }
    }

    private string _productCode;
    public string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }

    private string _providerCode;
    public string ProviderCode
    {
        get { return _providerCode; }
        set { _providerCode = value; }
    }

    private string _partnerCode;
    public string PartnerCode
    {
        get { return _partnerCode; }
        set { _partnerCode = value; }
    }

    private string _platform;
    public string Platform
    {
        get { return _platform; }
        set { _platform = value; }
    }
    #endregion
}