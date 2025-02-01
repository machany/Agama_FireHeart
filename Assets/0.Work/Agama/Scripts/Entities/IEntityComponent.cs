using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agama.Scripts.Entities
{
    public interface IEntityComponent
    {
        public void Initialize(Entity owner);
    }
}
