using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using DaystarPaymentGateway.FirstData;
using DaystarPaymentGateway2.Models;

namespace DaystarPaymentGateway
{
    public static partial class FirstDataAccess
    {
        public static bool TestMode;

       

        public static void Initialize(bool testMode)
        {
            TestMode = testMode;
          
        }

        public static Response ProcessEFT(EFTRequest request)
        {
            var transaction = new CheckTransaction
            {
                ExactID = TestMode ? Constants.TestExactID : Constants.ExactID,
                Password = TestMode ? Constants.TestPassword : Constants.Password,
                Transaction_Type = Definitions.FirstData.TransactionType.Purchase,
                BankAccountNumber = request.AccountNumber,
                BankRoutingNumber = request.RoutingNumber,
                DollarAmount = request.Amount.ToString(Constants.CurrencyFormat),
                CheckNumber = request.CheckNumber,
                CheckType = request.CheckType,
                CustomerName = Helper.Sanitize(request.NameOnAccount),
                Reference_No = request.PartnerAccountNumber.ToString(),
                Customer_Ref = request.ID.ToString(),
                CustomerIDType = null,
                CustomerID = null,
                Client_Email = string.IsNullOrEmpty(request.EmailAddress) ? Definitions.Emails.DPG : request.EmailAddress,
                Address = string.IsNullOrEmpty(request.AddressLine1)
                    ? Constants.DefaultAddress
                    : new Address_Type
                    {
                        Address1 = Helper.Sanitize(request.AddressLine1),
                        Address2 = string.IsNullOrEmpty(request.AddressLine2) ? null : Helper.Sanitize(request.AddressLine2),
                        City = Helper.Sanitize(request.City),
                        State = string.IsNullOrEmpty(request.State) ? Constants.FakeState : Helper.Sanitize(request.State),
                        Zip = string.IsNullOrEmpty(request.Zip)
                            ? Constants.FakeZip
                            : request.Zip.Length > 5 && request.Country.StartsWith(Constants.UnitedStatesCountryCode)
                                ? request.Zip.Substring(0, 5)
                                : Helper.Sanitize(request.Zip),
                        CountryCode = Constants.UnitedStatesCountryCode, // EFTs only valid in the USA
                        PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? Constants.DefaultPhone : request.PhoneNumber,
                        PhoneType = Constants.PhoneType
                    },
                Ecommerce_Flag = Constants.ECommerceFlag
            };

            //DataLayerStatic.DA.InsertTransactionRaw(request.ID, transaction, RawType.Request, Direction.RequestOut);

            //DataLayerStatic.DA.InsertOutgoingRequest(request.ID, transaction);

            var res = new TransactionResult(); // client.SendAndCommit(transaction);

            //DataLayerStatic.DA.InsertTransactionRaw(request.ID, res, RawType.Response, Direction.ResponseIn);

            //DataLayerStatic.DA.InsertIncomingResponse(request.ID, res);

            var response = new Response
            {
                ID = request.ID,
                ResponseCode = ComputeResponseCode(request.ID, res.Bank_Resp_Code, res.Bank_Message, res.Transaction_Approved),
                ApprovedAmount = res.Transaction_Approved ? request.Amount.ToString(Constants.CurrencyFormat) : 0.ToString(Constants.CurrencyFormat),
                GatewayID = res.Authorization_Num,
                Message = res.Bank_Message
            };

            return response;
        }

