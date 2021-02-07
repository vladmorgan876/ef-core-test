using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsoleApp2
{
  public class Car
  {
    public int Id { get; set; }
    public string Model { get; set; }


    public int CompanyId { get; set; }      // внешний ключ
    public Company Company { get; set; }    // навигационное свойство

    //public Driver Driver { get; set; }
    public List<Driver> Drivers { get; set; } = new List<Driver>();
  }
}
