using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSend
{
    public class NotificationItem
    {
        public int TransferId { get; set; }

        public string Explanation { get; set; }
        public NotificationItem()
        { }
    }
}