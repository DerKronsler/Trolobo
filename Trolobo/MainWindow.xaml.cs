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
// using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Data.SqlClient.SqlConnection con;

            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\TroloDB.mdf;Integrated Security=True";
            con.Open();
            InitializeComponent();
            if (con.State.ToString() == "Open") {
                string test = "Die Verbindung zum Datenserver wurde hergestellt.";
                conState.Text = test;
            }
            con.Close();

            
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kundeAnlegen kundeAnlegen = new kundeAnlegen();
           // Visibility = Visibility.Hidden;
            
            kundeAnlegen.ShowDialog();
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            auftragErfassen auftragErfassen = new auftragErfassen();
            auftragErfassen.ShowDialog();
        }

        private void kundenverwaltung_Click(object sender, RoutedEventArgs e)
        {

            // Close();

            Visibility = Visibility.Collapsed;
            kundeVerwalten kundeVerwalten1 = new kundeVerwalten();
            kundeVerwalten1.Show();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // DialogResult = false;
           Application.Current.Shutdown();
        }

        private void Haupt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                ImageBrush nordkorea = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(
                       "C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\korea.jpg"));
                nordkorea.ImageSource = image.Source;
                KundeAnlegen.Background = nordkorea;

                btn_kundeanlegen.Content = "Kamerad anlegen";
                btn_auftragerfassen.Content = "Atombomben bauen";
                kundenverwaltung.Content = "Kameraden verwalten";
                Unterschrift.Text = "Sponsored by the supreme leader";
                Unterschrift.HorizontalAlignment = HorizontalAlignment.Right;


                ImageBrush nordkoreaflagge = new ImageBrush();
                Image image2 = new Image();
                Logo.Source = new BitmapImage(
                    new Uri(
                       "C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\kim.jpg"));
                nordkoreaflagge.ImageSource = image2.Source;
                
            }
            if (e.Key == Key.F11) {

                SolidColorBrush brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF272727");
                KundeAnlegen.Background = brush;



                
                btn_kundeanlegen.Content = "Kunden anlegen";
                btn_auftragerfassen.Content = "Auftrag erfassen";
                kundenverwaltung.Content = "Kundenverwaltung";
                Unterschrift.Text = "Datenverwaltungssystem";
                Unterschrift.HorizontalAlignment = HorizontalAlignment.Left;

                Image image3 = new Image();
                Logo.Source = new BitmapImage(
                    new Uri(
                       "C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\logo-transparent-png.png"));
                
            }
        }
    }
        
}
