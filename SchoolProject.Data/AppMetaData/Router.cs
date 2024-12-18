namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string SingleRoute = "/{id}";
        public const string root = "Api";

        public const string version = "V1";

        public const string Rule = root + "/" + version + "/";


        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";

            public const string Delete = Prefix + "/{id}";


        }
    }
}
