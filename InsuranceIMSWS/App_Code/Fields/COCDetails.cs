using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for COCDetails
/// </summary>
public class COCDetails
{
    #region Constructors
    public COCDetails()
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

    private string _issueDate;
    public string IssueDate
    {
        get { return _issueDate; }
        set { _issueDate = value; }
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