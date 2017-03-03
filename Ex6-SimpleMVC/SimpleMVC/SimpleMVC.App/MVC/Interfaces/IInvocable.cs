using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.MVC.Interfaces
{
    /// <summary>
    /// Responsible for calling render()
    /// </summary>
    public interface IInvocable
    {
        string Invoke();
    }
}
