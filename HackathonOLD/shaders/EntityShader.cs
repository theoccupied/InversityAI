using OpenTK.Mathematics;

namespace Hackathon.shaders
{
    internal class EntityShader : ShaderProgram
    {
        protected UniformMatrix4 transformationU;
        protected UniformMatrix4 projectionU;
        protected UniformMatrix4 viewU;
        protected Camera cam;
        public EntityShader(Camera cam, string name) : base(name)
        {
            transformationU = new UniformMatrix4("model", shaderHandle);
            projectionU = new UniformMatrix4("projection", shaderHandle);
            viewU = new UniformMatrix4("view", shaderHandle);
            this.cam = cam;
        }
        public Entity? currentEntity;
        public override void uploadUniforms()
        {
            if (currentEntity == null) return;

            transformationU.upload(
                Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(currentEntity.rotation)) *
                Matrix4.CreateScale(currentEntity.scale) *
                Matrix4.CreateTranslation(currentEntity.position)
            );
            viewU.upload(cam.viewMatrix);
            projectionU.upload(cam.generateProjectionMatrix());
        }
    }
}
