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
    /// Interaction logic for UsCont_Add_Word.xaml
    /// </summary>
    public partial class UsCont_Edit_Word : UserControl
    {
        private Database_Tools Dtb_Tools;
        int id_voc;
        Vocabulary[] dt_before;
        public UsCont_Edit_Word()
        {
            InitializeComponent();
            Dtb_Tools = new Database_Tools(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adam\source\repos\UceniSlovicek\UceniSlovicek\dtb_slovicka.mdf;Integrated Security=True");
        }

        public void InicializeWord(int Id)
        {
            this.id_voc = Id;
            dt_before = Dtb_Tools.GetCzechandEnglishVocabularyById(id_voc);
            this.tb_cze_Noun.Text = dt_before[0].Noun;            
            this.tb_cze_Adjective.Text = dt_before[0].Adjective;
            this.tb_cze_Verb.Text = dt_before[0].Verb;

            this.tb_eng_Noun.Text = dt_before[1].Noun;
            this.tb_eng_Adjective.Text = dt_before[1].Adjective;
            this.tb_eng_Verb.Text = dt_before[1].Verb;
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] voc = Dtb_Tools.GetVocabularyById(id_voc);

            if (dt_before[0].Noun != tb_cze_Noun.Text || dt_before[0].Adjective != tb_cze_Adjective.Text || dt_before[0].Verb != tb_cze_Verb.Text)
            {
                dt_before[0].SetNoun(tb_cze_Noun.Text.Trim());
                dt_before[0].SetAdjective(tb_cze_Adjective.Text.Trim());
                dt_before[0].SetVerb(tb_cze_Verb.Text.Trim());

                Dtb_Tools.UpdateCzechVocById(voc[0], dt_before[0]);

            };
            if (dt_before[1].Noun != tb_eng_Noun.Text || dt_before[1].Adjective != tb_eng_Adjective.Text || dt_before[1].Verb != tb_eng_Verb.Text)
            {
                dt_before[1].SetNoun(tb_eng_Noun.Text.Trim());
                dt_before[1].SetAdjective(tb_eng_Adjective.Text.Trim());
                dt_before[1].SetVerb(tb_eng_Verb.Text.Trim());

                Dtb_Tools.UpdateCzechVocById(voc[1], dt_before[1]);
            }
        }
        private void TextBox_Clean()
        {
            tb_cze_Noun.Text = string.Empty;
            tb_cze_Adjective.Text = string.Empty;
            tb_cze_Verb.Text = string.Empty;

            tb_eng_Noun.Text = string.Empty;
            tb_eng_Adjective.Text = string.Empty;
            tb_eng_Verb.Text = string.Empty;

        }
       
        private void bt_zpet_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void tb_eng_Noun_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
