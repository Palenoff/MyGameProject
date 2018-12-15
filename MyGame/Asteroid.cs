using System;
using System.Drawing;

namespace MyGame
{
    public class Asteroid: BaseObject
    {
        private string _name;
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size, string name) : base(pos, dir, size)
        {
            Power = 1;
            _name = name;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Images_asteroids[_name], Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X < -Size.Width) Pos.X = Game.Width + Size.Width;
        }

        public override void Regeneration()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return _name;
        }
    }
}