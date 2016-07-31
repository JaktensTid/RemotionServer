using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Engine;
using RemotionServer.Engine.Commands;
using RemotionServer.Properties;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;

namespace RemotionServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private const double BOTTOM_HEIGHT = 238.833;
        private const double BOTTOM_WIDTH = 290.758;
        private const double UPPER_HEIGHT = 278.992;
        private const double UPPER_WIDTH = 529.758;
        private int scrollSens;
        private double touchpadSens;
        private bool notifyIconBaloonCanShow;
        ServerEngine server;
        //For xaml
        public List<IPAddress> adresses { get; set; }
        public IPAddress address { get; set; }
        readonly NotifyIcon notifyIcon;
        private bool InternetConnectionEstablished
        {
            get
            {
                bool internetConnectionInterrupted = adresses.Count == 1 & adresses[0].ToString() == "127.0.0.1";
                if (internetConnectionInterrupted)
                {
                    WriteUserDebugMessage(Properties.Resources.InternetExceptionString);
                }
                return !internetConnectionInterrupted;
            }
        }
        #endregion

        public MainWindow()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            InitializeComponent();
            UpdateNetworkInfo();
            notifyIconBaloonCanShow = true;
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.ico.GetHicon());
            notifyIcon.Text = "Remotion server";
            notifyIcon.MouseClick += (sender, args) =>
            {
                Show();
                notifyIcon.Visible = false;
            };

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Height = BOTTOM_HEIGHT;
            Width = BOTTOM_WIDTH;
            address = adresses[0];
            server = ServerEngine.CreateServer();
            scrollSens = ControllerEngine.MouseWheelSpeed;
            ScrollSensSlider.Value = scrollSens;
            ScrollingSensValueLabel.Content = ScrollSensSlider.Value;
            touchpadSens = ControllerEngine.TouchpadSens;
            TouchpadSensSlider.Value = touchpadSens;
            TouchpadSensValueLabel.Content = $"{TouchpadSensSlider.Value:0.0}";
            ChangeSettingsVisibility();
            BindComboboxDefaultValues();
        }

        #region Event handlers
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void OpenCloseConnectionToggleButton_Click(object sender, RoutedEventArgs e)
        {
            bool bChecked = OpenCloseConnectionToggleButton.IsChecked != null &&
                            (bool)OpenCloseConnectionToggleButton.IsChecked;

            if (bChecked)
            {
                OpenConnection();
            }
            else
            {
                CloseConnection();
            }
        }

        private void ExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSettingsVisibility();
            if (ExpanderButton.IsChecked != null & ExpanderButton.IsChecked == true)
            {
                Width = UPPER_WIDTH;
            }
            else
            {
                Width = BOTTOM_WIDTH;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            server?.CloseConnection();
            Close();
            ControllerEngine.MouseWheelSpeed = (int)ScrollSensSlider.Value;
            ControllerEngine.TouchpadSens = TouchpadSensSlider.Value;
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
            notifyIcon.Visible = true;
            if (notifyIconBaloonCanShow)
            {
                notifyIcon.ShowBalloonTip(3000, "Remotion server", Properties.Resources.NotifyIconBaloonText, ToolTipIcon.Info);
                notifyIconBaloonCanShow = false;
            }
        }

        private void helpMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.HelpMainWindowButton, "Help", MessageBoxButton.OK,
                MessageBoxImage.Question);
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            dynamic item = comboBox.SelectedItem;
            int flag = Convert.ToInt32(item.Value);

            switch (comboBox.Name)
            {
                case "comboBox1":
                    {
                        SettingsCommand.action1 = flag;
                    }
                    break;
                case "comboBox2":
                    {
                        SettingsCommand.action2 = flag;
                    }
                    break;
            }

            Settings.Default.Save();
            Visibility visibility = SettingsCommand.GetScrollInterfaceVisibility;
            ScrollSensSlider.Visibility = visibility;
            ScrollSpeedLabel.Visibility = visibility;
            ScrollingSensValueLabel.Visibility = visibility;
        }

        private void IpListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(IpListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                address = (IPAddress) item.Content;
            }
        }

        private void ScrollSensSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int nextValue = (int)ScrollSensSlider.Value;
            ControllerEngine._mouseWheelSpeed = nextValue;
            if(ScrollingSensValueLabel != null)
            ScrollingSensValueLabel.Content = nextValue;
        }
        private void TouchpadSensSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double nextValue = TouchpadSensSlider.Value;
            //ServerEngine.touchpadSens = nextValue;
            if (TouchpadSensValueLabel != null)
                TouchpadSensValueLabel.Content = $"{nextValue:0.0}";
        }
        #endregion

        #region Methods

        private void OpenConnection()
        {
            DebugMessageTextBlock.Text = "";
            UpdateNetworkInfo();

            if (InternetConnectionEstablished && address != null)
            {
                Height = UPPER_HEIGHT;
                try
                {
                    server.CloseConnection();
                    server.OpenConnection(address);
                    WriteUserDebugMessage($"{Properties.Resources.SocketOpenedString} - {Properties.Resources.AdressString}{address}");
                    OpenCloseConnectionToggleButton.Content = Properties.Resources.CloseSocketButton;
                }
                catch (ObjectDisposedException)
                {
                    server.CloseConnection();
                    server.OpenConnection(address);
                }
                catch (SocketException)
                {
                    WriteUserDebugMessage(Properties.Resources.ServerExceptionString);
                    OpenCloseConnectionToggleButton.IsChecked = false;
                }
            }
            else
            {
                Height = UPPER_HEIGHT;
                WriteUserDebugMessage(Properties.Resources.InternetExceptionString);
                //IpListBox.ItemsSource = null;
                System.Media.SystemSounds.Beep.Play();
                OpenCloseConnectionToggleButton.IsChecked = false;
            }
        }

        private void CloseConnection()
        {
            Height = BOTTOM_HEIGHT;
            UpdateNetworkInfo();

            if (server != null)
            {
                server.CloseConnection();
                if(server.runCounter > 1)
                WriteUserDebugMessage(Properties.Resources.SocketClosedString);
            }

            OpenCloseConnectionToggleButton.Content = Properties.Resources.OpenSocketButton;

            ChangeSettingsVisibility();
        }

        /// <summary>
        /// Обновить информацию о текущем подключении компьютера
        /// </summary>
        private void UpdateNetworkInfo()
        {
            try
            {
                adresses = ServerEngine.adressList;
            }
            catch (Exception)
            {
                WriteUserDebugMessage(Properties.Resources.InternetExceptionString);
            }
            IpListBox.ItemsSource = adresses;
            if (address != null && address.ToString() == "127.0.0.1")
            {
                address = adresses[0];
            }
        }

        /// <summary>
        /// Написать сообщение в дебаг-лейбел интерфейса
        /// </summary>
        /// <param name="message"></param>
        private void WriteUserDebugMessage(string message)
        {
            DebugMessageTextBlock.Dispatcher.Invoke(() =>
            {
                DebugMessageTextBlock.Text = message;
            });
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNetworkInfo();
        }

        /// <summary>
        /// Биндинг значений комбобокса по-умолчанию
        /// </summary>
        private void BindComboboxDefaultValues()
        {
            comboBox1.SelectedIndex = SettingsCommand.flag_name_dictionary.Keys.ToList()
                .IndexOf(SettingsCommand.action1);
            comboBox2.SelectedIndex = SettingsCommand.flag_name_dictionary.Keys.ToList()
                .IndexOf(SettingsCommand.action2);
        }

        private void ChangeSettingsVisibility()
        {

            var visibility = ExpanderButton.IsChecked != null && (bool) ExpanderButton.IsChecked
                ? Visibility.Visible : Visibility.Hidden;

            Shortcut1TextBlock.Visibility = visibility;
            comboBox1.Visibility = visibility;
            Shortcut2TextBlock.Visibility = visibility;
            comboBox2.Visibility = visibility;
            ScrollSpeedLabel.Visibility = visibility;
            ScrollSensSlider.Visibility = visibility;
            ScrollingSensValueLabel.Visibility = visibility;

            //TouchpadSensLabel.Visibility = visibility;
            //TouchpadSensSlider.Visibility = visibility;
            //TouchpadSensValueLabel.Visibility = visibility;
        }
        #endregion

        
    }

    /// <summary>
    /// Используется для биндинга шорткат-комбобоксов
    /// </summary>
    public static class SettingsData
    {
        public static Dictionary<string, int> name_flag_dict { get { return SettingsCommand.name_flag_dictionary; } }
    }
}
