using System.Collections.Generic;

namespace MyConsoleApp2
{
  public class Driver
  {
    public int Id { get; set; }
    public string Name { get; set; }
   // public int carid { get; set; }      // внешний ключ
   // public Car car { get; set; }    // навигационное свойство

    public List<Car> Cars { get; set; } =new List<Car>();
  
  }
}
