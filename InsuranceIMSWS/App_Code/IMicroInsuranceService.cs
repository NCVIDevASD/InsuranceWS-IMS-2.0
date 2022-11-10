using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.IO;

namespace MicroInsurance
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMicroInsuranceService" in both code and config file together.
    [ServiceContract]
    public interface IMicroInsuranceService
    {
        [OperationContract]
        [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "CreateNewCustomer"
            , BodyStyle = WebMessageBodyStyle.Bare)]
        CreateNewCustomerResult CreateNewCustomer(CreateNewCustomerRequest createNewCustomerRequest);

        [OperationContract]
        [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "ProcessInsuranceAction"
            , BodyStyle = WebMessageBodyStyle.Bare)]
        ProcessInsuranceActionResult ProcessInsuranceAction(ProcessInsuranceActionRequest processInsuranceActionRequest);
    }
}
