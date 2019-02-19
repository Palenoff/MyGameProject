using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс, представляющий объект пули
    /// </summary>
    class Bullet : BaseObject
    {
        /// <param name="pos">Верхняя левая точка пули.</param>
        /// <param name="dir">Направление дивижения пули.</param>
        /// <param name="size">Размер пули.</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size, "Пуля")
        {

        }
        /// <summary>
        /// Рисует пулю.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Images["Bullet"], Pos.X, Pos.Y, Size.Width + 30, Size.Height + 30);
        }
        /// <summary>
        /// Обновляет положение астероида.
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - 5 * Convert.ToInt32(Math.Sin(Convert.ToDouble(Dir.X))); //поменял на +, как и было рекомедовано в комментарии   
            Pos.Y = Pos.Y + 5 * Convert.ToInt32(Math.Cos(Convert.ToDouble(Dir.Y)));
            if (Pos.X > Game.Width) Pos.X = Size.Width;
            if (Pos.Y > Game.Height) Pos.Y = Size.Height;
            if (Pos.X < 0) Pos.X = 0;
            if (Pos.Y < 0) Pos.Y = 0;
        }
        /// <summary>
        /// Вызывает регенерацию пули
        /// </summary>
        public override void Regeneration()
        {
            //Game.Buffer.Graphics.DrawImage(Game.Images["Blast"], Pos.X, Pos.Y, Size.Width + 30, Size.Height + 30);
            Random rnd = new Random();
            Pos.X = rnd.Next(0, Game.Width);
            Pos.Y = rnd.Next(0, Game.Height);
            //Game.Buffer.Graphics.
        }
    }
}
