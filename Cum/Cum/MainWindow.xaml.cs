using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Cum
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<double> listDouble = new List<double>();
        List<string> listString = new List<string>();
        StringBuilder sbNow = new StringBuilder();
        StringBuilder equation = new StringBuilder();
        public List<Sources> history = new List<Sources>();
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(time_Tick);
            timer.Start();
        }
        private void time_Tick(Object sender, EventArgs e)
        {
            this.timeBlock.Text = string.Concat(DateTime.Now.ToString("HH:mm:ss"));
        }
        /// <summary>
        /// 功能键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ln_Click(object sender, RoutedEventArgs e)
        {
            try {
                double d = Convert.ToDouble(this.Number.Text);
                string str = ((Button)sender).Content.ToString();
                switch (str)
                {
                    case "1/x":
                        d = 1 / d;
                        break;
                    case "^2":
                        d = d * d;
                        break;
                    case "√":
                        d = Math.Sqrt(d);
                        break;
                    case "!":
                        //仍然存在很大的问题还不能计算阶乘，需要小数阶乘的计算算法
                        BuildString.Fact(ref d);
                        break;
                    case "ln":
                        d = Math.Log(d);
                        break;
                    case "lg":
                        d = Math.Log10(d);
                        break;
                    case "sin":
                        //仍然存在问题不能输入角度值，只能计算弧度值，cos，tan相同
                        d = Math.Sin(d);
                        break;
                    case "cos":
                        d = Math.Cos(d);
                        break;
                    case "tan":
                        d = Math.Tan(d);
                        break;
                }
                sbNow.Clear();
                sbNow.Append(d.ToString());
                this.Number.Text = sbNow.ToString();
            }
            catch (Exception e2)
            {
                sbNow.Clear();
                sbNow.Append(0f);
                this.Number.Text = sbNow.ToString();
                CreatErrorFile(e2);
            }
        }
        /// <summary>
        /// 运算符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Division_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Content.ToString();
            if (listDouble.Count != 0 || sbNow.Length != 0)
            {
                if (sbNow.ToString() == "")
                {
                    this.listString[listString.Count - 1] = s;
                    this.equation.Remove(equation.Length - 1, 1);
                    equation.Append(s);
                    this.Equation.Text = equation.ToString();
                }
                else {
                    listDouble.Add(double.Parse(sbNow.ToString()));
                    listString.Add(s);
                    equation.Append(sbNow.ToString());
                    equation.Append(s);
                    this.sbNow.Clear();
                    this.Equation.Text = equation.ToString();
                    this.Number.Text = "";
                }
            }
            else
            {
                listDouble.Add(0f);
                listString.Add(s);
                this.equation.Append("0");
                this.equation.Append(s);
                this.Equation.Text = equation.ToString();
            }
        }
        /// <summary>
        /// 等号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Contain_Click(object sender, RoutedEventArgs e)
        {
            try {
                string s = ((Button)sender).Content.ToString();
                string points = sbNow.ToString();
                if (points == "")
                    listDouble.Add(0f);
                else
                    listDouble.Add(Convert.ToDouble(points));
                listString.Add(s);
                equation.Append(points);
                equation.Append(s);
                NumberList numberList = new NumberList(listDouble, listString);
                string resurt = numberList.XResurt().ToString();
                this.Number.Text = resurt;
                this.Equation.Text = equation.ToString();
                equation.Append(resurt);
            }
            catch(Exception e1)
            {
                equation.Append("null");
                this.Equation.Text = "null";
                CreatErrorFile(e1);
            }
            Sources sour = new Sources() { time = DateTime.Now.ToString("HH:mm:ss"), str = equation.ToString() };
            history.Add(sour);
            this.Clean();
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clearn_Click(object sender, RoutedEventArgs e)
        {
            this.Clean();
            this.Equation.Text = "";
            this.Number.Text = "0";
        }
        /// <summary>
        /// 退格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BACK_Click(object sender, RoutedEventArgs e)
        {
            if (sbNow.Length > 0 )
            {
                sbNow.Remove(sbNow.Length - 1, 1);
                this.Number.Text = sbNow.ToString();
            }
        }
        /// <summary>
        /// 小数点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Point_Click(object sender, RoutedEventArgs e)
        {
            if (!(sbNow.ToString()).Subsist(".") && sbNow.Length > 0)
                sbNow.Append(".");
            this.Number.Text = sbNow.ToString();
        }
        /// <summary>
        /// 数字键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Content.ToString();
            if (s == "π")
                sbNow.Append((Math.PI).ToString());
            else
            {
                if (s == "0" && sbNow.ToString() == "0")
                    return;
                else
                    sbNow.Append(s);
            }
            this.Number.Text = sbNow.ToString();
        }
        /// <summary>
        /// 显示历史纪录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void History_Click(object sender, RoutedEventArgs e)
        {
            History history = new History(this.history);
            history.Show();
        }
        /// <summary>
        /// 通用的清空函数
        /// </summary>
        private void Clean()
        {
            this.listDouble.Clear();
            this.listString.Clear();
            this.sbNow.Clear();
            this.equation.Clear();
        }
        private void CreatErrorFile(Exception en)
        {
            FileStream fs = new FileStream("error.date", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(en.ToString());
            fs.Close();
            bw.Close();
        }
    }
}
