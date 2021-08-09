using System.ComponentModel;
using System.Runtime.Serialization;

namespace DaystarPaymentGateway
{
    [DataContract]
    public enum ResponseCode
    {
        // Approval - Gateway codes
        [EnumMember]
        [Description("Approved")]
        Approved = 0, // Orbital | 00 | Approved // Paperless | 0 | [empty string]
        [EnumMember]
        [Description("Partially Approved")]
        ApprovedPartial = 1, // Orbital | 00 | Approved (need to check ApprovedAmount for this one)
        [EnumMember]
        [Description("Partially Approved")]
        ApprovedPreviously = 2, // FirstData | 109 | Previously Processed Transaction


        // Declines - Gateway codes
        [EnumMember]
        [Description("Declined")]
        Declined = 1000, // Generic
        [EnumMember]
        [Description("Declined - Do Not Honor")]
        DeclinedDoNotHonor = 1001, // Orbital | 05 | Do Not Honor // Paperless | 0 | 05 - Do Not Honor
        [EnumMember]
        [Description("Declined - Pickup")]
        DeclinedPickup = 1002, // Orbital | 04 | Pickup
        [EnumMember]
        [Description("Declined - Call Voice Operator")]
        DeclinedCallVoiceOperator = 1003, // Orbital | 19 | Call Voice Operator
        [EnumMember]
        [Description("Declined - Duplicate Transaction Detected")]
        DeclinedDuplicateTransactionDetected = 1004, // Paperless | 2 | PT251 - Duplicate transaction detected.  Please wait 2 minutes and try again.
        [EnumMember]
        [Description("Declined - Invalid Credit Card Number")]
        DeclinedInvalidCreditCardNumber = 1005, // Orbital | 14 | Invalid Credit Card Number // Paperless | 0 | 14 - Invalid Credit Card Number // Paperless | 2 | PT230 - Invalid card number provided.
        [EnumMember]
        [Description("Declined - Invalid Expiration Date")]
        DeclinedInvalidExpirationDate = 1006, // Orbital | 74 | Invalid Expiration Date // Paperless | 0 | 74 - Invalid Expiration Date
        [EnumMember]
        [Description("Declined - Invalid Value in Message")]
        DeclinedInvalidValueMessage = 1007, // Orbital | 30 | Invalid value in message
        [EnumMember]
        [Description("Declined - Lost or Stolen Card")]
        DeclinedLostStolenCard = 1009, // Orbital | 43 | Lost / Stolen Card
        [EnumMember]
        [Description("Declined - Other Error")]
        DeclinedOtherError = 1010, // Orbital |  06 | Other Error // Paperless | 2 |    06 Other Error
        [EnumMember]
        [Description("Declined - Security Information Missing")]
        DeclinedSecurityInformationMissing = 1011, // Orbital - 20412 - Usually unregistered IP address
        [EnumMember]
        [Description("Declined - Call Voice Center")]
        DeclinedCallVoiceCenter = 1012, // Orbital | 01 | Call Voice Center
        [EnumMember]
        [Description("Declined - Revocation of All Authorizations - Closed Account")]
        DeclinedRevocationAllAuthorizations = 1013, // Orbital | PB | Revocation of All Authorizations - Closed Account // Paperless | 0 | PB - Revocation of All Authorizations - Closed Account 
        [EnumMember]
        [Description("Declined - Field [AVS STATE] exceeded max length of [2]")]
        DeclinedBadFormatState = 1014, // Orbital | [empty string] | Error. The Orbital Gateway has received a badly formatted message. Field [AVS STATE] exceeded max length of [2]
        [EnumMember]
        [Description("Declined - Field [AVS NAME] exceeded max length of [30]")]
        DeclinedBadFormatName = 1015, // Orbital | [empty string] | Error. The Orbital Gateway has received a badly formatted message. Field [AVS NAME] exceeded max length of [30]
        [EnumMember]
        [Description("Declined - Field [Address verification service address] exceeded max length of [30]")]
        DeclinedBadFormatAddress = 1016, // Orbital | [empty string] | Error. The Orbital Gateway has received a badly formatted message. Field [Address verification service address] exceeded max length of [30]
        [EnumMember]
        [Description("Declined - Field [AVS CITY] exceeded max length of [20]")]
        DeclinedBadFormatCity = 1017, // Orbital | [empty string] | Error. The Orbital Gateway has received a badly formatted message. Field [AVS CITY] exceeded max length of [20]
        [EnumMember]
        [Description("Declined - Field [Address verification service ZIP code] exceeded max length of [10]")]
        DeclinedBadFormatZip = 1018, // Orbital | [empty string] | Error. The Orbital Gateway has received a badly formatted message. Field [Address verification service ZIP code] exceeded max length of [10]
        [EnumMember]
        [Description("Declined - Field [Card Number] exceeded max length of [19]")]
        DeclinedBadFormatCardNumber = 1019, // Orbital | [empty string] | Error. The Orbital Gateway has received a badly formatted message. Field [Card Number] exceeded max length of [19]
        [EnumMember]
        [Description("Declined - Error validating card/account number range")]
        DeclinedValidatingCardAccountRange = 1020, // Orbital | [empty string] | Error validating card/account number range // Paperless | 2 |   841 Error validating card/account number range
        [EnumMember]
        [Description("Declined - Error validating card/account number checksum")]
        DeclinedValidatingCardAccountChecksum = 1021, // Orbital | [empty string] | Error validating card/account number checksum
        [EnumMember]
        [Description("Declined - Precondition Failed: Security Information is missing")]
        DeclinedPreconditionFailedSecurityInformationMissing = 1022, // Orbital | [empty string] | Precondition Failed: Security Information is missing
        [EnumMember]
        [Description("Declined - Request Does Not Adhere to the DTD")]
        DeclinedRequestAdhereDTD = 1023, // Orbital | [empty string] | Request does not adhere to the DTD. Please correct and send again.
        [EnumMember]
        [Description("Declined - The Mandatory Field [Order ID] Was Missing")]
        DeclinedMandatoryOrderID = 1024, // Orbital | [empty string] |  The mandatory field [Order ID] with id [2] was missing from a message of type [Authorize].
        [EnumMember]
        [Description("Declined - Card Number Not Eligible for PINLess Debit Processing")]
        DeclinedPINLessIneligible = 1025, // Orbital | [empty string] | PINLess Debit: Card Number Not Eligible for PINLess Debit Processing.
        [EnumMember]
        [Description("Declined - Invalid Security Code Provided")]
        DeclinedInvalidSecurityCode = 1026, // Paperless | 2 | PT218 - Invalid security code provided.
        [EnumMember]
        [Description("Declined - The Card Number Provided Is Not Valid for Testing")]
        DeclinedInvalidTestingCard = 1027, // Paperless | 2 | PT212 - The card number provided is not valid for testing.
        [EnumMember]
        [Description("Declined - International ACH Not Permitted")]
        DeclinedInternationalACHNotPermitted = 1028, // Paperless | 2 | PT233 - International ACH not permitted.
        [EnumMember]
        [Description("Declined - Guid Should Contain 32 Digits with 4 Dashes")]
        DeclinedGUIDImproperlyFormatted = 1029, // Paperless | 2 | Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).
        [EnumMember]
        [Description("Declined - Source not valid for this operation")]
        DeclinedSourceNotValid = 1030, // Paperless | 2 | PT321 - Source not valid for this operation.
        [EnumMember]
        [Description("Declined - The referenced profile could not be found")]
        DeclinedReferencedProfileNotFound = 1031, // Paperless | 2 | PT167 - The referenced profile could not be found.
        [EnumMember]
        [Description("Declined - The Account Provided Is Not Valid for Testing")]
        DeclinedInvalidTestingAccount = 1032, // Paperless | 2 | PT212 - The account provided is not valid for testing.
        [EnumMember]
        [Description("Declined - Test accounts can only be used in test mode")]
        DeclinedTestModeOnlyAccount = 1033, // Paperless | 2 | PT260 - Test accounts can only be used in test mode
        [EnumMember]
        [Description("Declined - No Answer")] // FirstData | 000 | Received no answer from auth network
        DeclinedNoAnswer = 1034,
        [EnumMember]
        [Description("Declined - Fraud")] // FirstData | 200 | Fraud
        DeclinedFraud = 1035,
        [EnumMember]
        [Description("Declined - Invalid Routing Number Provided")] 
        DeclinedInvalidRoutingNumber = 1036, // Paperless | 2 | PT200 - Invalid routing number provided.
        [EnumMember]
        [Description("Declined - Invalid Account Number Provided")]
        DeclinedInvalidAccountNumber = 1037, // Paperless | 2 | PT140 - Invalid account number provided.] 
        [EnumMember]
        [Description("Declined - Credit Card Expired")]
        DeclinedExpiredCard = 1038, // FirstData | 522 | Erroneous
        [EnumMember]
        [Description("Declined - High Risk")]
        DeclinedHighRisk = 1039, // FirstData | 787 | Decline High Risk
        [EnumMember]
        [Description("Declined - Invalid Division Number")]
        DeclinedInvalidDivisionNumber = 1040, // FirstData | 231 | Decline Invalid Division Number
        [EnumMember]
        [Description("Declined - Invalid Method of Payment for Division")]
        DeclinedInvalidMethodOfPaymentForDivision = 1041, // FirstData | 231 | Decline Invalid MOP for Division 
        [EnumMember]
        [Description("Declined - Invalid Field Data")] // FirstData | 225 | Invalid field data
        DeclinedInvalidFieldData = 1042,
        [EnumMember]
        [Description("Declined - Missing Companion Data")] // FirstData | 227 | Missing Companion Data
        DeclinedMissingCompanionData = 1043,
        [EnumMember]
        [Description("Declined - Invalid Currency")] // FirstData | 238 | Invalid Currency
        DeclinedInvalidCurrency = 1044,
        [EnumMember]
        [Description("Declined - Reserved")] // FirstData | 254, 255| Reserved
        DeclinedReserved = 1045,
        [EnumMember]
        [Description("Declined - Invalid Amount")]
        DeclinedInvalidAmount = 1046,
        [EnumMember]
        [Description("Declined - Invalid Temporary Services Data")]
        DeclinedInvalidTemporaryServicesData = 1048,
        [EnumMember]
        [Description("Declined - Illegal Action")]
        DeclinedIllegalAction = 1049,
        [EnumMember]
        [Description("Declined - Invalid Purchase Level 3")]
        DeclinedInvalidPurchaseLevel3 = 1050,
        [EnumMember]
        [Description("Declined - Invalid Encryption Format")]
        DeclinedInvalidEncryptionFormat = 1051,
        [EnumMember]
        [Description("Declined - Missing Or Invalid Secure Payment Data")]
        DeclinedMissingOrInvalidSecurePaymentData = 1052,
        [EnumMember]
        [Description("Declined - Merchant Master Card Secure Code Not Enabled")]
        DeclinedMerchantMasterCardSecureCodeNotEnabled = 1053,
        [EnumMember]
        [Description("Declined - Blanks Not Passed In Reserved Field")]
        DeclinedBlanksNotPassedInReservedField = 1054,
        [EnumMember]
        [Description("Declined - Invalid Merchant Category")]
        DeclinedInvalidMerchantCategory = 1055,
        [EnumMember]
        [Description("Declined - Invalid Transaction Type")]
        DeclinedInvalidTransactionType = 1056,
        [EnumMember]
        [Description("Declined - Missing Customer Service Phone")]
        DeclinedMissingCustomerServicePhone = 1057,
        [EnumMember]
        [Description("Declined - Not Authorized To Send Record")]
        DeclinedNotAuthorizedToSendRecord = 1059,
        [EnumMember]
        [Description("Declined - Authorization Code Response Date Invalid")]
        DeclinedAuthorizationCodeResponseDateInvalid = 1060,
        [EnumMember]
        [Description("Declined - Partial Authorization Not Allowed")]
        DeclinedPartialAuthorizationNotAllowed = 1061,
        [EnumMember]
        [Description("Declined - Invalid Purchase Level 2")]
        DeclinedInvalidPurchaseLevel2 = 1062,
        [EnumMember]
        [Description("Declined - Transaction Not Supported")]
        DeclinedTransactionNotSupported = 1063,
        [EnumMember]
        [Description("Declined - Invalid Time")]
        DeclinedInvalidTime = 1064,
        [EnumMember]
        [Description("Declined - InvalidDate")]
        DeclinedInvalidDate = 1065,
        [EnumMember]
        [Description("Declined - Velocity Block Reject")]
        DeclinedVelocityBlockReject = 1066,
        [EnumMember]
        [Description("Declined - Disbursement Reject")]
        DeclinedDisbursementReject = 1067,
        [EnumMember]
        [Description("Declined - Issuer Unavailable")]
        DeclinedIssuerUnavailable = 1068,
        [EnumMember]
        [Description("Declined - Credit Floor")]
        DeclinedCreditFloor = 1069,
        [EnumMember]
        [Description("Declined - Processor Decline")]
        DeclinedProcessorDecline = 1070,
        [EnumMember]
        [Description("Declined - Not On File")]
        DeclinedNotOnFile = 1071,
        [EnumMember]
        [Description("Declined - Already Reversed")]
        DeclinedAlreadyReversed = 1072,
        [EnumMember]
        [Description("Declined - Amount Mismatch")]
        DeclinedAmountMismatch = 1073,
        [EnumMember]
        [Description("Declined - Authorization Not Found")]
        DeclinedAuthorizationNotFound = 1074,
        [EnumMember]
        [Description("Declined - Trans Armor Service Unavailable")]
        DeclinedTransArmorServiceUnavailable = 1075,
        [EnumMember]
        [Description("Declined - Expired Lock")]
        DeclinedExpiredLock = 1076,
        [EnumMember]
        [Description("Declined - Trans Armor Invalid Token")]
        DeclinedTransArmorInvalidToken = 1077,
        [EnumMember]
        [Description("Declined - Trans Armor Invalid Result")]
        DeclinedTransArmorInvalidResult = 1078,
        [EnumMember]
        [Description("Declined - Invalid Institution")]
        DeclinedInvalidInstitution = 1079,
        [EnumMember]
        [Description("Declined - ExcessivePINTry")]
        DeclinedExcessivePINTry = 1080,
        [EnumMember]
        [Description("Declined - Over The Limit")]
        DeclinedOverTheLimit = 1081,
        [EnumMember]
        [Description("Declined - Over Limit Frequency")]
        DeclinedOverLimitFrequency = 1082,
        [EnumMember]
        [Description("Declined - On Negative File")]
        DeclinedOnNegativeFile = 1083,
        [EnumMember]
        [Description("Declined - Insufficient Funds")]
        DeclinedInsufficientFunds = 1084,
        [EnumMember]
        [Description("Declined - Blocked Account")]
        DeclinedBlockedAccount = 1085,
        [EnumMember]
        [Description("Declined - Stop Payment Order One Time Recurring")]
        DeclinedStopPaymentOrderOneTimeRecurring = 1086,
        [EnumMember]
        [Description("Declined - Revocation Of Authorization For All Recurring")]
        DeclinedRevocationOfAuthorizationForAllRecurring = 1087,
        [EnumMember]
        [Description("Declined - Account Previously Activated")]
        DeclinedAccountPreviouslyActivated = 1088,
        [EnumMember]
        [Description("Declined - UnableToVoid")]
        DeclinedUnableToVoid = 1089,
        [EnumMember]
        [Description("Declined - Block Activation Failed")]
        DeclinedBlockActivationFailed = 1090,
        [EnumMember]
        [Description("Declined - Issuance Does Not Meet Minimum Amount")]
        DeclinedIssuanceDoesNotMeetMinimumAmount = 1091,
        [EnumMember]
        [Description("Declined - No Original Authorization Found")]
        DeclinedNoOriginalAuthorizationFound = 1092,
        [EnumMember]
        [Description("Declined - Outstanding Authorization")]
        DeclinedOutstandingAuthorization = 1093,
        [EnumMember]
        [Description("Declined - Activation Amount Incorrect")]
        DeclinedActivationAmountIncorrect = 1094,
        [EnumMember]
        [Description("Declined - Maximum Redemption Limit Met")]
        DeclinedMaximumRedemptionLimitMet = 1095,
        [EnumMember]
        [Description("Declined - New Card Issued")]
        DeclinedNewCardIssued = 1096,
        [EnumMember]
        [Description("Declined - Invalid Institution Code")]
        DeclinedInvalidInstitutionCode = 1097,
        [EnumMember]
        [Description("Declined - BIN Block")]
        DeclinedBINBlock = 1098,
        [EnumMember]
        [Description("Declined - Match Failed")]
        DeclinedMatchFailed = 1099,
        [EnumMember]
        [Description("Declined - Validation Failed")]
        DeclinedValidationFailed = 1100,
        [EnumMember]
        [Description("Declined - Missing Name")]
        DeclinedMissingName = 1101,
        [EnumMember]
        [Description("Declined - Invalid Account Type")]
        DeclinedInvalidAccountType = 1102,
        [EnumMember]
        [Description("Declined - Account Closed")]
        DeclinedAccountClosed = 1103,
        [EnumMember]
        [Description("Declined - Account Not Found")]
        DeclinedAccountNotFound = 1104,
        [EnumMember]
        [Description("Declined - Account Frozen")]
        DeclinedAccountFrozen = 1105,
        [EnumMember]
        [Description("Declined - ACH Non Participant")]
        DeclinedACHNonParticipant = 1106,
        [EnumMember]
        [Description("Declined - Bad Account Number Data")]
        DeclinedBadAccountNumberData = 1107,
        [EnumMember]
        [Description("Declined - Original Transaction Not Approved")]
        DeclinedOriginalTransactionNotApproved = 1108,
        [EnumMember]
        [Description("Declined - Refund Greater Than Original")]
        DeclinedRefundGreaterThanOriginal = 1109,
        [EnumMember]
        [Description("Declined - Further Information Needed")]
        DeclinedFurtherInformationNeeded = 1110,
        [EnumMember]
        [Description("Declined - Card Restricted")]
        DeclinedCardRestricted = 1111,
        [EnumMember]
        [Description("Declined - Invalid PIN")]
        DeclinedInvalidPIN = 1112,
        [EnumMember]
        [Description("Declined - Invalid Merchant")]
        DeclinedInvalidMerchant = 1113,
        [EnumMember]
        [Description("Declined - Unauthorized User")]
        DeclinedUnauthorizedUser = 1114,
        [EnumMember]
        [Description("Declined - Process Unavailable")]
        DeclinedProcessUnavailable = 1115,
        [EnumMember]
        [Description("Declined - Card Inactive")]
        DeclinedCardInactive = 1116,
        [EnumMember]
        [Description("Declined - AcquirerError")]
        DeclinedAcquirerError = 1117,

