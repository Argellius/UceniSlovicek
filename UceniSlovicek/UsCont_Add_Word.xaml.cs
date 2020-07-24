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
    /// Interaction logic for UsCont_Add_Word.xaml
    /// </summary>
    public partial class UsCont_Add_Word : UserControl
    {
        private Database_Tools Dtb_Tools;

        public UsCont_Add_Word()
        {
            InitializeComponent();
            Dtb_Tools = new Database_Tools(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adam\source\repos\UceniSlovicek\UceniSlovicek\dtb_slovicka.mdf;Integrated Security=True");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((tb_cze_noun.Text != string.Empty || tb_cze_adjective.Text != string.Empty || tb_cze_verb.Text != string.Empty) && (tb_eng_noun.Text != string.Empty || tb_eng_adjective.Text != string.Empty || tb_eng_verb.Text != string.Empty))
            {
                Vocabulary cze_voc = new Vocabulary(KindOfVocabulary.Czech, tb_cze_noun.Text, tb_cze_adjective.Text, tb_cze_verb.Text);
                Vocabulary eng_voc = new Vocabulary(KindOfVocabulary.English, tb_eng_noun.Text, tb_eng_adjective.Text, tb_eng_verb.Text);
                Dtb_Tools.Add_Record(cze_voc, eng_voc);
                lb_sec_added.Visibility = Visibility.Visible;
                TextBox_Clean();
                StartAsyncTimedWork();

            }
            else
                MessageBox.Show("Nevyplněny české nebo anglické pole");
        }
        private void TextBox_Clean()
        {
            tb_cze_noun.Text = string.Empty;
            tb_cze_adjective.Text = string.Empty;
            tb_cze_verb.Text = string.Empty;

            tb_eng_noun.Text = string.Empty;
            tb_eng_adjective.Text = string.Empty;
            tb_eng_verb.Text = string.Empty;

        }
        private async Task delayedWork()
        {
            await Task.Delay(2000);
            lb_sec_added.Visibility = Visibility.Hidden;
        }

        //This could be a button click event handler or the like */
        private void StartAsyncTimedWork()
        {
            Task ignoredAwaitableResult = this.delayedWork();
        }

        private void bt_zpet_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
