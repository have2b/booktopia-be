using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

[Table("goods_receipts")]
public class GoodsReceipt
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int GoodsReceiptId { get; set; }
    [ForeignKey("PublisherId")] public int PublisherId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    [JsonIgnore] public virtual Publisher? Publisher { get; set; }
    [JsonIgnore] public virtual User? User { get; set; }

    [JsonIgnore]
    public virtual ICollection<GoodsReceiptDetail>? GoodsReceiptDetails { get; set; } = new List<GoodsReceiptDetail>();
}