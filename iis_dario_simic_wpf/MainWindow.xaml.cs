
using iis_dario_simic_wpf.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Xml.Serialization;

namespace iis_dario_simic_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string s1;
        private static string xml_to_validate;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickBtnXML(object sender, RoutedEventArgs e)
        {





        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            return new TextRange(rtb.Document.ContentStart,
            rtb.Document.ContentEnd).Text;
        }

        private void btnXsd_Click(object sender, RoutedEventArgs e)
        {
            ValidateAndInsert("xsd");
        }

        private void btnRng_Click(object sender, RoutedEventArgs e)
        {
            ValidateAndInsert("rng");
        }

        private void ValidateAndInsert(string type)
        {
            try
            {
                CultureInfo arSA = new CultureInfo("de-DE");
                DateTime dt = datetimePicker.DisplayDate;
                WebRequest request = WebRequest.Create("http://localhost:5000/appointments?validation=" + type);
                request.Method = "POST";
                request.ContentType = "application/xml";
                byte[] bytes;
                xml_to_validate = "<DentistAppointment xmlns=\"http://schemas.datacontract.org/2004/07/dario_simic_iis_web_api.Model\"><Dentist><Name>" + txtDoctor.Text + "</Name></Dentist><Patient>" +
     " <Name>" + txtPatient.Text + "</Name>" +
     " </Patient>" +
      "<TimeOfAppointment>" + dt.ToString("s", arSA) + "</TimeOfAppointment>" +
    "</DentistAppointment> ";
                bytes = System.Text.Encoding.ASCII.GetBytes(xml_to_validate);

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    lblStatus.Content = "Validated and Inserted";
                }
            }
            catch (Exception xe)
            {
                lblStatus.Content = xe.Message;
            }
        }

        private void btnAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CultureInfo arSA = new CultureInfo("de-DE");
                WebRequest request = WebRequest.Create("http://localhost:5000/AppointmentService.asmx");
                request.Method = "POST";
                request.ContentType = "application/xml";
                string s;

                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
"<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
  "<soap:Body>" +
    "<GetAppointments xmlns=\"http://tempuri.org/\">" +
      "<date>" + txtTest.Text + "</date>" +
    "</GetAppointments>" +
  "</soap:Body>" +
"</soap:Envelope>");

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        s1 = reader.ReadToEnd();
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.LoadXml(s1);
                        XmlNodeList nodes = xdoc.GetElementsByTagName("DentistAppointment");

                        XmlNode node = nodes.Item(0);
                        rtb.Document.Blocks.Clear();
                        rtb.Document.Blocks.Add(new Paragraph(new Run(node.OuterXml.ToString())));




                    }
                }

            }

            catch (Exception xe)
            {


            }

        }

        private void btnValidateJAXB_Click(object sender, RoutedEventArgs e)
        {

            WebRequest request = WebRequest.Create("http://localhost:8080/iis_dario_simic_java/JaxbValidation");
            request.Method = "POST";
            request.ContentType = "text/plain";

            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(s1);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                lblStatusSoap.Content = "Valid";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer =
        new XmlSerializer(typeof(Grad));

            Grad i;
            string s = "<methodCall><methodName>Vrijeme.podaciOGradu</methodName><params><param><value><string>" + txtGrad.Text + "</string></value></param></params></methodCall>";

            WebRequest request = WebRequest.Create("http://localhost:1312");
            request.Method = "POST";
            request.ContentType = "text/plain";

            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(s);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string grad_xml = reader.ReadToEnd();
                    XmlDocument xxx = new XmlDocument();
                    xxx.LoadXml(grad_xml);
                    XmlNodeList nodes = xxx.GetElementsByTagName("Grad");
                    XmlNode node = nodes.Item(0);
                    XDocument x2 = XDocument.Parse(node.OuterXml);
                    x2.Save("C:\\Projects Algebra\\Algebra_IIS\\iis_dario_simic_web_api\\dario_simic_iis_web_api\\temp\\tempGrad.xml");
                }


                using (Stream reader = new FileStream("C:\\Projects Algebra\\Algebra_IIS\\iis_dario_simic_web_api\\dario_simic_iis_web_api\\temp\\tempGrad.xml", FileMode.Open))
                {
                    i = (Grad)serializer.Deserialize(reader);
                    lblGrad.Content = i.ToString();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:5000/api/openuv?lat=" + txtLat.Text + "&lan=" + txtLan.Text + "&f=" + fromuv.Text + "&to=" + touv.Text;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            Console.WriteLine("Response stream received.");
            string s = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            
            //deserijalizacija???
            Result res = JsonConvert.DeserializeObject<Result>(s);
            lblResult.Content = res.to_time;
        }
    }
}
