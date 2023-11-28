using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VideosSubtitlesMatch.ContentDialogPage;

using Windows.Storage;

namespace VideosSubtitlesMatch.Models
{
    internal class FIleList
    {
        private IReadOnlyList<StorageFile> storagelist { set; get; }

        public int Count => storagelist.Count;

        public async Task<bool> Rename (FIleList video)
        {
            if (Count != video.Count)
            {
                CountNotMatch countNotMatch = new CountNotMatch();
                countNotMatch.XamlRoot = ((App.Current as App).MainWindow.Content as MainPage).XamlRoot;
                await countNotMatch.ShowAsync();
                return false;
            }
            else
            {
                string extension = storagelist[0].FileType;
                for (int i = 0 ; i < Count ; i++)
                {
                    await storagelist[i].RenameAsync(video[i].DisplayName + extension);
                }
                return true;
            }
        }

        public FIleList (IReadOnlyList<StorageFile> filelist)
        {
            storagelist = filelist;
            //GetFiles();
        }

        public StorageFile this[int i] => storagelist[i];
    }
}