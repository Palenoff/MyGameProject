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
        private static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        private static Healing[] _healings;
        public static Dictionary<string, Image> Images;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(100, 60));
        private static Timer _timer = new Timer();
        public static Random Rnd = new Random();
        private static StreamWriter _stream_log = new StreamWriter("log.txt");

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

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Left) _ship.Left();
            if (e.KeyCode == Keys.Right) _ship.Right();
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
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
                form.KeyDown += Form_KeyDown;
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show(e.Message, "Ошибочка вышла!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
            Ship.MessageDie += Finish;
            Ship.WriteCollide += Console.WriteLine;
            Ship.WriteCollide += _stream_log.WriteLine;
            Ship.WriteDecreaseEnergy += Console.WriteLine;
            Ship.WriteDecreaseEnergy += _stream_log.WriteLine;
            Ship.WriteDie += Console.WriteLine;
            Ship.WriteDie += _stream_log.WriteLine;
            Ship.WriteHeal += Console.WriteLine;
            Ship.WriteHeal += _stream_log.WriteLine;
            Ship.WriteIncreaseEnergy += Console.WriteLine;
            Ship.WriteIncreaseEnergy += _stream_log.WriteLine;
            Ship.WriteIncreasePoints += Console.WriteLine;
            Ship.WriteIncreasePoints += _stream_log.WriteLine;
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
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            foreach (Healing h in _healings)
            {
                h.Draw();
            }
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("\nPoints:" + _ship.Points, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }
            Buffer.Render();


        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[2];
            _healings = new Healing[2];
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
            for (var i = 0; i<_healings.Length;i++)
            {
                r = rnd.Next(5, 50);
                _healings[i] = new Healing(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(50, 50));
            }
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            _bullet?.Update();
            foreach (Healing h in _healings) h.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet.Collision(_asteroids[i]))
                {
                    _bullet.Regeneration();
                    continue;
                }
                if (_ship.Collision(_asteroids[i]))
                {
                    var rnd = new Random();
                    _ship?.Collide(rnd,_asteroids[i]);
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
                }
                if (_ship.Collision(_bullet))
                {
                    _bullet.Regeneration();
                    System.Media.SystemSounds.Beep.Play();
                    _ship?.Die();
                }
            }
            for (var i = 0; i < _healings.Length; i++)
            {
                if (_healings[i] == null) continue;
                _healings[i].Update();
                if (_ship.Collision(_healings[i]))
                {
                    var rnd = new Random();
                    _ship?.Energy_Up(rnd.Next(1, 10));
                    System.Media.SystemSounds.Hand.Play();
                }
            }
        }
        public static void CloseStream()
        {
            _stream_log.Close();
        }
    }
}
