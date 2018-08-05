#version 150 core

out vec4 outColor;

in vec2 texCoord;

uniform sampler2D texOverlay;

void main()
{
	outColor = texture(texOverlay, texCoord);
}
