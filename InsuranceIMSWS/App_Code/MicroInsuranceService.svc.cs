using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web; 
using System.Text;
using System.IO;
using System.Web;

namespace MicroInsurance
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MicroInsuranceService" in code, svc and config file together.
    public class MicroInsuranceService : IMicroInsuranceService
    {
        public CreateNewCustomerResult CreateNewCustomer(CreateNewCustomerRequest createNewCustomerRequest)
        {
            CreateNewCustomerResult returnValue = null;
            var valid = false;

            if (createNewCustomerRequest.Token != null)
            {
                try
                {
                    string requestToken = HttpUtility.UrlDecode(createNewCustomerRequest.Token);
                    valid = TokenAuth4.TokenAuth.IsValid(requestToken, Settings.TokenKey);
                }
                catch (Exception ex)
                {
                    valid = TokenAuth4.TokenAuth.IsValid(createNewCustomerRequest.Token, Settings.TokenKey);
                }
            }

            if (!valid)
            {
                returnValue = new CreateNewCustomerResult();

                SystemEventLog eventLog = new SystemEventLog();
                eventLog.WrapServerError(Settings.InvalidToken);

                returnValue.ResultStatus = ResultStatus.Failed;
                returnValue.Message = Settings.InvalidToken;

                return returnValue;
            }
            else
            {
                try
                {
                    returnValue = createNewCustomerRequest.Process();

                    return returnValue;
                }
                catch (RDFramework.ClientException clientex)
                {
                    returnValue = new CreateNewCustomerResult();

                    returnValue.ResultStatus = ResultStatus.Failed;
                    returnValue.Message = clientex.Message;

                    return returnValue;
                }
                catch (Exception ex)
                {
                    returnValue = new CreateNewCustomerResult();

                    SystemEventLog eventLog = new SystemEventLog();
                    eventLog.WrapServerError(ex.ToString());

                    returnValue.ResultStatus = ResultStatus.Error;
                    returnValue.Message = eventLog.Message;

                    return returnValue;
                }
            }
        }

        public ProcessInsuranceActionResult ProcessInsuranceAction(ProcessInsuranceActionRequest processInsuranceActionRequest)
        {
            ProcessInsuranceActionResult returnValue = null;
            var valid = false;

            if (processInsuranceActionRequest.Token != null)
            {
                try
                {
                    string requestToken = HttpUtility.UrlDecode(processInsuranceActionRequest.Token);
                    valid = TokenAuth4.TokenAuth.IsValid(requestToken, Settings.TokenKey);
                }
                catch (Exception ex)
                {
                    valid = TokenAuth4.TokenAuth.IsValid(processInsuranceActionRequest.Token, Settings.TokenKey);
                }
            }

            if (!valid)
            {
                returnValue = new ProcessInsuranceActionResult();

                SystemEventLog eventLog = new SystemEventLog();
                eventLog.WrapServerError(Settings.InvalidToken);

                returnValue.ResultStatus = ResultStatus.Failed;
                returnValue.Message = Settings.InvalidToken;

                return returnValue;
            }
            else
            {
                try
                {
                    returnValue = processInsuranceActionRequest.Process();

                    return returnValue;
                }
                catch (RDFramework.ClientException clientex)
                {
                    returnValue = new ProcessInsuranceActionResult();

                    returnValue.ResultStatus = ResultStatus.Failed;
                    returnValue.Message = clientex.Message;

                    return returnValue;
                }
                catch (Exception ex)
                {
                    returnValue = new ProcessInsuranceActionResult();

                    SystemEventLog eventLog = new SystemEventLog();
                    eventLog.WrapServerError(ex.ToString());

                    returnValue.ResultStatus = ResultStatus.Error;
                    returnValue.Message = eventLog.Message;

                    return returnValue;
                }
            }
        }
    }
}
