using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GAS
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IGraphicObject
    {
        Action<object> DrawOverride { get; set; }

        void Draw(object surface);

        object Editor { get; set; }

        IGraphicObject Clone();

        ShapeIcon ShapeOverride { get; set; }

        Status Status { get; set; }

        string Description { get; set; }

        object AdditionalInfo { get; set; }

        int Shape { get; set; }

        bool FlippedH { get; set; }

        bool FlippedV { get; set; }

        ObjectType ObjectType { get; set; }

        bool IsEnergyStream { get; set; }

        bool Active { get; set; }

        string Tag { get; set; }

        bool AutoSize { get; set; }

        bool IsConnector { get; set; }

        int X { get; set; }

        int Y { get; set; }

        string Name { get; set; }

        int Height { get; set; }

        int Width { get; set; }

        List<IConnectionPoint> InputConnectors { get; set; }

        List<IConnectionPoint> OutputConnectors { get; set; }

        List<IConnectionPoint> SpecialConnectors { get; set; }

        IConnectionPoint EnergyConnector { get; set; }

        int Rotation { get; set; }

        bool Calculated { get; set; }

        IPoint Position { get; set; }

        bool Selected { get; set; }

       // ISimulationObject Owner { get; set; }

        bool HitTest(object zoomedSelection);

        Dictionary<string, IGraphicObjectExtension> Extensions { get; set; }

    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IConnectionPoint
    {

        int X { get; set; }

        int Y { get; set; }

        ConType Type { get; set; }

        ConDir Direction { get; set; }

        IConnectorGraphicObject AttachedConnector { get; set; }

        bool IsAttached { get; set; }

        string ConnectorName { get; set; }

        IPoint Position { get; set; }

        bool IsEnergyConnector { get; set; }

        bool Active { get; set; }

    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IConnectorGraphicObject
    {

        int AttachedFromConnectorIndex { get; set; }

        int AttachedToConnectorIndex { get; set; }

        bool AttachedToEnergy { get; set; }

        bool AttachedFromEnergy { get; set; }

        IGraphicObject AttachedFrom { get; set; }

        IGraphicObject AttachedTo { get; set; }

        bool AttachedToOutput { get; set; }

        bool AttachedFromInput { get; set; }

    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IGraphicObjectExtension
    {

        void Draw(object surface);

        IGraphicObjectExtension Clone();

        string Description { get; set; }

        bool Active { get; set; }

        string Tag { get; set; }

        string Name { get; set; }

        int Height { get; set; }

        int Width { get; set; }

        IPoint RelativePosition { get; set; }

        bool Selected { get; set; }

        IGraphicObject Owner { get; set; }

        bool HitTest(object zoomedSelection);

    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IPoint
    {
        double X { get; set; }
        double Y { get; set; }
    }

}
