using System;
using System.Collections.Generic;
using System.Linq;

namespace JustTradeIt.Software.API.Models
{
    public class Envelope<T> where T : class
    {
        public Envelope(int pageNumber, int pageSize, IEnumerable<T> items)
        {
            Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            MaxPages = (int) Math.Ceiling((decimal) items.Count() / pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
            if (pageNumber > pageSize)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}