using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
namespace Diffusionlighting
{
    public partial class Form1 : Form
    {
        private Device device;
        private CustomVertex.PositionNormalColored[] vertex = new CustomVertex.PositionNormalColored[3];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);

            vertex[0] = new CustomVertex.PositionNormalColored(new Vector3(0, 1, 1), new Vector3(1, 0, 1), Color.Red.ToArgb());
            vertex[1] = new CustomVertex.PositionNormalColored(new Vector3(1, -1, 1), new Vector3(1, 0, -1), Color.Green.ToArgb());
            vertex[2] = new CustomVertex.PositionNormalColored(new Vector3(-1, -1, 1), new Vector3(-1, 0, 1), Color.Blue.ToArgb());


            device.RenderState.Lighting = true;
            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Direction = new Vector3(0, 0, -1);
            device.Lights[0].Update();
            device.Lights[0].Enabled = true;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if(device==null)
            {
                return;
            }
            device.Clear(ClearFlags.Target, Color.CadetBlue, 0, 1);
            device.BeginScene();
            device.VertexFormat = CustomVertex.PositionNormalColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, vertex);
            device.EndScene();
            device.Present();
        }
    }
}
