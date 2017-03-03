using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.MVC.Interfaces.Generic
{
    /// <summary>
    /// Has a model data container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRenderable<T> : IRenderable
    {
        T Model { get; set; }
    }
}
