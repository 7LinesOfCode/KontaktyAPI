using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Application.DTO
{
    public class ContactsPageDTO
    {
        public int PageSize { get; set; }
        public int? pageNo { get; set; }
        public string? SearchString { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}
