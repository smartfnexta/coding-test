using System.ComponentModel.DataAnnotations;

namespace coding_test_model.Entities
{
    /// <summary>
    /// 会社
    /// </summary>
    public class Company
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        public string Name { get; set; }
    }
}
