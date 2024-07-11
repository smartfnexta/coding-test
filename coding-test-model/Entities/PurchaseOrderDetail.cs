using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 発注明細
    /// </summary>
    public class PurchaseOrderDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// ヘッダID
        /// </summary>
        [Required]
        public long PurchaseOrderHeaderId { get; set; }

        /// <summary>
        /// アイテムID
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 発注数
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
