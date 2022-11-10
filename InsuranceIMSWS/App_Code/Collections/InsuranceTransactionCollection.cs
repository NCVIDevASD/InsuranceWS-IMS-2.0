using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for InsuranceTransactionCollection
/// </summary>
public class InsuranceTransactionCollection : List<InsuranceTransactionDetails>
{
    public void SavePolicy(IntegrationDetails integration, GeneralDetails general, CustomerDetails customer, GuardianDetails guardian, AddressCollection address, COCDetailsCollection coc, PaymentDetailsCollection payment)
    {
        try
        {
            AuditTrailDetails auditTrail = new AuditTrailDetails();

            for (int count = 1; count <= coc.Count; count++)
            {
                for (int counter = 1; counter <= payment.Count; counter++)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("[Updater].[usp_SavePolicy]", sqlConnection))
                        {
                            sqlConnection.Open();
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            #region transaction details
                            sqlCommand.Parameters.AddWithValue("@isPaid", general.IsPaid);
                            sqlCommand.Parameters.AddWithValue("@cocNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(coc[count - 1].COCNumber));
                            sqlCommand.Parameters.AddWithValue("@insuranceCustomerNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.InsuranceCustomerNo));
                            sqlCommand.Parameters.AddWithValue("@categoryId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.CategoryId));
                            sqlCommand.Parameters.AddWithValue("@productId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.ProductId));
                            sqlCommand.Parameters.AddWithValue("@providerId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.ProviderId));
                            sqlCommand.Parameters.AddWithValue("@partnerId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.PartnerId));
                            sqlCommand.Parameters.AddWithValue("@platformId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.PlatformId));
                            sqlCommand.Parameters.AddWithValue("@forRenewal", RDFramework.Utility.Conversion.SafeSetDatabaseValue<bool>(general.ForRenewal));
                            sqlCommand.Parameters.AddWithValue("@sourceCOC", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.SourceCOC));
                            sqlCommand.Parameters.AddWithValue("@cocStatusId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.COCStatusId));
                            #region deleted
                            //sqlCommand.Parameters.AddWithValue("@branchCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.BranchCode));
                            //sqlCommand.Parameters.AddWithValue("@serviceId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.ServiceId));
                            //sqlCommand.Parameters.AddWithValue("@transactionSourceId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.TransactionSourceId));
                            //sqlCommand.Parameters.AddWithValue("@transactionTypeId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.TransactionTypeId));                        
                            //sqlCommand.Parameters.AddWithValue("@cocStatusId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.COCStatusId));
                            #endregion
                            sqlCommand.Parameters.AddWithValue("@referenceNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.ReferenceNo));
                            sqlCommand.Parameters.AddWithValue("@referenceCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.ReferenceCode));
                            sqlCommand.Parameters.AddWithValue("@distributionChannel", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.DistributionChannel));
                            sqlCommand.Parameters.AddWithValue("@policyBookingStatusId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.PolicyBookingStatusId));
                            sqlCommand.Parameters.AddWithValue("@bookingApprover", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.BookingApprover));
                            sqlCommand.Parameters.AddWithValue("@agent", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.Agent));
                            sqlCommand.Parameters.AddWithValue("@subagent", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.Subagent));
                            sqlCommand.Parameters.AddWithValue("@userId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.UserId));

                            #region customer details
                            sqlCommand.Parameters.AddWithValue("@landline", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.Landline));
                            sqlCommand.Parameters.AddWithValue("@mobileNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.MobileNumber));
                            sqlCommand.Parameters.AddWithValue("@emailAddress", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.EmailAddress));
                            if (address != null)
                            {
                                sqlCommand.Parameters.AddWithValue("@address", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(address[count - 1].Address));
                                sqlCommand.Parameters.AddWithValue("@city", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(address[count - 1].City));
                                sqlCommand.Parameters.AddWithValue("@zipCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(address[count - 1].ZipCode));
                            }
                            else
                            {
                                sqlCommand.Parameters.AddWithValue("@address", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.Address));
                                sqlCommand.Parameters.AddWithValue("@city", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.City));
                                sqlCommand.Parameters.AddWithValue("@zipCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.ZipCode));
                            }
                            #endregion

                            #region guardian details
                            if (guardian != null)
                            {
                                DateTime guardianBirthday = DateTime.Parse(guardian.GuardianBirthday);

                                sqlCommand.Parameters.AddWithValue("@guardianName", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(guardian.GuardianName));
                                sqlCommand.Parameters.AddWithValue("@guardianBirthday", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(guardianBirthday));
                                sqlCommand.Parameters.AddWithValue("@guardianRelationship", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(guardian.GuardianRelationship));
                                sqlCommand.Parameters.AddWithValue("@guardianContactNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(guardian.GuardianContactNo));
                            }
                            #endregion

                            #region dates
                            if (integration.ProductCode == "MCTPL" || integration.ProductCode == "MCTP3")
                            {
                                if (!string.IsNullOrEmpty(coc[count - 1].IssueDate))
                                {
                                    sqlCommand.Parameters.AddWithValue("@issueDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].IssueDate)));
                                }
                                if (!string.IsNullOrEmpty(coc[count - 1].EffectiveDate))
                                {
                                    sqlCommand.Parameters.AddWithValue("@effectiveDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].EffectiveDate)));
                                }
                                if (!string.IsNullOrEmpty(coc[count - 1].TerminationDate))
                                {
                                    sqlCommand.Parameters.AddWithValue("@terminationDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].TerminationDate)));
                                }
                            }
                            else
                            {
                                sqlCommand.Parameters.AddWithValue("@issueDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].IssueDate)));
                                sqlCommand.Parameters.AddWithValue("@effectiveDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].EffectiveDate)));
                                sqlCommand.Parameters.AddWithValue("@terminationDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].TerminationDate)));
                            }
                            #endregion

                            #region payment details
                            if (!string.IsNullOrEmpty(payment[counter - 1].NotificationDate))
                            {
                                sqlCommand.Parameters.AddWithValue("@notificationDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(payment[counter - 1].NotificationDate)));
                            }
                            if (!string.IsNullOrEmpty(payment[counter - 1].DatePaid))
                            {
                                sqlCommand.Parameters.AddWithValue("@datePaid", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(payment[counter - 1].DatePaid)));
                            }
                            sqlCommand.Parameters.AddWithValue("@amountPaid", RDFramework.Utility.Conversion.SafeSetDatabaseValue<decimal>(payment[counter - 1].ProductAmount));
                            sqlCommand.Parameters.AddWithValue("@totalAmount", RDFramework.Utility.Conversion.SafeSetDatabaseValue<decimal>(payment[counter - 1].TotalAmountPaid));
                            sqlCommand.Parameters.AddWithValue("@paymentReferenceNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(payment[counter - 1].PaymentReferenceNo));
                            sqlCommand.Parameters.AddWithValue("@transactionCheckNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(payment[counter - 1].TransactionCheckNumber));
                            sqlCommand.Parameters.AddWithValue("@paymentMethod", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(payment[counter - 1].PaymentMethod));
                            sqlCommand.Parameters.AddWithValue("@paymentOrigin", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(payment[counter - 1].PaymentOrigin));
                            sqlCommand.Parameters.AddWithValue("@paymentNotes", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(payment[counter - 1].PaymentNotes));
                            sqlCommand.Parameters.AddWithValue("@paymentStatusId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(payment[counter - 1].PaymentStatusId));
                            #endregion

                            #endregion

                            using (SqlDataReader rd = sqlCommand.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    InsuranceTransactionDetails insuranceTransactionDetails = new InsuranceTransactionDetails();
                                    #region coc details
                                    insuranceTransactionDetails.COCNumber = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_COCNumber"]);
                                    DateTime inceptionDate = RDFramework.Utility.Conversion.SafeReadDatabaseValue<DateTime>(rd["fld_EffectiveDate"]);                                   
                                    DateTime expiryDate = RDFramework.Utility.Conversion.SafeReadDatabaseValue<DateTime>(rd["fld_TerminationDate"]);
                                    
                                    if (inceptionDate == DateTime.MinValue && expiryDate == DateTime.MinValue)
                                    {
                                        insuranceTransactionDetails.EffectiveDate = null;
                                        insuranceTransactionDetails.TerminationDate = null;
                                    }
                                    else
                                    {
                                        insuranceTransactionDetails.EffectiveDate = inceptionDate.ToString("MM/dd/yyyy HH:mm:ss");
                                        insuranceTransactionDetails.TerminationDate = expiryDate.ToString("MM/dd/yyyy HH:mm:ss");
                                    }

                                    #endregion
                                    this.Add(insuranceTransactionDetails);
                                }
                            }
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Save Policy";
            auditTrail.ActionDetails = "InsuranceIMSWS: Insurance policy saved.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("Error in Saving Policy.");
        }
    }

    public void GenerateCOCNo(GeneralDetails general, CustomerDetails customer)
    {
        try
        {
            InsuranceTransactionCollection collection = new InsuranceTransactionCollection();
            AuditTrailDetails auditTrail = new AuditTrailDetails();

            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Updater].[usp_IMSGenerateCOCNo]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    #region details
                    sqlCommand.Parameters.AddWithValue("@insuranceCustomerNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.InsuranceCustomerNo));
                    sqlCommand.Parameters.AddWithValue("@categoryId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.CategoryId));
                    sqlCommand.Parameters.AddWithValue("@productId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.ProductId));
                    sqlCommand.Parameters.AddWithValue("@providerId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.ProviderId));
                    sqlCommand.Parameters.AddWithValue("@partnerId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.PartnerId));
                    sqlCommand.Parameters.AddWithValue("@platformId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.PlatformId));
                    sqlCommand.Parameters.AddWithValue("@userId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.UserId));
                    #endregion

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            InsuranceTransactionDetails insuranceTransactionDetails = new InsuranceTransactionDetails();
                            insuranceTransactionDetails.COCNumber = RDFramework.Utility.Conversion.SafeReadDatabaseValue<string>(rd["fld_COCNumber"]);
                            this.Add(insuranceTransactionDetails);
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Generate COC Number";
            auditTrail.ActionDetails = "InsuranceIMSWS: COC number generated.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("Error in Generating COC Number");
        }
    }

    public void ValidateEffectiveDate(GeneralDetails general, COCDetailsCollection coc, CustomerDetails customer)
    {
        try
        {
            InsuranceTransactionCollection collection = new InsuranceTransactionCollection();
            AuditTrailDetails auditTrail = new AuditTrailDetails();

            for (int count = 1; count <= coc.Count; count++)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_ValidateEffectiveDate]", sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        #region details
                        sqlCommand.Parameters.AddWithValue("@insuranceCustomerNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.InsuranceCustomerNo));
                        sqlCommand.Parameters.AddWithValue("@productId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.ProductId));
                        sqlCommand.Parameters.AddWithValue("@partnerId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.PartnerId));
                        sqlCommand.Parameters.AddWithValue("@limitCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.LimitCode));
                        sqlCommand.Parameters.AddWithValue("@maxCOCPerDuration", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.MaxCOCPerDuration));
                        sqlCommand.Parameters.AddWithValue("@coverageDurationInDays", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.CoverageDurationInDays));
                        sqlCommand.Parameters.AddWithValue("@coverageDurationInMonths", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.CoverageDurationInMonths));
                        sqlCommand.Parameters.AddWithValue("@cocNumber", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(coc[count - 1].COCNumber));
                        sqlCommand.Parameters.AddWithValue("@issueDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].IssueDate)));
                        sqlCommand.Parameters.AddWithValue("@tempEffectiveDate", RDFramework.Utility.Conversion.SafeSetDatabaseValue<DateTime>(DateTime.Parse(coc[count - 1].EffectiveDate)));
                        #endregion

                        using (SqlDataReader rd = sqlCommand.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                InsuranceTransactionDetails insuranceTransactionDetails = new InsuranceTransactionDetails();
                                coc.IsValid = RDFramework.Utility.Conversion.SafeReadDatabaseValue<bool>(rd["ValidationStatus"]);
                                #region dates
                                DateTime inceptionDate = RDFramework.Utility.Conversion.SafeReadDatabaseValue<DateTime>(rd["EffectiveDate"]);
                                DateTime expiryDate = RDFramework.Utility.Conversion.SafeReadDatabaseValue<DateTime>(rd["TerminationDate"]);

                                if (inceptionDate == DateTime.MinValue && expiryDate == DateTime.MinValue)
                                {
                                    insuranceTransactionDetails.EffectiveDate = null;
                                    insuranceTransactionDetails.TerminationDate = null;
                                }
                                else
                                {
                                    insuranceTransactionDetails.EffectiveDate = inceptionDate.ToString("MM/dd/yyyy HH:mm:ss");
                                    insuranceTransactionDetails.TerminationDate = expiryDate.ToString("MM/dd/yyyy HH:mm:ss");
                                }
                                #endregion
                                this.Add(insuranceTransactionDetails);
                            }
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Validate Effective Date";
            auditTrail.ActionDetails = "InsuranceIMSWS: Effective date validated.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("Error in Generating Coverage Dates");
        }
    }

    #region reserved
    public void GenerateCoverageDates(GeneralDetails general, CustomerDetails customer)
    {
        try
        {
            InsuranceTransactionCollection collection = new InsuranceTransactionCollection();
            AuditTrailDetails auditTrail = new AuditTrailDetails();

            using (SqlConnection sqlConnection = new SqlConnection(Settings.MicroInsuranceConnectionStringWriter))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[Reader].[usp_GenerateCoverageDates]", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    #region details
                    sqlCommand.Parameters.AddWithValue("@insuranceCustomerNo", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(customer.InsuranceCustomerNo));
                    sqlCommand.Parameters.AddWithValue("@productId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.ProductId));
                    sqlCommand.Parameters.AddWithValue("@partnerId", RDFramework.Utility.Conversion.SafeSetDatabaseValue<long>(general.PartnerId));
                    sqlCommand.Parameters.AddWithValue("@numberOfCOC", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.NumberOfCOCs));
                    sqlCommand.Parameters.AddWithValue("@limitCode", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.LimitCode));
                    sqlCommand.Parameters.AddWithValue("@effectiveDateBasis", RDFramework.Utility.Conversion.SafeSetDatabaseValue<string>(general.EffectiveDateBasis));
                    sqlCommand.Parameters.AddWithValue("@maxCOCPerDuration", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.MaxCOCPerDuration));
                    sqlCommand.Parameters.AddWithValue("@coverageDurationInDays", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.CoverageDurationInDays));
                    sqlCommand.Parameters.AddWithValue("@coverageDurationInMonths", RDFramework.Utility.Conversion.SafeSetDatabaseValue<int>(general.CoverageDurationInMonths));
                    #endregion

                    using (SqlDataReader rd = sqlCommand.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            InsuranceTransactionDetails insuranceTransactionDetails = new InsuranceTransactionDetails();
                            #region dates
                            DateTime inceptionDate = RDFramework.Utility.Conversion.SafeReadDatabaseValue<DateTime>(rd["fld_EffectiveDate"]);
                            DateTime expiryDate = RDFramework.Utility.Conversion.SafeReadDatabaseValue<DateTime>(rd["fld_TerminationDate"]);

                            if (inceptionDate == DateTime.MinValue && expiryDate == DateTime.MinValue)
                            {
                                insuranceTransactionDetails.EffectiveDate = null;
                                insuranceTransactionDetails.TerminationDate = null;
                            }
                            else
                            {
                                insuranceTransactionDetails.EffectiveDate = inceptionDate.ToString("MM/dd/yyyy");
                                insuranceTransactionDetails.TerminationDate = expiryDate.ToString("MM/dd/yyyy");
                            }
                            #endregion
                            this.Add(insuranceTransactionDetails);
                        }
                    }
                }
            }

            #region Audit Trail
            //auditTrail.IPAddress = HttpContext.Current.Request.UserHostAddress;
            auditTrail.ActionTaken = "Get Coverage Dates";
            auditTrail.ActionDetails = "InsuranceIMSWS: Coverage dates generated.";
            auditTrail.InsertAuditTrailEntry();
            #endregion
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw new Exception("Error in Generating Coverage Dates");
        }
    }
    #endregion
}