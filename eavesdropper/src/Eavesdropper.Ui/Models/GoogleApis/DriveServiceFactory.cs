using System.IO;
using System.Reflection;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Eavesdropper.Ui.Models.GoogleApis
{
    internal class DriveServiceFactory : IDriveServiceFactory
    {
        private const string CredentialsFileName = "credentials.json";
        private const string UserCredentialsFolder = "GoogleDriveApiTokens";
        private const string User = "user";
        private const string ApplicationName = "Monitor Covid-19";
        private const string ClosePageResponse = "A aplicação foi autorizada. Esta janela deve ser fechada.";

        private static readonly string[] Scopes = { DriveService.Scope.DriveReadonly };
        
        private static string GetFilePathFromBinFolder(string fileName)
        {
            var binFolder = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            
            return Path.Combine(binFolder, fileName);
        }

        public DriveService Build()
        {
            using var stream = new FileStream(
                GetFilePathFromBinFolder(CredentialsFileName),
                FileMode.Open,
                FileAccess.Read
            );

            var userCredential = GoogleWebAuthorizationBroker
                .AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    User,
                    CancellationToken.None,
                    new FileDataStore(GetFilePathFromBinFolder(UserCredentialsFolder), true),
                    new LocalServerCodeReceiver(ClosePageResponse)
                ).Result;

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });
        }
    }
}