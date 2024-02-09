//Oliver Topley c2077500
//Vehicle registration project


using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
//Lists for the vehicles

List<Car> cars = new(50);
cars.Add(new Car("BV62SWK", "Ford Fiesta", 2012, 5500));
cars.Add(new Car("RW32JEF", "Vauxhall Corsa", 2018,11000));
cars.Add(new Car("HJ63NBO", "Volkswagen Polo", 2008,2500));
cars.Add(new Car("SW56FCO", "Ford Focus", 2022,2323));

List<Motorcycle> motorcycles = new(50);
motorcycles.Add(new Motorcycle("FG45WDS", "Honda PCX", 2022, 12000));
motorcycles.Add(new Motorcycle("GF62NVC", "Yamaha NMAX", 2014, 8000));
motorcycles.Add(new Motorcycle("BH64NBV", "BMW R 1250 GS Adventure", 2012, 14000));
motorcycles.Add(new Motorcycle("SW65FCP", "Royal Enfield Meteor 350", 2023, 23000));
//looping for choices 
while (true)
    {
    Console.WriteLine("Welcome To Topley's Vehicle Storage!"); 
   
        Console.WriteLine("1) For Cars");
        Console.WriteLine("2) For Motorcycles");
        Console.WriteLine("Q) Quit");

        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine().ToLower();

        if (choice == "q") break;

        if (choice == "1") CarChoice();
        else if (choice == "2") MotorCycleChoice();
        else Console.WriteLine("Unknown option.");
    }

void CarChoice()
{
    while (true)
    {
        Console.WriteLine("1) Display Cars");
        Console.WriteLine("2) Add a car to the list");
        Console.WriteLine("3) Remove a car from the list");
        Console.WriteLine("4) Count how many cars are on the list");
        Console.WriteLine("5) Place the cars in a text file");
        Console.WriteLine("6) Place the cars in a Binary file");
        Console.WriteLine("7) View our budget car options");
        Console.WriteLine("Q) Quit");

        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine().ToLower();

        if (choice == "q") break;

        if (choice == "1") DisplayCars();
        else if (choice == "2") AddCars();
        else if (choice == "3") RemoveCars();
        else if (choice == "4") CountCars();
        else if (choice == "5") WriteToTextFile("cars.txt", cars);
        else if (choice == "6") WriteCarToBinaryFile("cars.bin", cars);
        else if (choice == "7") PriceCars();
        else Console.WriteLine("Unknown option.");
    }

}
void MotorCycleChoice()
{
    while (true)
    {
        Console.WriteLine("1) Display Motorcycles");
        Console.WriteLine("2) Add a motorcycles to the list");
        Console.WriteLine("3) Remove a motorcycles from the list");
        Console.WriteLine("4) Count how many motorcycles are on the list");
        Console.WriteLine("5) Place the motorcycles in a text file");
        Console.WriteLine("6) Place the motorcycles in a Binary file");
        Console.WriteLine("7) View our budget motorcycle options");
        Console.WriteLine("Q) Quit");

        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine().ToLower();

        if (choice == "q") break;

        if (choice == "1") DisplayMotorcycles();
        else if (choice == "2") AddMotorcycles();
        else if (choice == "3") RemoveMotorcycles();
        else if (choice == "4") CountMotorcycles();
        else if (choice == "5") WriteToTextFile("motorcycles.txt", cars);
        else if (choice == "6") WriteCarToBinaryFile("motorcycles.bin", cars);
        else if (choice == "7") PriceMotorcycles();
        else Console.WriteLine("Unknown option.");
    }

}

//functions for vehicles
void DisplayCars()
{
    Parallel.ForEach(cars.OrderBy(car => car.Year), car =>
    {
        Console.WriteLine(car.Registration + ", " + car.Model + ", " + car.Year + ", " + car.Price);
    });
}


