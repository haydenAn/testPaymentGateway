using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using DaystarPaymentGateway2.Models;
using NLog;

namespace DaystarPaymentGateway
{
    public static class Encryption
    {
        private static readonly Logger Logger = LogManager.GetLogger("NLogWriter");
        private static string publicKey;
        private static readonly RSACryptoServiceProvider RSAPublic = new RSACryptoServiceProvider();

        public static void SetPublicKey(string key = null)
        {
            //if (string.IsNullOrEmpty(publicKey))
            //    publicKey = DataLayerStatic.DA.GetSetting("PublicKey");
            //else 
            if (key != null)
                publicKey = key;
            else
                return;

            Logger.Info($"Public Key initialized to: {publicKey}");
        }

        public static string EncryptRSA4K(string cardNumber)
        {
            var dataToEncrypt = Encoding.ASCII.GetBytes(cardNumber);

            if (RSAPublic.KeySize != 4096)
                RSAPublic.FromXmlString(publicKey);

            var encryptedRsa = RSAPublic.Encrypt(dataToEncrypt, false);
            var result = ByteArrayToHex(encryptedRsa);
            return result;
        }

        private static string ByteArrayToHex(byte[] arr)
        {
            var hex = BitConverter.ToString(arr).Replace("-", "");
            return hex;
        }
    }

    public static class Settings
    {
        public static string ActiveEnvironment = null;

        public static int DuplicateThreshold { get; set; }
        public static int TestCardApprovalThreshold { get; set; }


        public static readonly List<string> ApprovalCreditCards = new List<string>
        {
            "343434343434343",
            "4444424444444440",
            "5454545454545454",
            "6011111111111117",

        };

        public static readonly List<string> DeclineCreditCards = new List<string>
        {
            "341134113411347",
            "4111111111111111",
            "5105105105105100",
            "6011000995500000"
        };

        public static List<string> ApprovalEFTs = new List<string>
        {
            "111111118|12121214",
            "123456780|1234567890"
        };

        public static List<string> DeclineEFTs = new List<string>
        {
            "222222226|42222226"
        };

        public static string AssignTestCard()
        {
            var random = new Random();

            // Random is inclusive lower bound, exclusive upper bound
            var randomNumber = random.Next(0, 100) + 1;

            return randomNumber > TestCardApprovalThreshold
                ? DeclineCreditCards.ElementAt(randomNumber % DeclineCreditCards.Count)
                : ApprovalCreditCards.ElementAt(randomNumber % ApprovalCreditCards.Count);
        }

        public static string AssignTestEFT()
        {
            var random = new Random();

            var randomNumber = random.Next(0, 100) + 1;

            return randomNumber % 5 == 0
                ? DeclineEFTs.ElementAt(0)
                : ApprovalEFTs.ElementAt(randomNumber % ApprovalEFTs.Count);
        }

        public static bool IsTestCard(string hashKey)
        {
            foreach (var card in ApprovalCreditCards)
            {
                if (card.GetHashCode().ToString() == hashKey)
                    return true;
            }

            foreach (var card in DeclineCreditCards)
            {
                if (card.GetHashCode().ToString() == hashKey)
                    return true;
            }

            return false;
        }

    }

    public static class Helper
    {
        private static readonly Logger Logger = LogManager.GetLogger("NLogWriter");

        public static bool IsApproved(ResponseCode responseCode) => responseCode == ResponseCode.Approved || responseCode == ResponseCode.ApprovedPartial;

        public static void CheckForceFlags(Request request)
        {
            // Temporary testing
            if (Settings.ActiveEnvironment != Definitions.Environments.Production)
            {
                Logger.Error($"Check Force Flags skipped for {Settings.ActiveEnvironment} environment.");
                return;
            }

            var cardForceFlags = request is CardRequest cardRequest &&
                                  (cardRequest.ForceCardNumber || cardRequest.ForceExpiredCard);

            if (!request.ForceProcessor && !request.ForceTokenizer && !cardForceFlags)
                return;

            var emailBody = $"Gateway Alert{Environment.NewLine}{Environment.NewLine}" +
                            $"DPG ID:{request.ID}{Environment.NewLine}" +
                            $"Name: {request.NameOnAccount}{Environment.NewLine}" +
                            $"Account Number: {request.PartnerAccountNumber}{Environment.NewLine}" +
                            $"Processor: {request.Processor}{Environment.NewLine}" +
                            $"Requested Amount: {request.Amount:F}{Environment.NewLine}{Environment.NewLine}" +
                            $"Force Processor: {request.ForceProcessor}{Environment.NewLine}{Environment.NewLine}" +
                            $"Force Tokenizer: {request.ForceTokenizer}{Environment.NewLine}{Environment.NewLine}" +
                            $"Force Card: {cardForceFlags}{Environment.NewLine}{Environment.NewLine}";

            Logger.Info(emailBody);

            // TODO: Get rid of mihai make this a config param! 
            //Email.SendEmail(new MailAddress(Definitions.Emails.DPG),
            //    FusionConstants.EmailAddresses.MihaiCotet, //Definitions.Emails.Unknown
            //    $"[{Settings.ActiveEnvironment}] DPG Force Flags Alert",
            //    emailBody);
        }

        public static void HandleException(Request request, Response error, Exception ex, string identifier, string identifierValue, bool includeOutgoingResponse = true)
        {
            Logger.Error(ex);

            error.ID = request.ID;
            error.ResponseCode = ResponseCode.ErrorGatewayException;
            error.Message = $"DPG Error: {ex}";

            //DataLayerStatic.DA.InsertIncomingResponse(request.ID, error);

            //if (includeOutgoingResponse)
            //    DataLayerStatic.DA.InsertOutgoingResponse(request.ID, error);

            var emailBody = $"Gateway Exception{Environment.NewLine}{Environment.NewLine}" +
                            $"DPG ID:{request.ID}{Environment.NewLine}" +
                            $"External ID:{request.DocumentNumber}{Environment.NewLine}" +
                            $"Name: {request.NameOnAccount}{Environment.NewLine}" +
                            $"Account Number: {request.PartnerAccountNumber}{Environment.NewLine}" +
                            $"Processor: {request.Processor.ToUpper()}{Environment.NewLine}" +
                            $"{identifier}: {identifierValue}{Environment.NewLine}" +
                            $"Requested Amount: {request.Amount:F}{Environment.NewLine}{Environment.NewLine}" +
                            $"Exception: {ex}{Environment.NewLine}";

            //Email.SendEmail(new MailAddress(Definitions.Emails.DPG),
            //    Definitions.Emails.Unknown,
            //    $"[{Settings.ActiveEnvironment}] DPG Exception for Account {request.PartnerAccountNumber}",
            //    emailBody);

        }

        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var normalized = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            var result = new string(normalized.Where(c => c >= 'A' && c <= 'Z' || (c >= 'a' && c <= 'z') || char.IsWhiteSpace(c) || c == '-' || char.IsDigit(c)).ToArray());

            if (input != result)
                Logger.Info($"Sanitize Input: {input} Normalized: {normalized} Sanitized: {result}");

            return result;
        }
    }
}