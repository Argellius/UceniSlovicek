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

namespace UceniSlovicek
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<UserControl> All_UserControl;
        private List<Button> All_Buttons;

        public MainWindow()
        {
            InitializeComponent();
            UserControl_Add.Visibility = Visibility.Hidden;
            UserControl_Vypis.Visibility = Visibility.Hidden;
            bt_Add.Visibility = Visibility.Visible;
            bt_Vypis.Visibility = Visibility.Visible;

            All_UserControl = new List<UserControl>() { this.UserControl_Add, this.UserControl_Vypis, this.USerControl_FlashCard };
            All_Buttons = new List<Button>() { this.bt_Add, this.bt_Vypis, this.bt_FlashCards };
          }

        private void bt_Add_Click(object sender, RoutedEventArgs e)
        {
            UserControl_Add.Visibility = Visibility.Visible;
            bt_Add.Visibility = Visibility.Hidden;
            bt_Vypis.Visibility = Visibility.Hidden;
        }

        private void UserControl_Add_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (UserControl_Add.Visibility == Visibility.Hidden)
            {
                if (bt_Add.Visibility != Visibility.Visible)
                    bt_Add.Visibility = Visibility.Visible;
                if (bt_Vypis.Visibility != Visibility.Visible)
                    bt_Vypis.Visibility = Visibility.Visible;
            }
            else
            {
                if (bt_Add.Visibility != Visibility.Hidden)
                    bt_Add.Visibility = Visibility.Hidden;
                if (bt_Vypis.Visibility != Visibility.Hidden)
                    bt_Vypis.Visibility = Visibility.Hidden;
            }
        }

        private void bt_Vypis_Click(object sender, RoutedEventArgs e)
        {
            UserControl_Add.Visibility = Visibility.Hidden;
            UserControl_Vypis.Visibility = Visibility.Visible;

            bt_Add.Visibility = Visibility.Hidden;
            bt_Vypis.Visibility = Visibility.Hidden;


        }

        private void UsCont_Exercise_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserControl_Add.Visibility == Visibility.Hidden)
                bt_Add.Visibility = Visibility.Visible;
            else
                bt_Add.Visibility = Visibility.Hidden;
        }

        private void ShowHideAllButtons(bool action)
        {
            Visibility vs_action = action ? Visibility.Visible : Visibility.Hidden;

            foreach (Button bt in All_Buttons)
            {
                if (bt.Visibility != vs_action)
                bt.Visibility = vs_action;
            }

        }
        private void bt_FlashCards_Click(object sender, RoutedEventArgs e)
        {
            ShowHideAllButtons(false);
            USerControl_FlashCard.Visibility = Visibility.Visible;

        }
    }
}
