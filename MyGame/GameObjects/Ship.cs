using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        private int _points = 0;
        /// <summary>
        /// Энергия корабля.
        /// </summary>
        public int Energy => _energy;
        /// <summary>
        /// Очки корабля
        /// </summary>
        public int Points => _points;

        /// <summary>
        /// Событие, которое возбуждается при необходимости передать сообщение о смерти корабля
        /// </summary>
        public static event Message MessageDie;
        /// <summary>
        /// Событие, которое возбуждается при необходимости записать в поток информацию о понижении уровня энергии
        /// </summary>
        public static event Write WriteDecreaseEnergy;
        /// <summary>
        /// Событие, которое возбуждается при необходимости записать в поток информацию о повышении уровня энергии
        /// </summary>
        public static event Write WriteIncreaseEnergy;
        /// <summary>
        /// Событие, которое возбуждается при необходимости записать в поток информацию о повышении количества очков
        /// </summary>
        public static event Write WriteIncreasePoints;
        /// <summary>
        /// Событие, которое возбуждается при необходимости записать в поток информацию о столкновении
        /// </summary>
        public static event Write WriteCollide;
        /// <summary>
        /// Событие, которое возбуждается при необходимости записать в поток информацию о лечении
        /// </summary>
        public static event Write WriteHeal;
        /// <summary>
        /// Событие, которое возбуждается при необходимости записать в поток информацию о смерти
        /// </summary>
        public static event Write WriteDie;


        /// <summary>
        /// Понижает энергию
        /// </summary>
        public void Energy_Down(int n)
        {
            _energy -= n;
            WriteDecreaseEnergy?.Invoke(Action.ShipLostEnergy(n));
        }
        /// <summary>
        /// Повышает энергию
        /// </summary>
        public void Energy_Up(int n)
        {
            _energy += n;
            WriteIncreaseEnergy?.Invoke(Action.ShipGotEnergy(n));
            WriteHeal?.Invoke(Action.ShipHealed(Pos));
        }
        /// <summary>
        /// Добавляет очки
        /// </summary>
        public void Points_Up(int n)
        {
            _points += n;
            WriteIncreasePoints(Action.ShipGotPoints(n));
        }
        /// <summary>
        /// Конструктор класса космического корабля.
        /// </summary>
        /// <param name="pos">Верхняя левая точка космического корабля.</param>
        /// <param name="dir">Направление дивижения космического корабля.</param>
        /// <param name="size">Размер космического корабля.</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size, "Космический корабль")
        {
        }
        /// <summary>
        /// Рисует космический корабль.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Images["Spaceship"], Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Обновляет космический корабль.
        /// </summary>
        public override void Update()
        {
        }
        /// <summary>
        /// Двигает космический корабль вверх.
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        /// <summary>
        /// Двигает космический вниз.
        /// </summary>
        public void Down()
        {
            if (Pos.Y + Size.Height < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        /// <summary>
        /// Двигает космический корабль влево.
        /// </summary>
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }
        /// <summary>
        /// Двигает космический корабль вправо.
        /// </summary>
        public void Right()
        {
            if (Pos.X + Size.Width < Game.Width) Pos.X = Pos.X + Dir.X;
        }
        /// <summary>
        /// Умертвляет космический корабль.
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
            WriteDie?.Invoke(Action.ShipDied());
            Game.CloseStream();
        }
        /// <summary>
        /// Сталкивает космический корабль с астероидом.
        /// </summary>
        public void Collide(Random rnd, Asteroid asteroid)
        {
            Energy_Down(rnd.Next(1, 10));
            Points_Up(rnd.Next(0, 1000));
            WriteCollide?.Invoke(Action.ShipCollided(asteroid,Pos));
        }

        public override void Regeneration()
        {
            throw new NotImplementedException();
        }
        
    }

}
