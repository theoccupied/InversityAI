using Hackathon.gui;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using static OpenTK.Graphics.OpenGL.GL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using Hackathon.shaders;


namespace Hackathon.scenes
{
    internal class TalkScene : Scene
    {
        Skybox skybox;
        private MouseState mouse;
        GUIText inputBox;
        public TalkScene(MouseState mouse, EntityManager entityManager, GUIManager guiManager, SceneManager sceneManager) : base(entityManager, guiManager, sceneManager)
        {
            this.mouse = mouse;
        }
        OrbEntity sphere;
        public override void Start()
        {
            //MainWindow.instance.CursorState = CursorState.Normal;
            Toolbox.camera.position = new Vector3(0f, 0f, 0f);
            sphere = (OrbEntity)entityManager.addEntity(new OrbEntity(new Vector3(1f, 0, -2), Toolbox.models["sphere"], new OrbShader(Toolbox.camera)));
            
            skybox = new Skybox("Space", Toolbox.camera, OBJLoader.loadOBJ("Cube"), 1);
            inputBox = (GUIText)guiManager.Add(new GUIText(new Vector2(-3.7f, -3.5f), 0.25f, Fonts.FONT_READABLE, ""));
        }
        public override void Update()
        {
            base.Update();
            Vector2 mousePos = mouse.Position*new Vector2(2f/1920f, 2f/1080f);
            mousePos -= Vector2.One;
            Vector3 screenPos = new Vector3(mousePos.X, mousePos.Y, 0f);
            Vector3 dir = (new Vector3(0,0,-20f) - screenPos);
            dir.Normalize();

            float yaw = (float)Math.Atan2(dir.X, dir.Z);
            float pitch = (float)Math.Asin(dir.Y);

            //sphere.rotation.X = -pitch+MathF.PI;
            //sphere.rotation.Y = -yaw;
            //sphere.rotation.Z = MathF.PI;

            Toolbox.camera.rotation = new Vector3(mousePos.Y/10f, mousePos.X/10f, 0f);


            sphere.fancy = Program.thinking;
            char c = Keyboard.getTypedKey();
            if (c != '\0')
            {
                inputBox.AddChar(c);
            }
            if (Keyboard.getSpecialKey(Keys.Backspace) && inputBox.currentText.Length>0)
            {
                inputBox.Text = inputBox.Text.Substring(0, inputBox.Text.Length - 1);
            }
            if (Keyboard.getKey(Keys.Enter))
            {
                if(inputBox.Text.Length > 0)
                {
                    Program.request = inputBox.Text;
                    inputBox.Text = "";
                }
            }

            //Toolbox.camera.rotation.Y += Time.deltaTime;

        }
        public override void Render()
        {
            Clear(ClearBufferMask.DepthBufferBit);

            Disable(EnableCap.CullFace);
            skybox.Render();
            Enable(EnableCap.CullFace);

            Enable(EnableCap.Blend);
            BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            base.Render();
            Disable(EnableCap.Blend);

        }
        public override void OnClose()
        {

        }
    }
}
