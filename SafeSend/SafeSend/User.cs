using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSend
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string UDID { get; set; }

        public string DeviceToken { get; set; }

        public User()
        { }

        public User(int userId)
        {
            this.UserId = userId;
            GetUserInfo();
        }

        public Boolean Insert()
        {
            SafeSendEntities db = new SafeSendEntities();
            Users _user = new Users();
            _user.Name = this.Name;
            _user.Surname = this.Surname;
            _user.Email = this.Email;
            _user.Password = this.Password;
            _user.Phone = this.Phone;
            _user.UDID = this.UDID;
            _user.DeviceToken = this.DeviceToken;
            db.Users.Add(_user);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public Boolean Update()
        {
            SafeSendEntities db = new SafeSendEntities();
            Users _user = db.Users.Where(x => x.UserId == this.UserId).First();
            if (_user != null)
            {
                _user.Name = this.Name;
                _user.Surname = this.Surname;
                db.Users.Attach(_user);
                db.Entry(_user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean CheckUser()
        {
            SafeSendEntities db = new SafeSendEntities();
            if (db.Users.Where(x => x.Phone == this.Phone || x.Email == this.Email).Count() > 0)
            {
                return false;
            }
            return true;
        }

        public void GetUserInfo()
        {
            SafeSendEntities db = new SafeSendEntities();
            Users _user = db.Users.Where(x => x.UserId == this.UserId).FirstOrDefault();
            if(_user != null)
            {
                this.Name = _user.Name;
                this.Surname = _user.Surname;
                this.Email = _user.Email;
                this.Password = _user.Password;
                this.Phone = _user.Phone;
                this.UDID = _user.UDID;
                this.DeviceToken = _user.DeviceToken;
            }
        }

        public int Authenticate()
        {
            SafeSendEntities db = new SafeSendEntities();
            if (db.Users.Where(x => x.Email == this.Email && x.Password == this.Password).Count() > 0)
            {
                return db.Users.Where(x => x.Email == this.Email && x.Password == this.Password).First().UserId;
            }

            return -1;
        }

        public List<HistoryItem> GetHistory()
        {
            SafeSendEntities db = new SafeSendEntities();
            List<HistoryItem> history = new List<HistoryItem>();
            var list = db.FileTransfers.Where(x => x.SenderId == this.UserId || x.ReceiverId == this.UserId).ToList().OrderByDescending(x=> x.TransferDate);
            foreach (var item in list)
            {
                HistoryItem historyItem = new HistoryItem(item.TransferId, this.UserId);
                history.Add(historyItem);
            }
            return history;
        }

        public List<NotificationItem> GetNotifications()
        {
            SafeSendEntities db = new SafeSendEntities();
            List<NotificationItem> notifications = new List<NotificationItem>();
            var list = from f in db.FileTransfers
                       join u in db.Users on f.SenderId equals u.UserId
                       where f.ReceiverId == this.UserId && f.Status == 1
                       select new { f.TransferId, u.Name, u.Surname };
            foreach (var item in list)
            {
                NotificationItem notificationItem = new NotificationItem();
                notificationItem.TransferId = item.TransferId;
                notificationItem.Explanation = "You have an incoming file from " + item.Name + " " + item.Surname;
                notifications.Add(notificationItem);
            }

            return notifications;
        }

        public string GetContacts(string phoneListStr)
        {
            string returnStr = "";
            SafeSendEntities db = new SafeSendEntities();
            string[] tmp = phoneListStr.Split(';');
            List<ContactItem> contactList = new List<ContactItem>();
            for (int i = 0; i < tmp.Length; i++)
            {
                if(!string.IsNullOrEmpty(tmp[i]))
                {
                    string phone = tmp[i];
                    if (db.Users.Where(x => x.Phone == phone && x.UserId != UserId).Count() > 0)
                    {
                        returnStr = returnStr + phone + ";";
                    }
                }
            }

            return returnStr;
        }
    }
}