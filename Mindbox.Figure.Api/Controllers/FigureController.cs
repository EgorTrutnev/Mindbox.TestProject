using Microsoft.AspNetCore.Mvc;
using Mindbox.Figure.Lib;


namespace Mindbox.Figure.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FigureController : ControllerBase
{
    /// <summary>
    /// Получение площади квадрата
    /// </summary>
    /// <param name="line1">Первая сторона квадрата</param>
    /// <param name="line2">Вторая сторона квадрата</param>
    /// <param name="line3">Третья сторона квадрата</param>
    /// <returns></returns>
    [HttpGet("GetTriangleArea")]
    public IActionResult GetTriangleArea(int line1, int line2, int line3)
    {
        if (line1 == 0 || line2 == 0 || line3 == 0
            || line1 + line2 <= line3 || line1 + line3 <= line2 || line2 + line3 <= line1)
            return NotFound("Стороны треугольника заданы некорректно");
            
        FiguresWithCornersModel triangle = new Triangle(line1,line2,line3);
        
        double areaTriangle = triangle.GetArea();
        string isRightTriangle = triangle.CheckIsContainsRightAngle()
            ? "прямоугольный" 
            : "не прямоугольный";
        
        return Ok($"Площадь треугольника = {areaTriangle}. Он - {isRightTriangle}");
    }
    
    /// <summary>
    /// Получение площади прямоугольника
    /// </summary>
    /// <param name="line1">Первая сторона квадрата</param>
    /// <param name="line2">Вторая сторона квадрата (не обязательна)</param>
    /// <param name="line3">Третья сторона квадрата (не обязательна)</param>
    /// <param name="line4">Четвёртая сторона квадрата (не обязательна)</param>
    /// <returns></returns>
    [HttpGet("GetRectangleArea")]
    public IActionResult GetRectangleArea(int line1, int? line2, int? line3, int? line4)
    {
        if (line1 == 0)
            return NotFound("У прямоугольника должна быть задана минимум одна сторона (для прямоугольника)");
        
        if (line1 > 0 && line2 > 0 && line3 > 0 && line4 == null)
            return NotFound("У прямоугольника не может быть задано три его стороны. Укажите значение одной, двух или четырёх его сторон");

        if ((line3 == null && line4 == null && ((line1 > 0 && line2 == null) || (line1 > 0 && line2 > 0 && line1 == line2)))
            || ((line1 == line2) && (line2 == line3) && (line3 == line4)))
        {
            //Это квадрат
            FiguresWithCornersModel square = new Square(line1);
            double areaSquare = square.GetArea();
            return Ok($"Площадь квадрата = {areaSquare}");
        }
            
        FiguresWithCornersModel rectangle;
            
        if (line2 > 0 && line3 == null && line4 == null)
            rectangle = new Rectangle(line1, (int)line2);
        else
            rectangle = new Rectangle(line1, (int)line2, (int)line3, (int)line4);
        
        double areaRectangle = rectangle.GetArea();
        return Ok($"Площадь прямоугольника = {areaRectangle}");
    }

    /// <summary>
    /// Получение площади круга
    /// </summary>
    /// <param name="radius">Радиус круга</param>
    /// <returns></returns>
    [HttpGet("GetCircleArea")]
    public IActionResult GetCircleArea(int radius)
    {
        FiguresСircularModel circle = new Circle(radius);
        return Ok($"Площадь круга: {circle.GetArea()}");
    }

    /// <summary>
    /// Получение площади овала
    /// </summary>
    /// <param name="radius1">Первый радиус овала</param>
    /// <param name="radius2">Второй радиус овала</param>
    /// <returns></returns>
    [HttpGet("GetOvalArea")]
    public IActionResult GetOvalArea(int radius1, int radius2)
    {
        FiguresСircularModel oval = new Oval(radius1, radius2);
        return Ok($"Площадь овала: {oval.GetArea()}");
    }
}