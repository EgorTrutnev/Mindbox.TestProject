using Mindbox.Figure.Lib.Interfaces;

// Для соблюдения принципов SOLID создаётся базовый абстрактный класс всех фигур с углами,
// который реализует интерфес IFigureWithCorners
public abstract class FiguresWithCornersModel : IFigureWithCorners
{
    //Переменные для хранения площади и 
    public virtual double Area { get; set; }
    public bool? IsContainsRightAngle { get; set; }

    // Помимо площади, у фигур с углами должна быть задана как минимум одна длина отрезка
    protected int Line1Width { get; init; }

    // Метод получения площади фигуры
    public virtual double GetArea() =>
        Area;

    // Метод проверки на наличие углов с 90 градусами
    public virtual bool CheckIsContainsRightAngle() =>
        IsContainsRightAngle ?? false;
}

// Базовый класс для фигур с двумя разными(возможно) отрезками
public abstract class FiguresWith2CornersModel : FiguresWithCornersModel
{
    protected int Line2Width { get; init; }
}

// Базовый класс для фигур с тремя разными(возможно) отрезками
public abstract class FiguresWith3CornersModel : FiguresWith2CornersModel
{
    protected int Line3Width { get; init; }
}

// Класс треуголников
public class Triangle : FiguresWith3CornersModel
{
    // Конструктор треуголников
    public Triangle (int line1Width, int line2Width, int line3Width)
    {
        // Обязательная проверка на ввод значений
        if (line1Width <= 0 || line2Width <= 0 || line3Width <= 0
            || line1Width + line2Width <= line3Width || line1Width + line3Width <= line2Width || line2Width + line3Width <= line1Width)
            throw new Exception("Стороны треугольника заданы некорректно");
        
        Line1Width = line1Width;
        Line2Width = line2Width;
        Line3Width = line3Width;
    }

    // Получение площади треуголника через все 3 длины отрезков
    public override double GetArea()
    {
        double semiperimeter = (Line1Width + Line2Width + Line3Width) / 2.0;
        return Area = Math.Pow(semiperimeter * (semiperimeter - Line1Width) * (semiperimeter - Line2Width) * (semiperimeter - Line3Width), 0.5);
    }

    // Проверка треугольника на наличие прямого угла 
    public override bool CheckIsContainsRightAngle()
    {
        if (Line3Width > Line1Width && Line3Width > Line2Width)
            return (bool)(IsContainsRightAngle =
                Line1Width * Line1Width + Line2Width * Line2Width == Line3Width * Line3Width);
        if (Line1Width > Line2Width && Line1Width > Line3Width)
            return (bool)(IsContainsRightAngle =
                Line3Width * Line3Width + Line2Width * Line2Width == Line1Width * Line1Width);
        if (Line2Width > Line1Width && Line2Width > Line3Width)
            return (bool)(IsContainsRightAngle =
                Line3Width * Line3Width + Line1Width * Line1Width == Line2Width * Line2Width);
        
        throw new Exception();
    }
}

// Класс прямоуголников 
public class Rectangle : FiguresWith2CornersModel
{
    // Конструктор для прямоуголника в случаях, когда в него отправляются все 4 стороны, но у двух пар значения одинаковы
    public Rectangle(int line1Width, int line2Width, int line3Width, int line4Width)
    {
        // Обязательная проверка на ввод значений
        if (line1Width <= 0 || line2Width <= 0 || line3Width <= 0 || line4Width <= 0)
            throw new Exception("Стороны прямоуголника заданы некорректно");
        
        if ((line1Width.Equals(line2Width) && line3Width.Equals(line4Width))
            || (line1Width.Equals(line4Width) && line2Width.Equals(line3Width)))
        {
            Line1Width = line1Width;
            Line2Width = line3Width;
        }
        else if (line1Width.Equals(line3Width) && line2Width.Equals(line4Width))
        {
            Line1Width = line1Width;
            Line2Width = line4Width;
        }
        else
            throw new Exception("Данная фигура не является прямоуголником");
    }

    // Конструктор прямоуголника
    public Rectangle (int line1Width, int line2Width)
    {
        // Обязательная проверка на ввод значений
        if (line1Width <= 0 || line2Width <= 0)
            throw new Exception("Стороны прямоуголника заданы некорректно");
        
        Line1Width = line1Width;
        Line2Width = line2Width;
    }

    // Получение площади прямоуголника
    public override double GetArea() =>
        Area = Line1Width * Line2Width;

    //У прямоуголников углы всегда 90 градусов
    public override bool CheckIsContainsRightAngle() =>
        true;
}

// Класс квадратов
// Хоть и является частным случаем прямоуголника, но наследовать его от класса прямоуголника нельзя,
// т.к. это может привести к нарушению принципа подстановки Лисков
// Поэтому квадрат будет наследоваться от FiguresWithCornersModel
public class Square : FiguresWithCornersModel
{
    // Дополнительные варианты реализации конструкторов, которые могут быть, но являются избыточными для данной задачи
    /*public Square(int line1Width, int line2Width, int line3Width, int line4Width)
    {
        if ((line1Width == line2Width) && (line2Width == line3Width) && (line3Width == line4Width))
            Line1Width = line1Width;
        else
            throw new Exception("Данная фигура не является квадратом");
    }
    
    public Square(int line1Width, int line2Width)
    {
        if (line1Width == line2Width)
            Line1Width = line1Width;
        else
            throw new Exception("Данная фигура не является квадратом");
    }*/
    
    public Square(int line1Width) =>
            Line1Width = line1Width;
    
    // Получение площади прямоуголников
    public override double GetArea() =>
        Area = Line1Width * Line1Width;
    
    public override bool CheckIsContainsRightAngle() =>
        true;
}