        public static Response ChargeCard(CardRequest request)
        {
            var transaction = new Transaction
            {
                ExactID = TestMode ? Constants.TestExactID : Constants.ExactID,
                Password = TestMode ? Constants.TestPassword : Constants.Password,
                Transaction_Type = Definitions.FirstData.TransactionType.Purchase,
                Card_Number = request.CreditCardNumber,
                CAVV = string.IsNullOrEmpty(request.CardSecurityCode) ? Constants.FakeCardSecurityCode : request.CardSecurityCode,
                CardHoldersName = Helper.Sanitize(request.NameOnAccount),
                DollarAmount = request.Amount.ToString(Constants.CurrencyFormat),
                Currency = request.Currency,
                Reference_No = request.PartnerAccountNumber.ToString(),
                Customer_Ref = request.ID.ToString(),
                Reference_3 = request.Source,
                Client_Email = Definitions.Emails.DPG,
                Address = string.IsNullOrEmpty(request.AddressLine1)
                    ? Constants.DefaultAddress
                    : new Address_Type
                    {
                        Address1 = Helper.Sanitize(request.AddressLine1),
                        Address2 = string.IsNullOrEmpty(request.AddressLine2) ? null : Helper.Sanitize(request.AddressLine2),
                        City = Helper.Sanitize(request.City),
                        State = string.IsNullOrEmpty(request.State) ? Constants.FakeState : Helper.Sanitize(request.State),
                        Zip = string.IsNullOrEmpty(request.Zip)
                            ? Constants.FakeZip
                            : request.Zip.Length > 5 && request.Country.StartsWith("US")
                                ? request.Zip.Substring(0, 5)
                                : Helper.Sanitize(request.Zip),
                        CountryCode = request.Country,
                        PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? Constants.DefaultPhone : request.PhoneNumber,
                        PhoneType = Constants.PhoneType
                    },

                Expiry_Date = $"{request.ExpirationMonth}{request.ExpirationYear.Substring(request.ExpirationYear.Length - 2)}"
            };

            var res = new TransactionResult(); // client.SendAndCommit(transaction);


            var response = new Response
            {
                ID = request.ID,
                ResponseCode = ComputeResponseCode(request.ID, res.Bank_Resp_Code, res.Bank_Message, res.Transaction_Approved),
                ApprovedAmount = res.Transaction_Approved ? request.Amount.ToString(Constants.CurrencyFormat) : 0.ToString(Constants.CurrencyFormat),
                GatewayID = res.Authorization_Num,
                Message = res.Bank_Message
            };

            return response;
        }

        public static Response RefundTransaction(RefundRequest request, bool isEFT)
        {
            Response response;

            if (isEFT)
            {
                var checkTransaction = new CheckTransaction
                {
                    ExactID = TestMode ? Constants.TestExactID : Constants.ExactID,
                    Password = TestMode ? Constants.TestPassword : Constants.Password,
                    Transaction_Type = Definitions.FirstData.TransactionType.TaggedRefund,
                    DollarAmount = request.Amount.ToString(Constants.CurrencyFormat),
                    Transaction_Tag = request.TransactionID,
                    Ecommerce_Flag = null,
                    Client_Email = Definitions.Emails.DPG,
                    Authorization_Num = request.AuthorizationNumber,
                    Reference_No = request.PartnerAccountNumber.ToString(),
                    Customer_Ref = request.ID.ToString(),
                };

                var res = new TransactionResult(); // client.SendAndCommit(transaction);

                response = new Response
                {
                    ID = request.ID,
                    ResponseCode = res == null
                        ? ResponseCode.ErrorNullResponse
                        : ComputeResponseCode(request.ID, res.Bank_Resp_Code, res.Bank_Message, res.Transaction_Approved),
                    ApprovedAmount = res == null ? null : res.Transaction_Approved ? request.Amount.ToString(Constants.CurrencyFormat) : 0.ToString(Constants.CurrencyFormat),
                    GatewayID = res?.Authorization_Num,
                    Message = res?.Bank_Message
                };
            }
            else
            {
                var transaction = new Transaction
                {
                    ExactID = TestMode ? Constants.TestExactID : Constants.ExactID,
                    Password = TestMode ? Constants.TestPassword : Constants.Password,
                    Transaction_Type = Definitions.FirstData.TransactionType.TaggedRefund,
                    DollarAmount = request.Amount.ToString(Constants.CurrencyFormat),
                    Transaction_Tag = request.TransactionID,
                    Ecommerce_Flag = null,
                    Client_Email = Definitions.Emails.DPG,
                    Authorization_Num = request.AuthorizationNumber,
                    Reference_No = request.PartnerAccountNumber.ToString(),
                    Customer_Ref = request.ID.ToString(),
                    Reference_3 = request.Source
                };

                var res = new TransactionResult(); // client.SendAndCommit(transaction);

                response = new Response
                {
                    ID = request.ID,
                    ResponseCode = res == null
                        ? ResponseCode.ErrorNullResponse
                        : ComputeResponseCode(request.ID, res.Bank_Resp_Code, res.Bank_Message, res.Transaction_Approved),
                    ApprovedAmount = res == null ? null : res.Transaction_Approved ? request.Amount.ToString(Constants.CurrencyFormat) : 0.ToString(Constants.CurrencyFormat),
                    GatewayID = res?.Authorization_Num,
                    Message = res?.Bank_Message
                };
            }

            return response;
        }
    }
}
