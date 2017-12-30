using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;

namespace My
{
    public static class Computer
    {
        public readonly static Audio Audio = new Audio();
        public readonly static Clock Clock = new Clock();
        public readonly static ComputerInfo Info = new ComputerInfo();
        public readonly static Keyboard Keyboard = new Keyboard();
        public readonly static Mouse Mouse = new Mouse();
        public readonly static Network Network = new Network();
        public readonly static Ports Ports = new Ports();
    }
}