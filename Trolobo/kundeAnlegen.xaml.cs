using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Data;
using System.Data.SqlClient;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für kundeAnlegen.xaml
    /// </summary>
    public partial class kundeAnlegen : Window
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\TroloDB.mdf;Integrated Security=True");
        public kundeAnlegen()
        {  
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand save = con.CreateCommand();
                save.CommandType = CommandType.Text;
                save.CommandText = "insert into Firmen values(@firmenname, @strasse, @plz, @postfach, @ort)";
                save.Parameters.AddWithValue("@firmenname", FirmenName.Text);
                save.Parameters.AddWithValue("@strasse", Strasse.Text);
                save.Parameters.AddWithValue("@plz", plz.Text);
                save.Parameters.AddWithValue("@postfach", Postfach.Text);
                save.Parameters.AddWithValue("@ort", Ort.Text);
                int temp = save.ExecuteNonQuery();
                //MessageBox.Show(save.CommandText);
                //MessageBox.Show(temp.ToString());
               
                con.Close();
                MessageBox.Show("Kunde wurde angelegt!");
                DialogResult = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
        }

    }
}
