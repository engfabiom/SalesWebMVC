namespace SalesWebMVC.Models {
    public class Department {
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = null;
        public ICollection<Seller> Sellers { get; set; } = [];

        public Department() { }

        public Department(int id, string? name) {
            Id = id;
            Name = name;
        }

        public void AddSellers(Seller seller) {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final) {
            return Sellers.Sum(s => s.TotalSales(initial, final));
        }
    }
}
