using Xunit;
using Mindbox.Figure.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Mindbox.Figure.Lib;

namespace Mindbox.Figure.UnitTests;

public class ApiTests
{
    #region Точная проверка Треугольников на значения
    /// <summary>
    /// Проверка треугольника с прямым углом
    /// </summary>
    [Fact]
    public void GetTriangleAreaWhitRightAngle()
    {
        FigureController controller = new FigureController();

        ActionResult? actionResult = controller.GetTriangleArea(3, 4, 5) as ActionResult;

        var okObjectResult = actionResult as OkObjectResult;
        Assert.NotNull(okObjectResult);

        string? resultStr = okObjectResult.Value as string;
        Assert.Equal("Площадь треугольника = 6. Он - прямоугольный", resultStr);
    }

    /// <summary>
    /// Проверка треугольника без прямого угла
    /// </summary>
    [Fact]
    public void GetTriangleAreaWhitNoRightAngle()
    {
        FigureController controller = new FigureController();

        ActionResult? actionResult = controller.GetTriangleArea(1, 1, 1) as ActionResult;

        var okObjectResult = actionResult as OkObjectResult;
        Assert.NotNull(okObjectResult);

        string? resultStr = okObjectResult.Value as string;
        Assert.Equal("Площадь треугольника = 0,4330127018922193. Он - не прямоугольный", resultStr);
    }

    /// <summary>
    /// Проверка треугольника c некорректными сторонами
    /// </summary>
    [Fact]
    public void GetTriangleAreaWithIncorrectLines()
    {
        FigureController controller = new FigureController();

        ActionResult? actionResult = controller.GetTriangleArea(1, 2, 3) as ActionResult;

        var notFoundObjectResult = actionResult as NotFoundObjectResult;
        Assert.NotNull(notFoundObjectResult);

        string? resultStr = notFoundObjectResult.Value as string;
        Assert.Equal("Стороны треугольника заданы некорректно", resultStr);
    }

    #endregion

    #region Общая проверка нескольких Треугольников

    /// <summary>
    /// Проверка корректных треугольников
    /// </summary>
    [Fact]
    public void GetSomeTriangleArea()
    {
        var triangles = new List<Triangle>()
        {
            new Triangle(5, 6, 7),
            new Triangle(15, 12, 11),
            new Triangle(27, 14, 18)
        };

        FigureController controller = new FigureController();

        foreach (var triangle in triangles)
        {
            ActionResult? actionResult = controller.GetTriangleArea(triangle.Line1Width, triangle.Line2Width, triangle.Line3Width) as ActionResult;
            
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            string? resultStr = okObjectResult.Value as string;
            Assert.Equal(resultStr?.Contains("Он - не прямоугольный"), true);
        }
    }
    #endregion
}