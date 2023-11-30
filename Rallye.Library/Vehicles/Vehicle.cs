using System.Text.Json.Serialization;
using Rallye.Library.Exceptions;
using Rallye.Library.Extensions;

namespace Rallye.Library.Vehicles;

[JsonDerivedType(typeof(Car), typeDiscriminator: "Car")]
[JsonDerivedType(typeof(Truck), typeDiscriminator: "Truck")]
public abstract class Vehicle
{
	private string _manufacturer = null!;
	private string _number = null!;

	public string Manufacturer
	{
		get => _manufacturer;
		set
		{
			if (value.ContainNumbers())
				throw new InvalidManufacturerException();

			_manufacturer = value;
		}
	}

	public string Model { get; set; } = null!;

	public string Number
	{
		get => _number;
		set
		{
			if (!value.IsBetween4To6Digits())
				throw new InvalidNumberException();

			_number = value;
		}
	}

	protected Vehicle()
	{
	}

	protected Vehicle(string marque, string model, string number)
	{
		Manufacturer = marque;
		Model = model;
		Number = number;
	}

	public override string ToString()
	{
		return $"Numéro {Number,-6} - Marque {Manufacturer,-15} - Modèle {Model,-15}";
	}
}
