// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

using Microsoft.UI.Xaml.Controls;

namespace VideosSubtitlesMatch.ContentDialogPage
{
    public sealed partial class CountNotMatch : ContentDialog
    {
        public CountNotMatch ()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_CloseButtonClick (ContentDialog sender , ContentDialogButtonClickEventArgs args)
        {
        }
    }
}