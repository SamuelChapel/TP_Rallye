namespace Rallye.Library.Exceptions;

public class InvalidNumberException : Exception
{
	public override string Message => "Le longueur du numéro du vehicule doit contenir entre 4 et 6 chiffres";
}
