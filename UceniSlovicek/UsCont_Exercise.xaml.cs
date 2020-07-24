using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class RowWord
    {
        public string podst_jm { set; get; }
        public string prid_jm { set; get; }
        public string sloveso { set; get; }

        
        public string empty_cell
        {
            get { return string.Empty; }
        }


        public string Noun { set; get; }
        public string Adjective { set; get; }
        public string Verb { set; get; }

    }
    /// <summary>
    /// Interaction logic for UsCont_Exercise.xaml
    /// </summary>
    /// 
    public partial class UsCont_Exercise : UserControl
    {
        private Database_Tools dtb_t;
        public UsCont_Exercise()
        {
            InitializeComponent();
            dtb_t = new Database_Tools(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adam\source\repos\UceniSlovicek\UceniSlovicek\dtb_slovicka.mdf;Integrated Security=True");
            InicializeDataGrid();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                InicializeDataGrid();
        }

        private void AddColumns()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "CZ_Noun";
            c1.Binding = new Binding("podst_jm");
            c1.Width = 110;
            dg_print_words.Columns.Add(c1);


            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "CZ_Adjective";
            c2.Width = 110;
            c2.Binding = new Binding("prid_jm");
            dg_print_words.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "CZ_Verb";
            c3.Width = 110;
            c3.Binding = new Binding("sloveso");
            dg_print_words.Columns.Add(c3);

            DataGridTextColumn c7 = new DataGridTextColumn();
            c7.Header = "";
            c7.Binding = new Binding("empty_cell");
            c7.Width = 110;
            dg_print_words.Columns.Add(c7);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "ENG_Noun";
            c4.Binding = new Binding("Noun");
            c4.Width = 110;
            dg_print_words.Columns.Add(c4);

           
            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "ENG_Adjective";
            c5.Width = 110;
            c5.Binding = new Binding("Adjective");
            dg_print_words.Columns.Add(c5);

            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "ENG_Verb";
            c6.Width = 110;
            c6.Binding = new Binding("Verb");
            dg_print_words.Columns.Add(c6);

        }

        private void InicializeDataGrid()
        {
            AddColumns();
            
            DataTable All_Voc = dtb_t.Get_Id_Voc();


            
            foreach (DataRow row in All_Voc.Rows)
            {
                Vocabulary czeVoc = dtb_t.Get_Czech_Voc_By_Id(Convert.ToInt32(row.ItemArray[1]));
                Vocabulary engVoc = dtb_t.Get_English_Voc_By_Id(Convert.ToInt32(row.ItemArray[2]));
                this.dg_print_words.Items.Add(new RowWord { podst_jm = czeVoc.Noun, prid_jm = czeVoc.Adjective, sloveso = czeVoc.Verb, Noun = engVoc.Noun, Adjective = engVoc.Adjective, Verb = engVoc.Verb }); ; ;
            }
        }


    }


}
