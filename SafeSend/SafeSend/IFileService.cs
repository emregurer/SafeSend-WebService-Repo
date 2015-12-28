using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace SafeSend
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFileService" in both code and config file together.
    [ServiceContract]
    public interface IFileService
    {
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int TransferFile(int SenderId, string Phone, string Data, int Level, byte[] FileData);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Boolean UpdateTransferStatus(int TransferId);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Package DownloadFile(int TransferId);
    }
}
