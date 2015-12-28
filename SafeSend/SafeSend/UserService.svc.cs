using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace SafeSend
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehaviorAttribute(IncludeExceptionDetailInFaults = true, AddressFilterMode = AddressFilterMode.Any)]
    [DataContractFormat(Style = OperationFormatStyle.Document)]
    public class UserService : IUserService
    {
        public Boolean CheckUser(string Phone, string Email)
        {
            User _user = new User();
            _user.Phone = Phone;
            _user.Email = Email;
            return _user.CheckUser();
        }

        public Boolean Register(string Name, string Surname, string Email, string Password, string Phone, string UDID, string DeviceToken)
        {
            Phone = Phone.Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
            User _user = new User();
            _user.Name = Name;
            _user.Surname = Surname;
            _user.Email = Email;
            _user.Password = Password;
            _user.Phone = Phone;
            _user.UDID = UDID;
            _user.DeviceToken = DeviceToken;

            if(_user.CheckUser())
            {
                return _user.Insert();
            }
            else
            {
                return false;
            }
        }

        public Boolean UpdateUser(int UserId, string Name, string Surname)
        {
            User _user = new User(UserId);
            _user.Name = Name;
            _user.Surname = Surname;
            return _user.Update();
        }

        public User GetUserInfo(int UserId)
        {
            User _user = new User(UserId);
            return _user;
        }

        public int Signin(string Email, string Password)
        {
            User _user = new User();
            _user.Email = Email;
            _user.Password = Password;
            return _user.Authenticate();
        }

        public List<HistoryItem> GetHistory(int UserId)
        {
            User _user = new User(UserId);
            return _user.GetHistory();
        }

        public string GetNotifications(int UserId)
        {
            User _user = new User(UserId);
            return JsonConvert.SerializeObject(_user.GetNotifications());
        }

        public string GetContacts(int UserId, string PhoneList)
        {
            User _user = new User(UserId);
            return _user.GetContacts(PhoneList);
        }
    }
}
