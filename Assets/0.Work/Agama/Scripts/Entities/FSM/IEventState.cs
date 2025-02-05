using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agama.Scripts.Entities.FSM
{
    public interface IEventState
    {
        public Action OnEventEndEvent { get; set; }
    }
}
