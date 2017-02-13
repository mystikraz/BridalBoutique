using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class Page
    {
        public int PagesID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
