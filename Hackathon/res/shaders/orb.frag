#version 420 core

in vec2 vTexCoord;
in vec3 vNormal;

out vec4 outputColor;

layout(binding = 0) uniform sampler2D texSampler;

vec3 sunDir = vec3(0.3,0.1,0.1);            //my imaginary sun
uniform float blend;
uniform float time;

float noise(vec2 pos){
    return fract(sin(dot(pos,vec2(12.9898,78.233)))*43758.5453);
}

void main(){
    float dotProd = abs(dot(normalize(vNormal),sunDir));

    vec2 newCoords = vTexCoord-0.5;
    newCoords.x += sin(time+vTexCoord.y*4.0)/4.0;
    float moveNorm = (sin(time*4.0+newCoords.y*newCoords.y*32.0)*sin(time*4.0+newCoords.x*newCoords.x*32.0)+1.0)/2.0;
    float move = ((sin(time*4.0+newCoords.y*newCoords.y*32.0)*sin(time*4.0+newCoords.x*newCoords.x*32.0)+1.0)/4.0+0.5);
    outputColor = vec4((sin(time)+1.0)/6.0,move,abs(sin(time)),moveNorm);

    //if(moveNorm < 0.2) discard;
    //else if(blend>=1.0);
    
}