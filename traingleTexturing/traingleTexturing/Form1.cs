using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace traingleTexturing
{
    public partial class Form1 : Form
    {
        private Device device;
        private CustomVertex.TransformedColoredTextured[] vertices = new CustomVertex.TransformedColoredTextured[3];
        private Texture texture;
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
            device.Clear(ClearFlags.Target, Color.Black, 1.0f, 0);
            vertices[0].Position = new Vector4(400, 100, 0, 1.0f);
            vertices[1].Position = new Vector4(500, 350, 0, 1.0f);
            vertices[2].Position = new Vector4(300, 350, 0, 1.0f);

            vertices[0].Color = Color.White.ToArgb();
            vertices[1].Color = Color.Green.ToArgb();
            vertices[2].Color = Color.Blue.ToArgb();

            vertices[0].Tu = 0;
            vertices[0].Tv = 0;
            vertices[1].Tu = 1;
            vertices[1].Tv = 0;
            vertices[2].Tu = 1;
            vertices[2].Tv = 1;

            texture = new Texture(device, new Bitmap("F:\\diet\\one.jpg"), 0, Pool.Managed);

            device.BeginScene();
            device.SetTexture(0, texture);
            device.VertexFormat = CustomVertex.TransformedColoredTextured.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, vertices);
            device.EndScene();
            device.Present();
        }
    }
}
