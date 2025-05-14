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
    public class Level3 : Actor
    {
        CameraMan cameraMan;
        private Santy santa;
        private List<Block> blocks = new List<Block>();
        private List<SpikeUp> spikeups = new List<SpikeUp>();
        private List<SpikeDown> spikedowns = new List<SpikeDown>();
        private List<SpikeLeft> spikelefts = new List<SpikeLeft>();
        private List<Ball> balls = new List<Ball>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        public Level3(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
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
            blocks.Add(new Block(new RectF(0, 620, 500, 350)));// ล่างสุด 1
            blocks.Add(new Block(new RectF(420, 620, 700, 350)));// ล่างสุด 2
            blocks.Add(new Block(new RectF(920, 620, 880, 350)));// ล่างสุด 3
            blocks.Add(new Block(new RectF(0, 0, 2715, 140)));// บนสุด 1
            blocks.Add(new Block(new RectF(65, 550, 70, 140)));// ตั้งล่าง 1
            blocks.Add(new Block(new RectF(140, 480, 70, 280)));// ตั้งล่าง 2
            blocks.Add(new Block(new RectF(215, 410, 70, 420)));// ตั้งล่าง 3
            blocks.Add(new Block(new RectF(420, 390, 70, 470)));// ตั้งล่าง 4
            blocks.Add(new Block(new RectF(1255, 565, 70, 110)));// ตั้งล่าง 5
            blocks.Add(new Block(new RectF(1290, 495, 70, 250)));// ตั้งล่าง 6
            blocks.Add(new Block(new RectF(1325, 465, 70, 320)));// ตั้งล่าง 7
            blocks.Add(new Block(new RectF(65, 70, 70, 675)));// ตั้งบน 1
            blocks.Add(new Block(new RectF(140, 70, 70, 420)));// ตั้งบน 2
            blocks.Add(new Block(new RectF(215, 70, 70, 280)));// ตั้งบน 3
            blocks.Add(new Block(new RectF(420, 70, 70, 370)));// ตั้งบน 4
            blocks.Add(new Block(new RectF(600, 70, 70, 770)));// ตั้งบน 5
            blocks.Add(new Block(new RectF(635, 420, 1200, 70)));// บล็อคนอน

            balls.Add(new Ball(new RectF(340, 500, 50, 50))); // บอลใหญ่ 1 เลื่อนบนล่าง
            balls.Add(new Ball(new RectF(800, 150, 50, 50))); // บอลใหญ่ 2 เลื่อนบนล่าง
            balls.Add(new Ball(new RectF(950, 200, 50, 50))); // บอลใหญ่ 3 เลื่อนบนล่าง
            balls.Add(new Ball(new RectF(1100, 250, 50, 50))); // บอลใหญ่ 4 เลื่อนบนล่าง

            float Ballduration = 1f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            balls[0].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, -250), balls[0]),
                new MoveByAction(Ballduration, new Vector2(0, 250), balls[0])
            ));
            float Ballduration1 = 0.5f;
            balls[1].AddAction(Actions.Forever(
                new MoveByAction(Ballduration1, new Vector2(0, 250), balls[1]),
                new MoveByAction(Ballduration1, new Vector2(0, -250), balls[1])
            ));
            balls[2].AddAction(Actions.Forever(
                new MoveByAction(Ballduration1, new Vector2(0, 200), balls[2]),
                new MoveByAction(Ballduration1, new Vector2(0, -200), balls[2])
            ));
            balls[3].AddAction(Actions.Forever(
                new MoveByAction(Ballduration1, new Vector2(0, 150), balls[3]),
                new MoveByAction(Ballduration1, new Vector2(0, -150), balls[3])
            ));

            spikeups.Add(new SpikeUp(new RectF(105, 590, 35, 35))); // น้ำแข็งล่าง 1
            spikeups.Add(new SpikeUp(new RectF(180, 590, 35, 35))); // น้ำแข็งล่าง 2
            spikeups.Add(new SpikeUp(new RectF(450, 590, 35, 35))); // น้ำแข็งล่าง 3
            spikeups.Add(new SpikeUp(new RectF(480, 590, 35, 35))); // น้ำแข็งล่าง 4
            spikeups.Add(new SpikeUp(new RectF(510, 590, 35, 35))); // น้ำแข็งล่าง 5

            spikedowns.Add(new SpikeDown(new RectF(65, 400, 35, 35))); // น้ำแข็งบน 1
            spikedowns.Add(new SpikeDown(new RectF(140, 280, 35, 35))); // น้ำแข็งบน 2
            spikedowns.Add(new SpikeDown(new RectF(215, 210, 35, 35))); // น้ำแข็งบน 3
            spikedowns.Add(new SpikeDown(new RectF(420, 255, 35, 35))); // น้ำแข็งบน 4
            spikedowns.Add(new SpikeDown(new RectF(715, 455, 35, 35))); // น้ำแข็งบน 5
            spikedowns.Add(new SpikeDown(new RectF(750, 455, 45, 45))); // น้ำแข็งบน 6
            spikedowns.Add(new SpikeDown(new RectF(900, 455, 45, 45))); // น้ำแข็งบน 7
            spikedowns.Add(new SpikeDown(new RectF(945, 455, 35, 35))); // น้ำแข็งบน 7

            spikelefts.Add(new SpikeLeft(new RectF(555, 110, 45, 45))); // น้ำแข็งข้าง 1
            spikelefts.Add(new SpikeLeft(new RectF(555, 155, 45, 45))); // น้ำแข็งข้าง 2
            spikelefts.Add(new SpikeLeft(new RectF(555, 200, 45, 45))); // น้ำแข็งข้าง 3
            spikelefts.Add(new SpikeLeft(new RectF(555, 245, 45, 45))); // น้ำแข็งข้าง 4
            spikelefts.Add(new SpikeLeft(new RectF(555, 290, 45, 45))); // น้ำแข็งข้าง 5


            doors.Add(new Door(new RectF(625, 325, 100, 100)));

            // เพิ่ม santa
            santa = new Santy(new Vector2(10, 250));
            santa.Add(cameraMan);

            // เพิ่มวัตถุทั้งหมดในฉาก
            AddAllObjects();
        }

        private void AddAllObjects()
        {
            foreach (var ball in balls)
                Add(ball);

            foreach (var spikeup in spikeups)
                Add(spikeup);

            foreach (var spikedown in spikedowns)
                Add(spikedown);

            foreach (var spikeleft in spikelefts)
                Add(spikeleft);

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
            if (Math.Abs(santa.Position.X - 675) < 10f && Math.Abs(santa.Position.Y - 400) < 10f)
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
