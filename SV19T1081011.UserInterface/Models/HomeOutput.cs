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
    public class HomeOutput
    {
        public List<PostCategory> Categories { get; set; }
        public List<Post> MostRecentPosts { get; set; }
        public List<Post> TrendingPosts { get; set; }
        public List<Post> FeaturedPosts { get; set; }
    }
}