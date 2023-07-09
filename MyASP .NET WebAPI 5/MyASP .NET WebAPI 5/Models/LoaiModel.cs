using System.ComponentModel.DataAnnotations;

namespace MyASP_.NET_WebAPI_5.Models
{
    public class LoaiModel
    {
        [Required]
        [MaxLength(50)]
        public string TenLoai { get; set; }
    }
}
