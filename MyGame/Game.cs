using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        public static Dictionary<string, Image> Images;

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
                ImagesInit();
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

        private static void ImagesInit()
        {
            string path = @"..\..\Images";
            Images = new Dictionary<string, Image>();
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                Images[Path.GetFileNameWithoutExtension(file)] = Image.FromFile(file);
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[2];
            var rnd = new Random();
            int r;
            for (var i = 0; i < _objs.Length; i++)
            {
                r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            r = rnd.Next(5, 200);
            _asteroids[0] = new Ellipse(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            r = rnd.Next(5, 200);
            _asteroids[1] = new Mars(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet.Regeneration();
                }
            }
            _bullet.Update();
        }
    }
}
