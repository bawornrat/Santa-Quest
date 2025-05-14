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
    public class Level5 : Actor
    {
        CameraMan cameraMan;
        private Santy santa;
        private List<Block> blocks = new List<Block>();
        private List<SpikeUp> spikeups = new List<SpikeUp>();
        private List<SpikeRight> spikerights = new List<SpikeRight>();
        private List<Ball> balls = new List<Ball>();
        private List<Star> stars = new List<Star>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        public Level5(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
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
            blocks.Add(new Block(new RectF(-245, -10, 490, 1615))); //กันตกข้างซ้าย
            blocks.Add(new Block(new RectF(1360, -10, 490, 1615))); //กันตกข้างขวา
            blocks.Add(new Block(new RectF(0, 620, 210, 350)));// ล่างสุด 1
            blocks.Add(new Block(new RectF(150, 620, 1080, 350)));//ล่างสุด 2
            blocks.Add(new Block(new RectF(900, 620, 500, 350)));// ล่างสุด 3
            blocks.Add(new Block(new RectF(1200, 620, 500, 350)));// ล่างสุด 4
            blocks.Add(new Block(new RectF(300, 570, 150, 50)));// บล็อคนอน 1 นับจากล่างขึ้นบน ก่อนถึงบล็อคแนวตั้ง
            blocks.Add(new Block(new RectF(150, 500, 150, 50)));// บล็อคนอน 2
            blocks.Add(new Block(new RectF(300, 430, 150, 50)));// บล็อคนอน 3
            blocks.Add(new Block(new RectF(150, 360, 150, 50)));// บล็อคนอน 4
            blocks.Add(new Block(new RectF(300, 290, 150, 50)));// บล็อคนอน 5
            blocks.Add(new Block(new RectF(0, 100, 370, 50)));// บล็อคนอน 6
            blocks.Add(new Block(new RectF(270, 100, 230, 50)));// บล็อคนอน 7
            blocks.Add(new Block(new RectF(375, 235, 50, 775)));// บล็อคตั้ง 1 นับจากล่างขึ้นบน
            blocks.Add(new Block(new RectF(470, 480, 140, 280))); //บล็อคตั้ง 2
            blocks.Add(new Block(new RectF(375, 65, 50, 205)));// บล็อคตั้ง 3
            blocks.Add(new Block(new RectF(1120, 0, 50, 770))); //บล็อคตั้ง 4
            blocks.Add(new Block(new RectF(1200, 445, 70, 350))); //บล็อคตั้ง 5 ขวาสุด
            blocks.Add(new Block(new RectF(470, 130, 1000, 50)));// บล็อคนอนยาว 1นับจากบนลงล่าง เลยบล็อคแนวตั้งมาแล้ว
            blocks.Add(new Block(new RectF(470, 360, 1300, 50)));// บล็อคนอนยาว 2
            blocks.Add(new Block(new RectF(540, 480, 300, 140)));// บล็อคนอนยาว 3
            blocks.Add(new Block(new RectF(550, 70, 130, 50)));// บล็อคนอนเล็ก 1 นับจากบนลงล่าง
            blocks.Add(new Block(new RectF(800, 550, 130, 50)));// บล็อคนอนเล็ก 2
            blocks.Add(new Block(new RectF(1000, 130, 165, 55))); // ลิฟท์ขึ้นลงตลอดเวลา

            float Blockduration = 1f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            blocks[23].AddAction(Actions.Forever(
                new MoveByAction(Blockduration, new Vector2(0, 200), blocks[23]),
                new MoveByAction(Blockduration, new Vector2(0, -200), blocks[23])
            ));

            balls.Add(new Ball(new RectF(850, 500, 50, 50))); // บอล 1 ขยับขึ้นลง
            balls.Add(new Ball(new RectF(600, 110, 30, 30))); // บอล 2
            balls.Add(new Ball(new RectF(850, 110, 30, 30))); // บอล 3

            float Ballduration = 1f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            balls[0].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, -200), balls[0]),
                new MoveByAction(Ballduration, new Vector2(0, 200), balls[0])
            ));

            spikeups.Add(new SpikeUp(new RectF(400, 580, 45, 45))); // น้ำแข็ง 1 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(430, 580, 45, 45))); // น้ำแข็ง 2 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(690, 650, 45, 45))); // น้ำแข็ง 3 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(720, 650, 45, 45))); // น้ำแข็ง 4 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(750, 650, 45, 45))); // น้ำแข็ง 5 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(780, 650, 45, 45))); // น้ำแข็ง 6 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(810, 650, 45, 45))); // น้ำแข็ง 7 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(840, 650, 45, 45))); // น้ำแข็ง 8 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(870, 650, 45, 45))); // น้ำแข็ง 9 ล่างสุด
            spikeups.Add(new SpikeUp(new RectF(1230, 580, 45, 45))); // น้ำแข็ง 10ล่างสุด

            spikerights.Add(new SpikeRight(new RectF(0, 320, 22, 28))); // น้ำแข็งข้าง 1 นับจากบนลงล่าง
            spikerights.Add(new SpikeRight(new RectF(0, 450, 22, 28))); // น้ำแข็งข้าง 2

            stars.Add(new Star(new RectF(100, 80, 20, 20))); // ปุ่ม 2 
            stars.Add(new Star(new RectF(300, 80, 20, 20))); // ปุ่ม 3
            stars.Add(new Star(new RectF(480, 110, 20, 20))); // ปุ่ม 4
            stars.Add(new Star(new RectF(700, 110, 20, 20))); // ปุ่ม 5
            stars.Add(new Star(new RectF(550, 600, 20, 20))); // ปุ่ม 6

            doors.Add(new Door(new RectF(1275, 520, 100, 100)));

            // เพิ่ม santa
            santa = new Santy(new Vector2(10, 550));
            santa.Add(cameraMan);

            // เพิ่มวัตถุทั้งหมดในฉาก
            AddAllObjects();
        }

        private void AddAllObjects()
        {
            foreach (var ball in balls)
                Add(ball);

            foreach (var star in stars)
                Add(star);

            foreach (var spikeup in spikeups)
                Add(spikeup);

            foreach (var spikeright in spikerights)
                Add(spikeright);

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
            if (santa.Position.Y >= 1000)
            {
                ResetBlock(17, new RectF(1200, 445, 70, 350));
                
            }

        }

        private void CheckTriggers(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            if (Math.Abs(santa.Position.X - 100) < 10f && Math.Abs(santa.Position.Y - 80) < 10f)
            {
                float targetY = 620;
                Vector2 spikePosition = blocks[17].Position;

                if (spikePosition.Y < targetY)
                {
                    spikePosition.Y += 1000f * deltaTime;
                    if (spikePosition.Y > targetY)
                        spikePosition.Y = targetY;
                }

                blocks[17].Position = spikePosition;

            }
            if (Math.Abs(santa.Position.X - 300) < 10f && Math.Abs(santa.Position.Y - 80) < 10f)
            {
                santa.Position = new Vector2(420, 500);

            }
            if (Math.Abs(santa.Position.X - 480) < 10f && Math.Abs(santa.Position.Y - 110) < 10f)
            {
                santa.Position = new Vector2(420, 500);

            }
            if (Math.Abs(santa.Position.X - 700) < 10f && Math.Abs(santa.Position.Y - 110) < 10f)
            {
                santa.Position = new Vector2(420, 500);

            }
            if (Math.Abs(santa.Position.X - 1325) < 10f && Math.Abs(santa.Position.Y - 600) < 10f)
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

        private void ResetBlock(int index, RectF rect)
        {
            Remove(blocks[index]);
            blocks[index] = new Block(rect);
            Add(blocks[index]);
        }

    }
}
