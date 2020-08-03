using System;
using System.Collections.Generic;
using System.Data;
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
        //Database Tools
        private Database_Tools dt;

        //DataTable contains all word from database
        //Func ReloadWords() fill the Datatable
        private List<RowWord> List_AllWords;
        private RowWord actualWord;
        private KindOfVocabulary kindVoc;

        public UsCont_FlashCard()
        {
            InitializeComponent();
            dt = new Database_Tools(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adam\source\repos\UceniSlovicek\UceniSlovicek\dtb_slovicka.mdf;Integrated Security=True");
            List_AllWords = new List<RowWord>();

        }

        public void ReloadWords()
        {
            if (List_AllWords.Count != 0)
                this.List_AllWords.Clear();

            DataTable All_Voc = dt.Get_All_IDs_Voc();
            foreach (DataRow row in All_Voc.Rows)
            {
                Vocabulary czeVoc = dt.Get_Czech_Voc_By_Id(Convert.ToInt32(row.ItemArray[1]));
                Vocabulary engVoc = dt.Get_English_Voc_By_Id(Convert.ToInt32(row.ItemArray[2]));
                this.List_AllWords.Add(new RowWord { podst_jm = czeVoc.Noun, prid_jm = czeVoc.Adjective, Sloveso = czeVoc.Verb, Noun = engVoc.Noun, Adjective = engVoc.Adjective, Verb = engVoc.Verb }); ; ;
            }

            bt_nextWord_Click(null, null);

        }

        private void Button_Word_Click(object sender, RoutedEventArgs e)
        {
            if (kindVoc == KindOfVocabulary.Czech)
            {
                ButtonContent(KindOfVocabulary.English);
                kindVoc = KindOfVocabulary.English;
            }
            else
            {
                ButtonContent(KindOfVocabulary.Czech);
                kindVoc = KindOfVocabulary.Czech;
            }


        }

        private void bt_nextWord_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int actual_position = rnd.Next(0, List_AllWords.Count);
            actualWord = List_AllWords[actual_position];
            kindVoc = KindOfVocabulary.Czech;
            ButtonContent(KindOfVocabulary.Czech);
        }

        private void ButtonContent(KindOfVocabulary kv)
        {

            StringBuilder bt_content = new StringBuilder();
            if (kv == KindOfVocabulary.Czech)
            {

                SetLabelByLanguage(true);

                //Podstatné jméno
                if (actualWord.podst_jm == string.Empty)
                {
                    lb_Noun.Visibility = Visibility.Hidden;
                    bt_content.AppendLine("\n");
                }
                else
                {
                    bt_content.AppendLine(actualWord.podst_jm + "\n");
                    lb_Noun.Visibility = Visibility.Visible;
                }

                //Přídavné jméno
                if (actualWord.prid_jm == string.Empty)
                {
                    lb_Adjective.Visibility = Visibility.Hidden;
                    bt_content.AppendLine("\n");
                }
                else
                {
                    bt_content.AppendLine(actualWord.prid_jm + "\n");
                    lb_Adjective.Visibility = Visibility.Visible;
                }

                //Sloveso
                if (actualWord.Sloveso == string.Empty)
                {
                    lb_Verb.Visibility = Visibility.Hidden;
                    bt_content.AppendLine();
                }
                else
                {
                    bt_content.AppendLine(actualWord.Sloveso + "\n");
                    lb_Verb.Visibility = Visibility.Visible;
                }
            }

            else
            {
                SetLabelByLanguage(false);
                //Podstatné jméno
                if (actualWord.Noun == string.Empty)
                {
                    lb_Noun.Visibility = Visibility.Hidden;
                    bt_content.AppendLine();
                }
                else
                {
                    bt_content.AppendLine(actualWord.Noun + "\n");
                    lb_Noun.Visibility = Visibility.Visible;
                }

                //Přídavné jméno
                if (actualWord.Adjective == string.Empty)
                {
                    lb_Adjective.Visibility = Visibility.Hidden;
                    bt_content.AppendLine();
                }
                else
                {
                    bt_content.AppendLine(actualWord.Adjective + "\n");
                    lb_Adjective.Visibility = Visibility.Visible;
                }

                //Sloveso
                if (actualWord.Verb == string.Empty)
                {
                    lb_Verb.Visibility = Visibility.Hidden;
                    bt_content.AppendLine();
                }
                else
                {
                    bt_content.AppendLine(actualWord.Verb + "\n");
                    lb_Verb.Visibility = Visibility.Visible;
                }
            }

                button_word.Content = bt_content;
        }

        private void SetLabelByLanguage(bool czech)
        {
            if (czech)
            {
                tb_Noun.Text = "Podstatné jméno: ";
                tb_Adjective.Text = "Přídavné jméno: ";
                tb_Verb.Text = "Sloveso: ";
            }
            else
            {
                tb_Noun.Text = "Noun:";
                tb_Adjective.Text = "Adjective:";
                tb_Verb.Text = "Verb:";
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