        // Errors for internal DPG rules
        [EnumMember]
        [Description("Error - Card Not Found in Database")]
        ErrorCardNotFound = 2000,
        [EnumMember]
        [Description("Error - Communication Exception Encountered")]
        ErrorCommunication = 2001,
        [EnumMember]
        [Description("Error - Decryption Error")]
        ErrorDecryption = 2002,
        [EnumMember]
        [Description("Error - Duplicate Transaction Detected in DPG")]
        ErrorDuplicate = 2003,
        [EnumMember]
        [Description("Error - Credit Card Expired")]
        ErrorExpiredCard = 2004,
        [EnumMember]
        [Description("Error - Internal Generated Exception")]
        ErrorInternalException = 2005,
        [EnumMember]
        [Description("Error - Invalid Credit Card Expiration Date")]
        ErrorInvalidExpiration = 2006,
        [EnumMember]
        [Description("Error - Mismatch of Partner Account")]
        ErrorMismatchAccount = 2007,
        [EnumMember]
        [Description("Error - Mismatch of Currency")]
        ErrorMismatchCurrency = 2008,
        [EnumMember]
        [Description("Error - Mismatch of Registered Gateway")]
        ErrorMismatchGateway = 2009,
        [EnumMember]
        [Description("Error - Missing Account Information")]
        ErrorMissingAccount = 2010,
        [EnumMember]
        [Description("Error - Missing Token Information")]
        ErrorMissingToken = 2011,
        [EnumMember]
        [Description("Error - Disabled Gateway / Processor")]
        ErrorProcessorDisabled = 2012,
        [EnumMember]
        [Description("Error - Unavailable Gateway / Processor")]
        ErrorProcessorUnavailable = 2013,
        [EnumMember]
        [Description("Error - Timeout Encountered")]
        ErrorTimeoutException = 2014,
        [EnumMember]
        [Description("Error - Token Not Found in DPG")]
        ErrorTokenNotFound = 2015,
        [EnumMember]
        [Description("Error - Gateway Generated Exception")]
        ErrorGatewayException = 2016, // Generic third-party/gateway exception
        [EnumMember]
        [Description("Error - EFT Info Not Found in Database")]
        ErrorEFTNotFound = 2017,
        [EnumMember]
        [Description("Error - EFT Info Not Found in Database")]
        ErrorInvalidTokenType = 2018,
        [EnumMember]
        [Description("Error - Missing Processor")]
        ErrorMissingProcessor = 2019,
        [EnumMember]
        [Description("Error - Missing Tokenizer")]
        ErrorMissingTokenizer = 2020,
        [EnumMember]
        [Description("Error - Invalid Processor")]
        ErrorInvalidProcessor = 2021,
        [EnumMember]
        [Description("Error - Invalid Tokenizer")]
        ErrorInvalidTokenizer = 2022,
        [EnumMember]
        [Description("Error - Missing Currency")]
        ErrorMissingCurrency = 2023,
        [EnumMember]
        [Description("Error - Same Payment Method Recently Processed")]
        ErrorPaymentMethodRecentlyProcessed = 2024,
        [EnumMember]
        [Description("Error - Invalid Amount")]
        ErrorInvalidAmount = 2025,
        [EnumMember]
        [Description("Error - Unrecognized Response Code")]
        ErrorUnrecognizedResponseCode = 2026,
        [EnumMember]
        [Description("Error - NULL Response")]
        ErrorNullResponse = 2027,
    }

