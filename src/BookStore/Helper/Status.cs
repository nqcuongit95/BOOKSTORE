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

    public static class Content
    {
        public static string NewInvoice = "Đơn hàng mới";
        public static string NewCustomer = "Thêm khách hàng mới ";
        public static string ReturnProduct = "Khách hàng trả hàng";
        public static string ImportProduct = "Nhập hàng vào kho";
        public static string ReceiptVoucher = "Thu nợ khách hàng";
    }
}
