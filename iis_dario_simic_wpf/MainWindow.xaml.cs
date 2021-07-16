using iis_dario_simic_wpf.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using System.Xml.Linq;

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


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/appointment");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //tu si stao, znaci saljes xml kao plain text putem JSONA, odradili smo kralju bitan dio, sutra pitaj jeli ima bolji nacin ali ovako je sigurno prolazno
                string f = StringFromRichTextBox(txtXML);

                string xml ="";

                streamWriter.Write(xml);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                lblStatus.Content = result.ToString();
            }




        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            return new TextRange(rtb.Document.ContentStart,
            rtb.Document.ContentEnd).Text;
        }


    }
    
}