    [DataContract]
    public enum RawType
    {
        [EnumMember]
        [Description("Request")]
        Request = 1,
        [EnumMember]
        [Description("Response")]
        Response = 2
    }

    [DataContract]
    public enum PaymentMethod
    {
        [EnumMember]
        Token = 1,
        [EnumMember]
        Card = 2,
        [EnumMember]
        EFT = 3,
        [EnumMember]
        PAD = 4
    }

    [DataContract]
    public enum TransactionType
    {
        [EnumMember]
        Charge = 1,
        [EnumMember]
        Tokenize = 2,
        [EnumMember]
        Refund = 3
    }

    [DataContract]
    public enum Processor
    {
        [EnumMember]
        [Description("Daystar")]
        Daystar = 1,
        [EnumMember]
        [Description("Paperless")]
        Paperless = 2,
        [EnumMember]
        [Description("Orbital")]
        Orbital = 3,
        [EnumMember]
        [Description("First Data")]
        FirstData = 4
    }

    [DataContract]
    public enum Currency
    {
        [EnumMember]
        CAD,
        [EnumMember]
        USD
    }

    public enum Direction
    {
        RequestIn = 1,
        RequestOut = 2,
        ResponseIn = 3,
        ResponseOut = 4
    }

    public static class Definitions
    {
        public static class Processors
        {
            public const string Daystar = "DAYSTAR";
            public const string Paperless = "PAPERLESS";
            public const string Orbital = "ORBITAL";
            public const string FirstData = "FIRSTDATA";
        }

