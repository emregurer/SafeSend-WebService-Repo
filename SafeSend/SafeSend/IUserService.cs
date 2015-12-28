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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Boolean CheckUser(string Phone, string Email);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Boolean Register(string Name, string Surname, string Email, string Password, string Phone, string UDID, string DeviceToken);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Boolean UpdateUser(int UserId, string Name, string Surname);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        User GetUserInfo(int UserId);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int Signin(string Email, string Password);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<HistoryItem> GetHistory(int UserId);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string GetNotifications(int UserId);

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string GetContacts(int UserId, string PhoneList);
    }
}
