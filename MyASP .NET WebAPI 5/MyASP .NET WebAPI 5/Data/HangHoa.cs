using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyASP_.NET_WebAPI_5.Data
{
    [Table("HangHoa")]
    public class HangHoa 
    {
        [Key]
        public Guid MaHh { set; get; }
        
        [Required]
        [MaxLength(100)]
        public string TenHh { set; get; }  
        

        public string MoTa { set; get; }
        
        [Range(0, double.MaxValue)]
        public string DonGia{ set; get; }  
        
        public byte GiamGia{ set; get; }

        public int? MaLoai { get; set; }

        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }

        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        public HangHoa()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }

    }
}
