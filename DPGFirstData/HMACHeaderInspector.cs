using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using NLog;

namespace DaystarPaymentGateway
{
    internal class HMACHeaderInspector : IClientMessageInspector
    {
        private static readonly Logger Logger = LogManager.GetLogger("NLogWriter");

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Get the message, save a copy of it
            var buffer = request.CreateBufferedCopy(int.MaxValue);
            request = buffer.CreateMessage();
            var message = buffer.CreateMessage();
            var encoder = new ASCIIEncoding();

            // Convert message to XML
            var sb = new StringBuilder();
            var xmlWriter = XmlWriter.Create(sb, new XmlWriterSettings
                                                    {
                                                        OmitXmlDeclaration = true
                                                    });
            var writer = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter);
            message.WriteStartEnvelope(writer);
            message.WriteStartBody(writer);
            message.WriteBodyContents(writer);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            writer.Flush();

            // Eliminate unnecessary tag spaces
            var body = sb.ToString().Replace(" />", "/>");

            var xmlByte = encoder.GetBytes(body);
            var sha1Engine = new SHA1CryptoServiceProvider();

            // Assign values to hashing and header variables

            // HMAC sha1 hash with key + hash_data
            HMAC hmacSha1 = new HMACSHA1(Encoding.UTF8.GetBytes(FirstDataAccess.TestMode ? Constants.TestHMACKey : Constants.HMACKey));

            var hashData = $"{Constants.HMAC.Post}\n" +
                           $"{Constants.HMAC.SOAP}\n" +
                           $"{BitConverter.ToString(sha1Engine.ComputeHash(xmlByte)).Replace("-", "").ToLower()}\n" +
                           $"{DateTime.UtcNow.ToString(Constants.HMAC.DateFormat)}\n" +
                           $"{Constants.HMAC.URI}";

            var hmacData = hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(hashData)); //data
            var base64Hash = Convert.ToBase64String(hmacData);
            var authorizationContent = $"{Constants.HMAC.AuthorizationPrefix} {(FirstDataAccess.TestMode ? Constants.TestHMACKeyID : Constants.HMACKeyID)}:{base64Hash}";

            HttpRequestMessageProperty httpRequestMessage;
            var second = false;

            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out var httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                second = true;
            }

            // ReSharper disable once PossibleNullReferenceException
            httpRequestMessage.Headers[Constants.ContentType] = Constants.HMAC.SOAP;
            httpRequestMessage.Headers[Constants.ContentSHA1] = BitConverter.ToString(sha1Engine.ComputeHash(xmlByte)).Replace("-", "").ToLower();
            httpRequestMessage.Headers[Constants.Date] = DateTime.UtcNow.ToString(Constants.HMAC.DateFormat);
            httpRequestMessage.Headers[Constants.Authorization] = authorizationContent;

            if (second)
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);

            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}