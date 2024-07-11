using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 在庫
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// 品番名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 在庫個別
        /// </summary>
        public IEnumerable<InventoryItem> Items { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        public decimal TotalStock { get; set; }
    }

    /// <summary>
    /// 在庫個別
    /// </summary>
    public class InventoryItem
    {
        /// <summary>
        /// 拠点名
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        public decimal Stock { get; set; }
    }
}
