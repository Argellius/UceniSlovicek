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
    /// Interaction logic for UsCont_FlashCard.xaml
    /// </summary>
    public partial class UsCont_FlashCard : UserControl
    {
        public UsCont_FlashCard()
        {
            InitializeComponent();
        }

        private void Button_Word_Click(object sender, RoutedEventArgs e)
        {
            this.button_word.Content = "Hello";
        }
    }
}
