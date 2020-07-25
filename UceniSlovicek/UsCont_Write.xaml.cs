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

        public void ReloadWords()
        {
            if (List_AllWords.Count != 0)
                this.List_AllWords.Clear();

            DataTable All_Voc = dt.Get_Id_Voc();
            foreach (DataRow row in All_Voc.Rows)
            {
                Vocabulary czeVoc = dt.Get_Czech_Voc_By_Id(Convert.ToInt32(row.ItemArray[1]));
                Vocabulary engVoc = dt.Get_English_Voc_By_Id(Convert.ToInt32(row.ItemArray[2]));
                this.List_AllWords.Add(new RowWord { podst_jm = czeVoc.Noun.Trim(), 
                    prid_jm = czeVoc.Adjective.Trim(), 
                    Sloveso = czeVoc.Verb.Trim(), 
                    Noun = engVoc.Noun.Trim(), 
                    Adjective = engVoc.Adjective.Trim(), 
                    Verb = engVoc.Verb.Trim()
                }); ; ;
            }

            bt_nextWord_Click(null, null);

        }
        private void ButtonContent(KindOfVocabulary kv)
        {                       
            if (kv == KindOfVocabulary.Czech)
            {
                //Podstatné jméno
                if (actualWord.podst_jm.Trim() == string.Empty)
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
                if (actualWord.prid_jm.Trim() == string.Empty)
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
                if (actualWord.Sloveso.Trim() == string.Empty)
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
            var rnd = new Random(DateTime.Now.Millisecond);
            int actual_position = rnd.Next(0, List_AllWords.Count);
            actualWord = List_AllWords[actual_position];
            kindVoc = KindOfVocabulary.Czech;
            ButtonContent(KindOfVocabulary.Czech);
        }

        private void bt_check_Click(object sender, RoutedEventArgs e)
        {
            int correct = 0;
            int visible = 0;
            foreach ((TextBox tb, Int32 i) in List_AllTextBox.Select((value, i) => (value, i)))
                if (tb.Visibility == Visibility.Visible)
                {
                    visible++;
                    if (tb.Text.ToLower() == actualWord.GetPropertyById(i+3).ToLower())
                    {
                        correct++;
                    }
                }

            MessageBox.Show("Správně: " + correct + "/" + visible);

        }
    }
}
