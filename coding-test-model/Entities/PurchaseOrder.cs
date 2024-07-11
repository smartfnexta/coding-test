namespace coding_test_model.Entities
{
    /// <summary>
    /// 発注
    /// </summary>
    public class PurchaseOrder
    {
        /// 会社ID
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 品番名
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 個数
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Price { get; set; }
    }
}
