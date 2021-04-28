using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
namespace AiroplaneQue6
{
    public partial class Form1 : Form
    {
        private Device device;
        private PresentParameters pps;
        private Mesh mesh;
        private Material[] materials;
        private Texture[] textures;
        public Form1()
        {
            InitializeComponent();
        }

        public bool InitialGraphics()
        {
            pps = new PresentParameters();
            pps.Windowed = true;
            pps.SwapEffect = SwapEffect.Discard;
            pps.EnableAutoDepthStencil = true;
            pps.AutoDepthStencilFormat = DepthFormat.D16;
            device = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, pps);
            device.RenderState.CullMode = Cull.None;
            CreateMesh(@"airplane 2.x");
            return true;
        }

        private void CreateMesh(string v)
        {
            ExtendedMaterial[] exMaterials;
            mesh = Mesh.FromFile(v, MeshFlags.SystemMemory, device, out exMaterials);  
            if (textures != null)
            {
                DisposeTexture();
            }
            textures = new Texture[exMaterials.Length];
            materials = new Material[exMaterials.Length];

            for(int i=0;i<exMaterials.Length;i++)
            {
                if (exMaterials[i].TextureFilename!=null)
                {
                    string texturePath = Path.Combine(Path.GetDirectoryName(v), exMaterials[i].TextureFilename);
                    textures[i] = TextureLoader.FromFile(device, texturePath);

                }
                materials[i] = exMaterials[i].Material3D;
                materials[i].Ambient = materials[i].Diffuse;
          
            }
        }

        private void DisposeTexture()
        {
            if(textures==null)
            {
                return;
            }
            foreach(Texture t in textures)
            {
                if(t!=null)
                {
                    t.Dispose();
                }
            }
        }

        public void Render()
        {
            device.Clear(ClearFlags.Target|ClearFlags.ZBuffer, Color.SkyBlue, 1.0F, 0);
            device.BeginScene();
            SetUpMatrices();
            SetUpLights();
            RenderMesh();
            device.EndScene();
            device.Present();
        }

        private void RenderMesh()
        {
            for (int i = 0; i < materials.Length; i++)
            {
                if (textures[i] != null)
                {
                    device.SetTexture(0, textures[i]);

                }
                device.Material = materials[i];
                mesh.DrawSubset(i);

            }
        }

       private void SetUpLights()
        {
            device.RenderState.Lighting = true;
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Specular = Color.White;
            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Direction = new Vector3(-1, -1, 3);
            device.Lights[0].Enabled = true;
            device.RenderState.Ambient = Color.FromArgb(0x00, 0x00, 0x00);
        }

        private void SetUpMatrices()
        {
            
            float yaw = Environment.TickCount / 1000.0F;
            float pitch = Environment.TickCount / 90000.0F;
            device.Transform.World = Matrix.RotationYawPitchRoll(yaw, pitch, 0);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, -6), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 2.0F, 1.0F, 1.0F, 10.0F);
        }
        
        public void DisposeGraphics()
        {
            DisposeTexture();
            device.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
