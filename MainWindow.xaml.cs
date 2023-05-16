using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;
        private static List<string> Wallpapers = new List<string>()
        {
             @"C:\Users\Administrator\Desktop\CatPIC.jpg",
              @"C:\Users\Administrator\Desktop\DogPic.jpg"
        };



        public void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if(key.GetValue( "Wallpaper" ).ToString() != path)
            {
                ChangeWallpaper(0);
            }
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());
        
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        Random random = new Random();
        private void ChangeWallpaper(int timer)
        {

            for (int i = 0; i < Wallpapers.Count; i++)
            {
                
                int ran = random.Next(0, Wallpapers.Count);
                if (File.Exists(Wallpapers[ran]))
                {
                    Thread.Sleep(TimeSpan.FromSeconds(timer));
                    SetWallpaper(Wallpapers[ran]);
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeWallpaper(10);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeWallpaper(3600); // 1 hour;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChangeWallpaper(86400); // 1 day
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // do nothing
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            test.Text = e.ToString();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Multiselect = true;
            OpenFile.Title = "Select Picture(s)";
            OpenFile.Filter = "ALL supported Graphics| *.jpeg; *.jpg;*.png;";
            if (OpenFile.ShowDialog() == true)
            {
                foreach (String file in OpenFile.FileNames)
                {


                        if (Wallpapers.Contains(file))
                        {
                            Wallpapers.Remove(file);
                        }
                        else
                        {

                            Wallpapers.Add(file);
                        }
                    }
                }
            }
        }
    }



