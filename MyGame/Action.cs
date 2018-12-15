using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс сообщений о событиях.
    /// </summary>
    public static class Action
    {
        /// <summary>
        /// Возвращает ообщение о загрузке формы.
        /// </summary>
        public static string FormLoaded()
        {
            return $"Инициализация игрового поля";
        }
        /// <summary>
        /// Возвращает ообщение о загрузке картинок.
        /// </summary>
        public static string PicturesLoaded()
        {
            return $"Загружаются картинки";
        }
        /// <summary>
        /// Возвращает ообщение о загрузке картинки.
        /// <param name="name">Имя картинки</param>
        /// </summary>
        public static string PictureLoaded(string name)
        {
            return $"Загружается картинка {name}";
        }
        /// <summary>
        /// Возвращает ообщение о добавлении объекта.
        /// <param name="what">Добавленный объект</param>
        /// </summary>
        public static string ObjectAdded(BaseObject what)
        {
            return $"Создан объект {what.Name}";
        }
        /// <summary>
        /// Возвращает ообщение о понижении энергии корабля.
        /// <param name="n">Количество энергии</param>
        /// </summary>
        public static string ShipLostEnergy(int n)
        {
            return $"Корабль потерял {n} единиц энергии";
        }
        /// <summary>
        /// Возвращает ообщение о повышении энергии корабля.
        /// <param name="n">Количество энергии</param>
        /// </summary>
        public static string ShipGotEnergy(int n)
        {
            return $"Корабль получил {n} единиц энергии";
        }
        /// <summary>
        /// Возвращает ообщение о столкновении с объектом.
        /// </summary>
        /// <param name="with">Объект, с которым произошло столкновение</param>
        /// <param name="where">Точка, в которой произошло столкновение</param>
        /// <returns></returns>
        public static string ShipCollided(Asteroid with, Point where)
        {
            return $"Корабль столкнулся с астероидом {with.Name} в точке ({where.X}; {where.Y})";
        }
        /// <summary>
        /// Возвращает сообщение о лечении корабля.
        /// </summary>
        /// <param name="where">Точка, в которой произошло лечение.</param>
        /// <returns></returns>
        public static string ShipHealed(Point where)
        {
            return $"Корабль словил аптечку в точке ({where.X};{where.Y})";
        }
        /// <summary>
        /// Возвращает ообщение о наборе очков кораблём.
        /// <param name="n">Количество очков</param>
        /// </summary>
        public static string ShipGotPoints(int n)
        {
            return $"Корабль получил {n} очков";
        }
        /// <summary>
        /// Возвращает ообщение о столкновении с объектом.
        /// </summary>
        /// <param name="what">Объект, в который попала пуля</param>
        /// <param name="wherehits">Точка, в которой пуля попала в объект</param>
        /// <param name="whereregenerates">Точка, в которой пуля регенерировалась</param>
        public static string BulletHitedAndRegenerated(BaseObject what, Point wherehits, Point whereregenerates)
        {
            return $"Пуля попала в {what.Name} в точке ({wherehits.X};{wherehits.Y}) и регенерировалась в точке ({whereregenerates.X};{whereregenerates.Y})";
        }
        /// <summary>
        /// Возвращает сообщение о смерти корабля
        /// </summary>
        public static string ShipDied()
        {
            return $"Корабль героически погиб";
        }
        /// <summary>
        /// Возвращает сообщение о завершении игры
        /// </summary>
        /// <returns></returns>
        public static string Finish()
        {
            return $"Finita la comedia!";
        }
    }
}
