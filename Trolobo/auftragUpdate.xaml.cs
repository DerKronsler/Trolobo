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
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für auftragUpdate.xaml
    /// </summary>
    public partial class auftragUpdate : Window
    {
        public int aidPub;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\TroloDB.mdf;Integrated Security=True");
        public auftragUpdate(int aid)

        {
            InitializeComponent();
            aidPub = aid;
            con.Open();
            string sqlstm = "select * from Auftraege where AID = '" + aid + "'";
            // MessageBox.Show(sqlstm); // Test der SQL Abfrage
            SqlCommand sqlCommand = new SqlCommand(sqlstm, con);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                Beschreibung.Text = dr.GetString(2);
                Auftragshoehe.Text = dr.GetString(3);
                fid.Text = "FID: " + dr.GetInt32(1).ToString();
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
                update.CommandText = "update Auftraege set Beschreibung = @beschreibung, Auftragshoehe = @auftragshoehe where AID = " + aidPub;
                update.Parameters.AddWithValue("@beschreibung", Beschreibung.Text);
                update.Parameters.AddWithValue("@auftragshoehe", Auftragshoehe.Text);

                update.ExecuteNonQuery();
                MessageBox.Show("Kunde wurde aktualisiert!");
                con.Close();
                this.Close();
            }
            catch { }
        }

        private void Auftragshoehe_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9,]+").IsMatch(e.Text);
        }
    }
}
