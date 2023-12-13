using KontaktyAPI.Application.Viewmodels.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Application.Viewmodels.Collections
{
    public class ListContactForListVm
    {
        public List<ContactForListVm> Contacts { get; set; }
        public int CurrentylPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public List<string> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int Count { get; set; }

    }
}
