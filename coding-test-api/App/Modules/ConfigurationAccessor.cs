namespace coding_test_qa_api.App.Modules
{
    /// <summary>
    /// appsettings.json へのアクセッサのインターフェース
    /// </summary>
    [DIComponent]
    public interface IConfigurationAccessor
    {
        /// <summary>
        /// appsettings.json に設定した値を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        T? Get<T>(params string[] keys);
    }

    /// <summary>
    /// appsettings.json へのアクセッサ
    /// </summary>
    public class ConfigurationAccessor : IConfigurationAccessor
    {
        private IConfiguration configuration;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationAccessor()
        {
            this.configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        /// <inheritdoc/>
        public T? Get<T>(params string[] keys)
        {
            var key = string.Join(":", keys);
            return this.configuration.GetValue<T>(key);
        }
    }
}
