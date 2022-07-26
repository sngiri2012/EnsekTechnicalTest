using System;

namespace EnsekTechnicalTest.DTO
{
    public class BuyOrders
    {
        public int Quantity { get; set; }
        public float UnitCost { get; set; }
        public int UnitsRemaining { get; set; }
        public Guid OrderId { get; set; }
        public string EnergyType { get; set; }

    }
}
