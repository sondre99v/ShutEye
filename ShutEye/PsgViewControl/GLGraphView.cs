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

		public float ViewDuration => Width / ScaleX;

		private struct Channel
		{
			public Timeseries Timeseries;
			public int VertexArrayObject;
			public int VertexBufferObject;
		}

		private List<Channel> _dataChannels;

		const int SelectionEdgeThreshold = 5;
		private List<Selection> _selections;
		private Selection _editedSelection;
		private bool _editingEndEdge;

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

		private Bitmap _overlayBmp;

		private bool _isInDesignMode;

		public GLGraphView()
		{
			_dataChannels = new List<Channel>();
			_selections = new List<Selection>();

			/*_selections.Add(new Selection(1, 2));
			_selections.Add(new Selection(3, 4));
			_selections.Add(new Selection(6, 7));
			_selections.Add(new Selection(9, 11));
			_selections[0].SelectedChannelIndex = 1;
			_selections[0].Active = false;
			_selections[1].SelectedChannelIndex = 3;
			_selections[1].Active = true;
			_selections[2].SelectedChannelIndex = -1;
			_selections[2].Active = false;
			_selections[3].SelectedChannelIndex = 0;
			_selections[3].Active = false;*/

			_editedSelection = null;

			_isInDesignMode = (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

			_overlayBmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		}

		public float TimeToViewX(float time)
		{
			return (time - TimeOffset) * ScaleX;
		}

		public float ViewXToTime(float viewX)
		{
			return TimeOffset + viewX / ScaleX;
		}

		public void ClearSelections()
		{
			_selections.Clear();
			_editedSelection = null;
		}

		public void AddSelection(float startTime, float endTime)
		{
			_selections.Add(new Selection(startTime, endTime));
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
			_overlayBmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			base.OnResize(e);
		}


		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(_editedSelection != null)
			{
				Cursor = Cursors.SizeWE;

				if(_editingEndEdge)
				{
					_editedSelection.EndTime = ViewXToTime(e.X);
				}
				else
				{
					_editedSelection.StartTime = ViewXToTime(e.X);
				}

				Invalidate();
			}
			else
			{
				Cursor = Cursors.IBeam;

				foreach(Selection s in _selections)
				{
					int startX = (int) TimeToViewX(s.StartTime);
					int endX = (int) TimeToViewX(s.EndTime);

					if(Math.Abs(e.X - startX) <= SelectionEdgeThreshold ||
						Math.Abs(e.X - endX) <= SelectionEdgeThreshold)
					{
						Cursor = Cursors.SizeWE;
					}
					else if(startX < e.X && e.X < endX)
					{
						Cursor = Cursors.Arrow;
					}
				}
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			foreach(Selection s in _selections)
			{
				if(Math.Abs(e.X - TimeToViewX(s.StartTime)) <= SelectionEdgeThreshold)
				{
					_editedSelection = s;
					_editingEndEdge = false;
					break;
				}
				else if(Math.Abs(e.X - TimeToViewX(s.EndTime)) <= SelectionEdgeThreshold)
				{
					_editedSelection = s;
					_editingEndEdge = true;
					break;
				}
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(_editedSelection != null)
			{
				_editedSelection = null;
				Invalidate();
			}
			else
			{
				foreach(Selection s in _selections)
				{
					int startX = (int) TimeToViewX(s.StartTime);
					int endX = (int) TimeToViewX(s.EndTime);

					if(startX < e.X && e.X < endX)
					{
						if(s.Active)
						{
							if((ModifierKeys & Keys.Control) == Keys.Control)
							{
								foreach(Selection s2 in _selections.Where(s2 => s2.Active))
								{
									s2.SelectedChannelIndex = e.Y / ChannelHeight;
								}
							}
							else
							{
								s.SelectedChannelIndex = e.Y / ChannelHeight;
							}
						}
						else
						{
							s.Active = true;
						}
						Invalidate();
					}
					else
					{
						if((ModifierKeys & Keys.Control) != Keys.Control)
						{
							s.Active = false;
							Invalidate();
						}
					}
				}
			}

			base.OnMouseUp(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			if(_isInDesignMode)
			{
				e.Graphics.Clear(BackColor);
				return;
			}

			MakeCurrent();

			GL.Viewport(0, 0, Width, Height);

			GL.UseProgram(_graphShaderProgramID);

			GL.ClearColor(BackColor);
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
			
			Graphics g = Graphics.FromImage(_overlayBmp);
			g.Clear(Color.Transparent);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

			int firstSelectedIndex = _selections.FindIndex(s => s.Active);
			int lastSelectedIndex = _selections.FindLastIndex(s => s.Active);

			if(firstSelectedIndex != -1)
			{
				string text;

				if(lastSelectedIndex != firstSelectedIndex)
				{
					text = $"{firstSelectedIndex}..{lastSelectedIndex}";
				}
				else
				{
					text = $"{firstSelectedIndex}";
				}

				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.DrawString(text, new Font(FontFamily.GenericSansSerif, 12.0F), Brushes.White, new PointF(0, 0));
			}


			Brush blue25 = new SolidBrush(Color.FromArgb(64, Color.Blue));
			Brush blue50 = new SolidBrush(Color.FromArgb(96, Color.Blue));
			Brush red = new SolidBrush(Color.FromArgb(64, Color.Red));

			foreach(Selection s in _selections)
			{
				if(s.EndTime > TimeOffset || s.StartTime < TimeOffset + ViewDuration)
				{
					int xStart = (int) TimeToViewX(s.StartTime);
					int xEnd = (int) TimeToViewX(s.EndTime);

					if(xStart < 0) xStart = 0;
					if(xEnd >= Width) xEnd = Width - 1;

					g.FillRectangle(s.Active ? blue50 : blue25, xStart, 0, xEnd - xStart, Height);

					if(s.SelectedChannelIndex >= 0)
					{
						g.FillRectangle(red, xStart, ChannelHeight * s.SelectedChannelIndex, xEnd - xStart, ChannelHeight);
					}
				}
			}


			BitmapData data = _overlayBmp.LockBits(new Rectangle(0, 0, _overlayBmp.Width, _overlayBmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.BindTexture(TextureTarget.Texture2D, _overlayTextureID);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			
			_overlayBmp.UnlockBits(data);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);


			System.Diagnostics.Debug.Assert(GL.GetError() == ErrorCode.NoError);

			GL.BindVertexArray(_overlayVertexArrayObject);
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);

			SwapBuffers();

			sw.Stop();

			Console.WriteLine("Graph redraw took " + sw.Elapsed.TotalMilliseconds + "ms");
		}
	}
}
