using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1081011.UserInterface.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInterfaceOutput
    {
        public List<PostCategory> Categories { get; set; }
        public List<Post> MostRecentPosts { get; set; }
        public List<Post> TrendingPosts { get; set; }
        public List<Post> FeaturedPosts { get; set; }
        public PostCategory Category { get; set; }
        public List<Post> List { get; set; }
        public Post Post { get; set; }
        public List<PostComment> Comments { get; set; }
        public int RowCount { get; set; }
    }
}