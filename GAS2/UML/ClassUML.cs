using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using AngleSharp;
using AngleSharp.Dom.Html;

namespace GAS
{
    public class ClassUML : ElementUML
    {
        public VisibilityUML Visibility { get; set; } = VisibilityUML.Public;
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public bool IsAbstract { get; set; } = false;
        public string InheritingFrom { get; set; } = null;//herda de
        public string DependentOn { get; set; } = null;//depende de
        public string Documentation { get; set; } = "";
        public ClassUML()
        {       
            this.Id = Guid.NewGuid().ToString();
        }
       
    }

    public class PropertyUML
    {
        public VisibilityUML Visibility { get; set; } = VisibilityUML.Public;
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string DefaultValue { get; set; } = "";
        public bool IsStatic { get; set; } = false;
        public bool IsReadOnly { get; set; } = false;
        public bool IsUnique { get; set; } = false;
        public bool IsPrimaryKey { get; set; } = false;
        public bool IsForeignKey { get; set; } = false; 
        public DataTypeUML DataType { get; set; } = DataTypeUML.String;
        public string Documentation { get; set; } = "";
        public ConnectionUML ConnectedTo { get; set; } = null;
        public PropertyUML()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
    public class MethodUML
    {
        public VisibilityUML Visibility { get; set; } = VisibilityUML.Public;
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public DataTypeUML ReturnType { get; set; } = DataTypeUML.Void;
        public List<PropertyUML> Parameters { get; set; } = null;
        public string Documentation { get; set; } = "";
        public MethodUML()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
    public class ConnectionUML
    {
        public string Id { get; set; } = "";
        public string IdFrom { get; set; } = "";
        public string IdTo { get; set; } = "";
        public ConnectionUML()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
    public enum VisibilityUML {Public,Protected,Private}
    public enum DataTypeUML {Void, Integer, String, Bool, Double,Byte,Date,DateTime,TimeStamp }
}
