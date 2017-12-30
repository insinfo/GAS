using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAS2
{
    public partial class Teste : Form
    {
        public Teste()
        {
            InitializeComponent();
        }

        private void BtnAddRectangle_Click(object sender, EventArgs e)
        {
            viewPort.AddElement(new RoundedRectangleElement());
        }

        private void BtnAddText_Click(object sender, EventArgs e)
        {
            viewPort.AddElement(new TextElement());
        }
    }
}
