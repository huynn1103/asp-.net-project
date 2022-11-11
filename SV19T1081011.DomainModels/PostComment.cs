using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class PostComment
    {
        ///<summary>
        ///
        ///</summary>
        public long CommentId { get; set; }
        ///<summary>
        ///
        ///</summary>
        public DateTime CreatedTime { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string CommentContent { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool IsAccepted { get; set; }
        ///<summary>
        ///
        ///</summary>
        public long UserId { get; set; }
        ///<summary>
        ///
        ///</summary>
        public long PostId { get; set; }
    }

}
