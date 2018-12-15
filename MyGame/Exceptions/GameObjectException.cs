using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс-обёртка пользовательского исключения.
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException() : base()
        { }
        public GameObjectException(string message) : base(message)
        { }
    }
}
