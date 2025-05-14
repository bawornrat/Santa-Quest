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
    public class Level1 : Actor
    {
        CameraMan cameraMan;
        private Santy santa;
        private List<Block> blocks = new List<Block>();
        private List<SpikeUp> spikeups = new List<SpikeUp>();
        private List<SpikeDown> spikedowns = new List<SpikeDown>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        public Level1(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
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
            blocks.Add(new Block(new RectF(-500, -10, 1010, 1615))); //กันตกข้างซ้าย
            blocks.Add(new Block(new RectF(1360, -10, 490, 1615))); //กันตกข้างขวา
            blocks.Add(new Block(new RectF(0, 620, 250, 350)));// ล่างสุด 1
            blocks.Add(new Block(new RectF(400, 620, 800, 350)));// ล่างสุด 2
            blocks.Add(new Block(new RectF(850, 620, 140, 350)));// ล่างสุด 3
            blocks.Add(new Block(new RectF(960, 620, 140, 350)));// ล่างสุด 4
            blocks.Add(new Block(new RectF(1070, 620, 580, 350)));// ล่างสุด 5
            blocks.Add(new Block(new RectF(150, 550, 70, 70)));// บล็อคเล็ก 1
            blocks.Add(new Block(new RectF(220, 500, 70, 70)));// บล็อคเล็ก 2
            blocks.Add(new Block(new RectF(290, 450, 70, 70)));// บล็อคเล็ก 3
            blocks.Add(new Block(new RectF(360, 400, 70, 70)));// บล็อคเล็ก 4
            blocks.Add(new Block(new RectF(560, 450, 70, 70)));// บล็อคเล็ก 5
            blocks.Add(new Block(new RectF(430, 485, 350, 70)));// ยาวล่าง 1
            blocks.Add(new Block(new RectF(1070, 550, 210, 70)));// ยาวล่าง 2
            blocks.Add(new Block(new RectF(595, 415, 350, 70)));// ยาวกลาง 1
            blocks.Add(new Block(new RectF(1120, 480, 210, 70)));// ยาวกลาง 2
            blocks.Add(new Block(new RectF(595, 250, 350, 70)));// ยาวบน 1
            blocks.Add(new Block(new RectF(1170, 410, 210, 70)));// ยาวบน 2
            blocks.Add(new Block(new RectF(595, 415, 70, 210)));// บล็อคตั้ง 1

            spikeups.Add(new SpikeUp(new RectF(535, 600, 28, 22))); // น้ำแข็งล่างสุด 1
            spikeups.Add(new SpikeUp(new RectF(560, 600, 28, 22))); // น้ำแข็งล่างสุด 2
            spikeups.Add(new SpikeUp(new RectF(585, 600, 28, 22))); // น้ำแข็งล่างสุด 3
            spikeups.Add(new SpikeUp(new RectF(610, 600, 28, 22))); // น้ำแข็งล่างสุด 4
            spikeups.Add(new SpikeUp(new RectF(635, 600, 28, 22))); // น้ำแข็งล่างสุด 5
            spikeups.Add(new SpikeUp(new RectF(660, 600, 28, 22))); // น้ำแข็งล่างสุด 6
            spikeups.Add(new SpikeUp(new RectF(685, 600, 28, 22))); // น้ำแข็งล่างสุด 7
            spikeups.Add(new SpikeUp(new RectF(1100, 600, 28, 22))); // น้ำแข็งล่างสุด 8
            spikeups.Add(new SpikeUp(new RectF(1125, 600, 28, 22))); // น้ำแข็งล่างสุด 9
            spikeups.Add(new SpikeUp(new RectF(1150, 600, 28, 22))); // น้ำแข็งล่างสุด 10
            spikeups.Add(new SpikeUp(new RectF(1175, 600, 28, 22))); // น้ำแข็งล่างสุด 11
            spikeups.Add(new SpikeUp(new RectF(1200, 600, 28, 22))); // น้ำแข็งล่างสุด 12
            spikeups.Add(new SpikeUp(new RectF(1225, 600, 28, 22))); // น้ำแข็งล่างสุด 13

            spikedowns.Add(new SpikeDown(new RectF(600, 285, 45, 45))); // น้ำแข็งบนสุด 1
            spikedowns.Add(new SpikeDown(new RectF(640, 285, 45, 45))); // น้ำแข็งบนสุด 2
            spikedowns.Add(new SpikeDown(new RectF(680, 285, 45, 45))); // น้ำแข็งบนสุด 3
            spikedowns.Add(new SpikeDown(new RectF(720, 285, 45, 45))); // น้ำแข็งบนสุด 4


            doors.Add(new Door(new RectF(1275, 520, 100, 100)));

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

            foreach (var spikedown in spikedowns)
                Add(spikedown);

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
            if (Math.Abs(santa.Position.X - 1325) < 10f && Math.Abs(santa.Position.Y - 600) < 10f)//50,80
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
