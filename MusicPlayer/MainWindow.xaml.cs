using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace MusicPlayer

{
    


    public partial class MainWindow : Window
    {
        

        private MediaPlayer Media = new MediaPlayer();
        private  OpenFileDialog open = new OpenFileDialog();
        DispatcherTimer timer = new DispatcherTimer();
        ImageBrush Main = new ImageBrush();
        string StrPathAddMusic;
        bool MusicHasPause = false;
        string StopDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Asset\\Music.png";
        bool RepeatOnce = true;
        int AddMusicCounter = 1;
        // int Total_Second;
        List<string> AddMusic_List = new List<string>();
        double Volume_Kepper = 0;
        MaterialDesignThemes.Wpf.PackIconKind SoundIcon_Kepper;
        //MediaPlayer P = new MediaPlayer();
        //string Second, Minute;
        
        public MainWindow()
        {
            
            InitializeComponent();
            Media.Volume = 0.5;
            StartApp();
            
           
          
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

            PlayPause();

            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




     


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
         
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

            DragMove();

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            open.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
    
            MusicHasPause = false;
            if (open.ShowDialog() == true)
            {

                Media.Stop();
                SliderVolume.Value = 0;
                Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                
                
                
                timer.Start();

               var image= SetCoverMusic();
                MainImage.Fill = image;
                Media.Open(new Uri(open.FileName, UriKind.Absolute));
                Media.MediaOpened += Media_MediaOpened;
                Media.Play();


            }

        }

        private void Media_MediaOpened(object sender, EventArgs e)
        {
            if (Media.NaturalDuration.HasTimeSpan)
            {

                SliderVolume.Maximum = Media.NaturalDuration.TimeSpan.TotalSeconds;

            }

        }

        



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void StopButton_Click(object sender, RoutedEventArgs e)
        {



            Main.ImageSource = new BitmapImage(new Uri(@StopDirectory));
            MainImage.Fill = Main;
            timer.Start();

            MusicHasPause = false;
            TimeText.Text = "00:00 / 00:00";
            Media.Close();
            timer.Stop();
            Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            
         
                
            
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void timer_Tick(object sender, EventArgs e)
        {
            if (Media.Source != null)
            {
                try
                {
                    TimeText.Text = String.Format("{0} / {1}", Media.Position.ToString(@"mm\:ss"), Media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                    SliderVolume.Value += 1;
                }
                catch
                {
                    /// For Overtake Stupid Bug :)
                }
            }
        }

      



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void AddList_Click(object sender, RoutedEventArgs e)
        {

            AddList_Function();
           
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void AddList_Function()
        {
          
            Ellipse AddMusicPicture_List = new Ellipse();
            ImageBrush AddListBrush = new ImageBrush();
            TextBlock MusicNumber = new TextBlock();
            TextBlock TextBl = new TextBlock();
           //  TextBlock MusicDuration = new TextBlock();
            
            StackPanel S = new StackPanel();
            
            AddMusicPicture_List.Margin = new Thickness(20.0);
            AddMusicPicture_List.Height = 40;
            AddMusicPicture_List.Width = 40;
            AddMusicPicture_List.VerticalAlignment = VerticalAlignment.Center;
            // Start IF ..................
            if (open.ShowDialog() == true)
            {

                MusicList.Items.Remove(AddList_Help_Text);
                var v = SetCoverMusic();
                AddMusicPicture_List.Fill = v;
                MusicNumber.Text = AddMusicCounter.ToString();

                MusicNumber.VerticalAlignment = VerticalAlignment.Center;


                StrPathAddMusic = System.IO.Path.GetFileNameWithoutExtension(open.FileName);
                
                TextBl.Text = StrPathAddMusic;
                
                TextBl.VerticalAlignment = VerticalAlignment.Center;
                TextBl.Margin = new Thickness(7, 7, 0, 0);
                TextBl.Width = 100;
                TextBl.TextTrimming = TextTrimming.CharacterEllipsis;
                AddMusic_List.Add(open.FileName);
                TextBl.Uid = MusicNumber.Text;

             
                //MusicDuration.VerticalAlignment = VerticalAlignment.Center;
                //MusicDuration.Margin = new Thickness(7);
                
               // MusicDuration.Text = Minute + " : " + Second;
                
                S.Height = 78;
                S.Orientation = Orientation.Horizontal;
                S.Children.Add(MusicNumber);
                S.Children.Add(AddMusicPicture_List);
                S.Children.Add(TextBl);
               // S.Children.Add(MusicDuration);
                MusicList.Items.Add(S);

                AddMusicCounter = AddMusicCounter + 1;
                MusicList.IsEnabled = true;
            }
            // END IF     ......................
        }

   

        /// //////////////////////////////////////////////////////////////////////////////////////
        // private void P_MediaOpened(object sender, EventArgs e)
        //{
        // Total_Second= Convert.ToInt16( P.NaturalDuration.TimeSpan.TotalSeconds);
        // }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (RepeatIcon.Kind==MaterialDesignThemes.Wpf.PackIconKind.RepeatOff)
            {
                RepeatIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.RepeatOnce;
                

                Media.MediaEnded += Media_MediaEnded;
            }
          else  if (RepeatIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.RepeatOnce)
            {
                RepeatIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Repeat;
                
                Media.MediaEnded += Media_MediaEnded;
            }
           else if (RepeatIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Repeat)
            {
            
                RepeatIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.RepeatOff;
                
                Media.MediaEnded += Media_MediaEnded;
            }

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void Media_MediaEnded(object sender, EventArgs e)
        {
            Media.Stop();
            SliderVolume.Value = 0;
            if (RepeatIcon.Kind==MaterialDesignThemes.Wpf.PackIconKind.RepeatOff)
            {
                
                RepeatOnce = true;

                Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            }
            else if (RepeatIcon.Kind==MaterialDesignThemes.Wpf.PackIconKind.RepeatOnce)
            {
                if (RepeatOnce == true) {
                    Media.Play();
                    RepeatOnce = false;
                    Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;

                }
                else
                {
                    Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                }
            }
            else if (RepeatIcon.Kind==MaterialDesignThemes.Wpf.PackIconKind.Repeat)
            {
                Media.Play();
                RepeatOnce = true;

                Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



       


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void infor_Click(object sender, RoutedEventArgs e)
        {

            info O = new info();

            O.ShowDialog();
            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void StartApp()
        {

            Main.ImageSource = new BitmapImage(new Uri(@StopDirectory));
            MainImage.Fill = Main;
            TimeText.Text = "";
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PlayPause()
        {

            if (Media.Source == null)
            {


                if (open.ShowDialog() == true)
                {
                    Media.Open(new Uri(open.FileName));
                    MusicHasPause = false;

                }
            }

            if (Media.Source != null && Material.Kind == MaterialDesignThemes.Wpf.PackIconKind.Play)
            {
                Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                Media.Play();
              
                if (MusicHasPause == false)
                {
                    

                    timer.Start();
                    var g=SetCoverMusic();
                    MainImage.Fill = g;

                }
                MusicHasPause = true;
            }



            else if (Material.Kind == MaterialDesignThemes.Wpf.PackIconKind.Pause && Media.Source != null)
            {
                Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                Media.Pause();

            }
            Media.MediaOpened += Media_MediaOpened;
            SliderVolume.Value = 0;
            Media.MediaEnded += Media_MediaEnded;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private ImageBrush SetCoverMusic()
        {

            try
            {
                
                Mp3Lib.Mp3File MP3 = new Mp3Lib.Mp3File(open.FileName);
                System.Drawing.Image S = MP3.TagHandler.Picture;
                
                var image = S;
                var bitmap = new System.Drawing.Bitmap(image);
                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                bitmap.Dispose();
                var brush = new ImageBrush(bitmapSource);
                
                return brush;
            }

            catch
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory()+"\\Asset\\Music.png";
                Main.ImageSource = new BitmapImage(new Uri(CurrentDirectory));
                return Main;
                //B.ImageSource = new BitmapImage(new Uri(@"C:\Program Files (x86)\Harmonymous\HarmsPlayer/Asset/Music.png"));
                // MainImage.Fill = B;
            }
            
        }

        private void MusicList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
            ListView V = (ListView)sender;

            StackPanel St = (StackPanel)V.SelectedItem;
            TextBlock Num = (TextBlock)St.Children[0];
            int Number = Convert.ToInt16(Num.Text);
            
            
            Media.Open(new Uri((AddMusic_List[Number-1])));
            
            try
            {

                Mp3Lib.Mp3File MP3 = new Mp3Lib.Mp3File(AddMusic_List[Number-1]);
                System.Drawing.Image S = MP3.TagHandler.Picture;

                var image = S;
                var bitmap = new System.Drawing.Bitmap(image);
                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                bitmap.Dispose();
                var brush = new ImageBrush(bitmapSource);

                MainImage.Fill = brush;
            }

            catch
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Asset\\Music.png";
                Main.ImageSource = new BitmapImage(new Uri(CurrentDirectory));
                MainImage.Fill = Main;
                //B.ImageSource = new BitmapImage(new Uri(@"C:\Program Files (x86)\Harmonymous\HarmsPlayer/Asset/Music.png"));
                // MainImage.Fill = B;
            }
            if (Material.Kind == MaterialDesignThemes.Wpf.PackIconKind.Pause)
            {
                Material.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            }
            timer.Start();
            PlayPause();


        }

        private void SoundVolume_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double SoundVolumeValue = e.NewValue/100;
            Media.Volume = SoundVolumeValue;
            if (SoundVolumeValue < 0.5 && SoundVolumeValue>0)
            {
                MaterialSound.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeLow;
            }
           
            else if (SoundVolumeValue >= 0.6)
            {
                MaterialSound.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;
            }
            
            else if (SoundVolumeValue == 0)
            {
                MaterialSound.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
            }
           
        }

        private void SoundIcon_Click(object sender, RoutedEventArgs e)
        {
            
            if (MaterialSound.Kind == MaterialDesignThemes.Wpf.PackIconKind.VolumeOff)
            {
                MaterialSound.Kind = SoundIcon_Kepper;
                Media.Volume = Volume_Kepper;
            }
            else
            {
                Volume_Kepper = Media.Volume;
                SoundIcon_Kepper = MaterialSound.Kind;
                Media.Volume = 0;
                MaterialSound.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeOff;
            }
        }

        //private void PlayerPosition_Slider(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
           
       // }

        private void SliderVolume_MouseUp(object sender, MouseButtonEventArgs e)
        {
            double S = SliderVolume.Value;

            Media.Position = TimeSpan.FromSeconds(S);

        }

        private void SliderVolume_DragLeave(object sender, DragEventArgs e)
        {
            MessageBox.Show("Ddsad");
        }




        //private void MusicTime_ProgressBar(object sender, MouseButtonEventArgs e)
        // {
        //    MessageBox.Show(e.GetPosition(TimeProgress).X.ToString());
        // }















        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
