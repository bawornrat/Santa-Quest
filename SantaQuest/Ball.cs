using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace SantaQuest
{
    public class Ball : SpriteActor
    {
        public Ball(RectF rect)
        {
            // โหลด Texture
            var texture = TextureCache.Get("Ball.png");

            // คำนวณสเกลจากขนาดของ Texture
            var textureSize = new Vector2(texture.Width, texture.Height);   
            var scale = rect.Size / textureSize;

            // ตั้ง TextureRegion
            SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, textureSize)));
            Origin = RawSize / 2;
            Position = rect.Position;
            Scale = scale; // ปรับขนาดรูปให้ตรงกับ block

            // สร้าง CollisionObj
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            //collisionObj.DebugDraw = true;
            Add(collisionObj);
        }
    }
}
