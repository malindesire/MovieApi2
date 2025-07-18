﻿namespace MovieCore.Requests
{
    public class MoviePaginationParamaters
    {
        private int _pageSize = 10;
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        public int PageSize 
        { 
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
