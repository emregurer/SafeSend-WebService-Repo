using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSend
{
    public class HistoryItem
    {
        public int TransferId { get; set; }

        public DateTime TransferDate { get; set; }

        public string SenderName { get; set; }

        public string ReceiverName { get; set; }

        public int TransferDirection { get; set; }

        public int Status { get; set; }
        
        public HistoryItem()
        { }

        public HistoryItem(int transferId, int userId)
        {
            SafeSendEntities db = new SafeSendEntities();
            var item = from f in db.FileTransfers
                       join u1 in db.Users on f.SenderId equals u1.UserId
                       join u2 in db.Users on f.ReceiverId equals u2.UserId
                       where f.TransferId == transferId
                       select new { 
                           f.TransferId, 
                           f.TransferDate, 
                           SenderName = u1.Name + " " + u1.Surname,
                           ReceiverName = u2.Name + " " + u2.Surname,
                           f.SenderId,
                           f.ReceiverId,
                           f.Status
                       };

            this.TransferId = item.First().TransferId;
            this.TransferDate = item.First().TransferDate.Value;
            this.SenderName = item.First().SenderName;
            this.ReceiverName = item.First().ReceiverName;
            this.Status = item.First().Status.Value;
            if(userId == item.First().SenderId)
            {
                this.TransferDirection = 1;
            }
            else
            {
                this.TransferDirection = 2;
            }
        }
    }
}