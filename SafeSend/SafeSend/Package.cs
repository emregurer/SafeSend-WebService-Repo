using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSend
{
    public class Package
    {
        public int TransferId { get; set; }

        public int EncryptionLevel { get; set; }

        public string FileContent { get; set; }

        public byte[] FileData { get; set; }

        public Package()
        { }
    }
}