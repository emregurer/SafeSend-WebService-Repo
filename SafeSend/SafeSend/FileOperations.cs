using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSend
{
    public class FileOperations
    {
        public static int TransferFile(int senderId, string phone, string data, int level, byte[] fileData)
        {
            SafeSendEntities db = new SafeSendEntities();
            Users user = db.Users.Where(x => x.Phone == phone).FirstOrDefault();
            if(user != null)
            {
                FileTransfers transfer = new FileTransfers();
                transfer.SenderId = senderId;
                transfer.ReceiverId = user.UserId;
                transfer.Status = 1;
                transfer.TransferDate = DateTime.Now;
                transfer.EncryptionLevel = level;
                transfer.TransferredData = data;
                transfer.FileData = fileData;
                db.FileTransfers.Add(transfer);
                db.SaveChanges();
                PushNotification(user.DeviceToken);
                return transfer.TransferId;
            }
            else
            {
                return -1;
            }
        }

        public static Boolean UpdateTransferStatus(int transferId)
        {
            SafeSendEntities db = new SafeSendEntities();
            FileTransfers transfer = db.FileTransfers.Where(x => x.TransferId == transferId).FirstOrDefault();
            if(transfer != null)
            {
                transfer.Status = 2;
                db.FileTransfers.Attach(transfer);
                db.Entry(transfer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Package DownloadFile(int transferId)
        {
            SafeSendEntities db = new SafeSendEntities();
            FileTransfers transfer = db.FileTransfers.Where(x => x.TransferId == transferId).FirstOrDefault();
            if(transfer != null)
            {
                Package package = new Package();
                package.EncryptionLevel = transfer.EncryptionLevel.Value;
                package.TransferId = transfer.TransferId;
                package.FileContent = transfer.TransferredData;
                package.FileData = transfer.FileData;
                return package;
            }
            else
            {
                return null;
            }
        }

        private static void PushNotification(string deviceToken)
        {

        }
    }
}