        public static class ConnectionStrings
        {
            public const string DaystarPaymentGatewayDevelopment = @"user id=PaymentUser; password=P@ym3nt5; server=BDVSQLSANDBOX; database=DaystarPaymentGateway; connection timeout=30";
            public const string DaystarPaymentGatewayProduction = @"user id=PaymentUser; password=P@yN0w; server=VFSQLAPPSPROD; database=DaystarPaymentGateway; connection timeout=30";
        }

        public static class Emails
        {
            public const string Unknown = "DPSUnknown@daystar.com";
            public const string Duplicate = "DPSDuplicate@daystar.com";
            public const string Mismatch = "DPSMismatch@daystar.com";
            public const string Partial = "DPSPartial@daystar.com";
            public const string DPG = "DaystarPaymentGateway@daystar.com";
        }

        public static class Formats
        {
            public const string DateTimeStamp = "MM/dd/yyyy hh:mm:ss.fff tt";
        }

        public static class Misc
        {
            public const string Encrypted = "<Encrypted>";
            public const string DPG = "DPG";
        }

        public static class Environments
        {
            public const string Production = "PROD";
            public const string Development = "DEV";
        }

        public static class Boolean
        {
            public const string True = "TRUE";
            public const string False = "FALSE";
        }

        //public static class Currency
        //{
        //    public const string AmericanDollars = "USD";
        //    public const string CanadianDollars = "CAD";
        //}

