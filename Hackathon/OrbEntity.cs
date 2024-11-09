using Hackathon.shaders;
using OpenTK.Mathematics;

namespace Hackathon
{
    internal class OrbEntity : Entity
    {
        public float blend;
        public bool fancy = false;
        public OrbEntity(Vector3 position, IRenderable model, EntityShader shader) : base(position, model, shader)
        {

        }
        public override void uploadShaderVars()
        {
            ((OrbShader)shaderProgram).blend = blend;
            base.uploadShaderVars();
        }
        public override void Tick()
        {
            if (fancy && blend > 0)
            {
                blend = MathF.Max(0f, blend - Time.deltaTime);
            }
            else if(!fancy && blend < 1f)
            {
                blend = MathF.Min(1f, blend + Time.deltaTime);
            }
            base.Tick();
        }
    }
}
