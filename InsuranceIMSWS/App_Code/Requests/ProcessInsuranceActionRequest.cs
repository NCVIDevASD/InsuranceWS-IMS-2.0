using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for ProcessInsuranceActionRequest
/// </summary>
public class ProcessInsuranceActionRequest
{
    #region Constructor
    public ProcessInsuranceActionRequest()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #endregion

    #region Fields/Properties
    private string _actionDone;
    public string ActionDone
    {
        get { return _actionDone; }
        set { _actionDone = value; }
    }

    private IntegrationDetails _integrationDetails;
    public IntegrationDetails IntegrationDetails
    {
        get { return _integrationDetails; }
        set { _integrationDetails = value; }
    }

    private GeneralDetails _generalDetails;
    public GeneralDetails GeneralDetails
    {
        get { return _generalDetails; }
        set { _generalDetails = value; }
    }

    private CustomerDetails _customerDetails;
    public CustomerDetails CustomerDetails
    {
        get { return _customerDetails; }
        set { _customerDetails = value; }
    }

    private COCDetailsCollection _cocDetailsCollection;
    public COCDetailsCollection COCDetailsCollection
    {
        get { return _cocDetailsCollection; }
        set { _cocDetailsCollection = value; }
    }

    private GuardianDetails _guardianDetails;
    public GuardianDetails GuardianDetails
    {
        get { return _guardianDetails; }
        set { _guardianDetails = value; }
    }

    private BeneficiaryCollection _beneficiaryCollection;
    public BeneficiaryCollection BeneficiaryCollection
    {
        get { return _beneficiaryCollection; }
        set { _beneficiaryCollection = value; }
    }

    private AddressCollection _addressCollection;
    public AddressCollection AddressCollection
    {
        get { return _addressCollection; }
        set { _addressCollection = value; }
    }

    private PaymentDetailsCollection _paymentDetailsCollection;
    public PaymentDetailsCollection PaymentDetailsCollection
    {
        get { return _paymentDetailsCollection; }
        set { _paymentDetailsCollection = value; }
    }

