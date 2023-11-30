using Bogus;

namespace Rallye.Library.Vehicles;

public class Car : Vehicle
{
	public Car()
	{
	}

	public Car(string manufacturer, string model, string number, int power)
		: base(manufacturer, model, number) 
		=> Power = power;

	public int Power { get; set; }

	public override string ToString()
	{
		return "Voiture : " + base.ToString() + $"- Puissance {Power}";
	}

	public static Car CreateRandomVoiture(string numero)
	{
		return new Faker<Car>()
			.RuleFor(c => c.Manufacturer, f => f.Vehicle.Manufacturer())
			.RuleFor(c => c.Model, f => f.Vehicle.Model())
			.RuleFor(c => c.Number, numero)
			.RuleFor(c => c.Power, f => f.Random.Number(1000, 5000))
			.Generate();
	}
}
