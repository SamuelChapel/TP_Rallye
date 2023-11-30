using Rallye.Library.Exceptions;
using Rallye.Library.Vehicles;

namespace Rallye.TestsUnitaires;

public class VehicleTests
{
	[Theory]
	[InlineData("Manufacturer", "Model", "1000", 1000)]
	[InlineData("TestManu", "TestModel", "999999", 1)]
	public void CreateCar_WithValidParameters_ShouldReturnCar(string manufacturer, string model, string number, int power)
	{
		// Act
		var c = new Car(manufacturer, model, number, power);

		// Assert
		Assert.True(c is Car);
		Assert.Equal(manufacturer, c.Manufacturer);
		Assert.Equal(model, c.Model);
		Assert.Equal(number, c.Number);
		Assert.Equal(power, c.Power);
	}

	[Fact]
	public void CreateCar_WithManufacturerWithNumber_ShoudlReturnInvalidManufacturerException()
	{
		// Arrange

		Assert.Throws<InvalidManufacturerException>(() => new Car("Manu2", "Model", "1000", 1000));
	}

	[Fact]
	public void CreateCar_WithWrongNumber_ShoudlReturnInvalidNumberException()
	{
		// Arrange

		Assert.Throws<InvalidNumberException>(() => new Car("Manufacturer", "Model", "0", 1000));
	}
}