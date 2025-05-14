using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using ThanaNita.MonoGameTnt;

namespace SantaQuest
{
    public class CameraMan : Actor
    {
        OrthographicCamera camera;
        Vector2 screenSize;
        public CameraMan(OrthographicCamera camera, Vector2 screenSize)
        {
            this.camera = camera;
            this.screenSize = screenSize;
        }
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var myGlobalPosition = Parent.GlobalTransform.Transform(Position);
            camera.Position = myGlobalPosition - screenSize / 2;
            camera.Zoom = 2f;
        }
        public void DefaultCamera()
        {
            camera.Position = new Vector2(0, 0); // Default center
            camera.Zoom = 1f; // Normal zoom level
        }

    }
}
