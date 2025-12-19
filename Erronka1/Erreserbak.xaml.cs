using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for Erreserbak.xaml
    /// </summary>
    public partial class Erreserbak : Window
    {
        Database datubasea;
        List<Mahaia> mahai;
        int id;
        public Erreserbak(int idea)
        {
            InitializeComponent();
            id = idea;
            datubasea = new Database();
            datubasea.ConnectToDatabase();

            mahai = datubasea.getMahaiak();
            denakRefresh();
        }

        private void tpv_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow(id);
            datubasea.itxiKonekxioa();
            mahai1.konexioaAmaitu();
            mahai2.konexioaAmaitu();
            mahai3.konexioaAmaitu();
            mahai4.konexioaAmaitu();
            mahai5.konexioaAmaitu();
            mahai6.konexioaAmaitu();
            mahai7.konexioaAmaitu();
            mahai8.konexioaAmaitu();
            mahai9.konexioaAmaitu();
            mahai10.konexioaAmaitu();
            this.Close();
            window.ShowDialog();
        }
        private void denakRefresh()
        {
            mahai1.Id = mahai[0].id;
            mahai1.Hartua = mahai[0].erreserbatuta;
            mahai1.Refresh();
            mahai2.Id = mahai[1].id;
            mahai2.Hartua = mahai[1].erreserbatuta;
            mahai2.Refresh();
            mahai3.Id = mahai[2].id;
            mahai3.Hartua = mahai[2].erreserbatuta;
            mahai3.Refresh();
            mahai4.Id = mahai[3].id;
            mahai4.Hartua = mahai[3].erreserbatuta;
            mahai4.Refresh();
            mahai5.Id = mahai[4].id;
            mahai5.Hartua = mahai[4].erreserbatuta;
            mahai5.Refresh();
            mahai6.Id = mahai[5].id;
            mahai6.Hartua = mahai[5].erreserbatuta;
            mahai6.Refresh();
            mahai7.Id = mahai[6].id;
            mahai7.Hartua = mahai[6].erreserbatuta;
            mahai7.Refresh();
            mahai8.Id = mahai[7].id;
            mahai8.Hartua = mahai[7].erreserbatuta;
            mahai8.Refresh();
            mahai9.Id = mahai[8].id;
            mahai9.Hartua = mahai[8].erreserbatuta;
            mahai9.Refresh();
            mahai10.Id = mahai[9].id;
            mahai10.Hartua = mahai[9].erreserbatuta;
            mahai10.Refresh();
        }

            
    }
}


public class Mahaia
{
    public int id { get; set; }
    public int erreserbatuta { get; set; }
}
