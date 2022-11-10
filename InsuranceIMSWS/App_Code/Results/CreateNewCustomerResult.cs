using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CreateNewCustomerResult
/// </summary>
public class CreateNewCustomerResult
{
    #region Constructor
    public CreateNewCustomerResult()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #endregion

    #region Fields/Properties
    private CustomerDetails _customerDetails;
    public CustomerDetails CustomerDetails
    {
        get { return _customerDetails; }
        set { _customerDetails = value; }
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