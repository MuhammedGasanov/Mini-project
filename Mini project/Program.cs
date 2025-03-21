using System;
using System.Collections.Generic;

public abstract class BaseEntity
{
    public int Id { get; set; }
}

public class User : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class Category : BaseEntity
{
    public string Name { get; set; }
}

public class Medicine : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public static class DB
{
    public static List<User> Users = new List<User>();
    public static List<Category> Categories = new List<Category>();
    public static List<Medicine> Medicines = new List<Medicine>();
}

public class UserService
{
    public User Login(string email, string password)
    {
        foreach (var user in DB.Users)
        {
            if (user.Email == email && user.Password == password)
            {
                return user;
            }
        }
        throw new NotFoundException("User not found");
    }

    public void AddUser(User user)
    {
        DB.Users.Add(user);
    }
}

public class CategoryService
{
    public void CreateCategory(Category category)
    {
        DB.Categories.Add(category);
    }
}

public class MedicineService
{
    public void CreateMedicine(Medicine medicine)
    {
        medicine.CreatedDate = DateTime.Now;
        DB.Medicines.Add(medicine);
    }

    public Medicine GetMedicineById(int id)
    {
        foreach (var medicine in DB.Medicines)
        {
            if (medicine.Id == id)
            {
                return medicine;
            }
        }
        throw new NotFoundException("Medicine not found");
    }

    public List<Medicine> GetAllMedicines()
    {
        return DB.Medicines;
    }

    public void DeleteMedicine(int id)
    {
        var medicine = GetMedicineById(id);
        DB.Medicines.Remove(medicine);
    }

    public void UpdateMedicine(int id, Medicine updatedMedicine)
    {
        for (int i = 0; i < DB.Medicines.Count; i++)
        {
            if (DB.Medicines[i].Id == id)
            {
                DB.Medicines[i].Name = updatedMedicine.Name;
                DB.Medicines[i].Price = updatedMedicine.Price;
                DB.Medicines[i].CategoryId = updatedMedicine.CategoryId;
                DB.Medicines[i].UserId = updatedMedicine.UserId;
                DB.Medicines[i].CreatedDate = updatedMedicine.CreatedDate;
                return;
            }
        }
        throw new NotFoundException("Medicine not found");
    }
}

public class Program
{
    static void Main()
    {
        UserService userService = new UserService();
        userService.AddUser(new User { FullName = "Muhammed Gasanov", Email = "muhammedgasanov@gmail.com", Password = "ilovecoding1234" });

        Console.WriteLine("User added successfully!");

        UserService usersService = new UserService();
        userService.AddUser(new User { FullName = "Elnur Mehdiyev", Email = "muhammedgasanov@gmail.com", Password = "scubadiving123" });
    }
}

