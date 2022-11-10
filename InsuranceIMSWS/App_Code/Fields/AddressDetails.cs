using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AddressDetails
/// </summary>
public class AddressDetails
{
    #region Constructor
    public AddressDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    private string _city;
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    private string _zipCode;
    public string ZipCode
    {
        get { return _zipCode; }
        set { _zipCode = value; }
    }
    #endregion
}