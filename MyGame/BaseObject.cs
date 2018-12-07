using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    abstract class  BaseObject
    {
        public Point Pos;
        protected Point Dir;
        public Size Size;
        protected Image _image;
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
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
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public abstract void Update();
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    Pos.Y = Pos.Y + Dir.Y;
        //    if (Pos.X < 0) Dir.X = -Dir.X;
        //    if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //    if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //    if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        //}
    }
}
