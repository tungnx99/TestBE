using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Paganation
{
    public class SearchPaganationDTO<T>
    {

        public T Search { get; set; }
        public int PageIndex
        {
            get; set;
        } = 1;
        public int PageSize { get; set; } = 10;

    }
}
