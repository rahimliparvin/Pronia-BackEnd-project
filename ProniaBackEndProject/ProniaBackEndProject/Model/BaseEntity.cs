﻿namespace ProniaBackEndProject.Model
{
    public abstract class BaseEntity 
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Updated { get; set; } = DateTime.Now;

    }
}
