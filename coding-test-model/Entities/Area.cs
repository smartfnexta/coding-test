using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 拠点
    /// </summary>
    public class Area
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 拠点名
        /// </summary>
        public string Name { get; set; }
    }
}
