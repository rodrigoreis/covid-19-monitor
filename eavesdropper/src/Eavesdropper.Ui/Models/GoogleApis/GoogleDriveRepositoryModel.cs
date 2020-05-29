using System.Collections.Generic;
using System.Linq;
using Google.Apis.Drive.v3;

namespace Eavesdropper.Ui.Models.GoogleApis
{
    internal class GoogleDriveRepositoryModel : IGoogleDriveRepositoryModel
    {
        private const string TakeoutQuery = "name contains 'takeout'";
        
        private readonly FilesResource _filesResource;

        public GoogleDriveRepositoryModel(IDriveServiceFactory driveServiceFactory)
        {
            _filesResource = driveServiceFactory.Build().Files;
        }

        public IEnumerable<TakeoutFileModel> ListTakeoutFiles()
        {
            var request = _filesResource.List();
            request.Q = TakeoutQuery;

            var response = request.Execute();
            return response.Files.Select(f =>
                new TakeoutFileModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    MimeType = f.MimeType
                }
            );
        }
    }
}