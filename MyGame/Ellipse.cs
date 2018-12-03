using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Ellipse : BaseObject
    {
        public Ellipse(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Blue, new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
