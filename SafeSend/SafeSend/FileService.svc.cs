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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FileService.svc or FileService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehaviorAttribute(IncludeExceptionDetailInFaults = true, AddressFilterMode = AddressFilterMode.Any)]
    [DataContractFormat(Style = OperationFormatStyle.Document)]
    public class FileService : IFileService
    {
        public int TransferFile(int SenderId, string Phone, string Data, int Level, byte[] FileData)
        {
            return FileOperations.TransferFile(SenderId, Phone, Data, Level, FileData);
        }

        public Boolean UpdateTransferStatus(int TransferId)
        {
            return FileOperations.UpdateTransferStatus(TransferId);
        }

        public Package DownloadFile(int TransferId)
        {
            Package package = FileOperations.DownloadFile(TransferId);
            return package;
        }
    }
}
