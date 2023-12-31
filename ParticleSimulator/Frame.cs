using OpenTK;
using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL;
using ParticleSimulator.EngineWork;
using System.Numerics;

namespace ParticleSimulator
{
    public partial class Frame : Form
    {
        Engine engine = null;
        bool is3D = false;
        int parts = 0;
        public Frame()
        {
            //initialization
            InitializeComponent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            int RowofParts = (int)Math.Sqrt(engine.particles2D.Count);
            float offsetX = (PicBox.Width / 2) - (RowofParts * 7 / 2);
            float offsetY = (PicBox.Height / 2) - (RowofParts * 7 / 2);
            for (int i = 0; i < RowofParts; i++)
            {
                for (int j = 0; j < RowofParts; j++)
                {
                    engine.particles2D[i * RowofParts + j].velocity = new Vector2(0, 0);
                    engine.particles2D[i * RowofParts + j].point = new Vector2(i * 7 + offsetX, j * 7 + offsetY);
                }
            }
        }

        private void TB_SmoothingRadius_Validated(object sender, EventArgs e)
        {
            if (!is3D)
                engine.simulator2D.smoothingRadius = float.Parse(TB_SmoothingRadius.Text);
            else
                engine.simulator3D.smoothingRadius = float.Parse(TB_SmoothingRadius.Text);
        }

        private void TB_TargetDensity_Validated(object sender, EventArgs e)
        {
            if (!is3D)
                engine.simulator2D.targetDensity = float.Parse(TB_TargetDensity.Text);
            else
                engine.simulator3D.targetDensity = float.Parse(TB_TargetDensity.Text);
        }

        private void TB_PressureMult_Validated(object sender, EventArgs e)
        {
            if (!is3D)
                engine.simulator2D.pressureMultiplier = float.Parse(TB_PressureMult.Text);
            else
                engine.simulator3D.pressureMultiplier = float.Parse(TB_PressureMult.Text);
        }

        private void TB_ViscosityStrength_Validated(object sender, EventArgs e)
        {
            if (!is3D)
                engine.simulator2D.viscosityStr = float.Parse(TB_ViscosityStrength.Text);
            else
                engine.simulator3D.viscosityStr = float.Parse(TB_ViscosityStrength.Text);
        }

        private void TB_GravStr_Validated(object sender, EventArgs e)
        {
            if (!is3D)
                engine.simulator2D.GravStrength = float.Parse(TB_GravStr.Text);
            else
                engine.simulator3D.GravStrength = float.Parse(TB_GravStr.Text);
        }

        private void Frame_Load(object sender, EventArgs e)
        {
            //GLControl.Paint += GLControl_Paint;
            GLControl.Resize += GLControl_Resize;
        }
        public void GLControl_Resize(object? sender, EventArgs e)
        {
            GLControl.MakeCurrent();
            GL.Viewport(0, 0, GLControl.Width, GLControl.Height);
        }

        private void GLControl_Paint(object? sender, PaintEventArgs e)
        {
            GLControl.MakeCurrent();
            GL.ClearColor(Color.FromArgb(255, 30, 30, 30));
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GLControl.SwapBuffers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //GLControl.Invalidate();
        }

        private void Frame_FormClosing(object sender, FormClosingEventArgs e)
        {
            engine.renderer3D.ClearMemory();
        }

        private void GLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (engine != null)
                engine.MouseHandler(e, 1);
        }

        private void GLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (engine != null)
                engine.MouseHandler(e, 0);
        }

        private void GLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (engine != null)
                engine.MouseHandler(e, 2);
        }

        private void GLControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GLControl_MouseClick(object sender, MouseEventArgs e)
        {
            GLControl.Focus();
            Console.WriteLine("focus");
        }

        private void Frame_KeyDown(object sender, KeyEventArgs e)
        {
            //engine.KeyboardHandler(e);
        }

        private void GLControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            engine.KeyboardHandler(e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            is3D = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (engine == null)
            {
                engine = new Engine();
                engine.Init(this, is3D, parts);
            }
        }

        private void TB_ParticleAmount_Validated(object sender, EventArgs e)
        {
            parts = int.Parse(TB_ParticleAmount.Text);
        }

        private void TB_ParticleAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parts = int.Parse(TB_ParticleAmount.Text);
        }
    }
}