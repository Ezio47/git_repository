using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLW.Project.Springer.SpringerRemoteDataWcfService.Interfaces;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Services
{
    public partial class SpringerRemoteSystemService : ISpringerRemoteSystemService
    {
        public bool IsReady()
        {
            return true;
        }
    }
}
