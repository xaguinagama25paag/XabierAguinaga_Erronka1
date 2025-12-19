using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for Erreserba.xaml
    /// </summary>
    public partial class Erreserba : UserControl
    {
        Database datubasea;
        List<Mahaia> mahaiak;

        [Description("Test text displayed in the textbox"), Category("Data")]
        public int Hartua
        {
            get;
            set;
        }
        public int Id
        {
            get;
            set;
        }


        public Erreserba()
        {
            InitializeComponent();
            datubasea = new Database();
            datubasea.ConnectToDatabase();


        }

        public void konexioaAmaitu()
        {
            datubasea.itxiKonekxioa();
        }
        private void Button_Component_Click(object sender, RoutedEventArgs e)
        {
            if (Hartua==1)
            {
                Hartua = 0;
                Button_Component.Content = " Mahaia "+ this.Id +": Libre";
            }
            else
            {
                Hartua = 1;
                Button_Component.Content = " Mahaia " + this.Id + ": Erreserbatuta";

            }
            datubasea.updateMahaia(this.Id, Hartua);
        }
        public void Refresh()
        {
            if (Hartua == 1)
            {
                Button_Component.Content = " Mahaia " + this.Id + ": Libre";
            }
            else
            {
                Button_Component.Content = " Mahaia " + this.Id + ": Erreserbatuta";
            }
        }
    }
    public class Mahaia
    {
        public int id { get; set; }
        public int erreserbatuta { get; set; }
    }
}
