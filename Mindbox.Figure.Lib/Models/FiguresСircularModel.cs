using Mindbox.Figure.Lib.Interfaces;

namespace Mindbox.Figure.Lib;

// Абстрактый класс дял всех круговых фигур
public abstract class FiguresСircularModel : IFigure
{
    protected int Radius { get; init; }
    public virtual double Area { get; set; }
    
    public virtual double GetArea() =>
        Area;
}

//Класс круга
public class Circle : FiguresСircularModel
{
    public Circle(int radius)
    {
        if (radius <= 0)
            throw new Exception("Радиус круга указан некорректно");
        
        Radius = radius;
    }

    public override double GetArea() =>
        double.Pi * Radius * Radius;
}

//Класс овала
public class Oval : FiguresСircularModel
{
    private int Radius2 { get; }
    
    public Oval(int radius1, int radius2)
    {
        if (radius1 <= 0 || radius2 <= 0)
            throw new Exception("Радиус овала указан некорректно");
        else if (radius1 == radius2)
            throw new Exception("Это круг, а не овал");
            
        Radius = radius1;
        Radius2 = radius2;
    }

    public override double GetArea() =>
        double.Pi * Radius * Radius2;
}