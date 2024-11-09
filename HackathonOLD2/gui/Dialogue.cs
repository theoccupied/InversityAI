using OpenTK.Mathematics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Hackathon.gui
{
    internal struct Dialogue
    {
        public string title;
        public string mainText;
        public Color4 background;
        public Color4 textCol;

        public Dialogue(string title, string mainText, Color4 background, Color4 textCol)
        {
            this.mainText = mainText;
            this.title = title;
            this.background = background;
            this.textCol = textCol;
        }
    }
}
