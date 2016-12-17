using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotionServer.Engine.Commands
{
    public interface ICommand
    {
        void Execute(string parameter);
    }
}
