using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using EU.Iamia.Logging;
using JordvarmeMonitor.Businesslayer;

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
            Measurements = new List<ExtractData>(2);
        }

        private void XuLogin_Click(object sender, EventArgs e)
        {
            XuWeBrowser.Url = new Uri("https://cmi.ta.co.at/portal/ta/loginformular/");
        }

        private void XuSchema_Click(object sender, EventArgs e)
        {
            XuWeBrowser.Url = new Uri("https://cmi.ta.co.at/webi/CMI004096/schema.html#1");
        }

       private void oldAnalyze()
        {
            //XuAnalyzeDom.Enabled = false;
            try
            {
                var t1 = XuWeBrowser.Document;
                var t2 = XuWeBrowser.Document.DomDocument;
                var t3 = XuWeBrowser.DocumentText;
                //var t5 = XuWeBrowser.DocumentCompleted

                mshtml.IHTMLDocument2 iDoc = (mshtml.IHTMLDocument2)XuWeBrowser.Document.DomDocument;

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
                        $"{DateTime.UtcNow:HH:mm:ss};{speed:F};{(pressureOut - pressureIn) * 10:F};{Math.Min(99f, crossFlow) / 10f:F1};{pressureIn:F};{pressureOut:F};{tempBrineIn:F1};{tempBrineOut:f1};{tempAirIndor:F1};{tempAirOutdor:F1};{crossFlow}";

                    BoosterLog.Info(msg);

                    var t19 = t14;
                }



                var t4 = t1;
            }
            catch (NullReferenceException)
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


        private List<ExtractData> Measurements { get; set; }

        private void Analyze()
        {
            try
            {
                var iDoc = (mshtml.IHTMLDocument2) XuWeBrowser.Document.DomDocument;
                if (iDoc != null)
                {
                    var ed = new ExtractData {OuterHtmlSource = iDoc.body.outerHTML};
                    if (ed.TryParse())
                    {
                        if (ed.TryAnalyzeXml())
                        {
                            if (ed.IsValid())
                            {
                                Measurements.Add(ed);
                                if (Measurements.Count >= 2)
                                {
                                    if (!(Measurements[0].Equals(Measurements[1])))
                                    {
                                        // Measurement changed

                                        var current = Measurements[1];


                                        var fp = new CultureInfo("da");
                                        

                                        BoosterLog.InfoFormat(
                                            fp
                                            ,"{0};{1,6:F};{2,6:F};{3,6:F1};{4,6:F};{5,6:F};{6,6:F1};{7,6:F1};{8,6:F1};{9,6:F1};{10,6};"
                                            , current.TimeStamp
                                            , current.BoosterSpeed
                                            , (current.PressureOut - current.PressureIn)*10
                                            , current.CrossFlow/10f
                                            , current.PressureIn
                                            , current.PressureOut
                                            , current.TempBrineIn
                                            , current.TempBrineOut
                                            , current.TempAirIndoor
                                            , current.TempAirOutdoor
                                            , current.CrossFlow
                                            );
                                    }

                                    // strip off older measurements
                                    while (Measurements.Count > 1)
                                    {
                                        Measurements.RemoveAt(0);
                                    }
                                }
                            }
                            else
                            {
                                // ranges outside limits
                                Logger.WarnFormat("Measuremen outside limits: {0}", ed);
                            }
                        }
                        else
                        {
                            // unable to analyze Xml
                            // aleady logged by exception.
                        }
                    }
                    else
                    {
                        // unable to parse 
                        Logger.WarnFormat("Unable to parse Xml: \r\n{0}\r\n", iDoc.body.outerHTML);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("",ex);
            }
        }


        private void XuAnalyzeDom_Click(object sender, EventArgs e)
        {
            Analyze();
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
