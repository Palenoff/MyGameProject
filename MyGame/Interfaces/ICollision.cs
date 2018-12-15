using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Реализует столкновение объектов.
    /// </summary>
    public interface ICollision
    {
        /// <summary>
        /// Флаг, показывающий, произошло ли столкновение объектов.
        /// </summary>
        bool Collision(ICollision obj);
        /// <summary>
        /// Объект в графической интерпретации.
        /// </summary>
        Rectangle Rect { get; }
    }
}
