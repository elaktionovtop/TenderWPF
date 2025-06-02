﻿namespace TenderApp.Models
{
    public class Tender
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Product { get; set; } = null!;
        public decimal Budget { get; set; }
        public int Quantity { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public ICollection<TenderCriterion> Criteria { get; set; } 
            = [];
        public ICollection<Proposal> Proposals { get; set; } 
            = [];
    }
}
