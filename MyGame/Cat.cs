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
            _image = Image.FromFile("Cat.jpg");
            Size.Width += 30;
            Size.Height += 30;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X - 30; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
