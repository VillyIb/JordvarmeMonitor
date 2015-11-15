using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using EU.Iamia.Logging;

namespace JordvarmeMonitor
{
    public partial class Form1 : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger("eu.iamia.JordvarmeMonitor.Form1");
        private static readonly ILog BoosterLog = LogManager.GetLogger("Booster");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void XuLogin_Click(object sender, EventArgs e)
        {
            XuWeBrowser.Url = new Uri("https://cmi.ta.co.at/portal/ta/loginformular/");
        }

        private void XuSchema_Click(object sender, EventArgs e)
        {
            XuWeBrowser.Url = new Uri("https://cmi.ta.co.at/webi/CMI004096/schema.html#1");
        }

        private void XuAnalyzeDom_Click(object sender, EventArgs e)
        {
            //XuAnalyzeDom.Enabled = false;
            try
            {
                var t1 = XuWeBrowser.Document;
                var t2 = XuWeBrowser.Document.DomDocument;
                var t3 = XuWeBrowser.DocumentText;
                //var t5 = XuWeBrowser.DocumentCompleted

                mshtml.IHTMLDocument2 iDoc = (mshtml.IHTMLDocument2) XuWeBrowser.Document.DomDocument;

                if (iDoc != null)
                {
                    var t14 = iDoc.body.outerHTML;

                    var t21 = new XmlDocument();
                    var t22 = $"<?xml version=\"1.0\" encoding=\"utf - 8\" ?> {t14}";
                    var t23 = t22.Replace("<br>", "<br />").Replace("png\">", "png\" />");
                    t21.LoadXml(t23);


                    // <div id="pos8"> 1,14 bar  </div>
                    var xp1 = "//div[@id='pos8']";
                    var t33 = t21.DocumentElement.SelectSingleNode(xp1);

                    float pressureIn;
                    var t53 = t33.InnerText.Substring(2).Replace("bar", "");
                    float.TryParse(t53, out pressureIn);

                    // <div id="pos9"> 1,26 bar  </div>
                    var xp2 = "//div[@id='pos9']";
                    var t34 = t21.DocumentElement.SelectSingleNode(xp2);

                    float pressureOut;
                    var t54 = t34.InnerText.Substring(2).Replace("bar", "");
                    float.TryParse(t54, out pressureOut);

                    // <div id="pos10"><a href="javascript:loadChanger('11020C50180');">AUTO<br /> 3,90 V  </a>  </div>
                    var xp3 = "//div[@id='pos10']/a";
                    var t35 = t21.DocumentElement.SelectSingleNode(xp3);

                    float speed;
                    var t55 = t35.InnerXml.Substring(11).Replace("V", "");
                    float.TryParse(t55, out speed);

                    // <div id="pos5">~    0 l/h  </div>
                    float crossFlow;
                    {
                        const string xPath = "//div[@id='pos5']";
                        var xmlNode = t21.DocumentElement.SelectSingleNode(xPath);

                        var value = xmlNode.InnerText.Substring(4).Replace("l/h", "");
                        float.TryParse(value, out crossFlow);
                    }

                    //   <div id="pos2">Inde:   9,5  °C  </div>
                    float tempAirOutdor;
                    {
                        const string xPath = "//div[@id='pos2']";
                        var xmlNode = t21.DocumentElement.SelectSingleNode(xPath);

                        var value = xmlNode.InnerText.Substring(7).Replace("°C", "");
                        float.TryParse(value, out tempAirOutdor);
                    }

                    //   <div id="pos4">Ude:   7,6  °C  </div>
                    float tempAirIndor;
                    {
                        const string xPath = "//div[@id='pos4']";
                        var xmlNode = t21.DocumentElement.SelectSingleNode(xPath);

                        var value = xmlNode.InnerText.Substring(7).Replace("°C", "");
                        float.TryParse(value, out tempAirIndor);
                    }

                    // <div id="pos6">  9,1  °C  </div>
                    float tempBrineIn;
                    {
                        const string xPath = "//div[@id='pos6']";
                        var xmlNode = t21.DocumentElement.SelectSingleNode(xPath);

                        var value = xmlNode.InnerText.Substring(2).Replace("°C", "");
                        float.TryParse(value, out tempBrineIn);
                    }

                    //   <div id="pos7">  6,8  °C  </div>
                    float tempBrineOut;
                    {
                        const string xPath = "//div[@id='pos7']";
                        var xmlNode = t21.DocumentElement.SelectSingleNode(xPath);

                        var value = xmlNode.InnerText.Substring(2).Replace("°C", "");
                        float.TryParse(value, out tempBrineOut);
                    }

                    var msg =
                        $"{DateTime.UtcNow:HH:mm:ss};{speed:F};{(pressureOut - pressureIn)*10:F};{Math.Min(99f, crossFlow)/10f:F1};{pressureIn:F};{pressureOut:F};{tempBrineIn:F1};{tempBrineOut:f1};{tempAirIndor:F1};{tempAirOutdor:F1};{crossFlow}";

                    BoosterLog.Info(msg);

                    var t19 = t14;
                }



                var t4 = t1;
            }
            catch(NullReferenceException)
            { }

            XuAnalyzeDom.Enabled = false;
            System.Threading.Thread.Sleep(500);
            XuAnalyzeDom.Enabled = true;
        }

        private void XuWeBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var t1 = sender.GetType().Name;

            var browser = sender as WebBrowser;

            var ds = browser.DocumentStream;

            ds.Seek(0L, System.IO.SeekOrigin.Begin);

            var sr = new StreamReader(ds);

            var data = sr.ReadToEnd();

            var tx = data;


        }

        private void XuTimer_Tick(object sender, EventArgs e)
        {
            XuAnalyzeDom_Click(sender, e);
        }

        private void XuTimer1_Click(object sender, EventArgs e)
        {
            XuStop.Enabled = true;
            XuTimer1.Enabled = false;
            XuTimer.Start();
        }

        private void XuStop_Click(object sender, EventArgs e)
        {

            XuStop.Enabled = false;
            XuTimer1.Enabled = true;
            XuTimer.Stop();
            
        }
    }
}
