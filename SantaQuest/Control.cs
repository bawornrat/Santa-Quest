using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using ThanaNita.MonoGameTnt;

namespace SantaQuest
{
    public class Control : Game2D
    {
        Actor home, choose, test, success, level1, level2, level3, level4, level5, level6, level7,ranvel;
        CameraMan cameraMan;
        protected override void LoadContent()
        {
            base.LoadContent();
            cameraMan = new CameraMan(Camera, ScreenSize);

            // สร้าง OrthographicCamera
            CollisionDetectionUnit.AddDetector(1, 2);

            // ส่งกล้องไปยัง Game01
            home = new HomeScreen(ScreenSize, ExitNotifier, GraphicsDevice);
            All.Add(home);

            //level6 = new Level6(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
            //All.Add(level6); ;
        }


        private void ExitNotifier(Actor actor, int code)
        {
            if (actor == null)
                return;

            if (actor == home)
            {
                home.Detach();
                home = null;

                if (code == 1) // กดจาก StartPanel
                {
                    choose = new LevelScreen(ScreenSize, ExitNotifier, GraphicsDevice);
                    All.Add(choose);
                }
                else if (code == 2) // กดจาก ExitPanel
                {
                    Environment.Exit(0);
                }
            }

            if (actor == choose)
            {
                choose.Detach();
                choose = null;
                if (code == 1) // กดจาก StartPanel
                {
                    level1 = new Level1(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level1);
                }
                else if (code == 2) // กดจาก ExitPanel
                {
                    level2 = new Level2(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level2); 
                }
                else if (code == 3) // กดจาก ExitPanel
                {
                    level3 = new Level3(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level3); 
                }
                else if (code == 4) // กดจาก ExitPanel
                {
                    level4 = new Level4(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level4); 
                }
                else if (code == 5) // กดจาก ExitPanel
                {
                    level5 = new Level5(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level5); 
                }
                else if (code == 6) // กดจาก ExitPanel
                {
                    level6 = new Level6(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level6); 
                }
                else if (code == 7) // กดจาก ExitPanel
                {
                    level7 = new Level7(ScreenSize, ExitNotifier, cameraMan, GraphicsDevice);
                    All.Add(level7); 
                }
                else if (code == 8) // กดจาก ExitPanel
                {
                    home = new HomeScreen(ScreenSize, ExitNotifier, GraphicsDevice);
                    All.Add(home);
                }

            }

            if (actor == level1)
            {
                level1.Detach();
                level1 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }
            if (actor == level2)
            {
                level2.Detach();
                level2 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }
            if (actor == level3)
            {
                level3.Detach();
                level3 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }
            if (actor == level4)
            {
                level4.Detach();
                level4 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }
            if (actor == level5)
            {
                level5.Detach();
                level5 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }
            if (actor == level6)
            {
                level6.Detach();
                level6 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }
            /*if (actor == level7)
            {
                level7.Detach();
                level7 = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }*/
            if (actor == ranvel)
            {
                ranvel.Detach();
                ranvel = null;
                success = new Success(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(success);
            }

            if (actor == success)
            {
                success.Detach();
                success = null;
                choose = new LevelScreen(ScreenSize, ExitNotifier, GraphicsDevice);
                All.Add(choose);
            }
        }
    }

}
