using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace DirectXTraingle
{
    public partial class Form1 : Form
    {
        private Device device;
        private CustomVertex.PositionColored[] vertex = new CustomVertex.PositionColored[3];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PresentParameters pp = new PresentParameters();
                pp.Windowed = true;
                pp.SwapEffect = SwapEffect.Discard;
                device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing,pp);
                device.Transform.Projection = Matrix.PerspectiveFovLH(3.14f / 4, device.Viewport.Width / device.Viewport.Height, 1f, 1000f);
                device.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, 20), new Vector3(), new Vector3(0, 1, 0));
                device.RenderState.Lighting = false;

           
        


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
            

        }

        private void Render()
        {
            
            device.Clear(ClearFlags.Target, Color.DarkCyan, 1, 0);
            vertex[0] = new CustomVertex.PositionColored(new Vector3(0, 0, 0), Color.Green.ToArgb());
            vertex[1] = new CustomVertex.PositionColored(new Vector3(3, 0, 4), Color.Blue.ToArgb());
            vertex[2] = new CustomVertex.PositionColored(new Vector3(2, 3, 0), Color.Red.ToArgb());
            device.BeginScene();
            device.VertexFormat = CustomVertex.PositionColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, vertex.Length / 3, vertex);
            device.EndScene();
            device.Present();
        }
    }
}
