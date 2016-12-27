using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class MenuElement
    {
        public string Action;
        public string Controller;
    }

    public class SearchResult
    {
        public List<object> Results = new List<object>();
    }

    public class Menu : Dictionary<string, MenuElement>
    {
        public string SelectedKey;
    }

    public class HangHoaMenu : Menu
    {
        public HangHoaMenu(string selectedKey)
        {
            Add("HangHoa", new MenuElement
            {
                Action = "Index",
                Controller = "HangHoa"
            });

            Add("PhieuNhapHang", new MenuElement
            {
                Action = "Index",
                Controller = "PhieuNhapHang"
            });

            Add("PhieuChiNhapHang", new MenuElement
            {
                Action = "Index",
                Controller = "PhieuChiNhapHang"
            });

            Add("PhieuNhanHang", new MenuElement
            {
                Action = "Index",
                Controller = "PhieuNhanHang"
            });

            Add("PhieuKiemKho", new MenuElement
            {
                Action = "Index",
                Controller = "PhieuKiemKho"
            });

            Add("LoaiHangHoa", new MenuElement
            {
                Action = "Index",
                Controller = "LoaiHangHoa"
            });

            Add("NhaCungCap", new MenuElement
            {
                Action = "Index",
                Controller = "NhaCungCap"
            });

            Add("NhanHieu", new MenuElement
            {
                Action = "Index",
                Controller = "NhanHieu"
            });

            SelectedKey = selectedKey;
        }
    }

    public class DefaultHeaderActionMenu : Menu
    {
        public DefaultHeaderActionMenu(string selectedKey, string controller)
        {
            Add("Index", new MenuElement
            {
                Action = "Index",
                Controller = controller
            });
            Add("Create", new MenuElement
            {
                Action = "Create",
                Controller = controller
            });

            SelectedKey = selectedKey;
        }
    }

    public class PhieuChiNhapHangActionMenu : Menu
    {
        public PhieuChiNhapHangActionMenu(string selectedKey, string controller)
        {
            Add("Index", new MenuElement
            {
                Action = "Index",
                Controller = controller
            });

            SelectedKey = selectedKey;
        }
    }
}
