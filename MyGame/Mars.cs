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
            _image = Image.FromFile("Mars.jpg");
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X > Game.Width) Pos.X = Size.Width;
        }
    }
}
