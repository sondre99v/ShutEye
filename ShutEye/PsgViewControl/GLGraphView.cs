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

		private Timeseries[] _dataChannels;

		private int _timeOffsetUniformID;
		private int _yOffsetUniformID;
		private int _timeScaleUniformID;
		private int _shaderProgramID;
		private int[] _vertexArrayObjects;
		private int[] _vertexBufferObjects;

		private bool _isInDesignMode;

		public GLGraphView()
		{
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

			// Get uniform IDs
			_timeOffsetUniformID = GL.GetUniformLocation(_shaderProgramID, "TimeOffset");
			_yOffsetUniformID = GL.GetUniformLocation(_shaderProgramID, "OffsetY");
			_timeScaleUniformID = GL.GetUniformLocation(_shaderProgramID, "ScaleX");
		}

		public void LoadChannelData(Timeseries[] channels)
		{
			_dataChannels = channels;

			// TODO: Delete old vertex/buffer data if we are loading a new file?

			_vertexArrayObjects = new int[_dataChannels.Length];
			GL.CreateVertexArrays(_vertexArrayObjects.Length, _vertexArrayObjects);

			_vertexBufferObjects = new int[_dataChannels.Length];
			GL.CreateBuffers(_vertexBufferObjects.Length, _vertexBufferObjects);

			for(int i = 0; i < _dataChannels.Length; i++)
			{
				GL.BindVertexArray(_vertexArrayObjects[i]);

				GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[i]);
				GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _dataChannels[i].Data.Length, _dataChannels[i].Data, BufferUsageHint.StaticDraw);
				
				int dataAttribute = GL.GetAttribLocation(_shaderProgramID, "sampleData");
				GL.VertexAttribPointer(dataAttribute, 1, VertexAttribPointerType.Float, false, sizeof(float), 0);
				GL.EnableVertexAttribArray(dataAttribute);
			}
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

			GL.ClearColor(Color.DimGray);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Uniform1(_timeOffsetUniformID, TimeOffset);
			
			for(int i = 0; _dataChannels != null && i < _dataChannels.Length; i++)
			{
				GL.Uniform1(_yOffsetUniformID, (float) (13 - i) / 15);
				GL.Uniform1(_timeScaleUniformID, 2.0F/4000);

				GL.BindVertexArray(_vertexArrayObjects[i]);
				GL.DrawArrays(PrimitiveType.LineStrip, 0, _dataChannels[i].Data.Length);
			}

			SwapBuffers();
		}
	}
}
