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
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Microsoft.Identity.Client;
using System.Text.RegularExpressions;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für auftragErfassen.xaml
    /// </summary>
    public partial class auftragErfassen : Window
    {
        
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\TroloDB.mdf;Integrated Security=True");

        void fill_combo()
        {
            try {
                con.Open();
                string Query = "select * from Firmen";
                SqlCommand createCommand = new SqlCommand(Query, con);
                SqlDataReader dr = createCommand.ExecuteReader();
                while (dr.Read()) {
                int fid = dr.GetInt32(0);
                string name = dr.GetString(1);    
                    
                    FirmenAuswahl.Items.Add(name);
                }
                dr.Close();
                
            }
            catch { }
        }
        public auftragErfassen()
        {
            InitializeComponent();
            fill_combo();
            
        }

        private void FirmenAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string test = FirmenAuswahl.SelectedItem.ToString();
                int fidd = 0;
                string sqlstm = "select * from Firmen where Firmenname = '" + test + "'";
                // MessageBox.Show(sqlstm); // Test der SQL Abfrage
                SqlCommand sqlCommand = new SqlCommand(sqlstm, con);
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    fidd = dr.GetInt32(0);
                }
                fid.Text = "FID: " + fidd.ToString();
                dr.Close();
            }
            catch { }


        }

        private void FirmenAuswahl_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void saveAuftrag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               // con.Open();
                SqlCommand save = con.CreateCommand();
                save.CommandType = CommandType.Text;
                String result = Regex.Match(fid.Text, @"\d+").Value;
                int fidnummer = Int32.Parse(result);
                save.CommandText = "insert into Auftraege (FID, Beschreibung, Auftragshoehe) values(@FID, @Beschreibung, @Auftragshoehe)";
                save.Parameters.AddWithValue("@FID", fidnummer);
                save.Parameters.AddWithValue("@Beschreibung", Beschreibung.Text);
                save.Parameters.AddWithValue("@Auftragshoehe", Betrag.Text);
                // MessageBox.Show(result); int extrahierung testen
                save.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show(save.CommandText); SQL Abfrage testen
                MessageBox.Show("Auftrag wurde erfasst!");
                con.Close();
                DialogResult = false;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Con geschlossen");
            con.Close();
        }

        private void Betrag_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9,]+").IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(Betrag.Text);
            }
            catch { }
        }
    }
}
