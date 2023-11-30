using Rallye.Library.Exceptions;
using Rallye.Library.Services;
using Rallye.Library.Vehicles;
using Vehicle = Rallye.Library.Vehicles.Vehicle;

namespace Rallye.App;

enum VehicleType
{
	Car,
    Truck
}

enum VehicleProperties
{
	Manufacturer,
	Model,
	Number,
	Power,
    Weight
}

internal static class VehiclesHandler
{
	private static readonly Dictionary<string, Vehicle> _vehicles = [];

	private static readonly string _fileName = "vehicles.json";

	// static IEnumerable<string> VehiculeTypes = vehicules.Values.Select(v => v.GetType().Name).Distinct();

	/// <summary>
	/// Permet d'ajouter un véhicule pour le rallye
	/// </summary>
	/// <typeparam name="T">type de véhicule</typeparam>
	/// <param name="vehicule">véhicule à ajouter</param>
	/// <returns>message contenant la réponse</returns>
	public static string AddVehicle<T>(T vehicle) where T : Vehicle
    {
        if (_vehicles.ContainsKey(vehicle.Number))
            return "Véhicule déjà existant";
        
        _vehicles.Add(vehicle.Number, vehicle);
        return "Vehicule Créé";
    }

    public static string UpdateVehicle<T>(string number, T vehicle) where T : Vehicle
    {
        if (_vehicles.Any(v => v.Key == number))
        {
            _vehicles[number] = vehicle;
            return $"{typeof(T).Name} mis à jour";
        }

        return "Véhicule invalide";
    }

    public static string DeleteVehicle(string number)
    {
        if (_vehicles.Remove(number))
            return "Véhicule supprimé";

		return "Véhicule non présent";
	}

	public static (string message, Vehicle? vehicule) GetVehicleByNumber(string number)
	{
        var vehicule = _vehicles.FirstOrDefault(v => v.Key == number);
        var message = vehicule.Value is not null ? "Véhicule récupérer" : "Véhicule non disponible" ;

		return (message, vehicule.Value);
	}

	public static IEnumerable<Vehicle> GetAllVehicules() 
        => _vehicles.Values;

	public static IEnumerable<Vehicle> GetVehiculesByManufacturer(string manufacturer) 
        => _vehicles.Values.Where(v => v.Manufacturer.Contains(manufacturer, StringComparison.CurrentCultureIgnoreCase));

	public static IEnumerable<Vehicle> GetVehiclesByVehicleType(VehicleType vehicleType) 
        => _vehicles.Values.Where(v => v.GetType().Name == vehicleType.ToString());

	public static IEnumerable<Vehicle> GetVehiculesByModel(string model) 
        => _vehicles.Values.Where(v => v.Model.Contains(model, StringComparison.CurrentCultureIgnoreCase));

    public static IEnumerable<Vehicle> GetCarsByPower(int power) 
        => _vehicles.Values.Where(v => v is Car c && c.Power == power);

    public static IEnumerable<Vehicle> GetTruckByWeight(int weight) 
        => _vehicles.Values.Where(v => v is Truck c && c.Weight == weight);

    public static IEnumerable<Vehicle> OrderBy<T>( VehicleProperties vehicleProperties) where T : Vehicle
    {
		var vehicles = vehicleProperties switch
		{
			VehicleProperties.Power => _vehicles.Values,
			VehicleProperties.Weight => _vehicles.Values.Where(v => v is Truck),
			_ => _vehicles.Values
		};

        return vehicleProperties switch
		{
			VehicleProperties.Manufacturer => vehicles.OrderBy(v => v.Manufacturer),
			VehicleProperties.Model => vehicles.OrderBy(v => v.Model),
			VehicleProperties.Number => vehicles.OrderBy(v => v.Number),
			VehicleProperties.Power => vehicles.Where(v => v is Car).OrderBy(c => ((Car)c).Power),
			VehicleProperties.Weight => _vehicles.Values.Where(v => v is Truck).OrderBy(t => ((Truck)t).Weight),
			_ => throw new InvalidVehicleProperty()
		};
    }

    public static void LoadVehicles()
    {
		var vehiclesInFile = FileSerializer.DeSerializeFromFile<Vehicle>(_fileName);

		if (!vehiclesInFile.Any())
		{
			vehiclesInFile = Enumerable.Range(1000, 1000).Select(x => x % 2 == 0 ? Car.CreateRandomVoiture(x.ToString()) : Truck.CreateRandomTruck(x.ToString())).ToList();
			FileSerializer.SerializeInFile(vehiclesInFile, _fileName);
		}

        foreach (Vehicle vehicle in vehiclesInFile)
        {
            _vehicles.Add(vehicle.Number, vehicle);
        }
    }

	public static void SaveVehicles()
	{
		FileSerializer.SerializeInFile(_vehicles.Values, _fileName);
	}
}
