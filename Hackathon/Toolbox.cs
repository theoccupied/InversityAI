using Hackathon.particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Hackathon
{
    internal class Toolbox
    {
        public static Dictionary<string, ShaderProgram> shaders = new Dictionary<string, ShaderProgram>();

        public static Dictionary<string, ParticleTemplate> particleTemplates = new Dictionary<string, ParticleTemplate>();

        public static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public static Dictionary<string, IRenderable> models = new Dictionary<string, IRenderable>();

        public static Camera camera;

        public static RawModel square;

        public static void initToolbox()
        {
            models.Add("sphere", OBJLoader.loadOBJ("Sphere"));
            square = Loader.loadToVAO(new float[] { -1, 1, -1, -1, 1, 1, 1, -1 }, 2);
            textures.Add("button", new Texture("gui/Button", TextureMinFilter.Linear));
        }
    }
}
