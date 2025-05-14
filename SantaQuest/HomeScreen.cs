using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SantaQuest;
using ThanaNita.MonoGameTnt;

public class HomeScreen : Actor
{
    private Vector2 screenSize;
    private ExitNotifier exitNotifier;
    private Texture2D backgroundTexture;
    private Placeholder placeholder = new Placeholder();
    private Panel startPanel;
    private Panel exitPanel;

    public HomeScreen(Vector2 screenSize, ExitNotifier exitNotifier, GraphicsDevice graphicsDevice)
    {
        this.screenSize = screenSize;
        this.exitNotifier = exitNotifier;

        // พื้นหลังของ HomeScreen
        backgroundTexture = TextureCache.Get("SantaQuest.jpg");
        var panel = new Panel(screenSize, backgroundTexture, Color.Black, 5f);
        placeholder.Add(panel);
        Add(placeholder);

        // Start Panel
        var startPanelPosition = new Vector2(800, 500);
        var startPanelSize = new Vector2(300, 100);
        var startPanelBackgroundTexture = TextureCache.Get("StartButton.jpg");
        startPanel = new Panel(startPanelSize, startPanelBackgroundTexture, Color.White, 5f)
        {
            Position = startPanelPosition
        };
        placeholder.Add(startPanel);

        // Exit Panel
        var exitPanelPosition = new Vector2(800, 700);
        var exitPanelSize = new Vector2(300, 100);
        var exitPanelBackgroundTexture = TextureCache.Get("ExitButton.jpg");
        exitPanel = new Panel(exitPanelSize, exitPanelBackgroundTexture, Color.White, 5f)
        {
            Position = exitPanelPosition
        };
        placeholder.Add(exitPanel);
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
            // ตรวจสอบว่าคลิกบน StartPanel หรือไม่
            if (startPanel.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 1)) // ส่งรหัส 1 สำหรับ startPanel
                ));
            }
            // ตรวจสอบว่าคลิกบน ExitPanel หรือไม่
            else if (exitPanel.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 2)) // ส่งรหัส 2 สำหรับ exitPanel
                ));
            }
        }

        // ตัวอย่างการตรวจสอบปุ่ม Tab
        /*if (keyInfo.IsKeyPressed(Keys.Tab))
        {
            AddAction(new SequenceAction(
                Actions.FadeOut(0.5f, this),
                new RunAction(() => exitNotifier(this, 0))
            ));
        }*/
    }


}
