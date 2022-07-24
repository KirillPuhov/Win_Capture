using Domain.Models;
using System.Collections.ObjectModel;

namespace AppUi.Services
{
    public interface IRecentService
    {
        ObservableCollection<IOutFile> RecentFiles { get; }

        void AddRecentFile(IOutFile file);

        void RemoveRecentFile(IOutFile file);

        void ClearRecentList();

        void ClickToRecentFile();
    }
}
