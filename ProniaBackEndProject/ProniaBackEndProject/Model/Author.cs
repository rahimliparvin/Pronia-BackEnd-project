﻿namespace ProniaBackEndProject.Model
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }  
        public ICollection<Blog> Blogs { get; set; }
    }

}
