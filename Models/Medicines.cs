namespace Pharmacy_Management_System.Models
{
    public class Medicines
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string DateOfManufacture { get; set; }

        public string DateOfExpiry { get; set; }

        public int MedicineQuantity { get; set; }

        public double MedicinePricePerUnit { get; set; }

        
    }
}
