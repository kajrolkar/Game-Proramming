using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace SpectacularLighting
{
    public partial class Form1 : Form
    {
        private Device device;
        private PresentParameters pps;
        private VertexBuffer vertices;
        public Form1()
        {
            InitializeComponent();
        }

        public bool InitializeGraphics()
        {
            pps = new PresentParameters();
            pps.Windowed = true;
            pps.SwapEffect = SwapEffect.Discard;
            pps.EnableAutoDepthStencil = true;
            pps.AutoDepthStencilFormat = DepthFormat.D16;
            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pps);
            device.RenderState.CullMode = Cull.None;
            vertices = CreateVertexBuffer();
            return true;
            
        }

        public VertexBuffer CreateVertexBuffer()
        {
            VertexBuffer buf = new VertexBuffer(typeof(CustomVertex.PositionNormal), 100, device, 0, CustomVertex.PositionNormal.Format, Pool.Default);
            PopulateVertex(buf);
            return buf;

        }

        private void PopulateVertex(VertexBuffer buf)
        {
            CustomVertex.PositionNormal[] verts = (CustomVertex.PositionNormal[])buf.Lock(0, 0);
            for (int i= 0;i<50;i++)
            {
                float theta = (float)(2 * Math.PI * i) / 49;
                verts[2 * i].Position = (new Vector3((float)Math.Sin(theta), -1, (float)Math.Cos(theta)));
                verts[2 * i].Normal = (new Vector3((float)Math.Sin(theta), 0, (float)Math.Cos(theta)));
                verts[2 * i + 1].Position = (new Vector3((float)Math.Sin(theta), 1, (float)Math.Cos(theta)));
                verts[2 * i + 1].Normal = (new Vector3((float)Math.Sin(theta), 0, (float)Math.Cos(theta)));

            }
            buf.Unlock();
        }

        public void Render()
        {
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Bisque, 1.0F, 0);
            device.BeginScene();
            SetUpMatrices();
            SetUpLights();
            SetUpMaterials();
            device.VertexFormat = CustomVertex.PositionNormal.Format;
            device.SetStreamSource(0, vertices, 0);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 98);
            device.EndScene();
            device.Present();
        }

        public void SetUpMaterials()
        {
            float yaw = Environment.TickCount / 1000.0F;
            float pitch = Environment.TickCount / 612.0F;
            device.Transform.World = Matrix.RotationYawPitchRoll(yaw, pitch, 0);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, -6), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0F, 1.0F, 1.0F, 10.0F);

        }

        public void SetUpLights()
        {
            device.RenderState.Lighting = true;
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Specular = Color.White;
            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Direction = new Vector3(-1, -1, 3);
            device.Lights[0].Update();
            device.Lights[0].Enabled = true;
            device.RenderState.Ambient=Color.FromArgb(0x40, 0x40, 0x40);
        }

        public void SetUpMatrices()
        {
            Material mat = new Material();
            mat.Diffuse = Color.Blue;
            mat.Specular = Color.LightGray;
            mat.SpecularSharpness = 15.0F;
            device.Material = mat;
            device.RenderState.SpecularEnable = true;

            
        }

        public void DisposeGraphics()
        {

            vertices.Dispose();
            device.Dispose();
            
            
        }
    }
}
