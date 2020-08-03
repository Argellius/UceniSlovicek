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
        public int Id { set; get; }
        public string podst_jm { set; get; }
        public string prid_jm { set; get; }
        public string Sloveso { set; get; }      



        public string Noun { set; get; }
        public string Adjective { set; get; }
        public string Verb { set; get; }

        public string empty_cell
        {
            get { return string.Empty; }
        }
        public string GetPropertyById(int i)
        {
            switch (i)
            {
                case 0:
                    return this.podst_jm;
                    
                case 1:
                    return this.prid_jm;
                case 2:
                    return this.Sloveso;
                case 3:
                    return this.Noun;
                case 4:
                    return this.Adjective;
                case 5:
                    return this.Verb;
                default:
                    return string.Empty;
            }
        }

    }
    /// <summary>
    /// Interaction logic for UsCont_Exercise.xaml
    /// </summary>
    /// 
    public partial class UsCont_Vypis : UserControl
    {
        private Database_Tools dtb_t;
        public UsCont_Vypis()
        {
            InitializeComponent();


            dtb_t = new Database_Tools(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adam\source\repos\UceniSlovicek\UceniSlovicek\dtb_slovicka.mdf;Integrated Security=True");
            UserControl_Edit.Visibility = Visibility.Hidden;
            InicializeDataGrid();

            InicializeDoubleClickEvent();

        }

        private void InicializeDoubleClickEvent()
        {
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));
            dg_print_words.RowStyle = rowStyle;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            UserControl_Edit.Visibility = Visibility.Visible;
            UserControl_Edit.InicializeWord((row.Item as RowWord).Id);
            UserControl_Edit.Visibility = Visibility.Visible;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                InicializeDataGrid();
        }

        private void AddColumns()
        {
            DataGridTextColumn c0 = new DataGridTextColumn();
            c0.Header = "ID";
            c0.Binding = new Binding("Id");
            c0.Width = 110;
            c0.Visibility = Visibility.Hidden;
            dg_print_words.Columns.Add(c0);


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
            c3.Binding = new Binding("Sloveso");
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
            if (dg_print_words.Columns.Count != 0)
            dg_print_words.Columns.Clear();
            if (dg_print_words.Items.Count != 0)
                dg_print_words.Items.Clear();

            AddColumns();
            
            DataTable All_Voc = dtb_t.Get_All_IDs_Voc();


            
            foreach (DataRow row in All_Voc.Rows)
            {
                Vocabulary czeVoc = dtb_t.Get_Czech_Voc_By_Id(Convert.ToInt32(row.ItemArray[1]));
                Vocabulary engVoc = dtb_t.Get_English_Voc_By_Id(Convert.ToInt32(row.ItemArray[2]));
                this.dg_print_words.Items.Add(new RowWord {Id = (int)row.ItemArray[0], podst_jm = czeVoc.Noun, prid_jm = czeVoc.Adjective, Sloveso = czeVoc.Verb, Noun = engVoc.Noun, Adjective = engVoc.Adjective, Verb = engVoc.Verb }); ; ;
            }
        }

        private void bt_zpátky_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void dg_print_words_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Edit_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Edit_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (UserControl_Edit.Visibility == Visibility.Hidden)
                InicializeDataGrid();
        }
    }


}
