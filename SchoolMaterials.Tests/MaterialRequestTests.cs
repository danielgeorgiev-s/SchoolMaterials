using NUnit.Framework;
using SchoolMaterials.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SchoolMaterials.Tests
{
    public class MaterialRequestTests
    {
        [Test]
        public void MaterialRequest_WithoutDescription_ShouldBeInvalid()
        {
            // Arrange - Подготвяме заявка без задължителното описание
            var request = new MaterialRequest
            {
                Quantity = 5,
                Date = System.DateTime.Now
                // Умишлено не задаваме Description
            };

            var context = new ValidationContext(request, null, null);
            var results = new List<ValidationResult>();

            // Act - Опитваме се да валидираме обекта, както го прави системата
            var isModelStateValid = Validator.TryValidateObject(request, context, results, true);

            // Assert - Използваме новия синтаксис на NUnit 4+
            Assert.That(isModelStateValid, Is.False, "Моделът трябва да е невалиден, когато липсва описание.");
        }
    }
}