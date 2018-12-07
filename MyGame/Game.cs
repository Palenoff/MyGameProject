using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        private static Bullet _bulletobj;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Init(Form form)
        {
            try
            {
                // Графическое устройство для вывода графики            
                Graphics g;
                // Предоставляет доступ к главному буферу графического контекста для текущего приложения
                _context = BufferedGraphicsManager.Current;
                g = form.CreateGraphics();
                // Создаем объект (поверхность рисования) и связываем его с формой
                // Запоминаем размеры формы
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width < 0)
                    throw new ArgumentOutOfRangeException("Width", "Ширина слишком малеьнкая");
                if (Width > 1000)
                    throw new ArgumentOutOfRangeException("Width", "Ширина слишком большая");
                if (Height < 0)
                    throw new ArgumentOutOfRangeException("Height", "Высота слишком малеьнкая");
                if (Height > 1000)
                    throw new ArgumentOutOfRangeException("Height", "Высота слишком большая");
                // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
                Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
                Load();
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show(e.Message, "Ошибочка вышла!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {
            // Проверяем вывод графики
            //Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Blue, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            _objs[0] = _bulletobj = new Bullet(new Point(50, 250), new Point(1, 1), new Size(10, 20));
            for (int i = 1; i < _objs.Length / 2; i++)
                _objs[i] = new Cat(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
            _objs[_objs.Length / 2] = new Mars(new Point(200, 100), new Point(200, 50), new Size(100, 100));
            for (int i = _objs.Length / 2 + 1; i < _objs.Length - 1; i++)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
            _objs[_objs.Length - 1] = new Ellipse(new Point(600, 300), new Point(-100, 0), new Size(100, 100));
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                if (!(obj is Bullet))
                    if (obj.Pos.X < _bulletobj.Pos.X && _bulletobj.Pos.X < obj.Pos.X + obj.Size.Width && obj.Pos.Y < _bulletobj.Pos.Y && _bulletobj.Pos.Y < obj.Pos.Y + obj.Size.Height)
                        _bulletobj.Regeneration();
                obj.Update();
            }
        }
    }
}
