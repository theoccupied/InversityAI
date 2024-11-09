using OpenTK.Mathematics;

namespace Hackathon.particles
{
    internal struct ParticleTemplate
    {
        public int maxParticles;
        public Vector3 angle;
        public float spread;
        public float frequency;
        public int batchSize;
        public float timeAlive;
        public bool oneShot;
        public float scaleOverTime;
        public bool fade;
        public Texture texture;
        public int numTextures;
        public float startSize;
        public float gravity;

        public ParticleTemplate(int maxParticles, Vector3 angle,
            float spread, float frequency, int batchSize, float timeAlive, bool oneShot,
            Texture texture, int numTextures, bool fade, float startSize, float scaleOverTime, float gravity)
        {
            this.maxParticles = maxParticles;
            this.angle = angle;
            this.spread = spread;
            this.frequency = frequency;
            this.batchSize = batchSize;
            this.timeAlive = timeAlive;
            this.oneShot = oneShot;
            this.texture = texture;
            this.numTextures = numTextures;
            this.startSize = startSize;
            this.gravity = gravity;
            this.texture=texture;
            this.scaleOverTime = scaleOverTime;
            this.fade = fade;
        }
    }
}
