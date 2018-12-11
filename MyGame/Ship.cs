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
        public int Energy => _energy;
        public int Points => _points;

        public static event Message MessageDie;
        public static event Write WriteDecreaseEnergy;
        public static event Write WriteIncreaseEnergy;
        public static event Write WriteIncreasePoints;
        public static event Write WriteCollide;
        public static event Write WriteHeal;
        public static event Write WriteDie;



        public void Energy_Down(int n)
        {
            _energy -= n;
            WriteDecreaseEnergy?.Invoke(Action.ShipLostEnergy(n));
        }
        public void Energy_Up(int n)
        {
            _energy += n;
            WriteIncreaseEnergy?.Invoke(Action.ShipGotEnergy(n));
            WriteHeal?.Invoke(Action.ShipHealed(Pos));
        }
        public void Points_Up(int n)
        {
            _points += n;
            WriteIncreasePoints(Action.ShipGotPoints(n));
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Images["Spaceship"], Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y + Size.Height < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }
        public void Right()
        {
            if (Pos.X + Size.Width < Game.Width) Pos.X = Pos.X + Dir.X;
        }
        public void Die()
        {
            MessageDie?.Invoke();
            WriteDie?.Invoke(Action.ShipDied());
            Game.CloseStream();
        }
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

        public override string ToString()
        {
            return $"Космический корабль";
        }
        
    }

}
