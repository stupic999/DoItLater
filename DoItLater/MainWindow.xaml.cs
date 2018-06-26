using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace DoItLater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TodayToDoList Titem;
        PastNFutureToDoList PNFitem;
        FutureToDoList Fitem;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TodayBtn_Click(object sender, RoutedEventArgs e)
        {
            switchPage(Pg1, Pg2);
        }
        Grid g;
        private void switchPage(Grid preGide, Grid nextGrid)
        {
            preGide.Visibility = Visibility.Collapsed;
            g = nextGrid;
            Canvas.SetLeft(g, 0);
            Canvas.SetTop(g, 0);
            nextGrid.Visibility = Visibility.Visible;
            g.BringIntoView();
        }

        private void FutureBtn_Click(object sender, RoutedEventArgs e)
        {
            switchPage(Pg1, Pg4);
        }

        private void PastBtn_Click(object sender, RoutedEventArgs e)
        {
            switchPage(Pg1, Pg3);
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddBtn_1_Click(object sender, RoutedEventArgs e)
        {
            // Today
            Titem = new TodayToDoList();
            ToDolist_Today.Children.Add(Titem);
        }

        private void AddBtn_2_Click(object sender, RoutedEventArgs e)
        {
            // Past
            PNFitem = new PastNFutureToDoList();
            ToDolist_Past.Children.Add(PNFitem);
        }

        private void AddBtn_3_Click(object sender, RoutedEventArgs e)
        {
            // Future
            Fitem = new FutureToDoList();
            ToDolist_Future.Children.Add(Fitem);
        }

        private void GoBackBtn_3_Click(object sender, RoutedEventArgs e)
        {
            switchPage(Pg4, Pg1);
        }

        private void GoBackBtn_2_Click(object sender, RoutedEventArgs e)
        {
            switchPage(Pg3, Pg1);
        }

        private void GoBackBtn_1_Click(object sender, RoutedEventArgs e)
        {
            switchPage(Pg2, Pg1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] partsTime = DateTime.Now.ToString().Split('/');
            TodayDate1.Text = partsTime[0].ToString();
            if (partsTime.Length == 2)
            {
                TodayDate2.Text = "." + partsTime[1].ToString();
            }
            else
            {
                TodayDate3.Text = ".0";
            }
            string[] DateNow = DateTime.Now.ToString().Split(' ');
            string[] date = DateNow[0].Split('/');
            TodayDate3.Text = date[2].ToString() + "年";
            TodayDate2.Text = date[1].ToString() + "月";
            TodayDate1.Text = date[0].ToString() + "日";

            // ～～～～～今天～～～～～
            // 讀檔
            string[] Tlines = System.IO.File.ReadAllLines(@"C:\temp\Tdata.txt");

            // 逐行產生元件
            foreach (string Tline in Tlines)
            {
                // 用符號分隔
                string[] Tparts = Tline.Split('|');

                // 建立元件
                TodayToDoList Titem = new TodayToDoList();
                Titem.ItemName = Tparts[1];

                if (Tparts[0] == "+")
                    Titem.IsChecked = true;
                else
                    Titem.IsChecked = false;

                // 放入到 StackPanel
                ToDolist_Today.Children.Add(Titem);
            }

            // ～～～～～過去～～～～～
            string[] Plines = System.IO.File.ReadAllLines(@"C:\temp\Pdata.txt");

            // 逐行產生元件
            foreach (string Pline in Plines)
            {
                // 用符號分隔
                string[] Pparts = Pline.Split('|');

                // 建立元件
                PastNFutureToDoList Pitem = new PastNFutureToDoList();
                Pitem.ItemName = Pparts[1];

                if (Pparts[0] == "+")
                    Pitem.IsChecked = true;
                else
                    Pitem.IsChecked = false;

                // 放入到 StackPanel
                ToDolist_Past.Children.Add(Pitem);
            }

            // ～～～～～未來～～～～～
            string[] Flines = System.IO.File.ReadAllLines(@"C:\temp\Fdata.txt");

            // 逐行產生元件
            foreach (string Fline in Flines)
            {
                // 用符號分隔
                string[] Fparts = Fline.Split('|');

                // 建立元件
                FutureToDoList Fitem = new FutureToDoList();
                Fitem.ItemName = Fparts[1];

                if (Fparts[0] == "+")
                    Fitem.IsChecked = true;
                else
                    Fitem.IsChecked = false;

                // 放入到 StackPanel
                ToDolist_Future.Children.Add(Fitem);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // ～～～～～今天～～～～～
            string Tdata = "";

            // 取得每一個項目的文字
            foreach (TodayToDoList Titem in ToDolist_Today.Children)
            {
                if (Titem.ItemName != "")
                {
                    // 打勾符號
                    if (Titem.IsChecked == true)
                        Tdata += "+";
                    else
                        Tdata += "-";

                    // 文字
                    Tdata += "|" + Titem.ItemName + "\r\n";
                }
            }
            System.IO.File.WriteAllText(@"C:\temp\Tdata.txt", Tdata);


            // ～～～～～過去～～～～～
            string Pdata = "";

            // 取得每一個項目的文字
            foreach (PastNFutureToDoList Pitem in ToDolist_Past.Children)
            {
                if (Pitem.ItemName != "")
                {
                    // 打勾符號
                    if (Pitem.IsChecked == true)
                        Pdata += "+";
                    else
                        Pdata += "-";

                    // 文字
                    Pdata += "|" + Pitem.ItemName + "\r\n";
                }
            }
            System.IO.File.WriteAllText(@"C:\temp\Pdata.txt", Pdata);

            // ～～～～～未來～～～～～
            string Fdata = "";

            // 取得每一個項目的文字
            foreach (FutureToDoList Fitem in ToDolist_Future.Children)
            {
                if (Fitem.ItemName != "")
                {
                    // 打勾符號
                    if (Fitem.IsChecked == true)
                        Fdata += "+";
                    else
                        Fdata += "-";

                    // 文字
                    Fdata += "|" + Fitem.ItemName + "\r\n";
                }
            }
            System.IO.File.WriteAllText(@"C:\temp\Fdata.txt", Fdata);
        }
    }
}
