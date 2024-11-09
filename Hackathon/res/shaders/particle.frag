#version 420 core

in vec2 vTexCoord;

out vec4 outputColor;

layout(binding = 0) uniform sampler2D texSampler;

uniform float blend;
uniform int numTextures;
uniform int curTexture;

float noise(vec2 pos){
    return fract(sin(dot(pos,vec2(12.9898,78.233)))*43758.5453);
}

void main(){
    
    float width = 1.0 / float(numTextures);
    vec2 newTexCoords = vec2(vTexCoord.x*width + width*float(curTexture), vTexCoord.y);
    outputColor = texture(texSampler, newTexCoords);
    if(outputColor.a < 0.5) discard;
    if(blend < 1.0){
        float ns = noise(vTexCoord);
        if(ns>blend) discard;
    }
}