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
		public float ScaleX { get; set; } = 100.0F;
		public int OffsetY { get; set; } = 0;
		public int ChannelHeight { get; set; } = 57;

		private Timeseries[] _dataChannels;

		private int _timeOffsetUniformID;
		private int _scaleXUniformID;
		private int _scaleYUniformID;
		private int _offsetYUniformID;
		private int _sampleRateUniformID;
		private int _channelHeightUniformID;
		private int _viewSizeUniformID;
		private int _channelIndexUniformID;

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

			//DoubleBuffered = true;

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
			_offsetYUniformID = GL.GetUniformLocation(_shaderProgramID, "OffsetY");
			_sampleRateUniformID = GL.GetUniformLocation(_shaderProgramID, "SampleRate");
			_scaleXUniformID = GL.GetUniformLocation(_shaderProgramID, "ScaleX");
			_scaleYUniformID = GL.GetUniformLocation(_shaderProgramID, "ScaleY");
			_channelHeightUniformID = GL.GetUniformLocation(_shaderProgramID, "ChannelHeight");
			_viewSizeUniformID = GL.GetUniformLocation(_shaderProgramID, "ViewSize");
			_channelIndexUniformID = GL.GetUniformLocation(_shaderProgramID, "ChannelIndex");
		}

		public void LoadChannelData(Timeseries[] channels)
		{
			_dataChannels = channels;

			// Delete old vertex/buffer data in case we are loading a new file
			if(_vertexArrayObjects != null)
				GL.DeleteVertexArrays(_vertexArrayObjects.Length, _vertexArrayObjects);
			if(_vertexBufferObjects != null)
				GL.DeleteBuffers(_vertexBufferObjects.Length, _vertexBufferObjects);

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

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if(_isInDesignMode)
			{
				e.Graphics.Clear(Color.Red);
				return;
			}

			MakeCurrent();

			GL.Viewport(0, 0, Width, Height);

			GL.UseProgram(_shaderProgramID);

			GL.ClearColor(Color.DimGray);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Uniform1(_timeOffsetUniformID, TimeOffset);
			GL.Uniform1(_scaleXUniformID, ScaleX);
			GL.Uniform1(_channelHeightUniformID, ChannelHeight);
			GL.Uniform2(_viewSizeUniformID, Width, Height);
			GL.Uniform1(_offsetYUniformID, OffsetY);

			for(int i = 0; _dataChannels != null && i < _dataChannels.Length; i++)
			{
				GL.Uniform1(_channelIndexUniformID, i);
				GL.Uniform1(_sampleRateUniformID, _dataChannels[i].SampleRate);
				GL.Uniform1(_scaleYUniformID, 0.9F);
				GL.BindVertexArray(_vertexArrayObjects[i]);
				GL.DrawArrays(PrimitiveType.LineStrip, 0, _dataChannels[i].Data.Length);
			}

			SwapBuffers();
		}
	}
}
