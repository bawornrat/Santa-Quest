using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SantaQuest;
using ThanaNita.MonoGameTnt;

public class Success : Actor
{
    private Vector2 screenSize;
    private ExitNotifier exitNotifier;
    private Texture2D backgroundTexture;
    private Placeholder placeholder = new Placeholder();
    private Panel Menu;

    public Success(Vector2 screenSize, ExitNotifier exitNotifier, GraphicsDevice graphicsDevice)
    {
        this.screenSize = screenSize;
        this.exitNotifier = exitNotifier;

        // พื้นหลังของ HomeScreen
        backgroundTexture = TextureCache.Get("Success.jpg");
        var panel = new Panel(screenSize, backgroundTexture, Color.Black, 5f);
        placeholder.Add(panel);
        Add(placeholder);
        

        // Back Panel
        var MenuPosition = new Vector2(750, 600);
        var MenuSize = new Vector2(500, 150);
        var MenuBackgroundTexture = TextureCache.Get("BackButton.jpg");
        Menu = new Panel(MenuSize, MenuBackgroundTexture, Color.White, 5f)
        {
            Position = MenuPosition
        };
        placeholder.Add(Menu);
    }

    public override void Act(float deltaTime)
    {
        base.Act(deltaTime);

        var keyInfo = GlobalKeyboardInfo.Value;
        var mouseState = Mouse.GetState(); // ดึงสถานะของเมาส์
        var mousePosition = new Vector2(mouseState.X, mouseState.Y); // ตำแหน่งของเมาส์

        // ตรวจสอบการคลิกเมาส์ซ้าย
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (Menu.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 0))));
            }
        }
        /*
        if (keyInfo.IsKeyPressed(Keys.Tab))
            AddAction(new SequenceAction(
                Actions.FadeOut(0.5f, this),
                new RunAction(() => exitNotifier(this, 0))
            ));
        */
    }
}
