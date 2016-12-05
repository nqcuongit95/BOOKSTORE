using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public static class MessageType
    {
        public static readonly string Success = "success";
        public static readonly string Error = "error";
        public static readonly string Warning = "warning";
    }

    public class Message
    {
        public string Type= MessageType.Error;
        public string Header;
        public string Content;
        public Dictionary<string, object> Results = new Dictionary<string, object>();
    }
}
