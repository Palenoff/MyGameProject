using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс, представляющий объект аптечки.
    /// </summary>
    class Healing : BaseObject
    {
        /// <summary>
        /// Конструктор класса аптечки.
        /// </summary>
        /// <param name="pos">Верхняя левая точка аптечки.</param>
        /// <param name="dir">Направление дивижения аптечки.</param>
        /// <param name="size">Размер аптечки.</param>
        public Healing(Point pos, Point dir, Size size) : base(pos, dir, size, "Аптечка")
        {
        }
        /// <summary>
        /// Рисует аптечку.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Images["Healing"], Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Regeneration()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Обновляет положение аптечки.
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 5 * Convert.ToInt32(Math.Sin(Convert.ToDouble(Dir.X)));
            Pos.Y = Pos.Y + 5 * Convert.ToInt32(Math.Sin(Convert.ToDouble(Dir.Y)));
            if (Pos.X > Game.Width + 300) Pos.X = Size.Width;
            if (Pos.Y > Game.Height + 300) Pos.Y = Size.Height;
            if (Pos.X < -300) Pos.X = Game.Width;
            if (Pos.Y < -300) Pos.Y = Game.Height;
        }
    }
}
