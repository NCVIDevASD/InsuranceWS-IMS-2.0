using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
/// <summary>
/// Summary description for SystemEventLog
/// </summary>
public class SystemEventLog :  RDFramework.Utility.EventInformation
{
    #region Constructor
    public SystemEventLog() : base(HostingEnvironment.ApplicationPhysicalPath)
	{
        this.EventID = RDFramework.Utility.RandomGenerator.GenerateEventID();
        this.EventSource = Settings.EventSource;
	}
    #endregion

    #region Public Methods
    public void LogError(string error)
    {
        this.Message = error;
        this.EventType = System.Diagnostics.EventLogEntryType.Error;
        this.Log();
    }

    public void WrapServerError(string rawError)
    {
        this.Message = rawError;
        this.EventType = System.Diagnostics.EventLogEntryType.Error;
        this.Log();
        this.Message = string.Format(Settings.GenericServerMessage, this.EventID);
    }
    #endregion
}