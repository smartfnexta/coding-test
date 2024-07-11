using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace coding_test_qa_api.App.Modules
{
    /// <summary>
    /// DIコンテナ登録
    /// </summary>
    public static class DIComponentRegister
    {
        /// <summary>
        /// DIComponentAttributeを有するインターフェースをDIContainerに追加します
        /// </summary>
        /// <param name="serviceCollection">サービスコレクション</param>
        /// <param name="assembly">対象のアセンブリ</param>
        /// <remarks>
        /// 1. <paramref name="assembly"/>が有するTypeのうち、実体クラスのみを取得する
        /// 2. 上記１の中で <see cref="DIComponentAttribute"/> を直接有しているインターフェースを取得する
        /// 3. 上記２と実体クラスのデータ型をDIコンテナに登録する
        /// 4. 上記１の中で <see cref="DIControllerAttribute"/> を有するクラスをDIコンテナに登録する
        /// </remarks>
        public static void Regist(IServiceCollection serviceCollection, Assembly assembly)
        {
            var classTypes = assembly.GetTypes().Where(x => !x.IsInterface && !x.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.GetInterfaces();
                var registerType = interfaces.FirstOrDefault(x => null != x.GetCustomAttribute(typeof(DIComponentAttribute), false));

                if (registerType is null)
                {
                    continue;
                }

                var containerType = registerType.GetCustomAttribute<DIComponentAttribute>();

                if (containerType is null)
                {
                    continue;
                }

                switch (containerType.Type)
                {
                    case DIContainerType.Scope:
                        serviceCollection.AddScoped(registerType, type);
                        break;
                    case DIContainerType.Singleton:
                        serviceCollection.AddSingleton(registerType, type);
                        break;
                    case DIContainerType.Transient:
                    default:
                        serviceCollection.AddTransient(registerType, type);
                        break;
                }
            }

            //foreach (var type in classTypes)
            //{
            //    if (typeof(ControllerBase).IsAssignableFrom(type)
            //        || typeof(Controller).IsAssignableFrom(type))
            //    {
            //        serviceCollection.AddScoped(type);
            //    }
            //}
        }
    }
}
