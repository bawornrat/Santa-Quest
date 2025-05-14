using System;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace SantaQuest
{
    public class SpikeDown : SpriteActor
    {
        public SpikeDown(RectF rect)
        {
            // โหลด Texture
            var texture = TextureCache.Get("Icedown.png");

            // คำนวณสเกลจากขนาดของ Texture
            var textureSize = new Vector2(texture.Width, texture.Height);
            var scale = rect.Size / textureSize;

            // ตั้ง TextureRegion
            SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, textureSize)));
            Position = rect.Position;
            Scale = scale; // ปรับขนาดรูปให้ตรงกับ block

            // สร้าง CollisionObj
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            //collisionObj.DebugDraw = true;
            Add(collisionObj);
        }
    }
}
