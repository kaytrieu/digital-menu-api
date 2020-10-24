using Firebase.Auth;
using Firebase.Storage;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalMenuApi.Service
{
    public static class FirebaseService
    {
        private static readonly string ApiKey = "AIzaSyA86T_fgdzpcnZHw2FfvKql_kFRzxEmlEg";
        private static readonly string Bucket = "swd-digitalmenu.appspot.com";
        private static readonly string AuthEmail = "digital-menu-firebase@gmail.com";
        private static readonly string AuthPassword = "digitalmenufirebase";

        public static async Task<string> UploadFileToFirebaseStorage(Stream stream, string filename)
        {
            string uploadedFileLink = string.Empty;

            // of course you can login using other method, not just email+password
            FirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            FirebaseAuthLink a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            CancellationTokenSource cancellation = new CancellationTokenSource();

            FirebaseStorageTask task = new FirebaseStorage(
               Bucket,
               new FirebaseStorageOptions
               {
                   AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                   ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
               })
               .Child("TemplateStorage").Child("UiLinkJsFile")
               .Child(filename)
               .PutAsync(stream, cancellation.Token);
            try
            {
                uploadedFileLink = await task;
            }
            catch
            {

            }
            return uploadedFileLink;
        }
    }
}
