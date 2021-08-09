using System;
using System.Runtime.Serialization;

namespace DaystarPaymentGateway
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string Processor { get; set; }

        [DataMember]
        public string Tokenizer { get; set; }

        [DataMember]
        public string NameOnAccount { get; set; }

        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Zip { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public long PartnerAccountNumber { get; set; }

        [DataMember]
        public string DocumentNumber { get; set; } // TODO: Change to ExternalID

        [DataMember]
        public CustomFields CustomFields { get; set; }

        [DataMember]
        public DateTime DateTimeStamp { get; set; }

        [DataMember]
        public bool IsTestMode { get; set; }

        [DataMember]
        public bool ForceProcessor { get; set; }

        [DataMember]
        public bool ForceTokenizer { get; set; }

        public string ToJSON()
        {
            return Utility.ConvertToJSON(this);
        }
    }

    [DataContract]
    public class TokenRequest : Request
    {
        [DataMember]
        public long DaystarToken { get; set; }

        [DataMember]
        public string ExternalToken { get; set; }

        [DataMember]
        public string ExpirationMonth { get; set; }

        [DataMember]
        public string ExpirationYear { get; set; }
    }

    [DataContract]
    public class RefundRequest : Request
    {
        [DataMember]
        public long OriginalID { get; set; }

        [DataMember]
        public string TransactionID { get; set; }

        [DataMember]
        public string AuthorizationNumber { get; set; }
    }

    [DataContract]
    public class CardRequest : Request
    {
        [DataMember]
        public string CreditCardNumber { get; set; }

        [DataMember]
        public string CreditCardMask { get; set; }

        [DataMember]
        public string CreditCardBrand { get; set; }

        [DataMember]
        public string ExpirationMonth { get; set; }

        [DataMember]
        public string ExpirationYear { get; set; }

        [DataMember]
        public string CardSecurityCode { get; set; }

        [DataMember]
        public bool CardPresent { get; set; }

        [DataMember]
        public bool ForceCardNumber { get; set; }

        [DataMember]
        public bool ForceExpiredCard { get; set; }
    }

    [DataContract]
    public class EFTRequest : Request
    {
        [DataMember]
        public string RoutingNumber { get; set; }

        [DataMember]
        public string RoutingNumberMask { get; set; }

        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string AccountNumberMask { get; set; }

        [DataMember]
        public string AccountType { get; set; }

        [DataMember]
        public string CheckNumber { get; set; }

        [DataMember]
        public string CheckType { get; set; }

        [DataMember]
        public string BankName { get; set; }
    }

    [DataContract]
    public class CustomFields
    {
        [DataMember]
        public string SourceCode { get; set; }

        [DataMember]
        public string PledgeCode { get; set; }

        [DataMember]
        public string PledgeID { get; set; }

        [DataMember]
        public string ProjectCode { get; set; }

        [DataMember]
        public string BatchNumber { get; set; }

        [DataMember]
        public string PartnerAccountName { get; set; }
    }
}
