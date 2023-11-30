namespace Rallye.Library.Exceptions;

public class InvalidManufacturerException : Exception
{
	public override string Message => "La marque ne doit pas contenir de chiffres";
}
