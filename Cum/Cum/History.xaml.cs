using System.Collections.Generic;
using System.Windows;

namespace Cum
{
    /// <summary>
    /// History.xaml 的交互逻辑
    /// </summary>
    public partial class History : Window
    {
        
        public History(List<Sources> lss)
        {
            InitializeComponent();
            this.historys.ItemsSource = lss;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
