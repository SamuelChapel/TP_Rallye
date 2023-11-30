using Rallye.App;
using Rallye.Library.Exceptions;
using Rallye.Library.Services;
using Rallye.Library.Vehicles;
using static System.Console;
using static Rallye.App.Helpers.ConsoleHelper;

//string fileName = "vehicles.json";

//var vehicles = FileSerializer.DeSerializeFromFile<Vehicle>(fileName);

//if (!vehicles.Any())
//{
//	var vehicules = Enumerable.Range(1000, 1000).Select(x => x % 2 == 0 ? Car.CreateRandomVoiture(x.ToString()) : Truck.CreateRandomTruck(x.ToString())).ToList();
//	FileSerializer.SerializeInFile(vehicules, fileName);
//}

VehiclesHandler.LoadVehicles();

int choice;

do
{
	choice = DisplayMenu("Gestion des Vehicules",
		[
			"Enregistrer un véhicule",
			"Mettre à jour un véhicule",
			"Enlever un véhicule",
			"Lire un véhicule",
			"Lire tout les véhicules",
			"Filtrer les véhicules",
			"Trier les véhicules"
			]);

	switch (choice)
	{
		case 1:
			SaveVehicule();
			break;
		case 2:
			UpdateVehicule();
			break;
		case 3:
			RemoveVehicle();
			break;
		case 4:
			DisplayVehicle();
			break;
		case 5:
			DisplayAllVehicule();
			break;
		case 6:
			FilterVehicles();
			break;
		case 7:
			SortVehicles();
			break;
		case 0:
			break;
		default:
			WriteLine("Choix non valide!");
			ReadKey();
			break;
	}
} while (choice != 0);

VehiclesHandler.SaveVehicles();
WriteLine("Merci d'avoir utiliser le gestionnaire de véhicules!");
ExitConsoleApp();

static void SaveVehicule()
{
	Clear();

	var input = DisplayMenu("Creer vehicule", ["Voiture", "Camion"]);

	try
	{
		switch (input)
		{
			case 1:
				CreateCar();
				break;
			case 2:
				CreateTruck();
				break;
			default:
				throw new Exception("Véhicule non Valide");
		}
	}
	catch (Exception ex)
	{
		WriteLine(ex.Message);
	}
}

static void CreateTruck()
{
	WriteLine("----- Créer Camion -----");

	var manufacturer = GetStringFromConsole("Entrez la marque : ");
	var model = GetStringFromConsole("\nEntrez le modèle : ");
	var number = GetStringFromConsole("\nEntrez le numéro : ");
	var weight = GetIntFromConsole("\nEntrez le poids : ");

	var truck = new Truck(manufacturer, model, number, weight);

	string message = VehiclesHandler.AddVehicle(truck);
	WriteLine(message);

	ReadKey();
}

static void CreateCar()
{
	WriteLine("----- Créer Voiture -----");

	var manufacturer = GetStringFromConsole("Entrez la marque : ");
	var model = GetStringFromConsole("\nEntrez le modèle : ");
	var number = GetStringFromConsole("\nEntrez le numéro : ");
	var power = GetIntFromConsole("\nEntrez la puissance : ");

	var car = new Car(manufacturer, model, number, power);

	string message = VehiclesHandler.AddVehicle(car);
	WriteLine(message);

	ReadKey();
}

static void UpdateVehicule()
{
	Clear();

	WriteLine("Mise à jour d'un véhicule");

	var numero = GetStringFromConsole("Entrez le numéro : ");
	var (message, vehicle) = VehiclesHandler.GetVehicleByNumber(numero);

	WriteLine(message);
	if (vehicle == null)
		return;

	WriteLine(vehicle);

	string property = vehicle is Car ? "Puissance" : "Poids";

	int choice = DisplayMenu("Quel caractéristique voulez vous mettre à jour ?",
		[
			"Numéro",
			"Marque",
			"Modèle",
			property
		], false);

	string input;

	switch (choice)
	{
		case 1:
			input = GetStringFromConsole("Entrez la nouvelle valeur pour le numero :");
			vehicle.Number = input;
			break;
		case 2:
			input = GetStringFromConsole("Entrez la nouvelle valeur pour la marque :");
			vehicle.Manufacturer = input;
			break;
		case 3:
			input = GetStringFromConsole("Entrez la nouvelle valeur pour le modèle :");
			vehicle.Model = input;
			break;
		case 4:
			if (vehicle is Car c)
			{
				c.Power = GetIntFromConsole("Entrez la nouvelle valeur pour la puissance :");
			}
			else if (vehicle is Truck t)
			{
				t.Weight = GetIntFromConsole("Entrez la nouvelle valeur pour le poids :");
			}

			break;
		default:
			return;
	}

	message = VehiclesHandler.UpdateVehicle(numero, vehicle);
	WriteLine(message);

	ReadKey();
}

static void RemoveVehicle()
{
	Clear();

	var numero = GetStringFromConsole("\nEntrez le numéro du véhicule : ");

	var result = VehiclesHandler.DeleteVehicle(numero);

	WriteLine(result);

	ReadKey();
}

static void DisplayVehicle()
{
	Clear();

	var numero = GetStringFromConsole("\nEntrez le numéro du véhicule : ");

	var (message, vehicule) = VehiclesHandler.GetVehicleByNumber(numero);

	WriteLine(message);
	if (vehicule != null)
		WriteLine(vehicule);

	ReadKey();
}

static void DisplayAllVehicule()
{
	Clear();

	WriteLine("Afficher tous les véhicules");

	var vehicules = VehiclesHandler.GetAllVehicules().ToList();

	vehicules.ForEach(WriteLine);

	ReadKey();
}

static void FilterVehicles()
{
	Clear();

	int choice = DisplayMenu("Filtrer les Vehicules par",
		[
			"Marque",
			"Modele",
			"Puissance",
			"Poids"
		]);

	IEnumerable<Vehicle> vehicles = new List<Vehicle>();

	switch (choice)
	{
		case 1:
			var input = GetStringFromConsole();
			vehicles = VehiclesHandler.GetVehiculesByManufacturer(input);
			break;
		case 2:
			var input2 = GetStringFromConsole();
			vehicles = VehiclesHandler.GetVehiculesByModel(input2);
			break;
		case 3:
			var integer = GetIntFromConsole();
			vehicles = VehiclesHandler.GetCarsByPower(integer);
			break;
		case 4:
			var integer2 = GetIntFromConsole();
			vehicles = VehiclesHandler.GetTruckByWeight(integer2);
			break;
		default:
			break;
	}

	vehicles.ToList().ForEach(WriteLine);

	ReadKey();
}

static void SortVehicles()
{
	Clear();

	int choice = DisplayMenu("Trier les Vehicules par",
		[
			"Numero",
			"Marque",
			"Modele",
			"Puissance",
			"Poids"
		]);

	try
	{
		var vehicles = choice switch
		{
			1 => VehiclesHandler.OrderBy<Vehicle>(VehicleProperties.Number),
			2 => VehiclesHandler.OrderBy<Vehicle>(VehicleProperties.Manufacturer),
			3 => VehiclesHandler.OrderBy<Vehicle>(VehicleProperties.Model),
			4 => VehiclesHandler.OrderBy<Vehicle>(VehicleProperties.Power),
			5 => VehiclesHandler.OrderBy<Vehicle>(VehicleProperties.Weight)
		};

		vehicles.ToList().ForEach(WriteLine);
	}
	catch (Exception e)
	{
		WriteLine(e.Message);
	}

	ReadKey();
}