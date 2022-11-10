using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BeneficiaryDetails
/// </summary>
public class BeneficiaryDetails
{
    #region Constructor
    public BeneficiaryDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _beneficiaryName;
    public string BeneficiaryName
    {
        get { return _beneficiaryName; }
        set { _beneficiaryName = value; }
    }

    private string _beneficiaryRelationship;
    public string BeneficiaryRelationship
    {
        get { return _beneficiaryRelationship; }
        set { _beneficiaryRelationship = value; }
    }

    private string _beneficiaryBirthday;
    public string BeneficiaryBirthday
    {
        get { return _beneficiaryBirthday; }
        set { _beneficiaryBirthday = value; }
    }

    private string _beneficiaryContactNo;
    public string BeneficiaryContactNo
    {
        get { return _beneficiaryContactNo; }
        set { _beneficiaryContactNo = value; }
    }
    #endregion
}