using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyConsoleApp2
{
  class Program
  {
    static void Main(string[] args)
    {
      Company company1 = new Company { Name = "GilStroy-1" };
      Company company2 = new Company { Name = "GilStroy-2" };

      Car car1 = new Car { Model = "KRAZ", Company = company1 };
      Car car2 = new Car { Model = "GAZ", Company = company2 };
      Car car3 = new Car { Model = "GAZEL", Company = company2 };

      Driver driver1 = new Driver { Name = "Ivanov", Cars = new List<Car>() { car1 ,car2} };
      Driver driver2 = new Driver { Name = "Petrov", Cars = new List<Car>() { car2, car3 } };
      Driver driver3 = new Driver { Name = "Sidorov", Cars = new List<Car>() { car1, car3 } };
      Driver driver4 = new Driver { Name = "Zelenskiy", Cars = new List<Car>() { car1} };
      Driver driver5 = new Driver { Name = "Poroshenko", Cars = new List<Car>() { car1, car2,car3 } };
      Driver driver6 = new Driver { Name = "Kravchuk", Cars = new List<Car>() {  car2 } };
      var db = new ApplicationContext();

      ClearData(db);

      db.Companies.AddRange(company1, company2);  // добавление компаний
      db.Cars.AddRange(car1, car2, car3);
      db.Drivers.AddRange(driver1, driver2, driver3, driver4, driver5, driver6);// добавление пользователей
      db.SaveChanges();

      Console.WriteLine($" BASIC UNIVERSAL METHOD ");
      Console.WriteLine();

      foreach (var elem in db.Cars.ToList())
      {
        Console.Write($"{elem.Model} owned by the company {elem.Company?.Name}  ");

        foreach (var driver in elem.Drivers)
        {
          Console.Write($"  driver: {driver.Name}");
        }
        //--------------------------------------------------------------------------
        Console.WriteLine();
       
      }
      Console.WriteLine($" ------------------------------------------------------");
      Console.WriteLine();
      foreach (var driver in db.Drivers.Where(d => d.Name=="Ivanov").ToList())
      {
        Console.Write($"driver: {driver.Name} : ");
        foreach (var car in driver.Cars.ToList())
        {
          Console.Write($"car: {car.Model} ");
        }
        Console.WriteLine();
      }
      Console.WriteLine($" ------------------------------------------------------");
      Console.WriteLine();

      //-------------------------------------------------------------------------------
      foreach (var driver in db.Drivers.ToList())
      {
        Console.Write($"driver: {driver.Name} : ");
        foreach (var car in db.Cars.ToList())
        {
          Console.Write($"car: {car.Model}  ");
        }
        Console.WriteLine();
      }
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      //------------------------------------------------------------------------------
      Console.Write($"the machines belongs to the companies version 1");
      Console.WriteLine();
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      foreach (var elem in db.Cars.ToList())
      {
        Console.Write($"car: {elem.Model} : ");
        foreach (var elem2 in db.Companies.Where(c => c.Id == elem.Company.Id).ToList())
        {
          Console.Write($"belongs to the company: {elem2.Name} ");
        }
        Console.WriteLine();
      }
      //---------------------------------------------------------------------------------
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      Console.Write($"the machine belongs to the company GilStroy2 version 1");
      Console.WriteLine();
        Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();



      foreach (var elem in db.Companies.Where(c=>c.Name=="GilStroy-2").ToList())
      {
        Console.Write($"company: {elem.Name} : ");
        Console.WriteLine();
        foreach (var elem2 in db.Cars.Where(d => d.CompanyId == elem.Id).ToList())
        {
          Console.Write($"belongs to the car: {elem2.Model} ");
          Console.WriteLine();
        }
        Console.WriteLine();
      }

     //------------------------------------------------------------------------------
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      Console.Write($"the machine belongs to the company GilStroy2 version 2 ");
      Console.WriteLine();


      var cars = (from car in db.Cars where car.Company.Name == "GilStroy-2"
                  select car).ToList();
      foreach (var car in cars)
      {
        Console.WriteLine($"{car.Model} own {car.Company.Name}");
      }
      //-------------------------------------------------------------------------------  
      Console.WriteLine();
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      Console.Write($"the machines belongs to the companies version 2 ");
      Console.WriteLine();
      var cars22 = db.Cars.Select(p => new
      {
        Model = p.Model,
        Company = p.Company.Name
      });
      foreach (var car in cars22)
        Console.WriteLine($"{car.Model} ({car.Company}) ");
      //-------------------------------------------------------------------------------------
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      Console.Write($"the machines belongs to the companies version 3 method JOIN ");
      Console.WriteLine();


      var cars7 = db.Cars.Join(db.Companies, // второй набор
       u => u.CompanyId, // свойство-селектор объекта из первого набора
       c => c.Id, // свойство-селектор объекта из второго набора
       (u, c) => new // результат
        {
         Model = u.Model,
         Company = c.Name,
        
       });
      foreach (var u in cars7)
        Console.WriteLine($"{u.Model} ({u.Company})");
      //-------------------------------------------------------------------
      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();


      foreach (var elem in db.Cars.ToList())
      {
        Console.Write($"{elem.Model}   ");

        foreach (var driver in elem.Drivers)
        {
          Console.Write($"  driver: {driver.Name}");
        }
        Console.WriteLine();
      }

      Console.Write($"-------------------------------------------------------------");
      Console.WriteLine();
      Console.Write($"linked the company and the drivers");
      Console.WriteLine();
      foreach (var elem in db.Cars.ToList())
      {
        Console.Write($" work for the company {elem.Company?.Name}  ");

        foreach (var driver in elem.Drivers)
        {
          Console.Write($"  driver: {driver.Name}");
        }
        //--------------------------------------------------------------------------
        Console.WriteLine();

      }













    }


      private static void ClearData(ApplicationContext db)
    {
      db.Drivers.RemoveRange(db.Drivers);
      db.Cars.RemoveRange(db.Cars);
      db.Companies.RemoveRange(db.Companies);

      db.SaveChanges();
    }
  }
}
