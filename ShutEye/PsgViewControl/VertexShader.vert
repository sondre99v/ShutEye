#version 150
in float sampleData;

uniform float OffsetY;
uniform float ScaleX;
uniform float TimeOffset;

uniform float ViewWidth;
uniform float ViewHeight;

out float shade;

void main()
{
	shade = (sampleData + 1.0) / 2;
	gl_Position = vec4((float(gl_VertexID) - TimeOffset * 200.0) * ScaleX - 0.9, (sampleData * 0.001) + OffsetY, 0.0, 1.0); 
}
