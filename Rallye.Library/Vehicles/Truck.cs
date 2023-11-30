using Bogus;

namespace Rallye.Library.Vehicles;

public class Truck : Vehicle
{
	public int Weight { get; set; }
	public Truck()
	{
	}

	public Truck(string manufacturer, string model, string number, int weight)
		: base(manufacturer, model, number) 
		=> Weight = weight;

	public override string ToString()
	{
		return "Camion  : " + base.ToString() + $"Puissance : {Weight}";
	}

	public static Vehicle CreateRandomTruck(string number)
	{
		return new Faker<Truck>()
			.RuleFor(c => c.Manufacturer, f => f.Vehicle.Manufacturer())
			.RuleFor(c => c.Model, f => f.Vehicle.Model())
			.RuleFor(c => c.Number, number)
			.RuleFor(c => c.Weight, f => f.Random.Number(1000, 5000))
			.Generate();
	}
}
