using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS
{
    public enum ObjectType
    {
        Nenhum = 0,
        Image = 1,
        Text = 2,
        Rectangle = 3
    }

    public enum ShapeIcon
    {
        DefaultShape,
        NodeIn,
        NodeOut,
        Pump,
        Tank,
        Vessel,
        Compressor,
        Expander,
        Cooler,
        Heater,
        Pipe,
        Valve,
        RCT_Conversion,
        RCT_Equilibrium,
        RCT_Gibbs,
        RCT_CSTR,
        RCT_PFR,
        HeatExchanger,
        DistillationColumn,
        AbsorptionColumn,
        ComponentSeparator,
        OrificePlate
    }

    public enum Status
    {
        Calculated,
        Calculating,
        ErrorCalculating,
        Inactive,
        Idle,
        NotCalculated,
        Modified
    }

    public enum ConType
    {
        ConIn = -1,
        ConOut = 1,
        ConEn = 0,
        ConSp = 2
    }

    public enum ConDir
    {
        Up,
        Down,
        Right,
        Left
    }

}
