using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 拠点リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IAreaRepository : IRepositoryBase<Area>
    {
    }

    /// <summary>
    /// 拠点リポジトリ
    /// </summary>
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public AreaRepository(IDbSession dbSession) : base(dbSession)
        {
        }
    }
}
