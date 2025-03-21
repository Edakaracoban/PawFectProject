﻿using PawFect.Entities;
namespace PawFect.WebUI.Models
{
    public class ProductListModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
