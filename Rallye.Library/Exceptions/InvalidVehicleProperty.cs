using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rallye.Library.Exceptions;
public class InvalidVehicleProperty : Exception
{
	public override string Message => "Propriéte du véhicule invalide";
}
