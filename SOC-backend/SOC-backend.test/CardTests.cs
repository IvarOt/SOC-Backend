using SOC_backend.logic.ExceptionHandling.Exceptions;
using SOC_backend.logic.Models.DomainModel;
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
			CardModel card = new CardModel(0, "b", 10, 10);
		}
	}
}