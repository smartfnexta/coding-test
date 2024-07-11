using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 発注ヘッダ
    /// </summary>
    public class PurchaseOrderHeader
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public long CompanyId { get; set; }
    }
}
