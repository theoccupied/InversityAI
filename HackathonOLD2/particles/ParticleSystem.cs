using Hackathon.shaders;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using static OpenTK.Graphics.OpenGL.GL;
namespace Hackathon.particles
{
    class Particle
    {
        public Vector3 position;
        public Vector3 velocity;
        public float time;
        public float opacity = 1f;
        public float scale;
        public Particle(Vector3 position, Vector3 velocity, float time, float scale)
        {
            this.position = position;
            this.velocity = velocity;
            this.time = time;
            this.scale = scale;
        }
    }
    internal class ParticleSystem : Entity
    {
        public List<Particle> particles = new List<Particle>();
        public int maxParticles;
        public bool emitting = true;
        public Vector3 angle;
        public float spread;
        public float frequency;
        public int batchSize;
        public float timeAlive;
        public bool oneShot;
        public float scaleOverTime;
        public bool fade;
        public Texture texture;
        private Random random = new Random();
        public int numTextures;
        public float startSize;
        public float gravity;
        public ParticleSystem(Vector3 position, ParticleTemplate template) : base(position, (shaders.EntityShader)Toolbox.shaders["particle"])
        {
            this.maxParticles = template.maxParticles;
            this.angle = template.angle;
            this.spread = template.spread;
            this.gravity = template.gravity;
            this.frequency = template.frequency;
            this.batchSize = template.batchSize;
            this.timeAlive = template.timeAlive;
            this.startSize = template.startSize;
            this.oneShot = template.oneShot;
            this.texture = template.texture;
            this.numTextures = template.numTextures;
            this.fade = template.fade;
            this.scaleOverTime = template.scaleOverTime;
            if (oneShot)
            {
                //Emit all particles
                for (int i = 0; i < batchSize; i++)
                {
                    Emit();
                }
            }
        }
        public ParticleSystem(Vector3 position, int maxParticles, Vector3 angle,
            float spread, float frequency, int batchSize, float timeAlive, bool oneShot,
            Texture texture, int numTextures, bool fade, float startSize, float scaleOverTime, float gravity) :
            base(position, (shaders.EntityShader)Toolbox.shaders["particle"])
        {
            this.maxParticles = maxParticles;
            this.angle = angle;
            this.spread = spread;
            this.gravity = gravity;
            this.frequency = frequency;
            this.batchSize = batchSize;
            this.timeAlive = timeAlive;
            this.startSize = startSize;
            this.oneShot = oneShot;
            this.texture = texture;
            this.numTextures = numTextures;
            this.fade = fade;
            this.scaleOverTime = scaleOverTime;
            if (oneShot)
            {
                //Emit all particles
                for(int i = 0; i < batchSize; i++)
                {
                    Emit();
                }
            }
        }
        float time = 0;
        public override void Tick()
        {
            if (oneShot && particles.Count == 0) Destroy();
            time += Time.deltaTime;
            
            if(!oneShot && time > frequency && emitting)
            {
                time = 0f;
                if(particles.Count < maxParticles)
                {
                    for (int i = 0; i < batchSize; i++)
                        Emit();
                }
            }
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                Particle particle = particles[i];
                if(Time.time - particle.time >= timeAlive)
                {
                    particles.RemoveAt(i);
                    continue;
                }
                particle.position += particle.velocity * Time.deltaTime;
                particle.velocity.Y += gravity * Time.deltaTime;
                if(fade)
                    particle.opacity = 1f - (Time.time - particle.time)/timeAlive;
                particle.scale = startSize + (Time.time - particle.time) * scaleOverTime;

            }
        }
        private void Emit()
        {
            particles.Add(new Particle(position, angle + new Vector3((float)random.NextDouble() * 2f - 1f, (float)random.NextDouble() * 2f - 1f, (float)random.NextDouble() * 2f - 1f) * spread, Time.time, startSize));
        }
        public override void Render()
        {
            ParticleShader shader = (ParticleShader)shaderProgram;
            shader.bind();
            BindVertexArray(Toolbox.square.vaoID);
            EnableVertexAttribArray(0);
            texture.bind(TextureUnit.Texture0);
            shader.numTextures = numTextures;
            foreach (Particle particle in particles)
            {
                shader.currentParticle = particle;
                shader.curTexture = (int)((Time.time - particle.time)/timeAlive * numTextures);
                shader.uploadUniforms();
                DrawArrays(PrimitiveType.TriangleStrip, 0, Toolbox.square.vertexCount);
            }
            texture.unbind(TextureUnit.Texture0);
            DisableVertexAttribArray(0);
            BindVertexArray(0);
            shader.unbind();
        }

    }
}
