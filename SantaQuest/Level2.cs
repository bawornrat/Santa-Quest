using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using SantaQuest;
using ThanaNita.MonoGameTnt;


namespace SantaQuest
{
    public class Level2 : Actor
    {
        CameraMan cameraMan;
        private Santy santa;
        private List<Block> blocks = new List<Block>();
        private List<SpikeUp> spikeups = new List<SpikeUp>();   
        private List<Ball> balls = new List<Ball>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        public Level2(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
        {
            this.screenSize = screenSize;
            this.exitNotifier = exitNotifier;
            this.cameraMan = cameraMan;

            backgroundTexture = TextureCache.Get("LevelWall.jpg");
            var panel = new Panel(screenSize, backgroundTexture, Color.Black, 5f);
            placeholder.Add(panel);
            Add(placeholder);
            // เริ่มต้นการสร้างวัตถุ
            InitializeObjects();
            this.cameraMan = cameraMan;
        }

        private void InitializeObjects()
        {
            blocks.Add(new Block(new RectF(0, 600, 670, 2500)));   // พื้นริมซ้าย
            blocks.Add(new Block(new RectF(520, 600, 870, 2500))); // พื้นตรงกลาง
            blocks.Add(new Block(new RectF(1100, 600, 730, 2500)));   // พื้นริมขวา
            blocks.Add(new Block(new RectF(250, 530, 180, 50)));
            blocks.Add(new Block(new RectF(400, 470, 180, 50)));   // แท่น 1
            blocks.Add(new Block(new RectF(650, 400, 100, 30)));   // แท่น 2
            blocks.Add(new Block(new RectF(1040, 500, 150, 30)));  // แท่น 3
            blocks.Add(new Block(new RectF(550, 400, 150, 30))); 
            blocks.Add(new Block(new RectF(850, 400, 150, 30)));
            blocks.Add(new Block(new RectF(-500, -10, 1010, 1615))); //กันตกข้างซ้าย
            blocks.Add(new Block(new RectF(1360, -10, 490, 1615))); //กันตกข้างขวา

            float Blockduration = 0.8f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            blocks[4].AddAction(Actions.Forever(
                new MoveByAction(Blockduration, new Vector2(100, 0), blocks[4]),
                new MoveByAction(Blockduration, new Vector2(-100, 0), blocks[4])
            ));
            blocks[5].AddAction(Actions.Forever(
                new MoveByAction(Blockduration, new Vector2(100, -100), blocks[5]),
                new MoveByAction(Blockduration, new Vector2(-100, 100), blocks[5])
            ));


            spikeups.Add(new SpikeUp(new RectF(200, 580, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(225, 580, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(280, 580, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(305, 580, 28, 22)));

            balls.Add(new Ball(new RectF(250, 370, 30, 30)));       // บอลหนามที่ 1
            balls.Add(new Ball(new RectF(500, 370, 30, 30)));       // บอลหนามที่ 2
            balls.Add(new Ball(new RectF(750, 370, 30, 30)));       // บอลหนามที่ 3
            balls.Add(new Ball(new RectF(1000, 470, 30, 30)));      // บอลหนามที่ 4
            balls.Add(new Ball(new RectF(1250, 370, 30, 30)));      // บอลหนามที่ 5
            balls.Add(new Ball(new RectF(750, 550, 100, 100)));       // บอลหนามอยู่กับที่

            float Ballduration = 0.5f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            balls[0].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, 100), balls[0]),
                new MoveByAction(Ballduration, new Vector2(0, -100), balls[0])
            ));
            balls[1].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(-100, 0), balls[1]),
                new MoveByAction(Ballduration, new Vector2(100, 0), balls[1])
            ));
            balls[2].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, 100), balls[2]),
                new MoveByAction(Ballduration, new Vector2(0, -100), balls[2])
            ));
            balls[3].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, -150), balls[3]),
                new MoveByAction(Ballduration, new Vector2(0, 150), balls[3])
            ));
            balls[4].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, 250), balls[4]),
                new MoveByAction(Ballduration, new Vector2(0, -250), balls[4])
            ));


            doors.Add(new Door(new RectF(1275, 500, 100, 100)));

            // เพิ่ม santa
            santa = new Santy(new Vector2(10, 250));
            santa.Add(cameraMan);

            // เพิ่มวัตถุทั้งหมดในฉาก
            AddAllObjects();
        }

        private void AddAllObjects()
        {
            foreach (var spikeup in spikeups)
                Add(spikeup);

            foreach (var ball in balls)
                Add(ball);

            foreach (var door in doors)
                Add(door);

            foreach (var Block in blocks)
                Add(Block);

            Add(santa);

        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            // ตรวจสอบการชนกับปุ่มและวัตถุ
            CheckTriggers(deltaTime);           
        }

        private void CheckTriggers(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            if (Math.Abs(santa.Position.X - 1325) < 10f && Math.Abs(santa.Position.Y - 580) < 10f)
            {
                if (keyInfo.IsKeyPressed(Keys.F))
                { 
                    santa.StopMusic();
                    santa.Remove(cameraMan);
                    cameraMan.DefaultCamera();
                    AddAction(new SequenceAction(
                                    Actions.FadeOut(0.5f, this),
                                    new RunAction(() => exitNotifier(this, 0))
                    ));
                }
            }
        }

    }
}
