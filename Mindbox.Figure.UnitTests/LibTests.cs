using Xunit;
using Mindbox.Figure.Lib;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;

namespace Mindbox.Figure.UnitTests;

public class LibTests
{

    /// <summary>
    /// Точечная проверка создания круга
    /// </summary>
    [Fact]
    public void CreateCircleModel()
    {
        var circleFigure = new Circle(5);
        Assert.NotNull(circleFigure);
        
        var figureRadius = circleFigure.GetArea();

        Assert.Equal(figureRadius.GetType(), typeof(double));
        Assert.Equal(78.53981633974483, figureRadius);
    }

    /// <summary>
    /// Создание моделей, не являющихся овалами
    /// </summary>
    [Fact]
    public void CreateNoOvalsModel()
    {
        var ovalsRadius = new List<int[]>()
        {
            new int[2] { 8, 8 },
            new int[2] { -25, 5 },
        };

        foreach (var ovalRadius in ovalsRadius)
        {
            Action tryCreateOval = () => new Oval(ovalRadius[0], ovalRadius[1]);
            
            Assert.Throws<Exception>(tryCreateOval);
        }
    }
}