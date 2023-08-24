namespace Mindbox.Figure.Lib.Interfaces;

interface IFigure
{
    double Area { get; set; }
    double GetArea();
}

interface IFigureWithCorners : IFigure
{
    bool? IsContainsRightAngle { get; set; }
    bool CheckIsContainsRightAngle();
}