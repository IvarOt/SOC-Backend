using SOC_backend.logic.Exceptions;
using SOC_backend.logic.Models.Card;
using SOC_backend.logic.Services;
using System.ComponentModel.DataAnnotations;

namespace SOC_backend.test
{
    [TestClass]
	public class CardTests
	{
		[TestMethod]
		[ExpectedException(typeof(ValidationException))]
		public void TestCardNameValidation()
		{
			CardRequest card = new CardRequest();
			card.Name = "1";
			var context = new ValidationContext(card);
			Validator.ValidateObject(card, context, validateAllProperties: true);
		}
	}
}