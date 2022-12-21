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
    public class Post
    {
        ///<summary>
        ///
        ///</summary>
        public long PostId { get; set; }
        ///<summary>
        ///
        ///</summary>
        public DateTime CreatedTime { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Title { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string BriefContent { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string FullContent { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string UrlTitle { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Image { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool AllowComment { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool IsHidden { get; set; }
        ///<summary>
        ///
        ///</summary>
        public long UserId { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ViewCount { get; set; }
        public virtual Author Creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual PostCategory Category { get; set; }
    }    
}
