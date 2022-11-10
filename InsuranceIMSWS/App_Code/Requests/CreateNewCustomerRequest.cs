using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for CreateNewCustomerRequest
/// </summary>
public class CreateNewCustomerRequest
{
    #region Constructor
    public CreateNewCustomerRequest()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #endregion

    #region Fields/Properties
    private string _token;
    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }

    private CustomerDetails _customerDetails;
    public CustomerDetails CustomerDetails
    {
        get { return _customerDetails; }
        set { _customerDetails = value; }
    }

    private string _userId;
    public string UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }
    #endregion

    #region Public Methods
    public CreateNewCustomerResult Process()
    {
        try
        {
            CreateNewCustomerResult returnValue = new CreateNewCustomerResult();

            CustomerDetails.DateOfBirth = DateTime.ParseExact(CustomerDetails.Birthdate, "MM-dd-yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
            CustomerDetails.UserId = this.UserId;
            this.CustomerDetails.GetCustomerNo();

            returnValue.CustomerDetails = this.CustomerDetails;
            returnValue.ResultStatus = ResultStatus.Successful;
            if (this.CustomerDetails.CustomerExists == false)
            {
                returnValue.Message = "Creation of New Customer Successful";
            }
            else
            {
                returnValue.Message = "Customer already exists. Changes made to details that can be edited were saved.";
            }

            return returnValue;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Write(ex.Message);
            throw;
        }
    }
    #endregion
}