using static System.Console;

namespace Rallye.App.Helpers;

public static class ConsoleHelper
{
	public static string GetStringFromConsole(string label = "Entrez la valeur")
	{
		WriteLine(label);
		var input = ReadLine();

		while (string.IsNullOrWhiteSpace(input))
		{
			WriteLine("Entrée incorrecte, veuilllez réessayer");
			input = ReadLine();
		}

		return input;
	}

	public static int GetIntFromConsole(string label = "Entrez la valeur")
	{
		int inputNumber;
		var input = GetStringFromConsole(label);

		while (!int.TryParse(input, out inputNumber))
		{
			WriteLine("Ce n'est pas un nombre, veuilllez réessayer");
			input = ReadLine();
		}

		return inputNumber;
	}

	public static void ExitConsoleApp()
	{
		WriteLine("----- Programe terminé, appuyez sur Entrée pour quitter -----");
		ReadLine();
	}

	public static int DisplayMenu(string title, string[] options, bool clear = true)
	{
		if(clear) Clear();

		WriteLine("----- " + title + " -----\n");
		for (int i = 0; i < options.Length; i++)
		{
			WriteLine($"  {i + 1} : {options[i]}");
		}

		WriteLine("  0 : Quitter");

		int choice;
		do
		{
			var input = ReadLine();
			if (!int.TryParse(input, out choice))
				choice = -1;
		} while (choice == -1 || choice < 0 || choice > options.Length);

		return choice;
	}
}
