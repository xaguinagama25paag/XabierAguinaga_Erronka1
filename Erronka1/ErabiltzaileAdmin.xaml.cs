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
    /// Interaction logic for ErabiltzaileAdmin.xaml
    /// </summary>
    public partial class ErabiltzaileAdmin : Window
    {
        Erabiltzaile userra;
        Database datubasea;
        int idUser;
        int akzioa;
        public ErabiltzaileAdmin(int id, int akzioa, int userId)
        {
            InitializeComponent();
            idUser = userId;
            this.akzioa = akzioa;
            datubasea = new Database();
            datubasea.ConnectToDatabase();
            if (akzioa==0)
            {
                titulua.Text += "sortzen";
            }
            else
            {
                userra = datubasea.getKontuaId(id);
                titulua.Text += "Editatzen";
                izen.Text = userra.Nombre;
                pasahitz.Password = userra.Contraseña;
                if (userra.Rol=="Administrador")
                {
                    rol.SelectedIndex = 1;
                }
            }

        }

        private void eguneratu_Click(object sender, RoutedEventArgs e)
        {
            if (userra==null)
            {
                userra = new Erabiltzaile();
            }
                userra.Nombre = izen.Text;
            userra.Contraseña = pasahitz.Password.ToString();
           
                if (rol.SelectedIndex==1) {
                userra.Rol = "Administrador";
            }
            else
            {
                userra.Rol = "Usuario";
            }
            if (akzioa==0)
            {
                datubasea.sortuUser(userra);
            }
            else
            {
                datubasea.updateUser(userra);
            }
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