void AddCars()
{
    Console.Write("Please enter your car's registration, model, year, and price (separated by commas): ");
    string input = Console.ReadLine();

    string[] carDetails = input.Split(",");

    if (carDetails.Length == 4)
    {
        string registration = carDetails[0].Trim();
        string model = carDetails[1].Trim();

        try
        {
            if (int.TryParse(carDetails[2].Trim(), out int year) &&
                int.TryParse(carDetails[3].Trim(), out int price))
            {
                if (cars.Any(car => car.Registration == registration))
                {
                    Console.WriteLine("A car with that registration is already stored");
                }
                else
                {
                    cars.Add(new Car(registration, model, year, price));
                    Console.WriteLine("Car added successfully");
                }
            }
            else
            {
                Console.WriteLine("Invalid year or price. Please enter valid numeric values!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid year or price format. Please enter valid numeric values!");
        }
    }
    else
    {
        Console.WriteLine("Please enter registration, model, year, and price separated by commas!");
    }
}

void PriceCars()
{
    IEnumerable<Car> cheapCars = cars.Where(car => car.Price < 6000);

    foreach (Car car in cheapCars)
    {
        Console.WriteLine(car.Registration + ", " + car.Model + ", " + car.Year + ", Price: " + car.Price);
    }
}

void PriceMotorcycles()
{
    IEnumerable<Motorcycle> cheapMotorcycles = motorcycles.Where(motorcycle => motorcycle.Price < 6000);

    foreach (Motorcycle motorcycle in cheapMotorcycles)
    {
        Console.WriteLine(motorcycle.Registration + ", " + motorcycle.Model + ", " + motorcycle.Year + ", Price: " + motorcycle.Price);
    }
}


void RemoveCars()
{
    Console.Write("Please enter Registration to remove: ");
    string registrationToRemove = Console.ReadLine();

    Car carToRemove = cars.Find(car => car.Registration == registrationToRemove);

    if (carToRemove != null)
    {
        cars.Remove(carToRemove);
        Console.WriteLine("Your car has been removed successfully");
    }
    else
    {
        Console.WriteLine("Car could not be found!"); 
    }
}

void CountCars()
{
    Console.WriteLine("There are " + cars.Count + " cars on the list");
}

void DisplayMotorcycles()
{
    Parallel.ForEach(motorcycles.OrderBy(motorcycle => motorcycle.Year), motorcycle =>
    {
        Console.WriteLine(motorcycle.Registration + ", " + motorcycle.Model + ", " + motorcycle.Year);
    });
}

void AddMotorcycles()
{
    Console.Write("Please enter your Motorcycles registration, model and year: ");
    string input = Console.ReadLine();


    string[] motorcycleDetails = input.Split(",");

    if (motorcycleDetails.Length == 4)
    {
        string registration = motorcycleDetails[0].Trim();
        string model = motorcycleDetails[1].Trim();
        if (int.TryParse(motorcycleDetails[2].Trim(), out int year) &&
              int.TryParse(motorcycleDetails[3].Trim(), out int price))
        {
            if (motorcycles.Any(motorcycles => motorcycles.Registration == registration))
            {
                Console.WriteLine("A Motorcycle with that registration is already stored");
            }
            else
            {
                motorcycles.Add(new Motorcycle(registration, model, year, price));
                Console.WriteLine("Motorcycle added successfully");
            }
        }
        else
        {
            Console.WriteLine("Invalid year. Please enter a valid year!");
        }
    }
    else
    {
        Console.WriteLine("Please enter registration, model and year seperated by commas!");
    }
}

void RemoveMotorcycles()
{
    Console.Write("Please enter Registration to remove: ");
    string registrationToRemove = Console.ReadLine();

    Motorcycle motorcycleToRemove = motorcycles.Find(motorcycles => motorcycles.Registration == registrationToRemove);

    if (motorcycleToRemove != null)
    {
        motorcycles.Remove(motorcycleToRemove);
        Console.WriteLine("Your vehicle has been removed successfully");
    }
    else
    {
        Console.WriteLine("Vehichle could not be found!");
    }
}

void CountMotorcycles()
{
    Console.WriteLine("There are " + motorcycles.Count + " motorcycles on the list");
}

//file creation 
void WriteToTextFile(string filename, List<Car> cars)
{
    try
    {
        using (StreamWriter sw = File.CreateText(filename))
        {
            foreach (Car car in cars)
            {
                sw.WriteLine(car.Registration + ", " + car.Model + ", " + car.Year);
            }
        }
        Console.WriteLine("Cars written to " + filename + " successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error writing motorcycles to text file: " + ex.Message);
    }
}
void WriteMotorcycleToTextFile(string filename, List<Motorcycle> motorcycles)
{
    try
    {
        using (StreamWriter sw = File.CreateText(filename))
        {
            foreach (Motorcycle motorcycle in motorcycles)
            {
                sw.WriteLine(motorcycle.Registration + ", " + motorcycle.Model + ", " + motorcycle.Year);
            }
        }

        Console.WriteLine("Motorcycles written to " + filename + " successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error writing motorcycles to text file: " + ex.Message);
    }
}
void WriteCarToBinaryFile(string filename, List<Car> cars)
{
    try {
        using (FileStream fileWriter = File.Open(filename, FileMode.Create))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileWriter, cars);
        }
        Console.WriteLine("Successfully saved to a binary file");
    } catch (Exception ex) 
    {
        Console.WriteLine("Error writing cars to binary file: " + ex.Message);
    }
}

void WriteMotorcycleToBinaryFile(string filename, List<Motorcycle> motorcycles)
{
    try
    {
        using (FileStream fileWriter = File.Open(filename, FileMode.Create))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileWriter, motorcycles);
        }
        Console.WriteLine("Successfully saved motorcycles to a binary file");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error writing motorcycles to binary file: " + ex.Message);
    }
}

//classes
[Serializable]
public class Car
{
    private string registration;
    private string model;
    private int year;
    private int price;

    public string Registration
    {
        get { return registration; }
        private set { registration = value; }
    }

    public string Model
    {
        get { return model; }
        private set { model = value; }
    }

    public int Year
    {
        get { return year; }
        private set { year = value; }
    }

    public int Price
    {
        get { return price; }
        private set { price = value; }
    }

    public Car(string registration, string model, int year, int price)
    {
      this.Registration = registration;
      this.Model = model;
      this.Year = year;
      this.Price = price;
    }
}
[Serializable]
public class Motorcycle
{
    private string registration;
    private string model;
    private int year;
    private int price;

    public string Registration
    {
        get { return registration; }
        private set { registration = value; }
    }

    public string Model
    {
        get { return model; }
        private set { model = value; }
    }

    public int Year
    {
        get { return year; }
        private set { year = value; }
    }

    public int Price
    {
        get { return price; }
        private set { price = value; }
    }

    public Motorcycle(string registration, string model, int year, int price)
    {
        this.Registration = registration;
        this.Model = model;
        this.Year = year;
        this.Price = price;
    }
}
