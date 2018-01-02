using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS
{
    public class ElementUML
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int MinWidth { get; set; } = 150;
        public int MinHeight { get; set; } = 200;
        public ColorUML BackgroundColor { get; set; } = ColorUML.FromRGB(30, 110, 255);
        public ColorUML ForegroundColor { get; set; } = ColorUML.FromRGB(255, 255, 255);
        public ColorUML BorderColor { get; set; } = null;
        public FontUML Font { get; set; } = FontUML.Arial();
        public int Scale { get; set; } = 1;
        
    }

    public class ColorUML
    {
        public byte R { get; set; } = 255;
        public byte G { get; set; } = 255;
        public byte B { get; set; } = 255;
        public byte A { get; set; } = 255;

        public ColorUML(byte r , byte g , byte b , byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public ColorUML(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = 255;
        }

        public ColorUML()
        {            
        }

        public static ColorUML FromRGB(byte r, byte g, byte b)
        {
            return new ColorUML(r, g, b, 255);
        }
        public static ColorUML FromRGBA(byte r, byte g, byte b, byte a)
        {
            return new ColorUML(r, g, b, a);
        }
    }

    public class FontUML
    {
        public string Name { get; set; } = "Arial";
        public int Size { get; set; } = 15;
        public FontStyleUML FontStyle { get; set; } = FontStyleUML.Normal;
        public FontUML(string name, int size, FontStyleUML fontStyle)
        {
            this.Name = name;
            this.Size = size;
            this.FontStyle = fontStyle;
        }
        public FontUML()
        {
        }
        public static FontUML Arial()
        {
            return new FontUML("Arial",15, FontStyleUML.Normal);
        }
    } 
    public enum FontStyleUML { Normal, Italic }
}
