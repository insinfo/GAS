using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace GAS2
{
    public class Util
    {
        public static string CamelToUnderscore(string strCamelCase)
        {
            //str.replace(/ ([a - z])([A - Z]) /, '$1_$2').toUpperCase();
            string result = (Regex.Replace(strCamelCase, "(?<=.)([A-Z])", "_$0", RegexOptions.Compiled)).ToUpper();
            return result;
        }
        public static string UnderscoreToCamel(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains("_"))
            {
                return name;
            }
            string[] array = name.Split('_');
            for (int i = 0; i < array.Length; i++)
            {
                string s = array[i];
                string first = string.Empty;
                string rest = string.Empty;
                if (s.Length > 0)
                {
                    first = Char.ToUpperInvariant(s[0]).ToString();
                }
                if (s.Length > 1)
                {
                    rest = s.Substring(1).ToLowerInvariant();
                }
                array[i] = first + rest;
            }
            string newname = string.Join("", array);
            if (newname.Length > 0)
            {
                newname = Char.ToLowerInvariant(newname[0]) + newname.Substring(1);
            }
            else
            {
                newname = name;
            }
            return newname;
        }
        public static string ToPascalCase(string str)
        {
            /*
            "WARD_VS_VITAL_SIGNS".ToPascalCase() // WardVsVitalSigns
            "Who am I?".ToPascalCase()); // WhoAmI
            "I ate before you got here".ToPascalCase()); // IAteBeforeYouGotHere
            "Hello|Who|Am|I?".ToPascalCase()); // HelloWhoAmI
            "Live long and prosper".ToPascalCase()); // LiveLongAndProsper
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.".ToPascalCase()); // LoremIpsumDolorSitAmetConsecteturAdipiscingElit
             */
            // Replace all non-letter and non-digits with an underscore and lowercase the rest.
            string sample = string.Join("", str?.Select(c => Char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            // Split the resulting string by underscore
            // Select first character, uppercase it and concatenate with the rest of the string
            var arr = sample?
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            // Join the resulting collection
            sample = string.Join("", arr);

            return sample;
        }
        public static string ToCamelCase(string str)
        {
            return LowerCaseFirst(ToPascalCase(str));
        }
        public static string LowerCaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToLower(a[0]);
            return new string(a);
        }
        public static string UpperCaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        List<String> AddToList(DataTable dtData)
        {
            List<string> str = new List<string>();
            foreach (DataRow row in dtData.Rows)
            {
                foreach (DataColumn Col in dtData.Columns)
                {
                    str.Add(row[Col].ToString());
                }
            }
            return str;
        }
        public static bool JIsCamelCase(string str)
        {
            string[] strArr = str.Split(' ');
            var estring = "";
            for (var i = 0; i <= strArr.Length; i++)
            {
                if (strArr[i].ToUpper() == strArr[i])
                {
                    estring += "-" + strArr[i].ToLower();
                }
                else
                {
                    estring += strArr[i];
                }
            }

            if (ToCamelCase(estring) == str)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsCamelCase(string s)
        {
            return (s != s.ToLower() && s != s.ToUpper());
        }
        public static string BestToPascalCase(string name)
        {
            var nome = "";
            if (Util.IsCamelCase(name))
            {
                nome = name;
            }
            else if (name.ToLower().Contains('_'))
            {
                nome = Util.UnderscoreToCamel(name);
            }
            else
            {
                nome = Util.ToPascalCase(name);
            }
           
            return Util.UpperCaseFirst(nome);
        }
        public static string BestToCamelCase(string name)
        {
            var nome = "";
            if (Util.IsCamelCase(name))
            {
                nome = name;
            }
            else if (name.ToLower().Contains('_'))
            {
                nome = Util.UnderscoreToCamel(name);
            }
            else
            {
                nome = Util.ToPascalCase(name);
            }

            return Util.LowerCaseFirst(nome);
        }

        public static void DirectoryEmpty(string directoryPath)
        {
            System.IO.DirectoryInfo directory = new DirectoryInfo(directoryPath);
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        public static bool LaunchFolderView(string p_Filename)
        {
            bool l_result = false;

            // Check the file exists
            if (File.Exists(p_Filename))
            {
                // Check the folder we get from the file exists
                // this function would just get "C:\Hello" from
                // an input of "C:\Hello\World.txt"
                string l_folder = "";// FileSystemHelpers.GetPathFromQualifiedPath(p_Filename);

                // Check the folder exists
                if (Directory.Exists(l_folder))
                {
                    try
                    {
                        // Start a new process for explorer
                        // in this location     
                        ProcessStartInfo l_psi = new ProcessStartInfo();
                        l_psi.FileName = "explorer";
                        l_psi.Arguments = string.Format("/root,{0} /select,{1}", l_folder, p_Filename);
                        l_psi.UseShellExecute = true;

                        Process l_newProcess = new Process();
                        l_newProcess.StartInfo = l_psi;
                        l_newProcess.Start();

                        // No error
                        l_result = true;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error in 'LaunchFolderView'.", exception);
                    }
                }
            }

            return l_result;
        }

        public static string GetEmbeddedResource(string ns, string res)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}", ns, res))))
            {
                return reader.ReadToEnd();
            }
        }

        public static void CreateTextFileAdv(string fileContent,string fileName,string extencion, string directory)
        {
            string path = directory+"/"+ fileName +"."+ extencion;

            //if (!File.Exists(path))
            //{
                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(fileContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
           // }
        }

        public static string OpenTextFile(string filePath)
        {
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(filePath))
            {
                //string s = "";
                /*while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }*/
                return sr.ReadToEnd();
            }
            //return "";
        }

    }

    public static class StringExtensions
    {
        public static string ToPascalCase(this string str)
        {
            // Replace all non-letter and non-digits with an underscore and lowercase the rest.
            string sample = string.Join("", str?.Select(c => Char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            // Split the resulting string by underscore
            // Select first character, uppercase it and concatenate with the rest of the string
            var arr = sample?
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            // Join the resulting collection
            sample = string.Join("", arr);

            return sample;
        }
    }

}
