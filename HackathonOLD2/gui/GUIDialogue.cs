using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hackathon.gui
{
    internal class GUIDialogue : GUIObject
    {
        private GUIText titleGUI;
        private GUIText textGUI;
        public string mainText="";
        public string titleText="";
        float animTimer = 0f;
        bool intro = false;
        public bool visible { get; private set; } = false;
        public void displayDialogue(Dialogue dialogue)
        {
            this.titleText = dialogue.title;
            this.mainText = dialogue.mainText;
            color = dialogue.background;
            color.A = 0f;
            textGUI.color = dialogue.textCol;
            titleGUI.color = Color4.White;
            animTimer = 0f;
            intro = true;
            closing = false;
            visible = true;
        }
        public void closeDialogue()
        {
            animTimer = 1f;
            closing = true;

        }
        float targetY = 0f;
        private bool typingTitle = false;
        private bool typingText = false;
        private bool closing = false;
        public bool opened = false;
        public GUIDialogue() : base(Vector2.Zero, new Vector2(1.92f * 2.5f, 1.08f * 2.5f), new Texture("gui/Dialogue", TextureMinFilter.Nearest))
        {
            titleGUI = new GUIText(new Vector2(-2.3f,2f), 0.2f, Fonts.FONT_LARGE, "");
            textGUI = new GUIText(new Vector2(-2.3f, 1.5f), 0.2f, Fonts.FONT_SMALL, "");
            targetY = position.Y;
        }

        public override void Render(RawModel square, GUIShader shader)
        {
            if(!visible) return;
            base.Render(square, shader);
            titleGUI.Render(square, shader);
            textGUI.Render(square, shader);
        }
        public override void Update()
        {
            if (!visible) return;
            if (closing)
            {
                animTimer -= Time.deltaTime * 2f;
                if (animTimer <= 0f)
                {
                    visible = false;
                    closing = false;
                    opened = false;
                    textGUI.Text = "";
                    titleGUI.Text = "";
                }
                color.A = animTimer > 0.9f ? 0.9f : animTimer;
                textGUI.color.A = animTimer;
                titleGUI.color.A = animTimer;
                return;
            }
            if (intro)
            {
                animTimer += Time.deltaTime*2f;

                position.Y = targetY - 1f + animTimer;
                color.A = animTimer > 0.9f ? 0.9f : animTimer;

                if (animTimer >= 1f)
                {
                    animTimer = 0f;
                    intro = false;
                    typingTitle = true;
                    opened = true;
                }
            }else if (typingTitle)
            {
                animTimer += Time.deltaTime * 10f;
                if (animTimer >= 1f)
                {
                    animTimer = 0f;
                    if (titleGUI.currentText.Length >= titleText.Length)
                    {
                        typingTitle = false;
                        typingText = true;
                        return;
                    }
                    titleGUI.AddChar(titleText[titleGUI.currentText.Length]);
                }
            }else if (typingText)
            {
                animTimer += Time.deltaTime * 30f;
                if (animTimer >= 1f)
                {
                    animTimer = 0f;
                    if (textGUI.currentText.Length >= mainText.Length)
                    {
                        typingText = false;
                        return;
                    }
                    textGUI.AddChar(mainText[textGUI.currentText.Length]);
                }
            }
            
            if (opened)
            {
                if (Keyboard.getKey(Keys.E) && !closing)
                {
                    closeDialogue();
                }
            }
        }
    }
}
