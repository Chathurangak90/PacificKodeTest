using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificKode.Services
{
    public interface IHome
    {
        //Retrieves counts for home page
        object LoadDepEmpCount();
    }
}
