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
        public string savePath = "";
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
            string fileName = "";
            music.GetPath(DialogType.File, ref fileName, "Select Music", "mp3 File|*.mp3|every file|*.*");
            this.SongName.Text = fileName;
            player.Open(new Uri(fileName, UriKind.Relative));
            //Console.WriteLine(player.NaturalDuration);
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
            Source source = this.Resources["s"] as Source;
            int s1 = DriectionCheckBox.SelectedIndex;
            float s2 = float.Parse(this.WaitNextTime.Text);
            int s3 = this.SelectTpye.SelectedIndex;
            source.AddSource(new SourceBase() { _kind = s1, _waitTime = s2, _comboType = s3 });
            //foreach (var item in source)
            //{
            //    Console.WriteLine("{0}\t{1}\t{2}", item._comboType, item._kind, item._waitTime);
            //}
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            //FileStream fs = new FileStream(player);
            string fileName = "";
            GetMusicName(this.SongName.Text, ref fileName);
            fileName = fileName.Substring(0, fileName.Length - 1);
            fileName = savePath + fileName + ".txt";
           // Console.WriteLine(fileName);
            
            StringBuilder sb = new StringBuilder("");
            foreach (var stream in (this.Resources["s"] as Source))
            {
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            sb.Append(stream._kind);
                            break;
                        case 1:
                            sb.Append(stream._waitTime);
                            break;
                        case 2:
                            sb.Append(stream._comboType);
                            break;
                    }
                    sb.Append(" ");
                }
            }
            sb.Append("0a");
            //Console.WriteLine(sb.ToString());
            try {
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                StreamWriter bw = new StreamWriter(fs);
                bw.Write(sb.ToString());
                bw.Close();
                fs.Close();
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.ToString());
                FileStream fs = new FileStream("Error.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(ioe.ToString());
            }
        }
        private void GetMusicName(string MusicPath, ref string resultName)
        {
            Match match = Regex.Match(MusicPath, @"(^.*?\\)(.*\.)");
            if (match.Success)
            {
                //Console.WriteLine("{0}\n{1}",match.Groups[1].Value, match.Groups[2]);
                resultName = match.Groups[2].Value;
                GetMusicName(match.Groups[2].Value, ref resultName);
            }
        }

        private void SelectPath_Click(object sender, RoutedEventArgs e)
        {
            //string folderName = "";
            music.GetPath(DialogType.Folder, ref savePath, "Select Folder Save File");
            savePath += @"\Music File\";
            //Console.WriteLine(savePath);
            Directory.CreateDirectory(savePath);
        }
    }
}
