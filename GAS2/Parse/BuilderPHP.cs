using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace GAS2
{
    public class BuilderPHP
    {
        public ClassPHP CreateModel(string name, string primaryKey, List<KeyValuePair<string, string>> fields)
        {
            ClassPHP model = new ClassPHP();          
            model.Name = Util.BestToPascalCase(name);
            model.Properties.Add(new PropertiePHP(AccessTypePHP.Const, "TABLE_NAME", name));
            model.Properties.Add(new PropertiePHP(AccessTypePHP.Const, "KEY_ID", primaryKey, TypePHP.String, "integer"));

            var fieldsArray = "[";
            //adiciona as constantes da tabela
            for (var i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                if (primaryKey != field.Key)
                {
                    var contantName = Util.CamelToUnderscore(Util.BestToCamelCase(field.Key));
                    model.Properties.Add(new PropertiePHP(AccessTypePHP.Const, contantName, field.Key, TypePHP.String, field.Value));
                    if (i < fields.Count - 1)
                    {
                        fieldsArray += "self::" + contantName + ", ";
                    }
                    else
                    {
                        fieldsArray += "self::" + contantName + "";
                    }
                }
            }
            fieldsArray += "]";
            //adiciona um array das constantes
            model.Properties.Add(new PropertiePHP(AccessTypePHP.Const, "TABLE_FIELDS", fieldsArray, TypePHP.Array));
            //adiciona as propriedades 
            foreach (var field in fields)
            {
                model.Properties.Add(new PropertiePHP(AccessTypePHP.Public, Util.BestToCamelCase(field.Key), PropertiePHP.ValueNone));
            }
            //cria os metodos get e set
            foreach (var field in fields)
            {
                var prop = Util.BestToCamelCase(field.Key);
                model.Methods.Add(new MethodPHP(AccessTypePHP.Public, "get" + Util.BestToPascalCase(field.Key), "return $this->" + prop + ";"));
                var parameters = new List<ParameterPHP>(); parameters.Add(new ParameterPHP(prop));
                model.Methods.Add(new MethodPHP(AccessTypePHP.Public, "set" + Util.BestToPascalCase(field.Key), parameters, "$this->" + prop+" = $" + prop + ";"));
            }

            return model;
        }
        public ClassPHP CreateController(ClassPHP model)
        {
            ClassPHP controller = new ClassPHP();
            controller.Name = model.Name + "Controller";
            //cria os parametros 
            var parameters = new List<ParameterPHP>();
            parameters.Add(new ParameterPHP("Req", "request", ""));
            parameters.Add(new ParameterPHP("Resp", "response", ""));

            TemplatesPHP template = new TemplatesPHP();
            //Adiciona o metodo getAll 
            var methodGetAllBody = template.GetAll(model);
            controller.Methods.Add(new MethodPHP(AccessTypePHP.Public, ModifierPHP.Static, "getAll", parameters, methodGetAllBody));
            //Adiciona o metodo save
            var methodSaveBody = template.Save(model);
            controller.Methods.Add(new MethodPHP(AccessTypePHP.Public, ModifierPHP.Static, "save", parameters, methodSaveBody));
            //Adiciona o metodo get
            var methodGetBody = template.Get(model);
            controller.Methods.Add(new MethodPHP(AccessTypePHP.Public, ModifierPHP.Static, "get", parameters, methodGetBody));
            //Adiciona o metodo delete
            var methodDeleteBody = template.Delete(model);
            controller.Methods.Add(new MethodPHP(AccessTypePHP.Public, ModifierPHP.Static, "delete", parameters, methodDeleteBody));

            return controller;
        }

        public void CreateFilePHP(string fileName, string fileContent = "", string path = "", string nameSpace = "", List<string> useNameSpaces = null)
        {
            var content = "<?php " + Environment.NewLine;
            content += "namespace " + nameSpace + ";" + Environment.NewLine + Environment.NewLine;
            if (useNameSpaces != null)
            {
                foreach (var useNameSpace in useNameSpaces)
                {
                    content += "use " + useNameSpace + ";" + Environment.NewLine;
                }
                content += Environment.NewLine;
            }
            content += fileContent;
            File.WriteAllText(path + @"" + fileName + ".php", content);
        }
    }

    public class ClassPHP
    {
        public string Name { get; set; } = "";
        public List<PropertiePHP> Properties { get; set; } = null;
        public List<MethodPHP> Methods { get; set; } = null;
        public ClassPHP()
        {
            Properties = new List<PropertiePHP>();
            Methods = new List<MethodPHP>();
        }
        public string GetCode()
        {
            var open = Environment.NewLine + "{" + Environment.NewLine;
            var close = Environment.NewLine + "}";
            var code = "class " + Name + open;
            foreach (var propert in Properties)
            {
                code += propert.GetCode() + "" + Environment.NewLine;
            }
            foreach (var method in Methods)
            {
                code += method.GetCode() + Environment.NewLine;
            }
            code += close;
            return code;
        }
        public List<string> GetAllProperties(string accessType)
        {
            var result = new List<string>();
            foreach (var propert in Properties)
            {
                if (propert.AccessType == accessType)
                {
                    result.Add(propert.Identifier);
                }
            }
            return result;
        }

        public List<string> ImplodeProperties(string accessType, string separator = ",")
        {
            var result = new List<string>();
            foreach (var propert in Properties)
            {
                if (propert.AccessType == accessType)
                {
                    var prop = "";
                    if (propert.AccessType == AccessTypePHP.Const)
                    {
                        prop = Name + "::" + propert.Identifier;
                    }
                    else
                    {
                        prop = Name + "->" + propert.Identifier;
                    }
                    result.Add(prop);
                }
            }
            return result;
        }
    }

    public class PropertiePHP
    {
        public const string ValueNone = "@ValueNone@";
        public string AccessType { get; set; } = AccessTypePHP.None;
        public TypePHP Type { get; set; } = TypePHP.String;
        public string Identifier { get; set; } = "";
        public string Value { get; set; } = "";
        public string Comment { get; set; } = "";
        public string GetCode()
        {
            if (Type == TypePHP.String)
            {
                Value = "\"" + Value + "\"";
            }
            var dollarSign = " $";
            if (AccessType == AccessTypePHP.Const)
            {
                dollarSign = "";
            }
            var igual = " = ";
            if (Value == "\"" + ValueNone + "\"" || Value == ValueNone)
            {
                Value = "";
                igual = "";
            }
            if (Comment != "")
            {
                Comment = "//" + Comment;
            }
            return AccessType + " " + dollarSign + Identifier + igual + Value + "; " + Comment;
        }
        public PropertiePHP()
        {
        }
        public PropertiePHP(string accessType, string identifier, string value, TypePHP type = TypePHP.String, string comment = "")
        {
            AccessType = accessType;
            Identifier = identifier;
            Value = value;
            Type = type;
            Comment = comment;
        }
    }

    public class MethodPHP
    {
        public string AccessType { get; set; } = AccessTypePHP.None;
        public string Modifier { get; set; } = ModifierPHP.None;
        public string Name { get; set; } = "";
        public List<ParameterPHP> Parameters { get; set; } = null;
        public string Body { get; set; } = "";

        public string GetCode()
        {
            var open = Environment.NewLine + "{" + Environment.NewLine;
            var close = Environment.NewLine + "}";
            var parameters = "";
            if (Parameters != null)
            {
                for (var i = 0; i < Parameters.Count; i++)
                {
                    var parameter = Parameters[i];

                    if (i < Parameters.Count - 1)
                    {
                        parameters += parameter.GetCode() + ",";
                    }
                    else
                    {
                        parameters += parameter.GetCode() + "";
                    }
                }
            }

            return Environment.NewLine + "" + AccessType + " " + Modifier + " function " + Name + " (" + parameters + ")" + open + Body + close;
        }
        public MethodPHP()
        {
            Parameters = new List<ParameterPHP>();
        }

        public MethodPHP(string accessType, string modifier, string name, List<ParameterPHP> parameters, string body)
        {
            AccessType = accessType;
            Modifier = modifier;
            Name = name;
            Parameters = parameters;
            Body = body;
        }
        public MethodPHP(string accessType, string name, List<ParameterPHP> parameters, string body)
        {
            AccessType = accessType;
            Name = name;
            Parameters = parameters;
            Body = body;
        }
        public MethodPHP(string accessType, string name, string body)
        {
            AccessType = accessType;
            Name = name;
            Body = body;
        }
    }

    public class ParameterPHP
    {
        public string Identifier { get; set; } = "";
        public string Value { get; set; } = "";
        public string Type { get; set; } = "";
        public string GetCode()
        {
            if (String.IsNullOrEmpty(Value))
            {
                return Type + " $" + Identifier + "";
            }
            return Type + " $" + Identifier + "=" + Value + "";
        }
        public ParameterPHP(string identifier, string value = "")
        {
            Identifier = identifier;
            Value = value;
        }
        public ParameterPHP(string type, string identifier, string value = "")
        {
            Identifier = identifier;
            Value = value;
            Type = type;
        }
        public ParameterPHP()
        {
        }
    }

    public class AccessTypePHP
    {
        public const string Public = "public";
        public const string Private = "private";
        public const string Protected = "protected";
        public const string Const = "const";
        public const string None = "";
    }

    public class ModifierPHP
    {
        public const string Static = "static";
        public const string None = "";
    }
    public enum TypePHP { String, Integer, Object, Variable, Array, Const }

    public class TypesPHP
    {
        public const string String = "string";
        public const string Integer = "integer";
        public const string Object = "object";
        public const string Variable = "$";
        public const string Array = "Array()";
        public static string Set = "";
    }

}
