using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 受注
    /// </summary>
    public class ReceiveOrder
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 発注ヘッダのID
        /// </summary>
        public long? PurchaseOrderHeaderId { get; set; }

        /// <summary>
        /// 在庫ヘッダのID
        /// </summary>
        public long? InventoryHeaderId { get; set; }
    }
}
