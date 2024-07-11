using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 会社リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
    }

    /// <summary>
    /// 会社リポジトリ
    /// </summary>
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public CompanyRepository(IDbSession dbSession) : base(dbSession)
        {
        }
    }
}
