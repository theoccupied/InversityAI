using Hackathon.shaders;
using OpenTK.Mathematics;
using SFML.Graphics;

namespace Hackathon
{
    internal class Entity
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public EntityShader shaderProgram;
        public float opacity = 1f;
        public bool hasCollisions = false;

        public IRenderable model;
        protected EntityManager entities;
        public Entity(Vector3 position, IRenderable model)
        {
            this.position = position;
            rotation = Vector3.Zero;
            scale = Vector3.One;
            this.model = model;
            this.shaderProgram = (EntityShader)Toolbox.shaders["default"];
        }
        public Entity(Vector3 position, Vector3 scale, IRenderable model)
        {
            this.position = position;
            rotation = Vector3.Zero;
            this.scale = scale;
            this.model = model;
            this.shaderProgram = (EntityShader)Toolbox.shaders["default"];
        }
        public Entity(Vector3 position, IRenderable model, EntityShader shaderProgram)
        {
            this.shaderProgram = shaderProgram;
            this.position = position;
            rotation = Vector3.Zero;
            scale = Vector3.One;
            this.model = model;
        }
        public Entity(Vector3 position, EntityShader shaderProgram)
        {
            this.shaderProgram = shaderProgram;
            this.position = position;
            rotation = Vector3.Zero;
            scale = Vector3.One;
        }
        public virtual void Init(EntityManager entities)
        {
            this.entities = entities;
        }
        public void Destroy()
        {
            entities.removeEntity(this);
        }
        public virtual void uploadShaderVars() { }
        public virtual void Render() {
            shaderProgram.bind();
            shaderProgram.currentEntity = this;
            uploadShaderVars();
            shaderProgram.uploadUniforms();
            model.Bind();
            model.Render();
            model.Unbind();
            shaderProgram.unbind();
        }
        public virtual void Tick() { }
    }
}
