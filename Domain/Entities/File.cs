using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class File : BaseEntity
    {
        public string ContentType { get; set; }
        public string ContentDisposition { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}
