namespace TaskManagement.Shared
{
    //TODO: Se puede cambiar los valores de manera rápida
    public class ProjectConstants
    {
        public const string CONTENT_TYPE_APPLICATION_X_WWW_FORM = "application/x-www-form-urlencoded";
        public const string VERSION_HEADER = "api-version";
        public const string JWT_KEY_SECTION = "TokenSettinsg:JwtKey";
        public const string TIME_LIFE_SECTION = "TokenSettinsg:TimeLife";
        public const string CONTENT_TYPE_APPLICATION_ERROR = "application/problem+json";
        public const string BEARER = "Bearer";
        public const string USER_ID = "nameidentifier";
        #region "Tags"
        public const string TAG_TASK = "Task";
        public const string TAG_USER = "User";
        public const string TAG_TOKEN = "Token";
        #endregion"Tags"

        #region "Endpoints"
        public const string ENDPOINT_CREATE_TASK = "api/tasks";
        public const string ENDPOINT_GET_TASK = "api/tasks/{taskId}";
        public const string ENDPOINT_GET_TASKS = "api/tasks";
        public const string ENDPOINT_UPDATE_TASK = "api/tasks/{taskId}";
        public const string ENDPOINT_DELETE_TASK = "api/tasks/{taskId}";

        public const string ENDPOINT_CREATE_USER = "api/users";
        public const string ENDPOINT_GET_TOKEN = "api/token";
        public const string ENDPOINT_GET_USER = "api/users/{userId}";

        #endregion "Endpoints"

        #region "EndpointNames"
        public const string ENDPOINT_NAME_CREATE_TASK = "Create_Task";
        public const string ENDPOINT_NAME_GET_TASK = "GetTask";
        public const string ENDPOINT_NAME_GET_TASKS = "GetTasks";
        public const string ENDPOINT_NAME_UPDATE_TASK = "UpdateTask";
        public const string ENDPOINT_NAME_DELETE_TASK = "DeleteTask";

        public const string ENDPOINT_NAME_CREATE_USER = "CreateUser";
        public const string ENDPOINT_NAME_GET_USER = "GetUser";
        public const string ENDPOINT_NAME_GET_TOKEN = "GetToken";
        #endregion "EndpointNames"

        #region "Swagger"
        public const string STATUS_200_DESCRIPTION = "The request processed correctly";
        public const string STATUS_201_DESCRIPTION = "The request processed correctly (`A resource was created`)";
        public const string STATUS_204_DESCRIPTION = "The request processed correctly (`Doesn't return information`)";
        public const string STATUS_400_DESCRIPTION = "The request doesn't have the correct format";
        public const string STATUS_401_DESCRIPTION = "The request doesn't include a valid token to authentication";
        public const string STATUS_404_DESCRIPTION = "The requested resource doesn't exist";
        public const string STATUS_500_DESCRIPTION = "An internal error occurred while the request was being processing";

        public const string SUMMARY_CREATE_USER = "Create a new user";
        public const string SUMMARY_GET_USER = "Get user detail";
        public const string SUMMARY_GET_TOKEN = "Get JWT Token";
        public const string SUMMARY_CREATE_TASK = "";
        public const string SUMMARY_GET_TASK = "";
        public const string SUMMARY_GET_TASKS = "";
        public const string SUMMARY_UPDATE_TASK = "";
        public const string SUMMARY_DELETE_TASK = "";

        public const string DESCRIPTION_CREATE_USER = "Create a new user";
        public const string DESCRIPTION_GET_USER = "Get user detail";
        public const string DESCRIPTION_GET_TOKEN = "Get JWT Token";
        public const string DESCRIPTION_CREATE_TASK = "";
        public const string DESCRIPTION_GET_TASK = "";
        public const string DESCRIPTION_GET_TASKS = "";
        public const string DESCRIPTION_UPDATE_TASK = "";
        public const string DESCRIPTION_DELETE_TASK = "";
        #endregion "Swagger"

    }

}