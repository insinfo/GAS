using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS2
{
    public class SelectionChangedEventArgs : EventArgs
    {
        private GraphicObject m_SelectedObject;

        public SelectionChangedEventArgs(GraphicObject selectedObject)
        {
            m_SelectedObject = selectedObject;
        }

        public GraphicObject SelectedObject
        {
            get
            {
                return m_SelectedObject;
            }
        }
    }

    public enum StatusUpdateType
    {
        ObjectRotated = 0,
        ObjectMoved = 1,
        ObjectDeleted = 2,
        SurfaceZoomChanged = 3,
        FileLoaded = 4,
        FileSaved = 5,
        SelectionChanged = 6
    }

    public class StatusUpdateEventArgs : EventArgs
    {
        private StatusUpdateType m_UpdateType;
        private GraphicObject m_SelectedObject;
        private string m_Message;
        private MyPoint m_Coord;
        private float m_Amount;

        public StatusUpdateEventArgs(StatusUpdateType UpdateType, GraphicObject Selection, string StatusMessage, MyPoint Coord, float Amt)
        {
            m_UpdateType = UpdateType;
            m_SelectedObject = Selection;
            m_Message = StatusMessage;
            m_Coord = Coord;
            m_Amount = Amt;
        }

        public StatusUpdateType Type
        {
            get
            {
                return m_UpdateType;
            }
        }
        public GraphicObject SelectedObject
        {
            get
            {
                return m_SelectedObject;
            }
        }

        public string Message
        {
            get
            {
                return m_Message;
            }
        }

        public MyPoint Coordinates
        {
            get
            {
                return m_Coord;
            }
        }

        public float Amount
        {
            get
            {
                return m_Amount;
            }
        }

    }

}
