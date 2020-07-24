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
            InitializeButtonUserControl();


            ShowAllButtons(true);
            ShowAllUserControl(false);


        }
        private void InitializeButtonUserControl()
        {
            All_UserControl = new List<UserControl>() {
                this.UserControl_Add, this.UserControl_Vypis, this.UserControl_FlashCard
            };

            All_Buttons = new List<Button>() {
                this.bt_Add, this.bt_Vypis, this.bt_FlashCards
            };
        }

        private void bt_Add_Click(object sender, RoutedEventArgs e)
        {
            ShowUserControl(this.UserControl_Add, true);
            ShowAllButtons(false);
        }

        private void UserControl_Add_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (UserControl_Add.Visibility == Visibility.Hidden)
            {
                ShowAllButtons(true);
                ShowAllUserControl(false);
            }

        }

        private void bt_Vypis_Click(object sender, RoutedEventArgs e)
        {
            this.ShowUserControl(this.UserControl_Vypis, true);
            this.ShowAllButtons(false);


        }

        private void UsCont_Exercise_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserControl_Add.Visibility == Visibility.Hidden)
                bt_Add.Visibility = Visibility.Visible;
            else
                bt_Add.Visibility = Visibility.Hidden;
        }

        /*
         * Funkce pro skrytí tlačítek v main window
         * Hide = False
         * Visible = True
         * */
        private void ShowAllButtons(bool action)
        {
            Visibility vs_action = action ? Visibility.Visible : Visibility.Hidden;

            foreach (Button bt in All_Buttons)
            {
                if (bt.Visibility != vs_action)
                    bt.Visibility = vs_action;
            }

        }

        /*
        * Funkce pro skrytí UserControl v main window
        * Hide = False
        * Visible = True
         * */
        private void ShowAllUserControl(bool action)
        {
            Visibility vs_action = action ? Visibility.Visible : Visibility.Hidden;

            foreach (UserControl UC in All_UserControl)
            {
                if (UC.Visibility != vs_action)
                    UC.Visibility = vs_action;
            }

        }

        private void ShowUserControl(UserControl uc, bool action)
        {
            Visibility vs_action = action ? Visibility.Visible : Visibility.Hidden;
            Visibility vs_action_negation = action ? Visibility.Hidden : Visibility.Visible;

            foreach (UserControl UC in All_UserControl)
            {
                if (UC.Visibility != vs_action && UC == uc)
                    UC.Visibility = vs_action;
                else
                    if (UC.Visibility != vs_action_negation)
                    UC.Visibility = vs_action_negation;

            }

        }


        private void bt_FlashCards_Click(object sender, RoutedEventArgs e)
        {
            ShowAllButtons(false);
            UserControl_FlashCard.Visibility = Visibility.Visible;
            UserControl_FlashCard.ReloadWords();
        }

        private void UserControl_Vypis_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (UserControl_Vypis.Visibility == Visibility.Hidden)
            {
                ShowAllButtons(true);
                ShowAllUserControl(false);
            }
        }

        private void UserControl_FlashCards_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (UserControl_FlashCard.Visibility == Visibility.Hidden)
            {
                ShowAllButtons(true);
                ShowAllUserControl(false);
            }
        }
    }
}
