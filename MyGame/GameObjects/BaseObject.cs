using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    public abstract class BaseObject: ICollision
    {
        /// <summary>
        /// Имя объекта.
        /// </summary>
        private string name;
        /// <summary>
        /// Верхняя левая точка объекта.
        /// </summary>
        protected Point Pos;
        /// <summary>
        /// Направление дивижения объекта.
        /// </summary>
        protected Point Dir;
        /// <summary>
        /// Размер объекта.
        /// </summary>
        protected Size Size;
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Делегат для методов, вызывающихся при необходимости передать сообщение
        /// </summary>
        public delegate void Message();
        /// <summary>
        /// Делегат для методов, вызывающихся при необходимости передать информацию в поток
        /// </summary>
        /// <param name="str">Сообщение</param>
        public delegate void Write(string str);
        /// <param name="pos">Верхняя левая точка объекта.</param>
        /// <param name="dir">Направление дивижения объекта.</param>
        /// <param name="size">Размер объекта.</param>
        public BaseObject(Point pos, Point dir, Size size, string name)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
            Name = name;
            try
            {
                if (Size.Height < 0) throw new GameObjectException("Высота объекта не может быть отрицательной!");
                if (Size.Width < 0) throw new GameObjectException("Ширина объекта не может быть отрицательной!");
                if (Size.Height > 800) throw new GameObjectException("Высота объекта не может быть более 800!");
                if (Size.Width > 800) throw new GameObjectException("Ширина объекта не может быть более 800!");
                if (Pos.X < 0) throw new GameObjectException("Координата X не может быть отрицательной!");
                if (Pos.Y < 0) throw new GameObjectException("Координата Y не может быть отрицательной!");

            }
            catch (GameObjectException e)
            {
                MessageBox.Show(e.Message, "Ошибочка вышла!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Рисует объект.
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Обновляет объект.
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
        /// <summary>
        /// Регенерирует объект.
        /// </summary>
        public abstract void Regeneration();

        /// <summary>
        /// Имитирует столкновение.
        /// </summary>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(Rect);
        /// <summary>
        /// Объект в графической интерпретации.
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
