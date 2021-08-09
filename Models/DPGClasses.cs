using DaystarPaymentGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DaystarPaymentGateway2.Models
{
    [DataContract]
    public class Response : IExtensibleDataObject
    {
        public Response() { }

        public Response(long id) { ID = id; }

        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public ResponseCode ResponseCode { get; set; }

        [DataMember]
        public string ApprovedAmount { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string GatewayID { get; set; }

        [DataMember]
        public bool Approved
        {
            get => ResponseCode == ResponseCode.Approved || ResponseCode == ResponseCode.ApprovedPartial;
            private set => throw new NotImplementedException();
        }

        [DataMember]
        public bool Error
        {
            get => (int)ResponseCode >= 2000;
            private set => throw new NotImplementedException();
        }

        [NonSerialized]
        private ExtensionDataObject extensionData;

        public ExtensionDataObject ExtensionData
        {
            get => extensionData;
            set => extensionData = value;
        }

        public string ToJSON()
        {
            return Utility.ConvertToJSON(this);
        }
    }
}
