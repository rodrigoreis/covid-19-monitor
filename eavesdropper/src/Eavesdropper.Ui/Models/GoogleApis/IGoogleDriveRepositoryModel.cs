using System.Collections.Generic;

namespace Eavesdropper.Ui.Models.GoogleApis
{
    public interface IGoogleDriveRepositoryModel
    {
        IEnumerable<TakeoutFileModel> ListTakeoutFiles();
    }
}