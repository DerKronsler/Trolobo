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
using System.Data.SqlClient;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für KundenUpdate.xaml
    /// </summary>
    public partial class KundenUpdate : Window
    {
        public int uebergabe;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\TroloDB.mdf;Integrated Security=True");
        public KundenUpdate(int test)
        {
            InitializeComponent();
            uebergabe = test;
            con.Open();
            string sqlstm = "select * from Firmen where FID = '" + test + "'";
            // MessageBox.Show(sqlstm); // Test der SQL Abfrage
            SqlCommand sqlCommand = new SqlCommand(sqlstm, con);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                Firmenname.Text = dr.GetString(1);
                Strasse.Text = dr.GetString(2);
                plz.Text = dr.GetString(3);
                Postfach.Text = dr.GetString(4);
                Ort.Text = dr.GetString(5);
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand update = con.CreateCommand();
                update.CommandType = System.Data.CommandType.Text;
                update.CommandText = "update Firmen set Firmenname = @firmenname, Strasse = @Strasse, PLZ = @plz, Postfach = @postfach, Ort = @Ort where FID = " + uebergabe;
                update.Parameters.AddWithValue("@firmenname", Firmenname.Text);
                update.Parameters.AddWithValue("@strasse", Strasse.Text);
                update.Parameters.AddWithValue("@plz", plz.Text);
                update.Parameters.AddWithValue("@postfach", Postfach.Text);
                update.Parameters.AddWithValue("@ort", Ort.Text);
                update.ExecuteNonQuery();
                MessageBox.Show("Kunde wurde aktualisiert!");
                con.Close();
                DialogResult = false;
            }
            catch { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
