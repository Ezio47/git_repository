using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Springer.EntityModel.Entity
{
    #region 上传流

    [DataContract]
    public class FileUploadMessageModel
    {
        /// <summary>
        /// 上传文件名
        /// </summary>
        [DataMember]
        public string FileName;

        /// <summary>
        /// 上传文件描述
        /// </summary>
        [DataMember]
        public string FileDescripe;

        /// <summary>
        ///  上传文件类型
        /// </summary>
        [DataMember]
        public string FileType;

        /// <summary>
        /// 上传文件流
        /// </summary>
        [DataMember]
        public byte[] FileData;

    }

    #endregion

}
