using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class OnScreenLogger : Actor, Logger
    {
        private TextDrawable text;
        private LogList logList;

        public OnScreenLogger(int maxLine, string fontName, float fontSize, Color fontColor)
        {
            logList = new LogList(maxLine);
            text = new TextDrawable(fontName, fontSize, fontColor, "");

            var textActor = new DrawableAdapter(text);
            Add(textActor);
        }
        public void Log(string message)
        {
            logList.Log(message);
            text.Text = logList.GetCombined();
        }
    }
}
