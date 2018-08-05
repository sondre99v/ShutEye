#version 150

in vec2 position;

out vec2 texCoord;

void main()
{
	texCoord.x = (position.x + 1) / 2;
	texCoord.y = (1 - position.y) / 2;
	gl_Position = vec4(position, 0.0, 1.0); 
}
