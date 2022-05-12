using System.Collections.Generic;
using System.Linq;
using BicycleShopDB.Tables;

namespace BicycleShopDB.Repository
{
    public class BicycleRepository
    {
        private readonly BicycleContext _context;

        public BicycleRepository(BicycleContext context)
        {
            _context = context;
        }

        public IEnumerable<Bicycle> GetAll() => _context.Bicycles.ToList();

        public Bicycle GetById(int? id) => GetAll().FirstOrDefault(bicycle => bicycle.BicycleId == id);

        public void UpdateQuantity(int id, int quantity)
        {
            Bicycle bicycle = GetById(id);
            bicycle.Quantity -= quantity;
            _context.Bicycles.Attach(bicycle);
            _context.Entry(bicycle)
                .Property(note => note.Quantity).IsModified = true;
            SaveChanges();
        }

        public void ReturnQuantity(int id, int quantity)
        {
            Bicycle bicycle = GetById(id);
            bicycle.Quantity += quantity;
            _context.Bicycles.Attach(bicycle);
            _context.Entry(bicycle)
                .Property(note => note.Quantity).IsModified = true;
            SaveChanges();
        }

        public void Update(Bicycle editBicycle)
        {
            Bicycle bicycle = GetById(editBicycle.BicycleId);

            bicycle.BicycleName = editBicycle.BicycleName;
            bicycle.Brand = editBicycle.Brand;
            bicycle.Type = editBicycle.Type;
            bicycle.ReleaseYear = editBicycle.ReleaseYear;
            bicycle.FrameMaterial = editBicycle.FrameMaterial;
            bicycle.WheelSize = editBicycle.WheelSize;
            bicycle.BrakeType = editBicycle.BrakeType;
            bicycle.SpeedQuantity = editBicycle.SpeedQuantity;
            bicycle.MaxWeight = editBicycle.MaxWeight;
            bicycle.Price = editBicycle.Price;
            bicycle.Discount = editBicycle.Discount;
            bicycle.Total = editBicycle.Total;
            bicycle.Quantity = editBicycle.Quantity;
            bicycle.ImagePath = editBicycle.ImagePath;

            SaveChanges();
        }

        public void Create(Bicycle bicycle)
        {
            _context.Bicycles.Add(bicycle);

            SaveChanges();
        }

        public void DeleteById(int bicycleId)
        {
            Bicycle bicycle = _context.Bicycles.FirstOrDefault(note => note.BicycleId == bicycleId);
            Delete(bicycle);
        }

        private void Delete(Bicycle bicycle)
        {
            _context.Bicycles.Remove(bicycle);
            SaveChanges();
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