    private string _token;
    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }

    //added for Pet Insurance
    private PetDetails _petDetails;
    public PetDetails PetDetails
    {
        get { return _petDetails; }
        set { _petDetails = value; }
    }

    private DependentDetails _dependentDetails;
    public DependentDetails DependentDetails
    {
        get { return _dependentDetails; }
        set { _dependentDetails = value; }
    }

    private DependentCollection _dependentCollection;
    public DependentCollection DependentCollection
    {
        get { return _dependentCollection; }
        set { _dependentCollection = value; }
    }
    #endregion

    #region Public Methods
    public ProcessInsuranceActionResult Process()
    {
        try
        {
            ProcessInsuranceActionResult returnValue = new ProcessInsuranceActionResult();

            this.GeneralDetails.GetIntegrationIds(this.IntegrationDetails);

            if (this.ActionDone == "Save Customer Policy")
            {
                returnValue.InsuranceTransactionCollection = new InsuranceTransactionCollection();
                returnValue.InsuranceTransactionCollection.SavePolicy(this.IntegrationDetails, this.GeneralDetails, this.CustomerDetails, this.GuardianDetails, this.AddressCollection, this.COCDetailsCollection, this.PaymentDetailsCollection);
                if (this.BeneficiaryCollection != null)
                {
                    this.BeneficiaryCollection.UserId = this.GeneralDetails.UserId;
                    this.BeneficiaryCollection.AddBeneficiary(this.BeneficiaryCollection, "MM/dd/yyyy", returnValue.InsuranceTransactionCollection);
                }

                if (this.PetDetails != null)
                {
                    this.PetDetails.SavePetDetails(this.PetDetails);
                }

                if (this.DependentCollection != null)
                {
                    this.DependentCollection.AddDependent(this.DependentCollection);
                }

                returnValue.ResultStatus = ResultStatus.Successful;
                returnValue.Message = "Policy saved!";
            }
            else
            {
                this.GeneralDetails.GetProductDetails();

                if (this.ActionDone == "Validate Customer Age")
                {
                    CustomerDetails.DateOfBirth = DateTime.ParseExact(CustomerDetails.Birthdate, GeneralDetails.DateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None);
                    if (CustomerDetails.IsCustomerEligible(this.GeneralDetails, this.CustomerDetails) == false)
                    {
                        throw new RDFramework.ClientException("Insured age invalid!");
                    }

                    returnValue.ResultStatus = ResultStatus.Successful;
                    returnValue.Message = "Insured age valid.";
                }
                else if (this.ActionDone == "Check Customer COC")
                {
                    COCRecordDetails record = new COCRecordDetails();
                    record.CheckCustomerRecords(this.GeneralDetails, this.CustomerDetails);

                    if (record.CanPurchase == false)
                    {
                        if (record.AvailableCOCs == 0)
                        {
                            throw new RDFramework.ClientException("Maximum COC Limit Reached");
                        }
                        else
                        {
                            throw new RDFramework.ClientException("Customer can only purchase: " + record.AvailableCOCs);
                        }
                    }

                    returnValue.InsuranceTransactionCollection = new InsuranceTransactionCollection();
                    returnValue.InsuranceTransactionCollection.GenerateCOCNo(this.GeneralDetails, this.CustomerDetails);

                    returnValue.ResultStatus = ResultStatus.Successful;
                    returnValue.Message = "Customer can purchase.";
                }
                else if (this.ActionDone == "Validate Effective Date")
                {
                    returnValue.InsuranceTransactionCollection = new InsuranceTransactionCollection();
                    returnValue.InsuranceTransactionCollection.ValidateEffectiveDate(this.GeneralDetails, this.COCDetailsCollection, this.CustomerDetails);

                    if (this.COCDetailsCollection.IsValid == false)
                    {
                        returnValue.ResultStatus = ResultStatus.Failed;
                        returnValue.Message = "Effective date invalid!";
                    }
                    else
                    {
                        returnValue.ResultStatus = ResultStatus.Successful;
                        returnValue.Message = "Effective date valid.";
                    }   
                }
                else if (this.ActionDone == "Validate Pet Age")
                {
                    this.DependentDetails.GetDependentDetails();
                    this.DependentDetails.ValidateDependent(this.DependentDetails, this.PetDetails, this.DependentCollection);

                    if (this.DependentDetails.IsValid == false)
                    {
                        returnValue.ResultStatus = ResultStatus.Failed;
                        returnValue.Message = "Pet Age invalid!";
                    }
                    else
                    {
                        returnValue.ResultStatus = ResultStatus.Successful;
                        returnValue.Message = "Pet Age valid.";
                    }
                }
                else if (this.ActionDone == "Validate Dependent Age")
                {
                    for (int dependent = 1; dependent <= this.DependentCollection.Count; dependent++)
                    {
                        this.DependentDetails.Relationship = this.DependentCollection[dependent - 1].DependentRelationship;
                        this.DependentDetails.Birthdate = this.DependentCollection[dependent - 1].DependentBirth;
                        this.DependentDetails.GetDependentDetails();
                        this.DependentDetails.ValidateDependent(this.DependentDetails, this.PetDetails, this.DependentCollection);

                        if (this.DependentDetails.IsValid == false)
                        {
                            returnValue.ResultStatus = ResultStatus.Failed;
                            returnValue.Message = "Age invalid for Dependent #" + dependent + "!";
                            break;
                        }
                        else
                        {
                            returnValue.ResultStatus = ResultStatus.Successful;
                            returnValue.Message = "Dependent Age valid.";
                        }
                    }
                }
                else if (this.ActionDone == "Validate Dependent Limit")
                {
                    for (int dependent = 1; dependent <= this.DependentCollection.Count; dependent++)
                    {
                        this.DependentDetails.Relationship = this.DependentCollection[dependent - 1].DependentRelationship;
                        this.DependentDetails.GetDependentDetails();
                        this.DependentDetails.ValidateDependentLimit(this.DependentDetails, this.DependentCollection);

                        if (this.DependentDetails.IsValid == false)
                        {
                            returnValue.ResultStatus = ResultStatus.Failed;
                            returnValue.Message = "Dependent Limit for " + this.DependentDetails.Relationship + " has already been reached!";
                            break;
                        }
                        else
                        {
                            returnValue.ResultStatus = ResultStatus.Successful;
                            returnValue.Message = "Dependent Limit valid.";
                        }
                    }
                }
                else if (this.ActionDone == "Validate Dependent COC")
                {
                    if (this.PetDetails != null)
                    {
                        this.DependentDetails.GetDependentDetails();
                        this.DependentDetails.ValidateDependentCOCLimit(this.DependentDetails, this.PetDetails, this.GeneralDetails);
                        if (this.DependentDetails.CanPurchase == false)
                        {
                            throw new RDFramework.ClientException("Maximum COC Limit Reached for " + this.DependentDetails.Relationship + "!");
                        }
                        else
                        {
                            returnValue.ResultStatus = ResultStatus.Successful;
                            returnValue.Message = "Client can purchase for dependent " + this.DependentDetails.Relationship + ".";
                        }
                    }
                    else
                    {
                        for (int dependent = 1; dependent <= this.DependentCollection.Count; dependent++)
                        {
                            this.DependentDetails.Relationship = this.DependentCollection[dependent - 1].DependentRelationship;
                            this.DependentDetails.GetDependentDetails();
                            this.DependentDetails.DependentName = this.DependentCollection[dependent - 1].DependentName;
                            this.DependentDetails.DependentBirth = this.DependentCollection[dependent - 1].DependentBirth;
                            this.DependentDetails.ValidateDependentCOCLimit(this.DependentDetails, this.PetDetails, this.GeneralDetails);

                            if (this.DependentDetails.CanPurchase == false)
                            {
                                throw new RDFramework.ClientException("Maximum COC Limit Reached for " + this.DependentDetails.Relationship + "!");
                            }
                            else
                            {
                                returnValue.ResultStatus = ResultStatus.Successful;
                                returnValue.Message = "Client can purchase for dependent " + this.DependentDetails.Relationship + ".";
                            }
                        }

                    }
                }
                else if (this.ActionDone == "Get Coverage Dates")
                {
                    returnValue.InsuranceTransactionCollection = new InsuranceTransactionCollection();
                    returnValue.InsuranceTransactionCollection.GenerateCoverageDates(this.GeneralDetails, this.CustomerDetails);

                    returnValue.ResultStatus = ResultStatus.Successful;
                    returnValue.Message = "Coverage dates generated!";
                }
                else
                {
                    throw new RDFramework.ClientException("Invalid Action Done.");
                }
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