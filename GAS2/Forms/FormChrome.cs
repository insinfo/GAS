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


namespace GAS
{
    public partial class FormChrome : Form
    {
        public FormChrome()
        {
            InitializeComponent();
            /*
            string html = Util.OpenTextFile("Resources/umlCanvas.html");
            BrowserView browserView = new WinFormsBrowserView();
            Controls.Add((Control)browserView);

            Browser browser = browserView.Browser;
            
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
                    //the EventArgs DOM does not have the mouse position
                    body.AddEventListener(DOMEventType.OnClick, delegate (object se, DOMEventArgs ev)
                    {
                        //I'm using a function in javascript to get the current mouse position, 
                        //plus this has a devastating side effect, high overhead and low performace
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
                            //here again I use a javascript function to set the position of the element because the "dotnetbrowser"
                            //does not have an access method to the style object and there I can not define the position of the element via css
                            browser.ExecuteJavaScript("setElementPosition('bloco',"+ dx+","+ dy+")");                                                    
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
            };          
           
            browser.LoadHTML(html);
            //waitEvent.WaitOne();
            */
    
        }

        private void FormChrome_MouseDown(object sender, MouseEventArgs e)
        {
            Console.Out.WriteLine("clicou");          
        }

        private void FormChrome_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.Out.WriteLine("moveu");
        }

        private void FormChrome_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
