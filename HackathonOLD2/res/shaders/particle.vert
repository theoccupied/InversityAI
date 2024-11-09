#version 330 core

layout (location = 0) in vec2 aPosition;    //Vertex position

out vec2 vTexCoord;

uniform float scale;
uniform mat4 model;                         //Transformation matrix
uniform mat4 projection;                    //Projection matrix
uniform mat4 view;                          //View Matrix

void main(){
    vTexCoord = vec2((aPosition.x+1.0)/2.0, 1 - (aPosition.y+1.0)/2.0); //Allow for interpolation
    
    //Transform the vertex position by the transformation matrix
    mat4 mvp = projection * view * model;
    vec4 centrePos = mvp * vec4(0.0,0.0,0.0, 1.0);
    gl_Position = vec4(centrePos.x+aPosition.x*1080.0/1920.0*scale, centrePos.y+aPosition.y*scale, centrePos.z,centrePos.w);
}