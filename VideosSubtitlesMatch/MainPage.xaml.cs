namespace VideosSubtitlesMatch
{
    public sealed partial class MainPage : Page
    {
        private FIleList ZimuList;
        private FIleList VideoList;

        private MainPage mainPage;
        private ResourceLoader resourceLoader = new ResourceLoader();

        public MainPage ()
        {
            mainPage = this;
            InitializeComponent();
        }

        private async void PickSubtitlesButton_Click (object sender , RoutedEventArgs e)
        {
            string[] types = { ".smi" , ".srt" , ".ass" , ".ssa" };
            var picker = new FileOpenPicker();
            // Get the current window's HWND by passing a Window object
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle((App.Current as App).MainWindow);
            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(picker , hwnd);
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            foreach (var type in types)
            {
                picker.FileTypeFilter.Add(type);
            }
            var files = await picker.PickMultipleFilesAsync();
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

        private async void PickTargetVideosButton_Click (object sender , RoutedEventArgs e)
        {
            string[] types = { ".mp4" , ".mkv" , ".flv" , ".rmvb" };
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            foreach (var type in types)
            {
                picker.FileTypeFilter.Add(type);
            }
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle((App.Current as App).MainWindow);
            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(picker , hwnd);

            var files = await picker.PickMultipleFilesAsync();
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

        private async void StartTransformButton_Click (object sender , RoutedEventArgs e)
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