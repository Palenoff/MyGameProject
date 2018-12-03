using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Mars : BaseObject
    {
        public Mars(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Image newImage = Image.FromFile("Mars.jpg");
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X > Game.Width) Pos.X = 0 + Size.Width;
        }
    }
}
