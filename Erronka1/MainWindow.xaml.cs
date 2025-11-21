using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Erronka1.Database;

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Articulos> artikuluak;
        List<Articulos> listaBerri;
        List<string> listaIzenak;
        List<Articulos> a;
        List<Produktu> listaCompra;
        float totala = 0;
        Database datubasea;
        int ide = -1;
        public MainWindow(int idea)
        {

            InitializeComponent();
            datubasea = new Database();
            datubasea.ConnectToDatabase();
            artikuluak = datubasea.getArtikuluak();
            listaCompra = new List<Produktu>();
            a = new List<Articulos>();
            ide = idea;
            a.Add(new Articulos() { Id = 0, Articulo = new Random().Next(0, 100).ToString(), Precio = 1, Tipo = "a" });
            List<string> tipoak = datubasea.getTipos();
            foreach (var item in tipoak)
            {
                ListBoxItem objetua = new ListBoxItem();
                objetua.Content = item;
                produktuak.Items.Add(objetua);
            }
        }

        private void biltegira_Click(object sender, RoutedEventArgs e)
        {
            if ("Administrador" == datubasea.getKontuaId(ide).Rol)
            {
                var window = new biltegia(ide);
                datubasea.itxiKonekxioa();
                this.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ez daukazu beharrezko baimenak biltegira joateko");
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(datubasea.getKontua("Admin", "Admin123").ToString());
            lista.ItemsSource = a;
            lista.ItemsSource = artikuluak;
        }

        private void produktu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProduktuakAldatu();
        }
        private void ProduktuakAldatu()
        {
            listaBerri = new List<Articulos>();
            listaIzenak = new List<string>();
            int i = 0;
            while (i < artikuluak.Count)
            {
                if ("System.Windows.Controls.ListBoxItem: " + artikuluak[i].Tipo == produktuak.SelectedValue.ToString())
                {
                    listaBerri.Add(artikuluak[i]);
                    listaIzenak.Add(artikuluak[i].Articulo);
                }
                i++;
            }
            produktu.ItemsSource = listaIzenak;
        }

        private void produktu_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (produktu.SelectedIndex != -1)
            {
                Articulos i = listaBerri[produktu.SelectedIndex];
                int canti = int.Parse(kalku.calculation);
                if (i.Stock - canti >= 0)
                {
                    datubasea.updateStock(i.Id, i.Stock - canti);
                    artikuluak = datubasea.getArtikuluak();
                    ProduktuakAldatu();
                    float preci = (float)i.Precio / 100;
                    listaCompra.Add(new Produktu() { Articulo = i.Articulo, Precio = preci.ToString() + "€", Cant = "x" + canti, Impuestos = "1%", Importe = (preci * canti).ToString() + "€" });
                    lista.ItemsSource = a;
                    lista.ItemsSource = listaCompra;
                    totala += preci * canti;
                    total.Text = totala + "€";
                }
                else
                {
                    MessageBox.Show("Ez daude "+i.Articulo+" nahikorik, stock: "+i.Stock);
                }
                
            }
        }

        private void gora_Click(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedIndex == -1)
            {
                lista.SelectedIndex = 0;
            }
            else if (lista.SelectedIndex != 0)
            {
                lista.SelectedIndex -= 1;
            }
        }
        private void kendu_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in artikuluak)
            {
                if (item.Articulo == listaCompra[lista.SelectedIndex].Articulo)
                {
                    datubasea.updateStock(item.Id, item.Stock+int.Parse(listaCompra[lista.SelectedIndex].Cant.Substring(1)));
                    listaCompra.RemoveAt(lista.SelectedIndex);
                    artikuluak = datubasea.getArtikuluak();
                    ProduktuakAldatu();
                    break;
                }
            }
            totala -= int.Parse(listaCompra[lista.SelectedIndex].Precio.Substring(0, listaCompra[lista.SelectedIndex].Precio.Length - 1)) * int.Parse(listaCompra[lista.SelectedIndex].Cant.Substring(1));
            total.Text = totala + "€";
            listaCompra.Remove(listaCompra[lista.SelectedIndex]);
            lista.ItemsSource = a;
            lista.ItemsSource = listaCompra;
        }

        private void behera_Click(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedIndex == -1)
            {
                lista.SelectedIndex = 0;
            }
            else
            {
                lista.SelectedIndex += 1;
            }
        }


        public class Articulos
        {
            public int Id { get; set; }
            public string Articulo { get; set; }

            public int Precio { get; set; }

            public string Tipo { get; set; }
            public int Stock { get; internal set; }
        }
        public class Produktu
        {
            public string Articulo { get; set; }

            public string Precio { get; set; }

            public string Cant { get; set; }
            public string Impuestos { get; set; }
            public string Importe { get; set; }
        }
        private void salir_Click(object sender, RoutedEventArgs e)
        {
            datubasea.itxiKonekxioa();
            this.Close();
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(lista, "Print");
                }
            }
        }
    }
}