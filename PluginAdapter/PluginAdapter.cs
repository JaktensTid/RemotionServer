using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginAdapter
{
    public interface IPlugin
    {
        List<PluginAction> Actions { get; }
    }

    public abstract class PluginAction
    {
        public abstract int Flag { get; }
        public abstract string Name { get; }
        public abstract Action Action { get; }
    }
}
