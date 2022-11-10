using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PaymentDetails
/// </summary>
public class PaymentDetails
{
    #region Constructor
    public PaymentDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Fields/Properties
    private string _referenceNo; //insurance transaction reference number from register
    public string ReferenceNo
    {
        set { _referenceNo = value; }
        get { return _referenceNo; }
    }

    private string _notificationDate;
    public string NotificationDate
    {
        set { _notificationDate = value; }
        get { return _notificationDate; }
    }

    private DateTime _formattedNotificationDate;
    internal DateTime FormattedNotificationDate
    {
        set { _formattedNotificationDate = value; }
        get { return _formattedNotificationDate; }
    }

    private string _datePaid;
    public string DatePaid
    {
        set { _datePaid = value; }
        get { return _datePaid; }
    }

    private DateTime _formattedDatePaid;
    internal DateTime FormattedDatePaid
    {
        set { _formattedDatePaid = value; }
        get { return _formattedDatePaid; }
    }

    private int _numberOfCOCsPaid;
    public int NumberOfCOCsPaid
    {
        get { return _numberOfCOCsPaid; }
        set { _numberOfCOCsPaid = value; }
    }

    private decimal _productAmount;
    public decimal ProductAmount
    {
        set { _productAmount = value; }
        get { return _productAmount; }
    }

    private decimal _totalAmountPaid;
    public decimal TotalAmountPaid
    {
        set { _totalAmountPaid = value; }
        get { return _totalAmountPaid; }
    }

    private string _paymentReferenceNo; //other reference number from partner
    public string PaymentReferenceNo
    {
        set { _paymentReferenceNo = value; }
        get { return _paymentReferenceNo; }
    }

    private string _transactionCheckNumber;
    public string TransactionCheckNumber
    {
        set { _transactionCheckNumber = value; }
        get { return _transactionCheckNumber; }
    }

    private string _paymentMethod;
    public string PaymentMethod
    {
        set { _paymentMethod = value; }
        get { return _paymentMethod; }
    }

    private string _paymentOrigin; //payment channel
    public string PaymentOrigin
    {
        set { _paymentOrigin = value; }
        get { return _paymentOrigin; }
    }

    private string _paymentNotes;
    public string PaymentNotes
    {
        set { _paymentNotes = value; }
        get { return _paymentNotes; }
    }

    private int _paymentStatusId;
    public int PaymentStatusId
    {
        set { _paymentStatusId = value; }
        get { return _paymentStatusId; }
    }
    #endregion
}