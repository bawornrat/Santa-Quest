using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class Label : Actor
    {
        TextDrawable text;

        public string Text { get => text.Text; set => text.Text = value; }

        public Label(string fontName, float fontSize, Color color, string text)
        {
            this.text = new TextDrawable(fontName, fontSize, Color.White, text);
            this.Color = color;
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            base.Draw(target, state);
            text.Draw(target, CombineState(state));
        }
    }
}
