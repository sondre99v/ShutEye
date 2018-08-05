using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
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

		private struct Channel
		{
			public Timeseries Timeseries;
			public int VertexArrayObject;
			public int VertexBufferObject;
		}

		private List<Channel> _dataChannels;

		public List<Timeseries> ChannelsData { get => _dataChannels.Select(c => c.Timeseries).ToList(); }

		private int _graphShaderProgramID;

		private int _timeOffsetUniformID;
		private int _scaleXUniformID;
		private int _scaleYUniformID;
		private int _offsetYUniformID;
		private int _sampleRateUniformID;
		private int _channelHeightUniformID;
		private int _viewSizeUniformID;
		private int _channelIndexUniformID;

		private int _overlayShaderProgramID;
		private int _overlayVertexArrayObject;
		private int _overlayVertexBufferObject;
		private int _overlayTextureID;

		private bool _isInDesignMode;

		public GLGraphView()
		{
			_dataChannels = new List<Channel>();

			_isInDesignMode = (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
		}

		public void AddChannel(Timeseries channelData)
		{
			Channel channel = new Channel();
			channel.Timeseries = channelData;

			GL.CreateVertexArrays(1, out channel.VertexArrayObject);
			GL.CreateBuffers(1, out channel.VertexBufferObject);

			GL.BindVertexArray(channel.VertexArrayObject);

			GL.BindBuffer(BufferTarget.ArrayBuffer, channel.VertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * channelData.Data.Length, channelData.Data, BufferUsageHint.StaticDraw);

			int dataAttribute = GL.GetAttribLocation(_graphShaderProgramID, "sampleData");
			GL.VertexAttribPointer(dataAttribute, 1, VertexAttribPointerType.Float, false, 0, 0);
			GL.EnableVertexAttribArray(dataAttribute);


			_dataChannels.Add(channel);
		}

		public void RemoveChannel(int index)
		{
			Channel channel = _dataChannels[index];
			GL.DeleteVertexArray(channel.VertexArrayObject);
			GL.DeleteBuffer(channel.VertexBufferObject);
			_dataChannels.RemoveAt(index);
		}

		public void AddChannelRange(Timeseries[] channels)
		{
			foreach(var channel in channels)
			{
				AddChannel(channel);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if(_isInDesignMode)
			{
				return;
			}
			string vertex_shader_source;
			string fragment_shader_source;
			int vertexShaderID;
			int fragmentShaderID;
			int compileResult;
			string shaderInfoLog;

			// Compile Graph Shaders
			
			vertex_shader_source = new System.IO.StreamReader(
				System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ShutEye.PsgViewControl.GraphVertexShader.vert")).ReadToEnd();
			fragment_shader_source = new System.IO.StreamReader(
				System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ShutEye.PsgViewControl.GraphFragmentShader.frag")).ReadToEnd();

			vertexShaderID = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShaderID, vertex_shader_source);
			GL.CompileShader(vertexShaderID);

			fragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShaderID, fragment_shader_source);
			GL.CompileShader(fragmentShaderID);

			
			GL.GetShader(fragmentShaderID, ShaderParameter.CompileStatus, out compileResult);

			shaderInfoLog = GL.GetShaderInfoLog(fragmentShaderID);

			Console.WriteLine(shaderInfoLog);

			if(compileResult != 1)
			{
				throw new Exception(shaderInfoLog);
			}

			// Create shader program
			_graphShaderProgramID = GL.CreateProgram();
			GL.AttachShader(_graphShaderProgramID, vertexShaderID);
			GL.AttachShader(_graphShaderProgramID, fragmentShaderID);

			// Bind output color
			GL.BindFragDataLocation(_graphShaderProgramID, 0, "outColor");

			// Link and use program
			GL.LinkProgram(_graphShaderProgramID);
			GL.UseProgram(_graphShaderProgramID);

			// Get uniform IDs
			_timeOffsetUniformID = GL.GetUniformLocation(_graphShaderProgramID, "TimeOffset");
			_offsetYUniformID = GL.GetUniformLocation(_graphShaderProgramID, "OffsetY");
			_sampleRateUniformID = GL.GetUniformLocation(_graphShaderProgramID, "SampleRate");
			_scaleXUniformID = GL.GetUniformLocation(_graphShaderProgramID, "ScaleX");
			_scaleYUniformID = GL.GetUniformLocation(_graphShaderProgramID, "ScaleY");
			_channelHeightUniformID = GL.GetUniformLocation(_graphShaderProgramID, "ChannelHeight");
			_viewSizeUniformID = GL.GetUniformLocation(_graphShaderProgramID, "ViewSize");
			_channelIndexUniformID = GL.GetUniformLocation(_graphShaderProgramID, "ChannelIndex");
			
			
			// Setup overlay vertices
			GL.CreateVertexArrays(1, out _overlayVertexArrayObject);
			GL.BindVertexArray(_overlayVertexArrayObject);

			GL.CreateBuffers(1, out _overlayVertexBufferObject);

			GL.BindBuffer(BufferTarget.ArrayBuffer, _overlayVertexBufferObject);

			float[] quadVertices = new float[] { -1F, 1F, 1F, 1F, 1F, -1F, -1F, -1F };
			GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * quadVertices.Length, quadVertices, BufferUsageHint.StaticDraw);


			_overlayTextureID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, _overlayTextureID);

			// Compile Overlay Shaders
			vertex_shader_source = new System.IO.StreamReader(
				System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ShutEye.PsgViewControl.OverlayVertexShader.vert")).ReadToEnd();
			fragment_shader_source = new System.IO.StreamReader(
				System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ShutEye.PsgViewControl.OverlayFragmentShader.frag")).ReadToEnd();

			vertexShaderID = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShaderID, vertex_shader_source);
			GL.CompileShader(vertexShaderID);

			fragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShaderID, fragment_shader_source);
			GL.CompileShader(fragmentShaderID);

			GL.GetShader(fragmentShaderID, ShaderParameter.CompileStatus, out compileResult);

			shaderInfoLog = GL.GetShaderInfoLog(fragmentShaderID);

			Console.WriteLine(shaderInfoLog);

			if(compileResult != 1)
			{
				throw new Exception(shaderInfoLog);
			}

			// Create shader program
			_overlayShaderProgramID = GL.CreateProgram();
			GL.AttachShader(_overlayShaderProgramID, vertexShaderID);
			GL.AttachShader(_overlayShaderProgramID, fragmentShaderID);

			// Bind output color
			GL.BindFragDataLocation(_overlayShaderProgramID, 0, "outColor");

			// Link and use program
			GL.LinkProgram(_overlayShaderProgramID);
			GL.UseProgram(_overlayShaderProgramID);
			
			int posAttribute = GL.GetAttribLocation(_overlayShaderProgramID, "position");
			GL.EnableVertexAttribArray(posAttribute);
			GL.VertexAttribPointer(posAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);
			
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			if(_isInDesignMode)
			{
				e.Graphics.Clear(Color.DimGray);
				return;
			}

			MakeCurrent();

			GL.Viewport(0, 0, Width, Height);

			GL.UseProgram(_graphShaderProgramID);

			GL.ClearColor(Color.Gray);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Uniform1(_timeOffsetUniformID, TimeOffset);
			GL.Uniform1(_scaleXUniformID, ScaleX);
			GL.Uniform1(_channelHeightUniformID, ChannelHeight);
			GL.Uniform2(_viewSizeUniformID, Width, Height);
			GL.Uniform1(_offsetYUniformID, OffsetY);

			for(int i = 0; _dataChannels != null && i < _dataChannels.Count; i++)
			{
				Timeseries ts = _dataChannels[i].Timeseries;
				GL.Uniform1(_channelIndexUniformID, i);
				GL.Uniform1(_sampleRateUniformID, ts.SampleRate);
				GL.Uniform1(_scaleYUniformID, ChannelHeight / 2 * ts.ViewAmplitude);
				GL.BindVertexArray(_dataChannels[i].VertexArrayObject);

				int startIndex = (int) Math.Floor(TimeOffset * ts.SampleRate);
				int samplesInView = (int) Math.Ceiling(Width * ts.SampleRate / ScaleX);

				if(startIndex < 0)
					startIndex = 0;
				if(startIndex >= ts.Data.Length)
					startIndex = ts.Data.Length - 1;

				GL.DrawArrays(PrimitiveType.LineStrip, startIndex, Math.Min(samplesInView, ts.Data.Length - startIndex));
			}


			// Draw overlay
			GL.UseProgram(_overlayShaderProgramID);

			Bitmap overlayBmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(overlayBmp);
			g.Clear(Color.Transparent);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			g.DrawString("1", new Font(FontFamily.GenericMonospace, 12.0F), Brushes.LightGreen, new PointF(0, 0));
			g.DrawString("2", new Font(FontFamily.GenericMonospace, 12.0F), Brushes.LightGreen, new PointF(Width - 16, 0));
			g.DrawString("3", new Font(FontFamily.GenericMonospace, 12.0F), Brushes.LightGreen, new PointF(0, Height - 16));
			g.DrawString("4", new Font(FontFamily.GenericMonospace, 12.0F), Brushes.LightGreen, new PointF(Width - 16, Height - 16));
			
			Brush blue25 = new  SolidBrush(Color.FromArgb(64, Color.Blue));
			Brush blue50 = new  SolidBrush(Color.FromArgb(96, Color.Blue));
			Brush red = new  SolidBrush(Color.FromArgb(64, Color.Red));

			g.FillRectangle(blue25, 100, 0, 100, Height);
			
			//g.FillRectangle(blue50, 95, 0, 5, Height);
			//g.FillRectangle(blue50, 200, 0, 5, Height);

			g.FillRectangle(red, 100, 57, 100, 57);

			BitmapData data = overlayBmp.LockBits(new Rectangle(0, 0, overlayBmp.Width, overlayBmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			
			GL.BindTexture(TextureTarget.Texture2D, _overlayTextureID);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			
			
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

			overlayBmp.UnlockBits(data);

			System.Diagnostics.Debug.Assert(GL.GetError() == ErrorCode.NoError);

			GL.BindVertexArray(_overlayVertexArrayObject);
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);

			SwapBuffers();

			sw.Stop();

			Console.WriteLine("Graph redraw took " + sw.Elapsed.TotalMilliseconds + "ms");
		}
	}
}
