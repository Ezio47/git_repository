using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Springer.EntityModel.Entity
{
    [Serializable]
    [DataContract]
    //[KnownType(typeof(Points))]
    public class Points
    {
        [DataMember]
        public string JD { get; set; }
        [DataMember]
        public string WD { get; set; }
    }
}