        public static class Paperless
        {
            public const string GatewayURL = "https://svc.paperlesstrans.com:9999/";

            public const string TerminalID = @"2a0721b0-36a8-4684-9192-c41aa4edd525";
            public const string TerminalKey = @"715628723";
        }

        public static class FirstData
        {
            public class TransactionType
            {
                public const string Purchase = "00";
                public const string TaggedRefund = "34";
                public const string TaggedVoid = "33";
                public const string Refund = "04";
            }
        }

        public static class Orbital
        {
            //Login
            //public const string TestGatewayURL = "https://orbitalvar1.paymentech.net/authorize";
            public const string TestMerchantLogin = @"700000208430";
            public const string TestMerchantPassword = @"000002";
            public const string TestOrbitalUsername = @"DAYSTAR1";
            public const string TestOrbitalPassword = @"TELEPA55";

            // Gateway URL is now set in the SDK line.properties config file
            //public const string GatewayURL = "https://orbital1.paymentech.net/authorize";
            public const string MerchantLogin = @"000010128306";
            public const string MerchantPassword = @"000002";
            public const string OrbitalUsername = @"DAYSTARTELE8306";
            public const string OrbitalPassword = @"YFL83GFJG213JKP";

            // Tokenizations
            public const string GenerateToken = "OrbitalCustomerProfileFromOrderInd=A";

