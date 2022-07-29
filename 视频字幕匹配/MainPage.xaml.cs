using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.ApplicationModel.Resources;

using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

using VideosSubtitlesMatch.Models;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804
// 上介绍了“空白页”项模板

namespace VideosSubtitlesMatch
{
    /// <summary> 可用于自身或导航至 Frame 内部的空白页。 </summary>
    public sealed partial class MainPage : Page
    {
        private FIleList ZimuList;
        private FIleList VideoList;

        private MainPage mainPage;
        private ResourceLoader resourceLoader = new ResourceLoader();

        public MainPage()
        {
            mainPage = this;
            this.InitializeComponent();
        }

        private async void PickSubtitlesButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string[] types = { ".smi", ".srt", ".ass", ".ssa" };
            var files = await MyUWPLibrary.StorageItemPicker.PickMultiFilesAsync(PickerLocationId.Downloads, types);
            if (files.Count != 0)
            {
                string str1 = resourceLoader.GetString("part_11");
                string str2 = resourceLoader.GetString("part_12");

                ShowInfomationTextBlock.Text += $"\n" + str1 + files.Count + str2 + "\n";
                foreach (var file in files)
                {
                    ShowInfomationTextBlock.Text += $"\n\t\t{file.Name}";
                }

                ZimuList = new FIleList(files);
            }
        }

        private async void PickTargetVideosButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string[] types = { ".mp4", ".mkv", ".flv", ".rmvb" };
            var files = await MyUWPLibrary.StorageItemPicker.PickMultiFilesAsync(PickerLocationId.Desktop, types);
            if (files.Count != 0)
            {
                string str1 = resourceLoader.GetString("part_11");
                string str2 = resourceLoader.GetString("part_12");

                ShowInfomationTextBlock.Text += $"\n" + str1 + files.Count + str2 + "\n";
                foreach (var file in files)
                {
                    ShowInfomationTextBlock.Text += $"\n\t\t{file.Name}";
                }
                VideoList = new FIleList(files);
            }
        }

        private async void StartTransformButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string str = resourceLoader.GetString("StartTransformButton/Content");
            string finished = resourceLoader.GetString("Infomation_Finished");
            string repick = resourceLoader.GetString("Infomation_RePick");

            ShowInfomationTextBlock.Text += "\n" + str;
            if (ZimuList != null && VideoList != null)
            {
                if (await ZimuList.Rename(VideoList))
                {
                    ShowInfomationTextBlock.Text += "\n" + finished;
                }
                else
                    ShowInfomationTextBlock.Text += "\n" + repick;
            }
        }
    }
}