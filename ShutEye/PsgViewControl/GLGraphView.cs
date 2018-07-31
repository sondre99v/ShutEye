using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ShutEye
{
	class GLGraphView: GLControl
	{
		public float TimeOffset { get; set; } = 0.0F;

		public List<Timeseries> DataChannels { get; private set; }

		private int _timeOffsetUniformID;
		private int _yOffsetUniformID;
		private int _shaderProgramID;
		private int[] _vertexArrays;

		private bool _isInDesignMode;

		public GLGraphView()
		{
			DataChannels = new List<Timeseries>();

			_isInDesignMode = (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
		}


		protected override void OnLoad(EventArgs e)
		{
			if(_isInDesignMode)
			{
				return;
			}

			// Compile Shaders
			string vertex_shader_source = new System.IO.StreamReader(
				System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ShutEye.PsgViewControl.VertexShader.vert")).ReadToEnd();
			string fragment_shader_source = new System.IO.StreamReader(
				System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ShutEye.PsgViewControl.FragmentShader.frag")).ReadToEnd();

			int VertexShaderID = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(VertexShaderID, vertex_shader_source);
			GL.CompileShader(VertexShaderID);

			int FragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(FragmentShaderID, fragment_shader_source);
			GL.CompileShader(FragmentShaderID);

			int compileResult;
			GL.GetShader(FragmentShaderID, ShaderParameter.CompileStatus, out compileResult);

			string shaderInfoLog = GL.GetShaderInfoLog(FragmentShaderID);

			Console.WriteLine(shaderInfoLog);

			if(compileResult != 1)
			{
				throw new Exception(shaderInfoLog);
			}

			// Create shader program
			_shaderProgramID = GL.CreateProgram();
			GL.AttachShader(_shaderProgramID, VertexShaderID);
			GL.AttachShader(_shaderProgramID, FragmentShaderID);

			// Bind output color
			GL.BindFragDataLocation(_shaderProgramID, 0, "outColor");

			// Link and use program
			GL.LinkProgram(_shaderProgramID);
			GL.UseProgram(_shaderProgramID);

			// Setup vertex array interpretation
			int posAttrib = GL.GetAttribLocation(_shaderProgramID, "sampleData");
			GL.VertexAttribPointer(posAttrib, 1, VertexAttribPointerType.Float, false, sizeof(float), 0);
			GL.EnableVertexAttribArray(posAttrib);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if(_isInDesignMode)
			{
				e.Graphics.Clear(Color.Red);
				return;
			}

			MakeCurrent();

			GL.UseProgram(_shaderProgramID);

			//GL.Uniform1(_timeOffsetUniformID, TimeOffset);

			for(int i = 0; i < DataChannels.Count; i++)
			{
				GL.Uniform1(_yOffsetUniformID, (float) i / 4);

				GL.BindVertexArray(_vertexArrays[i]);
				GL.DrawArrays(PrimitiveType.LineStrip, 0, DataChannels[i].Data.Length);
			}

			SwapBuffers();
		}
	}
}
