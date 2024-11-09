#version 420 core

in vec2 vTexCoord;
in vec3 vNormal;

out vec4 outputColor;

layout(binding = 0) uniform sampler2D texSampler;

vec3 sunDir = vec3(0.3,0.6,0.1);            //my imaginary sun
uniform float blend;

float noise(vec2 pos){
    return fract(sin(dot(pos,vec2(12.9898,78.233)))*43758.5453);
}

void main(){
    float dotProd = dot(normalize(vNormal),sunDir);
    outputColor = texture(texSampler, vTexCoord)*max(min(dotProd*1.7,1.7),0.5);
    if(outputColor.a<0.5) discard;
    if(blend < 1.0){
        float ns = noise(vTexCoord);
        if(ns>blend) discard;
    }
}