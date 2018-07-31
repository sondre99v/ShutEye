#version 150
in float sampleData;

void main()
{
	gl_Position = vec4(gl_VertexID, sampleData, 0.0, 1.0); 
}
