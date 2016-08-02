using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Forms;

namespace LoveToolV0_2
{
    public enum ComboType : int
    {
       Short,
       Long
    }

    public enum DialogType
    {
        File, 
        Folder
    }

    public  class Music
    {
        public Music()
        { }
        public double MusicTime(TimeSpan timeSpan)
        {
            string time = timeSpan.ToString();
            Match match = Regex.Match(time, @"(\d{2}):(\d{2}):(\d{2}.\d{0,7})");
            double seconds = 0;
            if (match.Success)
            {
                for(int i = 1; i < match.Groups.Count; i++)
                {
                    double t = double.Parse(match.Groups[i].Value);
                    switch(i)
                    {
                        case 1:
                            seconds += 3600 * t;
                            break;
                        case 2:
                            seconds += 60 * t;
                            break;
                        case 3:
                            seconds += t;
                            break;
                    }
                }

            }
            return seconds;
        }

        public void GetPath(DialogType dialog, ref string fileName, string titleName = "", string filter = "")
        {
            if (dialog == DialogType.File)
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Title = titleName;
                openFileDialog.Filter = filter;
                openFileDialog.FileName = string.Empty;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == false)
                    return;
                fileName = openFileDialog.FileName;
            }
            else if(dialog == DialogType.Folder)
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = titleName;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    fileName = folderDialog.SelectedPath;
                else if (folderDialog.ShowDialog() == DialogResult.Cancel)
                    return;
            }
        }
    }

    public class SourceBase
    {
        
        public int _kind { get; set; }
        public float _waitTime { get; set; }
        public int _comboType { get; set; }
    }

    public class AddComboType : ObservableCollection<ComboType>
    {
        public AddComboType() : base()
        {
            Add(ComboType.Short);
            Add(ComboType.Long);
        }
    }
    public class AddKind :ObservableCollection<int>
    {
        public AddKind() : base()
        {
            for(int i = 0; i < 7; i++)
            {
                Add(i);
            }
        }
    }
    public class Source : ObservableCollection<SourceBase>
    {
        //public List<Source> _listSource = new List<Source>();
        public void AddSource(SourceBase sBase)
        {
            base.Add(sBase);
        }
    }
}
