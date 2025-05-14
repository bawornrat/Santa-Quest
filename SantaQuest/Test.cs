using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using SantaQuest;
using ThanaNita.MonoGameTnt;


namespace SantaQuest
{
    public class Test : Actor
    {
        CameraMan cameraMan;
        private Santy santa;
        private List<Block> blocks = new List<Block>();
        private List<Ball> balls = new List<Ball>();
        private List<SpikeUp> spikes = new List<SpikeUp>();
        private List<Star> stars = new List<Star>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        // ตัวแปรเพิ่มเติมสำหรับการเคลื่อนที่
        private float Block4MoveSpeed = 100f;
        private bool Block4MovingUp = true;

        public Test(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
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
            stars.Add(new Star(new RectF(310, 450, 50, 60)));
            stars.Add(new Star(new RectF(810, 575, 50, 60)));

            balls.Add(new Ball(new RectF(330, 450, 50, 60)));

            // เพิ่ม Block
            blocks.Add(new Block(new RectF(-500, 0, 500, 600)));
            blocks.Add(new Block(new RectF(000, 600, 200, 50)));
            blocks.Add(new Block(new RectF(250, 600, 200, 50)));
            blocks.Add(new Block(new RectF(450, 500, 100, 150))); //3
            blocks.Add(new Block(new RectF(650, 600, 100, 50))); //4
            blocks.Add(new Block(new RectF(550, 690, 100, 50))); //5
            blocks.Add(new Block(new RectF(750, 600, 500, 50)));

            float Blockduration = 1.0f;
            blocks[5].AddAction(Actions.Forever(
                new MoveByAction(Blockduration, new Vector2(0, -150), blocks[5]),
                new MoveByAction(Blockduration, new Vector2(0, 150), blocks[5])
            ));

            // เพิ่ม Spike
            spikes.Add(new SpikeUp(new RectF(200, 575, 50, 30)));
            spikes.Add(new SpikeUp(new RectF(1000, 575, 50, 30)));

            float Spikeduration = 0.5f; // ระยะเวลาในการเคลื่อนที่แต่ละครั้ง (ในวินาที)
            spikes[0].AddAction(Actions.Forever(
                new MoveByAction(Spikeduration, new Vector2(-100, 0), spikes[0]),
                new MoveByAction(Spikeduration, new Vector2(100, 0), spikes[0])
            ));

            // เพิ่มประตู
            doors.Add(new Door(new RectF(1000, 500, 100, 100)));

            // เพิ่ม Elf
            santa = new Santy(new Vector2(10, 250));
            santa.Add(cameraMan);

            // เพิ่มวัตถุทั้งหมดในฉาก
            AddAllObjects();
        }

        private void AddAllObjects()
        {
            foreach (var ball in balls)
                Add(ball);

            foreach (var spike in spikes)
                Add(spike);

            foreach (var button in stars)
                Add(button);

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

            // รีเซ็ต Block[3] เมื่อ Elf ตกลงหลุม
            if (santa.Position.Y >= 1000)
            {
                ResetBlock(3, new RectF(450, 500, 100, 150));
                ResetSpike(1, new RectF(1000, 575, 50, 30));
            }
        }

        private void CheckTriggers(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            if (Math.Abs(santa.Position.X - 340) < 10f && Math.Abs(santa.Position.Y - 500) < 10f)
            {
                santa.Position = new Vector2(450, 900);

            }
            if (santa.Position.X >= 810 && Math.Abs(santa.Position.Y - 575) < 10f) // จาก 1000 ไป 800
            {
                float targetX = 800; // จุดเป้าหมาย
                Vector2 spikePosition = spikes[1].Position;

                // ลดค่า X หาก Spike ยังไม่ได้ถึงเป้าหมาย
                if (spikePosition.X > targetX)
                {
                    spikePosition.X -= 1000f * deltaTime; // ลดค่าตาม deltaTime
                    if (spikePosition.X < targetX)
                        spikePosition.X = targetX; // ป้องกันไม่ให้เลยเป้าหมาย
                }

                spikes[1].Position = spikePosition;

            }

            /*if (santa.Position.X >= 750 && (Math.Abs(santa.Position.Y - 575) < 10f))  // จาก 670 ไป 800
            {
                float targetX = 800;
                Vector2 spikePosition = spikes[0].Position;

                if (spikePosition.X < targetX)
                {
                    spikePosition.X += 1000f * deltaTime;
                    if (spikePosition.X > targetX)
                        spikePosition.X = targetX;
                }

                spikes[0].Position = spikePosition;
            }*/

            if (Math.Abs(santa.Position.X - 1050) < 10f && Math.Abs(santa.Position.Y - 580) < 10f)
            {
                if (keyInfo.IsKeyPressed(Keys.F))
                    AddAction(new SequenceAction(
                                    Actions.FadeOut(0.5f, this),
                                    new RunAction(() => exitNotifier(this, 0))
                        ));
            }
        }

        private void ResetBlock(int index, RectF rect)
        {
            Remove(blocks[index]);
            blocks[index] = new Block(rect);
            Add(blocks[index]);
        }

        private void ResetSpike(int index, RectF rect)
        {
            Remove(spikes[index]);
            spikes[index] = new SpikeUp(rect);
            Add(spikes[index]);
        }

    }
}
