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

namespace JordvarmeMonitor
{
    public partial class Form1 : Form
    {
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
            var t1 = XuWeBrowser.Document;
            var t2 = XuWeBrowser.Document.DomDocument;
            var t3 = XuWeBrowser.DocumentText;
            //var t5 = XuWeBrowser.DocumentCompleted

            mshtml.IHTMLDocument2 iDoc = (mshtml.IHTMLDocument2)XuWeBrowser.Document.DomDocument;

            if (iDoc != null)
            {
                var t11 = iDoc.body.document;
                var t12 = iDoc.body.children;
                var t13 = iDoc.body.innerHTML;
                var t14 = iDoc.body.outerHTML;

                var t21 = new XmlDocument();
                var t22 = String.Format("<?xml version=\"1.0\" encoding=\"utf - 8\" ?> {0}", t14);
                var t23 = t22.Replace("<br>", "<br />").Replace("png\">", "png\" />");
                t21.LoadXml(t23);


                // <div id="pos8"> 1,14 bar  </div>
                var xp1 = "//div[@id='pos8']";
                var t33 = t21.DocumentElement.SelectSingleNode(xp1);

                float t43;
                var t53 = t33.InnerText.Substring(2).Replace("bar", "");
                float.TryParse(t53, out t43);

                // <div id="pos9"> 1,26 bar  </div>
                var xp2 = "//div[@id='pos9']";
                var t34 = t21.DocumentElement.SelectSingleNode(xp2);

                float t44;
                var t54 = t34.InnerText.Substring(2).Replace("bar", "");
                float.TryParse(t54, out t44);

                // <div id="pos10"><a href="javascript:loadChanger('11020C50180');">AUTO<br /> 3,90 V  </a>  </div>
                var xp3 = "//div[@id='pos10']/a";
                var t35 = t21.DocumentElement.SelectSingleNode(xp3);

                float t45;
                var t55 = t35.InnerXml.Substring(11).Replace("V", "");
                float.TryParse(t55, out t45);

                // <div id="pos5">~    0 l/h  </div>
                var xp4 = "//div[@id='pos5']";
                var t36 = t21.DocumentElement.SelectSingleNode(xp4);

                float t46;
                var t56 = t36.InnerText.Substring(4).Replace("l/h", "");
                float.TryParse(t56, out t46);

                var msg = String.Format(
                    "{0:yyyy-MM-dd HH:mm:ss};{1};{2};{3};{4};{5}"
                    , DateTime.UtcNow
                    , t43
                    , t44
                    , t45
                    , t46
                    , t44-t43
                );

                var t19 = t14;
            }



            var t4 = t1;

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
    }
}
