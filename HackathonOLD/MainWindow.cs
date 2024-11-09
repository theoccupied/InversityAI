using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using static OpenTK.Graphics.OpenGL.GL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Hackathon.gui;
using Hackathon.shaders;
using Hackathon.scenes;

namespace Hackathon
{
    internal class MainWindow : GameWindow
    {
        SceneManager sceneManager;
        EntityManager entities = new EntityManager();
        GUIManager gui;

        public MainWindow() : base(
            GameWindowSettings.Default,
            new NativeWindowSettings()
            {
                Title = "Hackathon",                   //Title of the window
                ClientSize = new Vector2i(1920, 1080),    //Window size
                WindowBorder = WindowBorder.Fixed,      //Window cannot be resized
                StartVisible = false,                   //Window only set visible after initialised to avoid funny behaviour
                StartFocused = true,                    //Focuses the window upon creation
                Vsync = VSyncMode.On,                   //Frame rate matched to monitors frame rate
                API = ContextAPI.OpenGL,                //Using OpenGL
                Profile = ContextProfile.Core,          //Core OpenGL
                APIVersion = new Version(3, 3)          //OpenGL version 3.3
            })
        {
            CenterWindow();
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            Keyboard.keyDown(e.Key);
            base.OnKeyDown(e);

        }
        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            Keyboard.keyUp(e.Key);
            base.OnKeyUp(e);
        }
        
        protected override void OnLoad()
        {
            IsVisible = true;
            CursorState = CursorState.Normal;
            ClearColor(OpenTK.Mathematics.Color4.DarkCyan); //i like this colour
            Enable(EnableCap.DepthTest);        //For later on when 3d stuff is happening
            Enable(EnableCap.CullFace);
            CullFace(CullFaceMode.Back);
            Toolbox.camera = new Camera(new Vector3(0f, 3f, 2f), Vector3.Zero);
            Toolbox.initToolbox();
            FrontFace(FrontFaceDirection.Ccw);
            DefaultShader shader = new DefaultShader(Toolbox.camera);
            Toolbox.shaders.Add("default", shader);
            
            gui = new GUIManager(1080f / 1920f);
            
            Fonts.initFonts();
            sceneManager = new SceneManager(entities, gui);
            sceneManager.LoadScene(new TalkScene(MouseState, entities, gui, sceneManager));
            Time.init();
            base.OnLoad();
        }
        //acab even the memory police
        protected override void OnUnload()
        {
            sceneManager.Unload();
            base.OnUnload();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Time.update();
            sceneManager.Update();
            Toolbox.camera.update();
            base.OnUpdateFrame(args);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            sceneManager.Render();
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
