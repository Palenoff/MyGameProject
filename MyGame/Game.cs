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
        /// <summary>
        /// Массив объектов.
        /// </summary>
        private static BaseObject[] _objs;
        /// <summary>
        /// Список пуль.
        /// </summary>
        private static List<Bullet> _bullets = new List<Bullet>();
        /// <summary>
        /// Список астероидов.
        /// </summary>
        private static List<Asteroid> _asteroids;
        /// <summary>
        /// Текущее количество астероидов в игре.
        /// </summary>
        private static int n_asteroids;
        /// <summary>
        /// Массив аптечек.
        /// </summary>
        private static Healing[] _healings;
        /// <summary>
        /// Словарь картинок.
        /// </summary>
        public static Dictionary<string, Image> Images;
        /// <summary>
        /// Словрь картиок для астероидов.
        /// </summary>
        public static Dictionary<string, Image> Images_asteroids;
        /// <summary>
        /// Космический корабль.
        /// </summary>
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(100, 60));
        /// <summary>
        /// Таймер.
        /// </summary>
        private static Timer _timer = new Timer();
        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        public static Random Rnd = new Random();
        /// <summary>
        /// Объект, предназначенный для записи потока в файл.
        /// </summary>
        private static StreamWriter _stream_log = new StreamWriter("log.txt");

        /// <summary>
        /// Ширина игрового поля.
        /// </summary>
        public static int Width { get; set; }
        /// <summary>
        /// Высота игрового поля.
        /// </summary>
        public static int Height { get; set; }

        static Game()
        {
        }
        /// <summary>
        /// Обработчик срабатывания тика тймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        /// <summary>
        /// Обработчик нажатия на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));

            if (e.KeyCode == Keys.Space) _bullets.Add(new Bullet(new Point(Rnd.Next(0,Width), Rnd.Next(0,Height)), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Left) _ship.Left();
            if (e.KeyCode == Keys.Right) _ship.Right();
        }
        /// <summary>
        /// Завершение игры.
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        /// <summary>
        /// Инициализация игры.
        /// </summary>
        /// <param name="form">Игровая форма</param>
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
                Images = ImagesInit(@"..\..\Images");
                Images_asteroids = ImagesInit(@"..\..\Images\Asteroids");
                n_asteroids = Rnd.Next(1, Images_asteroids.Count);
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
        /// <summary>
        /// Инициализация картинок.
        /// </summary>
        /// <param name="path">Путь к директории расположения картинок.</param>
        /// <returns></returns>
        private static Dictionary<string,Image> ImagesInit(string path)
        {
            Dictionary<string, Image> collection = new Dictionary<string, Image>();
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                collection[Path.GetFileNameWithoutExtension(file)] = Image.FromFile(file);
            return collection;
        }
        /// <summary>
        /// Инициализация списка астероидов.
        /// </summary>
        private static void AsteroidsInit()
        {
            var rnd = new Random();
            for (var i = 0; i < n_asteroids; i++)
            {
                int r = rnd.Next(5, 200);
                int index_image = rnd.Next(1,Images_asteroids.Count);
                _asteroids.Add(new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(Images_asteroids.Values.ElementAt(index_image).Width / 10, Images_asteroids.Values.ElementAt(index_image).Height / 10), Images_asteroids.Keys.ElementAt(index_image)));
            }
            n_asteroids += 1;
        }
        /// <summary>
        /// Отрисовка объектов.
        /// </summary>
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
            foreach (Bullet b in _bullets) b.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("\nPoints:" + _ship.Points, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }
            Buffer.Render();


        }
        /// <summary>
        /// Инициализация объектов.
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[30];
            _asteroids = new List<Asteroid>();
            _healings = new Healing[2];
            var rnd = new Random();
            int r;
            for (var i = 0; i < _objs.Length; i++)
            {
                r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            AsteroidsInit();            
            for (var i = 0; i<_healings.Length;i++)
            {
                r = rnd.Next(5, 50);
                _healings[i] = new Healing(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(50, 50));
            }
        }
        /// <summary>
        /// Обновление объектов.
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet b in _bullets) b.Update();
            foreach (Healing h in _healings) h.Update();
            for (var i = 0; i < _asteroids.Count; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_asteroids[i] == null || _ship.Collision(_asteroids[i]))
                {
                    var rnd = new Random();
                    _ship?.Collide(rnd,_asteroids[i]);
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
                    _asteroids.RemoveAt(i);
                }
                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        _asteroids[i] = null;
                        //_bullets.RemoveAt(j);
                        _bullets[j].Regeneration();
                        j--;
                    }
                }

            }
            if (_asteroids.Count == 0) AsteroidsInit();
            for (int j = 0; j < _bullets.Count; j++)
            {
                if (_ship.Collision(_bullets[j]))
                {
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
        /// <summary>
        /// Закрытие файлового потока.
        /// </summary>
        public static void CloseStream()
        {
            _stream_log.Close();
        }
    }
}
