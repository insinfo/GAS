using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
/*
using Noesis.Javascript;
using Jint.Parser;
using Jint.Runtime;
using Jint.Native;
using Jint;
*/
using Npgsql;
using System.IO;

namespace GAS2
{
    public partial class Gas : Form
    {
        public Gas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            //MessageBox.Show("df");
            // Initialize the context

            JavascriptContext context = new JavascriptContext();
            context.Run("var _url= 'http://www.some.url.com';");
            String url = (String)context.GetParameter("_url");
          
            //Debug.WriteLine(url);

            var engine = new Engine();
            JavaScriptParser javascriptParse = new JavaScriptParser();
            string scriptString = (@"var $Page = new function()
            {
                var _url = 'http://www.some.url.com';
                this.Download = function()
                {
                    window.location = _url;
                }
            }
            ");


            var parser = new Jint.Parser.JavaScriptParser();
            var program = parser.Parse(scriptString, new Jint.Parser.ParserOptions { Tokens = true });
            program.Tokens;
            */

            DBHandler dB = new DBHandler();
            BuilderPHP builderPHP = new BuilderPHP();
            dB.Connect();
            var tables = dB.GetAllTables();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            var aplicationName = Util.BestToPascalCase(dB.Scheme);
            var diretorioDeSaida = appPath + @"\Result\";
            var diretorioModelos = diretorioDeSaida + @"Model\";
            var diretorioVo = diretorioModelos + @"Vo\";
            var diretorioControllers = diretorioDeSaida + @"Controllers\";

            if (Directory.Exists(diretorioDeSaida))
            {
                Util.DirectoryEmpty(diretorioDeSaida);
                //Directory.Delete(diretorioDeSaida);
            }
            else
            {
                Directory.CreateDirectory(diretorioDeSaida);
            }           
            Directory.CreateDirectory(diretorioModelos);
            Directory.CreateDirectory(diretorioVo);
            Directory.CreateDirectory(diretorioControllers);
            txtResults.Text = "";
            foreach (string table in tables)
            {
                var columns = dB.GetColumnsName(table);
                var primaryKeys = dB.GetPrimaryKeys(table);

                var model = builderPHP.CreateModel(table, primaryKeys[0].Key, columns);
                var modelCode = model.GetCode();
                var controller = builderPHP.CreateController(model);
                var controllerCode = controller.GetCode();


                var modelFileName = Util.BestToPascalCase(table);
                var controllerFileName = modelFileName + "Controller";

                var nameSpaceModelo = aplicationName + "\\" + "Model" + "\\" + "VO";
                builderPHP.CreateFilePHP(modelFileName, modelCode, diretorioVo, nameSpaceModelo);

                var nameSpaceController = aplicationName + "\\" + "Controller"; 

                //cria os use namespaces do arquivo php controller
                List<string> useNameSpacesContrl = new List<string>();
                useNameSpacesContrl.Add(@"\Slim\Http\Request as Req");
                useNameSpacesContrl.Add(@"\Slim\Http\Response as Resp");
                useNameSpacesContrl.Add(@"\Exception");
                // useNameSpacesContrl.Add(aplicationName+@"\Model\DAL\TipoEquipamentoDAL");
                useNameSpacesContrl.Add(aplicationName + @"\Util\DBLayer");
                useNameSpacesContrl.Add(aplicationName + @"\Util\Utils");
                useNameSpacesContrl.Add(aplicationName + @"\Model\VO\"+ modelFileName);

                builderPHP.CreateFilePHP(controllerFileName, controllerCode, diretorioControllers, nameSpaceController, useNameSpacesContrl);

                txtResults.Text += modelFileName + " | OK" + Environment.NewLine;
                              
            }

            //Process.Start(diretorioDeSaida);
            Process.Start("file://" +diretorioDeSaida);

        }

        private void BtnOpenDesigner_Click(object sender, EventArgs e)
        {
            var formGDI = new FormGDI();
            formGDI.Show();
        }

        private void BtnShowDesigner_Click(object sender, EventArgs e)
        {
            var formChrome = new FormChrome();
            formChrome.Show();
        }
    }
}
