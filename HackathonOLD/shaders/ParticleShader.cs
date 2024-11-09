using Hackathon.particles;
using OpenTK.Mathematics;

namespace Hackathon.shaders
{
    internal class ParticleShader : EntityShader
    {
        private UniformFloat blendU;
        private UniformFloat scaleU;
        private UniformInteger numTexturesU;
        private UniformInteger curTextureU;
        public ParticleShader(Camera cam) : base(cam,"particle")
        {
            currentParticle = new Particle(Vector3.Zero, Vector3.Zero,0f, 0f);
            blendU = new UniformFloat("blend", shaderHandle);
            scaleU = new UniformFloat("scale", shaderHandle);
            numTexturesU = new UniformInteger("numTextures", shaderHandle);
            curTextureU = new UniformInteger("curTexture", shaderHandle);
        }
        public Particle currentParticle;
        public int numTextures;
        public int curTexture;
        public override void uploadUniforms()
        {
            blendU.upload(currentParticle.opacity);
            scaleU.upload(currentParticle.scale);
            numTexturesU.upload(numTextures);
            curTextureU.upload(curTexture);

            transformationU.upload(
                Matrix4.CreateScale(currentParticle.scale) *
                Matrix4.CreateTranslation(currentParticle.position)
            );
            viewU.upload(cam.viewMatrix);
            projectionU.upload(cam.generateProjectionMatrix());
        }
        
    }
}
