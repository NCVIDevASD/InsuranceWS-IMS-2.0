using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for ProcessInsuranceActionResult
/// </summary>
public class ProcessInsuranceActionResult
{
    #region Constructor
    public ProcessInsuranceActionResult()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #endregion

    #region Fields/Properties
    private InsuranceTransactionCollection _insuranceTransactionCollection;
    public InsuranceTransactionCollection InsuranceTransactionCollection
    {
        get { return _insuranceTransactionCollection; }
        set { _insuranceTransactionCollection = value; }
    }

    private ResultStatus _resultStatus;
    public ResultStatus ResultStatus
    {
        get { return _resultStatus; }
        set { _resultStatus = value; }
    }

    private string _message;
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }
    #endregion

}