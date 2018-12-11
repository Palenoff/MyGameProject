using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public static class Action
    {
        public static string FormLoaded()
        {
            return $"Инициализация игрового поля";
        }

        public static string PicturesLoaded()
        {
            return $"Загружаются картинки";
        }

        public static string PictureLoaded(string name)
        {
            return $"Загружается картинка {name}";
        }

        public static string ObjectAdded(BaseObject what)
        {
            return $"Создан объект {what.ToString()}";
        }

        public static string ShipLostEnergy(int n)
        {
            return $"Корабль потерял {n} единиц энергии";
        }

        public static string ShipGotEnergy(int n)
        {
            return $"Корабль получил {n} единиц энергии";
        }

        public static string ShipCollided(Asteroid with, Point where)
        {
            return $"Корабль столкнулся с астероидом {with.ToString()} в точке ({where.X}; {where.Y})";
        }

        public static string ShipHealed(Point where)
        {
            return $"Корабль словил аптечку в точке ({where.X};{where.Y})";
        }

        public static string ShipGotPoints(int n)
        {
            return $"Корабль получил {n} очков";
        }

        public static string BulletHitedAndRegenerated(BaseObject what, Point wherehits, Point whereregenerates)
        {
            return $"Пуля попала в {what.ToString()} в точке ({wherehits.X};{wherehits.Y}) и регенерировалась в точке ({whereregenerates.X};{whereregenerates.Y})";
        }

        public static string ShipDied()
        {
            return $"Корабль героически погиб";
        }

        public static string Finish()
        {
            return $"Finita la comedia!";
        }
    }
}
