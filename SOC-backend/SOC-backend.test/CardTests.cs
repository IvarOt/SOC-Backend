using SOC_backend.logic.ExceptionHandling.Exceptions;
using SOC_backend.logic.Models.Card;
using SOC_backend.logic.Services;

namespace SOC_backend.test
{
    [TestClass]
	public class CardTests
	{
		[TestMethod]
		[ExpectedException(typeof(PropertyException))]
		public void TestCardNameValidation()
		{
			Card card = new Card(0, "b", 10, 10, "rgb(255, 255, 255)");
		}
	}
}