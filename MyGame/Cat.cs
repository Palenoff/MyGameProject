using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Cat : BaseObject
    {
        public Cat(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Image newImage = Image.FromFile("cat.jpg");
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width + 30, Size.Height + 30);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X - 30; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
