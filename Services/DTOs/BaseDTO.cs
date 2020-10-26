﻿using System;
using System.Collections.Generic;

namespace Services.DTOs
{
    public class ResponseBaseDTO
    {
        public int Id { get; set; }       

        public DateTime AddedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }

    public class PagedResult<T>
    {
        public int PageIndex { get; set; }
        public int PerPage { get; set; }
        public int TotalPageCount { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

    public class EnumResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}