using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Paganation
{
    public class SerachPaganationDTO<T>
    {
        public SerachPaganationDTO()
        {
            if (PageNumber < 1)
            {
                PageNumber = 1;
            }
        }

        public T Search { get; set; }
        public int PageNumber
        {
            get; set;
        }
        public int PageSize { get; set; } = 10;
        public int Take
        {
            get
            {
                return PageNumber * PageSize;
            }
        }
        public int Skip
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
        }
    }
}
