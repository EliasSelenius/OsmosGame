
#version 430 core

uniform mat4 projection;
uniform mat4 camera;
uniform mat4 transform;
uniform float time;

in vec2 pos;
out vec2 fragpos;

void main() {
	vec2 offs = cos((pos * 5) + time);
	gl_Position = projection * camera * transform * vec4(pos + offs * 0.05, 0.0, 1.0);
	fragpos = pos;
}