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

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Database datubasea;

        public Login()
        {
            InitializeComponent();
            datubasea = new Database();
            datubasea.ConnectToDatabase();
        }

        private void logeatu_Click(object sender, RoutedEventArgs e)
        {
            Erabiltzaile erabiltzailea = datubasea.getKontua(izena.Text, pasahitza.Password.ToString());
            if (erabiltzailea.Id!=-1)
            {
                MessageBox.Show("Kaixo "+erabiltzailea.Nombre);
                var window = new MainWindow(erabiltzailea.Id);
                datubasea.itxiKonekxioa();
                this.Close();
                window.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Pasahitza edo/eta erabiltzailea gaizki daude");
            }
        }
    }
}
