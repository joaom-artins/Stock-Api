using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; }="";
        public string? CompanyName { get; set; }="";
        public string? SortBy { get; set; }="";
        public bool IsDescending { get; set; }=false;
        public int PagesNumber { get; set; }=1;
        public int PagesSize { get; set; }=2;
    }
}