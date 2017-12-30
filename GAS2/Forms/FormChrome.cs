using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetBrowser;
using DotNetBrowser.WinForms;
using System.Threading;
using DotNetBrowser.Events;
using DotNetBrowser.DOM;
using DotNetBrowser.DOM.Events;
using System.Diagnostics;
using System.Reflection;

namespace GAS2
{
    public partial class FormChrome : Form
    {
        public FormChrome()
        {
            InitializeComponent();

            string html = Util.OpenTextFile("Resources/umlCanvas.html");//Util.GetEmbeddedResource("GAS2", "UMLjs.html");
            BrowserView browserView = new WinFormsBrowserView();
            Controls.Add((Control)browserView);

            Browser browser = browserView.Browser;//BrowserFactory.Create();
            /*
            //ManualResetEvent waitEvent = new ManualResetEvent(false);
            browser.FinishLoadingFrameEvent += delegate (object sender, FinishLoadingEventArgs e)
            {
                // Wait until main document of the web page is loaded completely.
                if (e.IsMainFrame)
                {
                    DOMDocument document = e.Browser.GetDocument();
                    DOMNode body = document.GetElementsByTagName("body")[0];
                    double mouseStartPosX = 0;
                    double mouseStartPosY = 0;
                    bool isDragging = false;
                    DOMElement elementSelected = null;

                    body.AddEventListener(DOMEventType.OnClick, delegate (object se, DOMEventArgs ev)
                    {/*
                        JSValue mousePosFunc = browser.ExecuteJavaScriptAndReturnValue("getMousePos();");
                        mouseStartPosX = mousePosFunc.AsObject().GetProperty("x").GetNumber();
                        mouseStartPosY = mousePosFunc.AsObject().GetProperty("y").GetNumber();
                       
                        elementSelected = (DOMElement)ev.Target;
                        if (elementSelected.GetAttribute("id") == "bloco")
                        {
                            isDragging = true;
                        }
                       
                    }, false);


                    body.AddEventListener(DOMEventType.OnMouseMove, delegate (object se, DOMEventArgs ev)
                    {
                        /*
                        // get the current mouse position
                        JSValue mousePosFunc = browser.ExecuteJavaScriptAndReturnValue("getMousePos();");
                        double mousePosX = mousePosFunc.AsObject().GetProperty("x").GetNumber();
                        double mousePosY = mousePosFunc.AsObject().GetProperty("y").GetNumber();                       

                        // calculate the distance the mouse has moved
                        // since the last mousemove
                        var dx = (int)(mousePosX - mouseStartPosX);
                        var dy = (int)(mousePosY - mouseStartPosY);

                        if (isDragging)
                        {
                            //setElementPosition(idElement,x,y)
                            browser.ExecuteJavaScript("setElementPosition('bloco',"+ dx+","+ dy+")");
                            //elementSelected.TextContent = "x "+dx;
                            //elementSelected.Y += dy;                          
                        }

                        // reset the starting mouse position for the next mousemove
                        mouseStartPosX = mousePosX;
                        mouseStartPosY = mousePosY;
                        
                        
                    }, false);

                    body.AddEventListener(DOMEventType.OnMouseUp, delegate (object se, DOMEventArgs ev)
                    {
                        isDragging = false;
                    }, false);

                    //waitEvent.Set();
                }
            };*/
            var correntPath = Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(correntPath).Replace(@"\", "/");
            //AppDomain.CurrentDomain.BaseDirectory
            var filePath = directory + @"/Resources/umlCanvas.html";
            Debug.WriteLine(filePath);
            browser.LoadURL(filePath);
            //waitEvent.WaitOne();


        }

        private void FormChrome_MouseDown(object sender, MouseEventArgs e)
        {
            Console.Out.WriteLine("clicou");
        }

        private void FormChrome_MouseMove(object sender, MouseEventArgs e)
        {
            Console.Out.WriteLine("moveu");
        }

        private void FormChrome_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
