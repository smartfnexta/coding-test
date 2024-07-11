namespace coding_test_qa_api.App.Modules
{
    public enum DIContainerType
    {
        Transient,
        Scope,
        Singleton
    }

    /// <summary>
    /// DIComponent属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class DIComponentAttribute : Attribute
    {
        public DIContainerType Type { get; set; }

        public DIComponentAttribute(DIContainerType type = DIContainerType.Transient)
        {
            this.Type = type;
        }
    }
}
