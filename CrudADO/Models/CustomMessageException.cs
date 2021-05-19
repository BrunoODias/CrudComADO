using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudADO.Models
{
    public class CustomMessageException:Exception
    {
        public CustomMessageException(string error):base(error)
        {

        }
    }
}
