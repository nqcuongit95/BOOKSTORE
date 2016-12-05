using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class MenuElements
    {
        public string Action;
        public string Controller;
    }

    public class Menu : Dictionary<string, MenuElements>
    {
        public string SelectedKey;
    }

    public class HangHoaMenu : Menu
    {
        public HangHoaMenu(string selectedKey)
        {
            Add("HangHoa", new MenuElements
            {
                Action = "Index",
                Controller = "HangHoa"
            });

            Add("NhaCungCap", new MenuElements
            {
                Action = "Index",
                Controller = "NhaCungCap"
            });

            Add("NhanHieu", new MenuElements
            {
                Action = "Index",
                Controller = "NhanHieu"
            });

            SelectedKey = selectedKey;
        }
    }

    #region NhaCungCap
    public class DefaultHeaderActionMenu : Menu
    {
        public DefaultHeaderActionMenu(string selectedKey, string controller)
        {
            Add("Index", new MenuElements
            {
                Action = "Index",
                Controller = controller
            });
            Add("Create", new MenuElements
            {
                Action = "Create",
                Controller = controller
            });

            SelectedKey = selectedKey;
        }
    }
    #endregion
}
