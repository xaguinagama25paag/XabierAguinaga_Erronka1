using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
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
using static Erronka1.MainWindow;

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class biltegia : Window
    {
        List<Articulos> artikuluak;
        List<Articulos> listaBerri;
        List<string> listaIzenak;
        List<Erabiltzaile> a;
        List<Erabiltzaile> listaErabil;
        List<Erabiltzaile> erabiltzaileLista;
        Database datubasea;
        int ide;
        public biltegia(int idea)
        {
            InitializeComponent();
            datubasea = new Database();
            datubasea.ConnectToDatabase();
            ide = idea;
            List<string> tipoak = datubasea.getTipos();
            foreach (var item in tipoak)
            {
                ListBoxItem objetua = new ListBoxItem();
                objetua.Content = item;
                produktuak.Items.Add(objetua);
            }
            artikuluak = datubasea.getArtikuluak();
            a = new List<Erabiltzaile>();
            a.Add(new Erabiltzaile() { Id = 0, Nombre = "a", Contraseña = "a", Rol = "a" });

            erabiltzaileakJarri();
        }
        private void erabiltzaileakJarri()
        {
            listaErabil = new List<Erabiltzaile>();

            erabiltzaileLista = datubasea.getKontuak();

            foreach (var item in erabiltzaileLista)
            {
                item.Contraseña = "******";
                listaErabil.Add(item);
            }
            lista.ItemsSource = a;
            lista.ItemsSource = listaErabil;
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
                    listaIzenak.Add(artikuluak[i].Articulo+" Stock: " + artikuluak[i].Stock);
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
                int canti = int.Parse(Interaction.InputBox("Sartu GEHITU edo KENTZEKO (jartzen "+'"'+"-"+'"'+" hasieran kentzeko casuan) stock-a", "Stocka aldatzen"));
                if (i.Stock + canti >= 0)
                {
                    datubasea.updateStock(i.Id, i.Stock + canti);
                    artikuluak = datubasea.getArtikuluak();
                    ProduktuakAldatu();
                }
                else
                {
                    MessageBox.Show("Ez daude " + i.Articulo + " nahikorik, Eta ezin da jarri 0 baino gutxiagoko stock-a");
                }

            }
        }
        private void salir_Click(object sender, RoutedEventArgs e)
        {
            datubasea.itxiKonekxioa();
            this.Close();
        }
        private void biltegira_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow(ide);
            datubasea.itxiKonekxioa();
            this.Close();
            window.ShowDialog();

        }

        private void editatu(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedIndex!=-1)
            {
                Erabiltzaile erabil = lista.SelectedItem as Erabiltzaile;
                var window = new ErabiltzaileAdmin(erabil.Id, 1, ide);
                this.Close();
                window.ShowDialog();
            }
            else{
                MessageBox.Show("Aukeratu lehenengo erabiltzaile bat mesedez");
            }
           
            
            
        }
        private void sortu(object sender, RoutedEventArgs e)
        {
            var window = new ErabiltzaileAdmin(0, 0, ide);
            this.Close();
            window.ShowDialog();

        }
        private void ezabatu(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedIndex != -1)
            {
                Erabiltzaile erabil = lista.SelectedItem as Erabiltzaile;
                if (erabil.Id == 1 || erabil.Id==ide)
                {
                    MessageBox.Show("ezin da borratu");
                }
                else
                {            
                datubasea.deleteUser(erabil.Id);
                MessageBox.Show("Erabiltzailea ezabatuta");
                erabiltzaileakJarri();
            }
            }else{
                MessageBox.Show("Aukeratu lehenengo erabiltzaile bat mesedez");
            }
        }
    }
}