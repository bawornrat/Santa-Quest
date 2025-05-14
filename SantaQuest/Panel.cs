using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class Panel : Actor
    {
        SpriteActor backgroundSprite; // สำหรับแสดงภาพพื้นหลัง
        HollowRectActor frame;        // สำหรับกรอบของ panel
        RectF rawRect;

        public Panel(Vector2 size, Color backgroundColor, Color outlineColor, float outlineWidth = 2)
        {
            rawRect = new RectF(Vector2.Zero, size);
            backgroundSprite = new SpriteActor(); // สร้าง SpriteActor ไม่มีภาพพื้นหลัง
            backgroundSprite.Color = backgroundColor; // กำหนดสีพื้นหลัง
            frame = new HollowRectActor(outlineColor, outlineWidth, rawRect.CreateExpand(-outlineWidth / 2));
        }

        public Panel(Vector2 size, Texture2D backgroundTexture, Color outlineColor, float outlineWidth = 2)
        {
            rawRect = new RectF(Vector2.Zero, size);

            // สร้าง SpriteActor สำหรับพื้นหลัง
            backgroundSprite = new SpriteActor(backgroundTexture);

            // คำนวณสัดส่วน (Scale) ของ SpriteActor เพื่อให้พอดีกับขนาด Panel
            var textureSize = new Vector2(backgroundTexture.Width, backgroundTexture.Height);
            var scale = size / textureSize; // Vector2 Division
            backgroundSprite.Scale = scale;

            // สร้างกรอบของ Panel
            frame = new HollowRectActor(outlineColor, outlineWidth, rawRect.CreateExpand(-outlineWidth / 2));
        }
        public Rectangle GetBoundingBox()
        {
            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)rawRect.Size.X,
                (int)rawRect.Size.Y
            );
        }


        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            var combine = CombineState(state);

            // วาดภาพพื้นหลัง
            backgroundSprite.Draw(target, combine);

            // วาดกรอบของ panel
            frame.Draw(target, combine);
        }

    }
}
