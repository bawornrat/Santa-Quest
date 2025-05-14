using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace SantaQuest
{
    public class Block : SpriteActor
    {
        public Block(RectF rect)
        {

            var texture = TextureCache.Get("snowblock.png");
            SetTextureRegion(new TextureRegion(texture, rect));
            Position = rect.Position;
            Scale = new Vector2(0.5f, 0.5f);

            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            //collisionObj.DebugDraw = true;
            Add(collisionObj);
        }
    }
}
