using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for COCDetailsCollection
/// </summary>
public class COCDetailsCollection : List<COCDetails>
{
    private bool _isValid;
    internal bool IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
}