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
            public const string Paginated = Prefix + "/Paginated";


        }

        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "Department";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/id";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";

            public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";


        }

        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule + "User";
            public const string GetById = Prefix + "/id";
            public const string Create = Prefix + "/Create";
            public const string Delete = Prefix + "/{id}";
            public const string ChangePassword = Prefix + "/Change-Password";

            public const string Paginated = Prefix + "/Paginated";

            public const string Edit = Prefix + "/Edit";

        }

        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/Refresh-Token";
            public const string ValidateToken = Prefix + "/Validate-Token";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";


        }

        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "AuthorizationRouting";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";

            public const string Create = Prefix + "/Role/Create";

            public const string ManageUserRoles = Roles + "/ManageUserRoles/{userid}";
            public const string ManageUserClaims = Claims + "/ManageUserClaims/{userid}";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";



        }
        public static class EmailsRoute
        {
            public const string Prefix = Rule + "EmailsRoute";
            public const string SendEmail = Prefix + "/SendEmail";




        }
        public static class InstructorRouting
        {
            public const string Prefix = Rule + "InstructorRouting";
            public const string GetSalarySummationOfInstructor = Prefix + "/Salary-Summation-Of-Instructor";
            public const string AddInstructor = Prefix + "/Create";
        }
    }
}
