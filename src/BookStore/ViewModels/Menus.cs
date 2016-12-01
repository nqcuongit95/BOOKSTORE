using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class MenuElements
    {
        public string Name;
        public string Action;
        public string Controller;
    }

    public static class HangHoaMenu
    {
        public static readonly Dictionary<string, MenuElements> Elements = new Dictionary<string, MenuElements>()
        {
            {
                "HangHoa", new MenuElements{
                Name ="Hàng hóa",
                Action = "Index",
                Controller="HangHoa"}
            },
            {
                "NhaCungCap", new MenuElements{
                Name ="Nhà cung cấp",
                Action = "Index",
                Controller="NhaCungCap"}
            },
            {
                "NhaHieu", new MenuElements{
                Name ="Nhãn hiệu",
                Action = "Index",
                Controller="NhaHieu"}
            }
        };
    }

    #region NhaCungCap
    public static class NhaCungCapActionMenu
    {
        public static readonly Dictionary<string, MenuElements> Elements = new Dictionary<string, MenuElements>()
        {
            {
                "Index", new MenuElements{
                Name ="Danh sách nhà cung cấp",
                Action = "Index",
                Controller="NhaCungCap"}
            },
            {
                "Create", new MenuElements{
                Name ="Thêm nhà cung cấp",
                Action = "Create",
                Controller="NhaCungCap"}
            }
        };
    }
    #endregion
}
