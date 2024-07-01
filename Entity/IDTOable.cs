using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Entity;

public interface IDTOable<TDTO>
{
    public TDTO createDTO();
    public void update(TDTO updateFrom);
}
