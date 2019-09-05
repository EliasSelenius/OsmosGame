
#version 430 core

uniform float time;
uniform mat4 transform;

out vec4 fragColor;
in vec2 fragpos;

void main() {
	vec2 timevec = (transform * vec4(time, time/10.0, 0.0, 0.0)).xy;
	vec2 wave = sin(timevec);
	wave = (wave + 1.0) / 2.0;
	fragColor = vec4(wave, 1.0, 1.0) / max(length(fragpos) * 8.0, 1.0);
}