using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class Placeholder : Actor
    {
        public Placeholder()
        {
        }
        public Placeholder(Actor child)
        {
            Add(child);
        }
        public bool Enable { get; set; } = true;
        public void Toggle()
        {
            Enable = !Enable;
        }
        public override void Act(float deltaTime)
        {
            if (Enable)
                base.Act(deltaTime);
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            if (Enable)
                base.Draw(target, state);
        }

    }
}
