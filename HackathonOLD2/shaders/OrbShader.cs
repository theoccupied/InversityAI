using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.shaders
{
    internal class OrbShader : EntityShader
    {
        protected UniformFloat blendU;
        protected UniformFloat timeU;
        public OrbShader(Camera cam) : base(cam, "orb")
        {
            blendU = new UniformFloat("blend", shaderHandle);
            timeU = new UniformFloat("time", shaderHandle);
        }
        public float blend;
        public override void uploadUniforms()
        {
            if (currentEntity == null) return;
            base.uploadUniforms();
            blendU.upload(blend);
            timeU.upload(Time.time);
        }
    }
}
