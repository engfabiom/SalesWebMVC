using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models {
    public class Seller {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department) {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sales) {
            Sales.Add(sales);
        }

        public void Remove(SalesRecord sales) {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime initial, DateTime final) {
            return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Amount);
        }

    }
}
