using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SantaQuest;
using ThanaNita.MonoGameTnt;

public class LevelScreen : Actor
{
    private Vector2 screenSize;
    private ExitNotifier exitNotifier;
    private Texture2D backgroundTexture;
    private Placeholder placeholder = new Placeholder();
    private Panel Level1, Level2, Level3, Level4, Level5, Level6, Level7, Ranvel, Menu;

    public LevelScreen(Vector2 screenSize, ExitNotifier exitNotifier, GraphicsDevice graphicsDevice)
    {
        this.screenSize = screenSize;
        this.exitNotifier = exitNotifier;

        // พื้นหลังของ HomeScreen
        backgroundTexture = TextureCache.Get("SantaQuest.jpg");
        var panel = new Panel(screenSize, backgroundTexture, Color.Black, 5f);
        placeholder.Add(panel);
        Add(placeholder);

        // Start Panel
        var Level1Position = new Vector2(100, 500);
        var Level1Size = new Vector2(100, 100);
        var Level1BackgroundTexture = TextureCache.Get("level1.jpg");
        Level1 = new Panel(Level1Size, Level1BackgroundTexture, Color.White, 5f)
        {
            Position = Level1Position
        };
        placeholder.Add(Level1);

        // Exit Panel
        var Level2Position = new Vector2(400, 500);
        var Level2Size = new Vector2(100, 100);
        var Level2BackgroundTexture = TextureCache.Get("level2.jpg");
        Level2 = new Panel(Level2Size, Level2BackgroundTexture, Color.White, 5f)
        {
            Position = Level2Position
        };
        placeholder.Add(Level2);

        var Level3Position = new Vector2(700, 500);
        var Level3Size = new Vector2(100, 100);
        var Level3BackgroundTexture = TextureCache.Get("level3.jpg");
        Level3 = new Panel(Level3Size, Level3BackgroundTexture, Color.White, 5f)
        {
            Position = Level3Position
        };
        placeholder.Add(Level3);

        var Level4Position = new Vector2(1000, 500);
        var Level4Size = new Vector2(100, 100);
        var Level4BackgroundTexture = TextureCache.Get("level4.jpg");
        Level4 = new Panel(Level4Size, Level4BackgroundTexture, Color.White, 5f)
        {
            Position = Level4Position
        };
        placeholder.Add(Level4);

        var Level5Position = new Vector2(1300, 500);
        var Level5Size = new Vector2(100, 100);
        var Level5BackgroundTexture = TextureCache.Get("level5.jpg");
        Level5 = new Panel(Level5Size, Level5BackgroundTexture, Color.White, 5f)
        {
            Position = Level5Position
        };
        placeholder.Add(Level5);

        var Level6Position = new Vector2(1600, 500);
        var Level6Size = new Vector2(100, 100);
        var Level6BackgroundTexture = TextureCache.Get("level6.jpg");
        Level6 = new Panel(Level6Size, Level6BackgroundTexture, Color.White, 5f)
        {
            Position = Level6Position
        };
        placeholder.Add(Level6);

        /*var Level7Position = new Vector2(100, 800);
        var Level7Size = new Vector2(100, 100);
        var Level7BackgroundTexture = TextureCache.Get("level7.jpg");
        Level7 = new Panel(Level7Size, Level7BackgroundTexture, Color.White, 5f)
        {
            Position = Level7Position
        };
        placeholder.Add(Level7);*/

        var LevelRanPosition = new Vector2(400, 800);
        var LevelRanSize = new Vector2(100, 100);
        var LevelRanBackgroundTexture = TextureCache.Get("levelrandom.jpg");
        Ranvel = new Panel(LevelRanSize, LevelRanBackgroundTexture, Color.White, 5f)
        {
            Position = LevelRanPosition
        };
        placeholder.Add(Ranvel);

        var MenuPosition = new Vector2(1300, 800);
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
            // ตรวจสอบว่าคลิกบน Level1 หรือไม่
            if (Level1.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 1)) // ส่งรหัส 1 สำหรับ Level1
                ));
            }
            // ตรวจสอบว่าคลิกบน Level2 หรือไม่
            else if (Level2.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 2)) // ส่งรหัส 2 สำหรับ Level2
                ));
            }

            else if (Level3.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 3)) // ส่งรหัส 2 สำหรับ Level2
                ));
            }
            else if (Level4.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 4)) // ส่งรหัส 2 สำหรับ Level2
                ));
            }
            else if (Level5.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 5)) // ส่งรหัส 2 สำหรับ Level2
                ));
            }
            else if (Level6.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 6)) // ส่งรหัส 2 สำหรับ Level2
                ));
            }
            /*else if (Level7.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 7)) // ส่งรหัส 2 สำหรับ Level2
                ));
            }*/

            else if (Ranvel.GetBoundingBox().Contains(mousePosition))
            {
                var random = new Random();
                int ran = random.Next(1, 7);

                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, ran)) // ส่งค่าที่สุ่มได้
                ));
            }
            else if (Menu.GetBoundingBox().Contains(mousePosition))
            {
                AddAction(new SequenceAction(
                    Actions.FadeOut(0.5f, this),
                    new RunAction(() => exitNotifier(this, 8)) // ส่งรหัส 2 สำหรับ Level2
                ));
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
