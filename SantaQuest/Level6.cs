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
    public class Level6 : Actor
    {
        CameraMan cameraMan;
        private Santy2 santa;
        private List<Block> blocks = new List<Block>();
        private List<SpikeUp> spikeups = new List<SpikeUp>();
        private List<SpikeDown> spikedowns = new List<SpikeDown>();
        private List<SpikeRight> spikerights = new List<SpikeRight>();
        private List<Ball> balls = new List<Ball>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        public Level6(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
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
            blocks.Add(new Block(new RectF(-245, -10, 490, 2500))); //กันตกข้างซ้าย
            blocks.Add(new Block(new RectF(1360, -10, 490, 1615)));
            blocks.Add(new Block(new RectF(0, 620, 270, 70)));// บล็อคนอนล่างสุด 1
            blocks.Add(new Block(new RectF(0, 220, 2300, 70)));// บล็อคนอน 2
            blocks.Add(new Block(new RectF(250, 595, 70, 1300)));// บล็อคตั้งล่างสุด 1
            blocks.Add(new Block(new RectF(370, 560, 70, 1300)));// บล็อคตั้งล่างสุด 2
            blocks.Add(new Block(new RectF(490, 595, 70, 1300)));// บล็อคตั้งล่างสุด 3
            blocks.Add(new Block(new RectF(610, 490, 70, 1300)));// บล็อคตั้งล่างสุด 4
            blocks.Add(new Block(new RectF(730, 560, 70, 1300)));// บล็อคตั้งล่างสุด 5
            blocks.Add(new Block(new RectF(850, 420, 70, 1300)));// บล็อคตั้งล่างสุด 6
            blocks.Add(new Block(new RectF(970, 385, 70, 1300)));// บล็อคตั้งล่างสุด 7
            blocks.Add(new Block(new RectF(250, 115, 70, 210)));// บล็อคตั้งบนสุด
            blocks.Add(new Block(new RectF(250, 255, 70, 350)));// บล็อคตั้งกลาง 1
            blocks.Add(new Block(new RectF(370, 255, 70, 280)));// บล็อคตั้งกลาง 2
            blocks.Add(new Block(new RectF(490, 255, 70, 350)));// บล็อคตั้งกลาง 3
            blocks.Add(new Block(new RectF(610, 255, 70, 140)));// บล็อคตั้งกลาง 4

            spikeups.Add(new SpikeUp(new RectF(250, 560, 35, 35))); // น้ำแข็งล่างสุด 1
            spikeups.Add(new SpikeUp(new RectF(370, 525, 35, 35))); // น้ำแข็งล่างสุด 2
            spikeups.Add(new SpikeUp(new RectF(490, 560, 35, 35))); // น้ำแข็งล่างสุด 3
            spikeups.Add(new SpikeUp(new RectF(610, 455, 35, 35))); // น้ำแข็งล่างสุด 4
            spikeups.Add(new SpikeUp(new RectF(730, 525, 35, 35))); // น้ำแข็งล่างสุด 5
            spikeups.Add(new SpikeUp(new RectF(850, 385, 35, 35))); // น้ำแข็งล่างสุด 6
            spikeups.Add(new SpikeUp(new RectF(970, 350, 35, 35))); // น้ำแข็งล่างสุด 7
            spikeups.Add(new SpikeUp(new RectF(1005, 665, 35, 35))); // น้ำแข็งล่างสุด 8
            spikeups.Add(new SpikeUp(new RectF(1040, 665, 35, 35))); // น้ำแข็งล่างสุด 9
            spikeups.Add(new SpikeUp(new RectF(1325, 665, 35, 35))); // น้ำแข็งล่างสุด 10
            spikeups.Add(new SpikeUp(new RectF(70, 185, 35, 35)));
            spikeups.Add(new SpikeUp(new RectF(250, 80, 35, 35)));

            spikedowns.Add(new SpikeDown(new RectF(250, 430, 35, 35))); // น้ำแข็งกลาง 1
            spikedowns.Add(new SpikeDown(new RectF(370, 395, 35, 35))); // น้ำแข็งกลาง 2
            spikedowns.Add(new SpikeDown(new RectF(490, 430, 35, 35))); // น้ำแข็งกลาง 3
            spikedowns.Add(new SpikeDown(new RectF(610, 325, 35, 35))); // น้ำแข็งกลาง 4

            spikerights.Add(new SpikeRight(new RectF(0, 20, 35, 35)));

            balls.Add(new Ball(new RectF(350, 120, 100, 100))); // บอล 1 นับจากข้างบน
            balls.Add(new Ball(new RectF(550, 210, 100, 100))); // บอล 2
            balls.Add(new Ball(new RectF(650, 20, 100, 100))); // บอล 3
            balls.Add(new Ball(new RectF(850, 210, 100, 100))); // บอล 4            
            balls.Add(new Ball(new RectF(1000, 20, 100, 100))); // บอล 5
            balls.Add(new Ball(new RectF(1200, 80, 100, 100))); // บอล 6
            balls.Add(new Ball(new RectF(1280,300,50 ,50))); // บอล 7
            balls.Add(new Ball(new RectF(1100, 450, 50, 50))); // บอล 8

            float Ballduration = 1f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            balls[7].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(0, -100), balls[7]),
                new MoveByAction(Ballduration, new Vector2(0, 100), balls[7])
            ));
            balls[6].AddAction(Actions.Forever(
                new MoveByAction(Ballduration, new Vector2(-100, 0), balls[6]),
                new MoveByAction(Ballduration, new Vector2(100, 0), balls[6])
            ));
            doors.Add(new Door(new RectF(0, 120, 100, 100)));

            // เพิ่ม santa
            santa = new Santy2(new Vector2(10, 550));
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
        }

        private void CheckTriggers(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            if (Math.Abs(santa.Position.X - 50) < 10f && Math.Abs(santa.Position.Y - 200) < 10f)
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
