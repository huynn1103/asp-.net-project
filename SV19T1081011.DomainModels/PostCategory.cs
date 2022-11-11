using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DomainModels
{
    /// <summary>
    /// Phân loại bài viết
    /// </summary>
    public class PostCategory
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryUrlName { get; set; }

        public string CategoryDescriptions { get; set; }

        public int DisplayOrder { get; set; }
    }
}
