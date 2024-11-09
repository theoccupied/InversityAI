using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using static OpenTK.Graphics.OpenGL.GL;

namespace Hackathon
{
    internal class TexturedModel : IRenderable
    {
        //The model and texture we are connecting in one class
        public  Model model;
        private Texture texture;
        public TexturedModel(Model model, Texture texture)
        {
            this.model = model;
            this.texture = texture;
        }
        public void Bind()
        {
            model.Bind();
            texture.bind(TextureUnit.Texture0);
        }
        public void Unbind() {
            model.Unbind();
            texture.unbind(TextureUnit.Texture0);
        }
        public void Render()
        {
            model.Render();
        }
        //acab even the memory police
        public void CleanUp()
        {
            model.CleanUp();
        }
    }
    internal class RawModel
    {
        public int vaoID;
        public int vertexCount;

        public RawModel(int vaoID, int vertexCount)
        {
            this.vaoID = vaoID;
            this.vertexCount = vertexCount;
        }
    }
    internal class Model : IRenderable 
    {
        //Information about the model on the GPU
        public int vaoHandle { get; }
        public int vboHandle { get; }
        public int indexHandle { get; }
        public int vertexCount { get; }
        public int indicesCount { get; }

        public int[] textures;
        public Triangle[]? triangles;
        public Vector3 smallestPoint;
        public Vector3 largestPoint;

        public Model(int vaoHandle, int vboHandle, int indexHandle, int vertexCount, int indicesCount, int[] textures)
        {
            this.vboHandle = vboHandle;
            this.indexHandle = indexHandle;
            this.vaoHandle = vaoHandle;
            this.vertexCount = vertexCount;
            this.indicesCount = indicesCount;
            this.textures = textures;
        }
        public Model(int vaoHandle, int vboHandle, int indexHandle, int vertexCount, int indicesCount, int[] textures, Triangle[] triangles)
        {
            this.triangles = triangles;
            this.vboHandle = vboHandle;
            this.indexHandle = indexHandle;
            this.vaoHandle = vaoHandle;
            this.vertexCount = vertexCount;
            this.indicesCount = indicesCount;
            this.textures = textures;
        }
        //Cleans up memory when program ends
        public void CleanUp()
        {
            BindVertexArray(0);
            DeleteVertexArray(vaoHandle);
            BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            BindBuffer(BufferTarget.ArrayBuffer, 0);
            DeleteBuffer(indexHandle);
            DeleteBuffer(vboHandle);
        }
        //Binds the model to the GPU before being rendered
        public void Bind()
        {
            BindVertexArray(vaoHandle);
            BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);
        }
        //Unbinds after being rendered
        public void Unbind()
        {
            BindVertexArray(0);
            BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            BindBuffer(BufferTarget.ArrayBuffer, 0);

        }
        //does the actual rendering thing
        public void Render()
        {
            DrawElements(PrimitiveType.Triangles, indicesCount, DrawElementsType.UnsignedInt, 0);
        }
        public void RenderSection(int start, int length)
        {
            DrawElements(PrimitiveType.Triangles, length*3, DrawElementsType.UnsignedInt, start*3*sizeof(int));
        }
    }
}