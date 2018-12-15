using System;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Класс, представляющий объект астероида.
    /// </summary>
    public class Asteroid: BaseObject
    {
        /// <summary>
        /// Энергия астероида.
        /// </summary>
        public int Power { get; set; }
        /// <summary>
        /// Конструктор класса астероида.
        /// </summary>
        /// <param name="pos">Верхняя левая точка астероида.</param>
        /// <param name="dir">Направление дивижения астероида.</param>
        /// <param name="size">Размер астероида.</param>
        /// <param name="name">Имя астероида.</param>
        public Asteroid(Point pos, Point dir, Size size, string name) : base(pos, dir, size, name)
        {
            Power = 1;
        }
        /// <summary>
        /// Рисует астероид.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Images_asteroids[Name], Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Обновляет положение астероида.
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X; //поменял на +, как и было рекомедовано в комментарии
            if (Pos.X < -Size.Width) Pos.X = Game.Width + Size.Width;
        }

        public override void Regeneration()
        {
            throw new System.NotImplementedException();
        }
    }
}