            //public const string RawRequest = "RawRequest";
            //public const string RawResponse = "RawResponse";

            // Industry Type
            public const string ECommerce = "EC";

            // Partial Authorization
            public const string Yes = "Y";

            // Message Type
            public const string Authorize = "A";
            public const string AuthorizeCapture = "AC";
            public const string Refund = "R";

            // Security Code Present / Approval Status
            public const string True = "1";

            // CustomerProfileFromOrderInd - See 9.8.7.0
            public const string Auto = "A";

            // CustomerProfileOrderOverrideInd - See 9.8.7.1
            public const string None = "NO"; // No mapping to older data

            //Currency
            public const string CanadianDollarsCode = "124";

            public class Params
            {
                public const string Username = "OrbitalConnectionUsername";
                public const string Password = "OrbitalConnectionPassword";
                public const string MerchantID = "MerchantID";
                public const string BIN = "BIN";
                public const string IndustryType = "IndustryType";
                public const string MessageType = "MessageType";
                public const string CurrencyCode = "CurrencyCode";
                public const string CardSecurity = "CardSecValInd";
                public const string CardSecurityCode = "CardSecVal";
                public const string CardNumber = "AccountNum";
                public const string CardExpiration = "Exp";
                public const string CardName = "AVSname";
                public const string Address1 = "AVSaddress1";
                public const string Address2 = "AVSaddress2";
                public const string City = "AVScity";
                public const string State = "AVSstate";
                public const string Zip = "AVSzip";
                public const string Country = "AVScountryCode";
                public const string OrderID = "OrderID";
                public const string ReferenceNumberMode = "CustomerProfileFromOrderInd";
                public const string ReferenceNumberOverride = "CustomerProfileOrderOverrideInd";
                public const string Amount = "Amount";
                public const string Comments = "Comments";
                public const string PartialAuthorizationAllowed = "PartialAuthInd";
                public const string RedeemedAmount = "RedeemedAmount";
                public const string RequestedAmount = "RequestedAmount";
                public const string PartialAuthorization = "PartialAuthOccurred";
                public const string ReferenceNumber = "TxRefNum";
                public const string ApprovalStatus = "ApprovalStatus";
            }
        }

    }
}
