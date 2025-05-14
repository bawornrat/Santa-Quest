
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class MoveByAction : RelativeTemporalAction
    {
        private Actor actor;
        private Vector2 displacement;

        public MoveByAction(float duration, Vector2 displacement, Actor actor)
            : base(duration, null)
        {
            this.actor = actor;
            this.displacement = displacement;
        }

        protected override void UpdateRelative(float percentDelta)
        {
            actor.Position += displacement * percentDelta;
        }
    }
}
