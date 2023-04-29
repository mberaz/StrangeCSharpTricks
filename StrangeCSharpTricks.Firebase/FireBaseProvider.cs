using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;

namespace StrangeCSharpTricks.Firebase
{
    public class FireBaseProvider
    {
        //https://github.com/step-up-labs/firebase-database-dotnet
        private static readonly FireBaseConfigInfo _configInfo = new FireBaseConfigInfo
        {
            
        };

        private static FirebaseClient _fireBaseClient = null;

        private static Task<FirebaseClient> GetFireBaseClient()
        {
            return _fireBaseClient != null ? Task.FromResult(_fireBaseClient) : Connect();
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

            _fireBaseClient = new FirebaseClient(
                _configInfo.BaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(token)
                });

            return _fireBaseClient;
        }

        public static async Task<string> Post<T>(T message, string path)
        {
            var fireBaseClient = await GetFireBaseClient();
            var userMessages = fireBaseClient.Child(path);

            var response = await userMessages.PostAsync(message);
            return response.Key;
        }

        public static async Task Delete(string path)
        {
            var fireBaseClient = await GetFireBaseClient();
            var userMessages = fireBaseClient.Child(path);
            await userMessages.DeleteAsync();
        }

        public static async Task<T> Get<T>(string path) where T : class
        {
            var fireBaseClient = await GetFireBaseClient();
            var userMessages = fireBaseClient.Child(path);
            return await userMessages.OnceSingleAsync<T>();
        }


        public static async Task<List<FirebaseObject<T>>> GetList<T>(string path) where T : class
        {
            var fireBaseClient = await GetFireBaseClient();
            var userMessages = fireBaseClient.Child(path);

            var objects = await userMessages.OnceAsync<T>();
            return objects.ToList();
        }

        public static async Task Observ<T>(string path,Action<FirebaseEvent<T>> onNext)
        {
            var fireBaseClient = await GetFireBaseClient();
            var observable = fireBaseClient
                .Child(path)
                .AsObservable<T>()
                .Subscribe(onNext);
        }
 
    }
}