using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace StrangeCSharpTricks.Firebase
{
    public class FireBaseProvider
    {
        private static FireBaseConfigInfo _configInfo = new FireBaseConfigInfo
        {

        };

        private static FirebaseClient _firebaseClient = null;

        private static Task<FirebaseClient> GetFireBaseClient()
        {
            return _firebaseClient != null ? Task.FromResult(_firebaseClient) : Connect();
        }

        private static async Task<FirebaseClient> Connect()
        {
            var authConfig = new FirebaseAuthConfig
            {
                ApiKey = _configInfo.ApiKey,
                AuthDomain = _configInfo.AuthDomain
            };
            var firebaseAuthClient = new FirebaseAuthClient(authConfig);
            var userCredential = await firebaseAuthClient.SignInAnonymouslyAsync();
            var token = await userCredential.User.GetIdTokenAsync();

            _firebaseClient = new FirebaseClient(
                _configInfo.BaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(token)
                });

            return _firebaseClient;
        }

        public static async Task SendMessage(MessageModel message)
        {
            var firebaseClient = await GetFireBaseClient();
            ChildQuery userMessages = firebaseClient.Child($"{message.ToUserId}/messages");

            await userMessages.PostAsync(message);
        }

        public static async Task DeleteMessages(int userId)
        {
            var firebaseClient = await GetFireBaseClient();
            ChildQuery userMessages = firebaseClient.Child($"{userId}/messages");
            await userMessages.DeleteAsync();
        }

    }
}