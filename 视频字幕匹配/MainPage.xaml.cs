using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.ApplicationModel.Resources;

using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

using SubtitlesMatch.Models;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace SubtitlesMatch
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FIleList ZimuList;
        private FIleList VideoList;

        private MainPage mainPage;
        private ResourceLoader resourceLoader = new ResourceLoader();

        public MainPage ()
        {
            mainPage = this;
            this.InitializeComponent();
        }

        private async void PickSubtitlesButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add(".smi");
            fileOpenPicker.FileTypeFilter.Add(".srt");
            fileOpenPicker.FileTypeFilter.Add(".ass");
            fileOpenPicker.FileTypeFilter.Add(".ssa");
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            fileOpenPicker.ViewMode = PickerViewMode.List;
            var files = await fileOpenPicker.PickMultipleFilesAsync();
            if (files.Count != 0)
            {
                string str1 = resourceLoader.GetString("part_11");
                string str2 = resourceLoader.GetString("part_12");

                ShowInfomationTextBlock.Text += $"\n"+str1+files.Count+str2+"\n";
                foreach (var file in files)
                {
                    ShowInfomationTextBlock.Text += $"\n\t\t{file.Name}";
                }

                ZimuList = new FIleList(files);
            }
        }

        private async void PickTargetVideosButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add(".mp4");
            fileOpenPicker.FileTypeFilter.Add(".mkv");
            fileOpenPicker.FileTypeFilter.Add(".flv");
            fileOpenPicker.FileTypeFilter.Add(".rmvb");

            fileOpenPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            fileOpenPicker.ViewMode = PickerViewMode.List;
            var files = await fileOpenPicker.PickMultipleFilesAsync();
            if (files.Count != 0)
            {
                string str1 = resourceLoader.GetString("part_11");
                string str2 = resourceLoader.GetString("part_12");

                ShowInfomationTextBlock.Text += $"\n"+str1+files.Count+str2 + "\n";
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