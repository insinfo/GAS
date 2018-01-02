using AngleSharp;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using DotNetBrowser;
using DotNetBrowser.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAS2.UML
{
    class CanvasUML : UserControl
    {
        public List<ElementUML> Elements = new List<ElementUML>();
        public ElementUML ElementSelected = null;
        private IHtmlDocument dom = null;
        private Browser browser = null;

        public CanvasUML()
        {
            //carrega o renderizador HTML
            BrowserView browserView = new WinFormsBrowserView();
            Controls.Add((Control)browserView);
            browser = browserView.Browser;
            
            var correntPath = Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(correntPath);//.Replace(@"\", "/");      
            var filePath = directory + @"/Resources/umlCanvas.html";

            //var config = Configuration.Default.WithDefaultLoader();
            //var dom = BrowsingContext.New(config).OpenAsync(filePath);
            var config = Configuration.Default;
            config.WithDefaultLoader();
            var parser = new HtmlParser(config);
            dom = parser.Parse(Util.OpenTextFile(filePath));
            //Debug.WriteLine("construtor");
            browser.LoadHTML(dom.Source.Text);
        }
        private void UpdateView()
        {
            browser.Reload();
        }

        public void DrawObjects()
        {
            var config = Configuration.Default.WithCss();
            var parser = new HtmlParser(config);
            var dom = parser.Parse(@"<div class='draggableElement elementUMLContainer' style='                           
                            width: auto; height: auto;
                            min - width: 150px;
                            min - height: 200px;
                            display: block; position: absolute;
                            z - index: 10; top: 50px; left: 50px;
                            color:#fff; margin: 0; padding: 0;
                            border - radius: 10px;
                            overflow: hidden;
                            '></div>");
            var elementUMLContainer = (IHtmlElement)dom.QuerySelector("div");
            //var style = elementUMLContainer.GetAttribute("style");
            elementUMLContainer.Style.Background = "rgb(30, 110, 255)";
            elementUMLContainer.AppendChild(dom.CreateElement("p"));
            //elementUMLContainer.OuterHtml;
        }

        public void AddElement(ElementUML element)
        {
            Elements.Add(element);           
        }
    }
}
