using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 品番
    /// </summary>
    public class Item
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 品番名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
