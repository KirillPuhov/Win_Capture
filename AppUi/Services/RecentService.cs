using Domain.Models;
using System;
using System.Collections.ObjectModel;

namespace AppUi.Services
{
    public sealed class RecentService : IRecentService
    {
        private ObservableCollection<IOutFile> _recentFiles 
            = new ObservableCollection<IOutFile>();

        public ObservableCollection<IOutFile> RecentFiles =>
            _recentFiles;

        public void AddRecentFile(IOutFile file) =>
            _recentFiles.Add(file);

        public void RemoveRecentFile(IOutFile file) =>
            _recentFiles.Remove(file);

        public void ClearRecentList() =>
            _recentFiles.Clear();

        public void ClickToRecentFile()
        {
            throw new NotImplementedException();
        }
    }
}
