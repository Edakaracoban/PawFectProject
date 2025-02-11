using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Image { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
