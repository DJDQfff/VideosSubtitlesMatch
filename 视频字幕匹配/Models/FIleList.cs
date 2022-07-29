using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;

using VideosSubtitlesMatch.ContentDialogPage;

namespace VideosSubtitlesMatch.Models
{
    internal class FIleList
    {
        private IReadOnlyList<StorageFile> storagelist { set; get; }

        public int Count => storagelist.Count;

        public async Task<bool> Rename(FIleList video)
        {
            if (Count != video.Count)
            {
                CountNotMatch countNotMatch = new CountNotMatch();
                await countNotMatch.ShowAsync();
                return false;
            }
            else
            {
                string extension = storagelist[0].FileType;
                for (int i = 0; i < Count; i++)
                {
                    await storagelist[i].RenameAsync(video[i].DisplayName + extension);
                }
                return true;
            }
        }

        public FIleList(IReadOnlyList<StorageFile> filelist)
        {
            this.storagelist = filelist;
            //GetFiles();
        }

        public StorageFile this[int i] => storagelist[i];
    }
}