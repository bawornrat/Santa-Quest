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
    public class Level7 : Actor
    {
        CameraMan cameraMan;
        private Santy santa;
        private List<Block> blocks = new List<Block>();
        private List<SpikeUp> spikeups = new List<SpikeUp>();
        private List<SpikeDown> spikedowns = new List<SpikeDown>();
        private List<SpikeLeft> spikelefts = new List<SpikeLeft>();
        private List<SpikeRight> spikerights = new List<SpikeRight>();
        public List<Star> stars = new List<Star>();
        private List<Ball> balls = new List<Ball>();
        private List<Door> doors = new List<Door>();
        private Vector2 screenSize;
        ExitNotifier exitNotifier;
        private Texture2D backgroundTexture;
        private Placeholder placeholder = new Placeholder();

        public Level7(Vector2 screenSize, ExitNotifier exitNotifier, CameraMan cameraMan, GraphicsDevice graphicsDevice)
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
            blocks.Add(new Block(new RectF(0, 410, 500, 140)));
            blocks.Add(new Block(new RectF(360, 445, 200, 140)));
            blocks.Add(new Block(new RectF(560, 445, 485, 140)));
            blocks.Add(new Block(new RectF(900, 240, 315, 760)));
            blocks.Add(new Block(new RectF(1170, 345, 415, 350)));
            blocks.Add(new Block(new RectF(300, 280, 1000, 50)));
            blocks.Add(new Block(new RectF(125, 320, 200, 50)));
            blocks.Add(new Block(new RectF(0, 365, 110, 50)));
            blocks.Add(new Block(new RectF(0, -10, 1400, 280)));
            blocks.Add(new Block(new RectF(0, 620, 400, 2500)));
            blocks.Add(new Block(new RectF(350, 620, 2040, 2500)));
            blocks.Add(new Block(new RectF(200, 655, 300, 1000)));
            blocks.Add(new Block(new RectF(-320, -10, 650, 2500)));
            blocks.Add(new Block(new RectF(1370, -5, 650, 2500)));
            blocks.Add(new Block(new RectF(450, 445, 220, 140)));//slide
            blocks.Add(new Block(new RectF(810, 445, 165, 55)));//Up-down

            spikeups.Add(new SpikeUp(new RectF(470, 600, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(495, 600, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(520, 600, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(640, 600, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(1060, 600, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(200, 635, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(225, 635, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(250, 635, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(275, 635, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(300, 635, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(325, 635, 28, 22)));
            spikeups.Add(new SpikeUp(new RectF(165, 300, 28, 22)));

            spikedowns.Add(new SpikeDown(new RectF(900, 0, 56, 44)));
            spikedowns.Add(new SpikeDown(new RectF(950, 0, 56, 44)));
            spikedowns.Add(new SpikeDown(new RectF(1000, 0, 56, 44)));

            spikerights.Add(new SpikeRight(new RectF(1055, 350, 22, 28)));
            spikerights.Add(new SpikeRight(new RectF(1055, 380, 22, 28)));

            spikelefts.Add(new SpikeLeft(new RectF(1150, 460, 22, 28)));
            spikelefts.Add(new SpikeLeft(new RectF(1150, 490, 22, 28)));

            balls.Add(new Ball(new RectF(615, 340, 50, 50)));
            balls.Add(new Ball(new RectF(50, 190, 50, 50)));

            doors.Add(new Door(new RectF(1275, 520, 100, 100)));

            // เพิ่ม santa
            santa = new Santy(new Vector2(10, 550));
            //santa.Add(cameraMan);

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

    }
}
