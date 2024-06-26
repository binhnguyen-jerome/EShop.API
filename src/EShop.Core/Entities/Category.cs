﻿namespace EShop.Core.Entities
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
