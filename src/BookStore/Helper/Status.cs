using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public static class Result
    {
        public static string Succeed = "Succeed";
        public static string Error = "Error";
    }

    public class Status
    {
        public string Type;
        public string Url;
        public string Details;
    }
}
