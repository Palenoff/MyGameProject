using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Bullet : BaseObject
    {
        Image _blast;
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _image = Image.FromFile("Bullet.jpg");
            _blast = Image.FromFile("Blast.gif");
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y, Size.Width + 30, Size.Height + 30);
        }

        public override void Update()
        {
            Pos.X = Pos.X + 5 * Convert.ToInt32(Math.Sin(Convert.ToDouble(Dir.X))); //поменял на +, как и было рекомедовано в комментарии   
            Pos.Y = Pos.Y + 5 * Convert.ToInt32(Math.Cos(Convert.ToDouble(Dir.Y)));
            if (Pos.X > Game.Width) Pos.X = Size.Width;
            if (Pos.Y > Game.Height) Pos.Y = Size.Height;
        }

        public void Regeneration()
        {
            //Game.Buffer.Graphics.DrawImage(_blast, Pos.X, Pos.Y, Size.Width + 30, Size.Height + 30);
            Random rnd = new Random();
            Pos.X = rnd.Next(0, Game.Width);
            Pos.Y = rnd.Next(0, Game.Height);
            //Game.Buffer.Graphics.
        }
    }
}
