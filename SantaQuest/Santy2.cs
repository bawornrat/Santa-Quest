using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ThanaNita.MonoGameTnt;

namespace SantaQuest
{
    public class Santy2 : SpriteActor
    {
        AnimationStates states;
        Vector2 V;
        bool onFloor;
        bool IsDead;
        float DeathTimer;
        Vector2 StartPosition;
        SoundEffect soundEffect1, soundEffect2, soundEffect3;
        Song song;

        public Santy2(Vector2 position)
        {
            StartPosition = position;
            var size = new Vector2(37.5f, 52f);
            Position = position;
            Origin = size / 2;
            Scale = new Vector2(0.55f, 0.55f);

            var texture = TextureCache.Get("SantaClaus.png");
            var texture1 = TextureCache.Get("SantaClaus1.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var regions2d1 = RegionCutter.Cut(texture1, size);
            var selector = new RegionSelector(regions2d);
            var selector1 = new RegionSelector(regions2d1);
            var stay = new Animation(this, 1.0f, selector1.Select1by1(0, 15));
            var left = new Animation(this, 1.0f, selector.Select(start: 0, count: 15));
            var right = new Animation(this, 1.0f, selector1.Select(start: 0, count: 15));
            var dead = new Animation(this, 1.0f, selector1.Select1by1(21, 22));
            states = new AnimationStates([stay, left, right, dead]);
            AddAction(states);

            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            //collisionObj.DebugDraw = true;
            Add(collisionObj);

            song = Song.FromUri(name: "song1", new Uri("Song1.ogg", UriKind.Relative));
            MediaPlayer.Play(song);
            soundEffect1 = SoundEffect.FromFile("Avu.wav");
            soundEffect2 = SoundEffect.FromFile("Avu.wav");
            soundEffect3 = SoundEffect.FromFile("Dead.wav");
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
        private void OnCollide(CollisionObj objB, CollideData data)
        {
            if (IsDead) return;

            var direction = data.objA.RelativeDirection(data.OverlapRect);

            // ตรวจสอบการชนกับ Spike
            if (objB.Parent is SpikeUp || objB.Parent is SpikeDown || objB.Parent is SpikeLeft || objB.Parent is SpikeRight || objB.Parent is Ball)
            {
                // แสดงอนิเมชันตายและตั้งสถานะ
                states.Animate(3); // แสดงอนิเมชันตาย
                IsDead = true;
                MediaPlayer.Pause();
                soundEffect3.Play();
                DeathTimer = 1.5f; // ตั้งตัวจับเวลา 1.5 วินาที

                // กระเด้งตัวละครขึ้นเล็กน้อย
                V = new Vector2(0, -300);
                return;
            }


            if (direction.Y == 1)
                onFloor = true;

            if ((direction.Y > 0 && V.Y > 0) ||
                (direction.Y < 0 && V.Y < 0))
            {
                V.Y = 0;
                Position -= new Vector2(0, data.OverlapRect.Height * direction.Y);
            }
            if ((direction.X > 0 && V.X > 0) ||
                (direction.X < 0 && V.X < 0))
            {
                V.X = 0;
                Position -= new Vector2(data.OverlapRect.Width * direction.X, 0);
            }
        }

        public override void Act(float deltaTime)
        {
            if (Position.Y >= 1000)
            {
                ResetPosition(); // รีเซ็ตตำแหน่งเมื่อ Position.Y >= 1000
                soundEffect1.Play();
            }
            if (IsDead)
            {
                // ถ้าตัวละครตาย กระเด้งและหล่นลงด้านล่าง
                V.Y += 1500 * deltaTime; // แรงโน้มถ่วง
                Position += V * deltaTime;

                // ลดตัวจับเวลาการตาย
                DeathTimer -= deltaTime;
                if (DeathTimer <= 0)
                {
                    states.Animate(0); // รีเซ็ตกลับไปอนิเมชันปกติ
                }

                return; // ไม่ทำงานปกติในขณะที่ตัวละครตาย
            }

            ChangeVy(deltaTime);

            var direction = DirectionKey.Direction;
            V.X = direction.X * 200; // เปลี่ยนแค่ V.X

            if (direction.X > 0)
                states.Animate(2);
            else if (direction.X < 0)
                states.Animate(1);
            else
                states.Animate(0);

            base.Act(deltaTime);
            Position += V * deltaTime; // s += v*dt
            onFloor = false;
        }

        private void ChangeVy(float deltaTime)
        {
            // 1.Gravitation
            var a = new Vector2(0, 700);
            V.Y += a.Y * deltaTime;

            // 2.Jump
            var keyInfo = GlobalKeyboardInfo.Value;
            if (keyInfo.IsKeyPressed(Keys.Space))
                V.Y = -150;
            if (keyInfo.IsKeyPressed(Keys.Space))
                soundEffect2.Play();
                        // 3.Jet
            //if (keyInfo.IsKeyDown(Keys.Tab))
             //   V.Y = -500;
        }

        private void ResetPosition()
        {
            // รีเซ็ตตำแหน่งตัวละครกลับไปเริ่มต้น
            Position = StartPosition;
            V = Vector2.Zero;
            IsDead = false; // รีเซ็ตสถานะการตาย
            DeathTimer = 0; // รีเซ็ตตัวจับเวลา
            MediaPlayer.Play(song);
        }
    }
}