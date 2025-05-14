using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace ThanaNita.MonoGameTnt
{
    public class DirectionKey
    {
        private static float sin45 = MathF.Sin(MathF.PI / 4.0f);
        private static int yUp = 0;
        static DirectionKey()
        {
            yUp = GlobalConfig.GeometricalYAxis ? 1 : -1;
        }
        public static Vector2 Direction
        {
            get
            {
                var state = GlobalKeyboardInfo.Value;
                Vector2 direction = new Vector2();
                if (state.IsKeyDown(Keys.Right))
                    direction += new Vector2(1, 0);
                if (state.IsKeyDown(Keys.Left))
                    direction += new Vector2(-1, 0);
                if (state.IsKeyDown(Keys.Up))
                    direction += new Vector2(0, 1 * yUp);
                if (state.IsKeyDown(Keys.Down))
                    direction += new Vector2(0, -1* yUp);
                return direction;
            }
        }
        public static Vector2 Normalized
        {
            get
            {
                var direction = Direction;
                if (direction.X != 0 && direction.Y != 0)
                    direction *= sin45;
                return direction;
            }
        }

        public static Vector2 Direction4(Keys key)
        {
            Vector2 direction = new Vector2();
            if (key == Keys.Right)
                direction += new Vector2(1, 0);
            if (key == Keys.Left)
                direction += new Vector2(-1, 0);
            if (key == Keys.Up)
                direction += new Vector2(0, -1 * yUp);
            if (key == Keys.Down)
                direction += new Vector2(0, 1 * yUp);
            return direction;
        }
        // ถ้ามีปุ่มเฉียง จะตัดทิ้งกลายเป็น (0,0)
        public static Vector2 Normalized4
        {
            get
            {
                var direction = Direction;
                if (direction.X != 0 && direction.Y != 0)
                    direction = new Vector2();
                return direction;
            }
        }
    }
}
