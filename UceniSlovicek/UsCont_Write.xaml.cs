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
    /// Interaction logic for UsCont_Write.xaml
    /// </summary>
    public partial class UsCont_Write : UserControl
    {
        //Database Tools
        private Database_Tools dt;

        //DataTable contains all word from database
        //Func ReloadWords() fill the Datatable
        private List<RowWord> List_AllWords;
        private List<TextBox> List_AllTextBox;
        private RowWord actualWord;
        private KindOfVocabulary kindVoc;

        public UsCont_Write()
        {
            InitializeComponent();
            dt = new Database_Tools(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adam\source\repos\UceniSlovicek\UceniSlovicek\dtb_slovicka.mdf;Integrated Security=True");
            List_AllWords = new List<RowWord>();
            List_AllTextBox = new List<TextBox>() { tbox_Noun, tbox_Adjective, tbox_Verb };
        }

        //Load words from database
        public void ReloadWords()
        {
            if (List_AllWords.Count != 0)
                this.List_AllWords.Clear();

            DataTable All_Voc = dt.Get_All_IDs_Voc();
            foreach (DataRow row in All_Voc.Rows)
            {
                Vocabulary czeVoc = dt.Get_Czech_Voc_By_Id(Convert.ToInt32(row.ItemArray[1]));
                Vocabulary engVoc = dt.Get_English_Voc_By_Id(Convert.ToInt32(row.ItemArray[2]));
                this.List_AllWords.Add(new RowWord { podst_jm = czeVoc.Noun, 
                    prid_jm = czeVoc.Adjective, 
                    Sloveso = czeVoc.Verb, 
                    Noun = engVoc.Noun, 
                    Adjective = engVoc.Adjective, 
                    Verb = engVoc.Verb
                }); ; ;
            }

            bt_nextWord_Click(null, null);

        }

        //Set button content by kind of word
        //Show and hide label and textbox by property of actual word (selected by random number)
        private void ButtonContent(KindOfVocabulary kv)
        {                       
            if (kv == KindOfVocabulary.Czech)
            {
                //Podstatné jméno
                if (actualWord.podst_jm == string.Empty)
                {
                    lb_Noun.Visibility = Visibility.Hidden;
                    tbox_Noun.Visibility = Visibility.Hidden;
                }
                else
                {                    
                    lb_Noun.Visibility = Visibility.Visible;
                    tbox_Noun.Visibility = Visibility.Visible;
                    tb_Noun.Text = actualWord.podst_jm;
                }

                //Přídavné jméno
                if (actualWord.prid_jm == string.Empty)
                {
                    lb_Adjective.Visibility = Visibility.Hidden;
                    tbox_Adjective.Visibility = Visibility.Hidden;
                }
                else
                {
                    lb_Adjective.Visibility = Visibility.Visible;
                    tbox_Adjective.Visibility = Visibility.Visible;
                    tb_Adjective.Text = actualWord.prid_jm;
                }

                //Sloveso
                if (actualWord.Sloveso == string.Empty)
                {
                    lb_Verb.Visibility = Visibility.Hidden;
                    tbox_Verb.Visibility = Visibility.Hidden;
                }
                else
                {
                    lb_Verb.Visibility = Visibility.Visible;
                    tbox_Verb.Visibility = Visibility.Visible;
                    tb_Verb.Text = actualWord.Sloveso;
                }
            }
        }

        private void bt_nextWord_Click(object sender, RoutedEventArgs e)
        {

            ClearAllTextBox();

            //random number
            var rnd = new Random(DateTime.Now.Millisecond);
            int actual_position = rnd.Next(0, List_AllWords.Count);
            //get word by random number
            actualWord = List_AllWords[actual_position];
           
            //set kind of word
            kindVoc = KindOfVocabulary.Czech;
            //Set button content by kind of word
            ButtonContent(KindOfVocabulary.Czech);

        }

        //Clear all txtBox
        private void ClearAllTextBox()
        {
            ClearTxtBox(tbox_Adjective);
            ClearTxtBox(tbox_Verb);
            ClearTxtBox(tbox_Noun);
        }


        //Clear textbox by parameter
        private void ClearTxtBox(TextBox tb)
        {
            if (tb.Text != String.Empty)
                tb.Clear();
        }


        //Check the correct answer + show results as x/x
        private void bt_check_Click(object sender, RoutedEventArgs e)
        {
            int correct = 0; // correct results
            int visible = 0; // all results

            //Projdu všechny textboxy - vracím textbox a pozici index
            foreach ((TextBox tb, Int32 i) in List_AllTextBox.Select((value, i) => (value, i)))
                if (tb.Visibility == Visibility.Visible)
                {
                    visible++;
                    if (tb.Text.Trim().ToLower() == actualWord.GetPropertyById(i+3).ToLower())
                    {
                        correct++;
                    }
                }

            MessageBox.Show("Správně: " + correct + "/" + visible);

        }

        private void bt_zpatky_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
