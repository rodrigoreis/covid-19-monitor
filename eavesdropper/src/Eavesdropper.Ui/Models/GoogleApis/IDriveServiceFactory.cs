using Google.Apis.Drive.v3;

namespace Eavesdropper.Ui.Models.GoogleApis
{
    public interface IDriveServiceFactory
    {
        DriveService Build();
    }
}