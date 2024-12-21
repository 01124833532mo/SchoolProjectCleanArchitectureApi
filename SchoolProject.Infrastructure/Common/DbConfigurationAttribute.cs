namespace SchoolProject.Infrastructure.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    class DbConfigurationAttribute : Attribute
    {
        public Type Dbcontext { get; set; }

        public DbConfigurationAttribute(Type dbcontext)
        {

            Dbcontext = dbcontext;
        }

    }
}
