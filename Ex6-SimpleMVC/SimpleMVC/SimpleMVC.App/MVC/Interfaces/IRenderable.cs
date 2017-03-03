using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.MVC.Interfaces
{
    /// <summary>
    /// Class implementing this needs to structure and provide content for the response
    /// </summary>
    public interface IRenderable
    {
        string Render();
    }
}
