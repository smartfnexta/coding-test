using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 在庫ヘッダ
    /// </summary>
    public class InventoryHeader
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 製品ID
        /// </summary>
        public long ItemId { get; set; }
    }
}
