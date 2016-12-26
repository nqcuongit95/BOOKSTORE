using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class ChiTietPhieuKiemKho
    {
        public int Id { get; set; }
        public int PhieuKiemKhoId { get; set; }

        [Display(Name = "HangHoa",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int HangHoaId { get; set; }

        [Display(Name = "TonKho",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public int TonKho { get; set; }

        [Display(Name = "TonKhoThucTe",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public int TonKhoThucTe { get; set; }

        [Display(Name = "LyDo",
            ResourceType = typeof(Resources.DataAnnotations))]
        [MaxLength(100, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string LyDo { get; set; }

        public virtual HangHoa HangHoa { get; set; }
        public virtual PhieuKiemKho PhieuKiemKho { get; set; }
    }
}
