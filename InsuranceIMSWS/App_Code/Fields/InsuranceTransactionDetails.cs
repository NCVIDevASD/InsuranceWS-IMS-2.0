using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InsuranceTransactionDetails
/// </summary>
public class InsuranceTransactionDetails
{
    #region Constructors
    public InsuranceTransactionDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _cocNumber;
    public string COCNumber
    {
        get { return _cocNumber; }
        set { _cocNumber = value; }
    }

    private string _effectiveDate;
    public string EffectiveDate
    {
        get { return _effectiveDate; }
        set { _effectiveDate = value; }
    }

    private string _terminationDate;
    public string TerminationDate
    {
        get { return _terminationDate; }
        set { _terminationDate = value; }
    }
    #endregion
}