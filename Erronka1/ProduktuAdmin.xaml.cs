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
using System.Windows.Shapes;
using static Erronka1.Database;
using static Erronka1.MainWindow;

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for ErabiltzaileAdmin.xaml
    /// </summary>
    public partial class ProduktuAdmin : Window
    {
        Articulos artikulu;
        Database datubasea;
        int idUser;
        public ProduktuAdmin(int id, int userId)
        {
            InitializeComponent();
            idUser = userId;
            datubasea = new Database();
            datubasea.ConnectToDatabase();

        }

        private void eguneratu_Click(object sender, RoutedEventArgs e)
        {
                artikulu = new Articulos();
         
            artikulu.Articulo = artikuloa.Text;
            artikulu.Precio = int.Parse(prezioa.Text);
            artikulu.Tipo = tipoa.Text;
            artikulu.Stock = int.Parse(stocka.Text);
            datubasea.sortuProduktu(artikulu);
            MessageBox.Show("Egina");
            var window = new biltegia(idUser);
            this.Close();
            window.ShowDialog();
        }
        private void atzeratu(object sender, RoutedEventArgs e)
        {
            var window = new biltegia(idUser);
            this.Close();
            window.ShowDialog();
        }
    }
}
