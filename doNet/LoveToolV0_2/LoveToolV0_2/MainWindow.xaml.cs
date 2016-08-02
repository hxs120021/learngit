using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LoveToolV0_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MediaPlayer player = new MediaPlayer();
        DispatcherTimer timer = new DispatcherTimer();
        Music music = new Music();
        
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += Time_tick;
            
        }

        private void Time_tick(object sender, EventArgs e)
        {
            //this.NowTime.SetBinding(TextBox.TextProperty, new Binding("Value") { ElementName = "player" });
            string s = string.Concat(DateTime.Now.ToString("mm:ss:ms"));
            //this.NowTime.Text = music.MusicTime(s).ToString();
            this.NowTime.Text = s;
        }

        private void SelectSong_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Music";
            openFileDialog.Filter = "mp3 File|*.mp3|every file|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == false)
                return;
            string fileName = openFileDialog.FileName;
            this.SongName.Text = fileName;
            player.Open(new Uri(fileName, UriKind.Relative));
            Console.WriteLine(player.NaturalDuration);
            this.AllTime.Text = player.NaturalDuration.ToString();
        }

        private void PlayButtoon_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            timer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            timer.Stop();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            timer.Stop();
        }
        private void BackTimeButton_Click(object sender, RoutedEventArgs e)
        {
            player.Position -= TimeSpan.FromSeconds(1);
            this.NowTime.Text = music.MusicTime(player.Position).ToString();  
        }
        private void AdvanceTimeButton_Click(object sender, RoutedEventArgs e)
        {
            player.Position += TimeSpan.FromSeconds(1);
            this.NowTime.Text = music.MusicTime(player.Position).ToString();
        }
        private void UsingSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            string speed = this.SongSpeed.Text;
            player.SpeedRatio = double.Parse(speed);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Source source = this.Resources["source"] as Source;
            //float s0 = float.Parse( this.NowTime.Text);
            int s1 = DriectionCheckBox.SelectedIndex;
            float s2 = float.Parse(this.WaitNextTime.Text);
            int s3 = this.SelectTpye.SelectedIndex;
            source.AddSource(new SourceBase(s1, s2, s3));
            foreach (var item in source)
            {
                Console.WriteLine("{0}\t{1}\t{2}", item._comboType, item._kind, item._waitTime);
            }
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            //FileStream fs = new FileStream(player);
            string fileName = "";
            GetMusicName(this.SongName.Text, ref fileName);
            fileName = fileName.Substring(0, fileName.Length - 1);
            Console.WriteLine(fileName);
            FileStream fs = new FileStream(fileName, FileMode.Create);
        }
        private void GetMusicName(string MusicPath, ref string resultName)
        {
            Match match = Regex.Match(MusicPath, @"(^.*?\\)(.*\.)");
            if (match.Success)
            {
                Console.WriteLine("{0}\n{1}",match.Groups[1].Value, match.Groups[2]);
                resultName = match.Groups[2].Value;
                GetMusicName(match.Groups[2].Value, ref resultName);
            }
        }
        
    }
}
