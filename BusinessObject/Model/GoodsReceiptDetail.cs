using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("goods_receipt_details")]
public class GoodsReceiptDetail
{
    [ForeignKey("ProductId")] public int ProductId { get; set; }
    [ForeignKey("GoodsReceiptId")] public int GoodsReceiptId { get; set; }
    [Required] public int Quantity { get; set; }
    [Required, Precision(4, 2)] public decimal CostPrice { get; set; }
    
    [JsonIgnore] public virtual Product? Product { get; set; }
    [JsonIgnore] public virtual GoodsReceipt? GoodsReceipt { get; set; }
}