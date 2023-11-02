using System.ComponentModel.DataAnnotations;
namespace Cart.Models
{
    public class Carts
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "cart id 不能为空")]
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public decimal? ProductPrice { get; set; }//可空decimal变量
        public int? userId { get; set; }
        [Display(Name = "添加日期")]
        [DataType(DataType.Date)]// DataType.Date表示日期格式类型，作用：用户无需在该字段中输入时间信息，显示时也仅显示日期。
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}

