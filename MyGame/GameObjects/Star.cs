using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс, представляющий объект звезды.
    /// </summary>
    class Star : BaseObject
    {
        /// <summary>
        /// Конструктор класса звезды.
        /// </summary>
        /// <param name="pos">Верхняя левая точка звезы.</param>
        /// <param name="dir">Направление дивижения звезы.</param>
        /// <param name="size">Размер звезы.</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size, "Звезда")
        {
        }
        /// <summary>
        /// Рисует звезду.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        public override void Regeneration()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Обновляет положение звезды.
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }

    }
}
