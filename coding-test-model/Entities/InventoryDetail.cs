using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 在庫詳細
    /// </summary>
    public class InventoryDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 製品ID
        /// </summary>
        public long InventoryHeaderId { get; set; }

        /// <summary>
        /// 拠点ID
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        public decimal StockQuantity { get; set; }
    }
}
