using iis_dario_simic_wpf.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace iis_dario_simic_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickBtnXML(object sender, RoutedEventArgs e)
        {
          //  lblStatus.Content = "Unknown";
            lblStatus.Foreground = Brushes.Orange;




            Book b = new Book { Name = "nono", NumberOfPages = 3, Price = 6 };


           

            DataContractSerializer serijaliziraj = new DataContractSerializer(typeof(Book));
            MemoryStream podaci = new MemoryStream();
            XmlWriter pisac = XmlWriter.Create(podaci);
            serijaliziraj.WriteObject(pisac, b);
            pisac.Close();

            byte[] podaciZaZahtjev = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(podaci.ToArray()));

            HttpWebRequest zahtjev = (HttpWebRequest)WebRequest.Create("http://localhost:5000/books");
            zahtjev.Method = "POST";          
            zahtjev.Accept = "application/xml";
            zahtjev.ContentType = "application/xml";
            Stream requestData = zahtjev.GetRequestStream();
            requestData.Write(podaciZaZahtjev, 0, podaciZaZahtjev.Length);
            requestData.Close();


            HttpWebResponse odgovor = (HttpWebResponse)zahtjev.GetResponse();
           
            Stream podaciOdgovor = odgovor.GetResponseStream();

            DataContractSerializer deserijaliziraj = new DataContractSerializer(typeof(bool));
            bool uspjesnoDodano = (bool)deserijaliziraj.ReadObject(podaciOdgovor);

            if (uspjesnoDodano)
            {
                lblStatus.Content = "BRAVO";
            }
            else
            {
                lblStatus.Content = "NOOOOO";
            }




        }
    }
    
}
