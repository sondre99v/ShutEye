#version 150
in float sampleData;

uniform float TimeOffset;	// Time axis offset in seconds
uniform int OffsetY;		// Y axis offset in pixels
uniform float SampleRate;	// Sample rate of the signal in samples pr. second
uniform float ScaleX;		// Scale of time axis in pixels pr. seconds
uniform float ScaleY;		// Scale of Y axis in pixels pr. unit
uniform int ChannelHeight;	// Height of each channel in pixels
uniform ivec2 ViewSize;		// Size of view in pixels
uniform int ChannelIndex;	// Zero based index starting with the top-most channel

void main()
{
	float time = gl_VertexID / SampleRate;
	float y0 = (ChannelIndex + 0.5) * ChannelHeight - OffsetY;
	
	float xPos = (time - TimeOffset) * ScaleX;
	float yPos = y0 + sampleData * ScaleY;

	vec2 screenPos2 = (vec2(xPos, yPos) / ViewSize * 2.0 - vec2(1.0)) * vec2(1.0, -1.0);

	gl_Position = vec4(screenPos2, 0.0, 1.0); 
}
