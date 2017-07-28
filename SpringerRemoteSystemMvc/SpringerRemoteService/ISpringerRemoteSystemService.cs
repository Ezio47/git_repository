using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Interfaces
{

    [ServiceContract]
    public interface ISpringerRemoteSystemService
    {

        [OperationContract(Name = "IsReady")]
        bool IsReady();
    }
}
