using System;

abstract class Shape
{
    #region Field data
    protected string name;
    protected string color = "Black";
    private static int count = 0;
    const string defaultName = "defaultName";
    #endregion
    
    #region Properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Color
    {
        get { return color; }
        set { color = value; }
    }
    #endregion
    
    #region Constructors
    public Shape()
    {
        Name = defaultName;
        Console.WriteLine("Shape called {0} created", name);
        count++;
    }
    public Shape(string name)
    {
        Name = name;
        Console.WriteLine("Shape called {0} created", name);
        count++;
    }
    #endregion
    
    #region Members
    public abstract double calculateArea();
    public override string ToString()
    {
        return System.String.Format("{0} This is a shape object called {1}", count, name);
    }
    #endregion
}

class Circle : Shape {
    #region Field data
    protected double radius = 0;
    new protected string color = "Blue";
    const double defaultRadius = 1;
    const double pi = 3.14;
    #endregion
    
    #region Properties
    public double Radius
    {
        get { return radius; }
        set { radius = value; }
    }
    new public string Color
    {
        get { return color; }
        set { color = value; }
    }
    #endregion
    
    #region Constructors
    public Circle()
        : base()
    {
        Radius = defaultRadius;
        Console.WriteLine("Circle called {0} whose radius is {1} created", name, radius);
    }
    public Circle(string name)
        : base(name)
    {
        Radius = defaultRadius;
        Console.WriteLine("Circle called {0} whose radius is {1} created", name, radius);
    }
    public Circle(string name, int radius)
        : base(name)
    {
        Radius = radius;
        Console.WriteLine("Circle called {0} whose radius is {1} created", name, radius);
    }
    #endregion
    
    #region Members
    public override double calculateArea()
    {
        return Radius * Radius * pi;
    }
    public override string ToString()
    {
        return System.String.Format("This is a Circle object called {0} whose radius is {1} and area is {2} and of color {3}", name, radius, calculateArea(), color);
    }
    #endregion
}

class Rectangle : Shape
{
    #region Field data
    protected double length = 0;
    protected double width = 0;
    new protected string color = "Red";
    const double defaultLength = 1;
    const double defaultWidth = 1;
    #endregion
    
    #region Properties
    public double Length
    {
        get { return length; }
        set { length = value; }
    }
    public double Width
    {
        get { return width; }
        set { width = value; }        
    }
    new public string Color
    {
        get { return color; }
        set { color = value; }
    }
    #endregion
    
    #region Constructors
    public Rectangle()
        : base()
    {
        Length = defaultLength;
        Width = defaultWidth;
        Console.WriteLine("Rectangle called {0} whose length is {1} and width is {2} created", name, length, width);
    }
    public Rectangle(string name)
        : base(name)
    {
        Length = defaultLength;
        Width = defaultWidth;
        Console.WriteLine("Rectangle called {0} whose length is {1} and width is {2} created", name, length, width);
    }
    public Rectangle(string name, double length, double width)
        : base(name)
    {
        Length = length;
        Width = width;
        Console.WriteLine("Rectangle called {0} whose length is {1} and width is {2} created", name, length, width);
    }
    #endregion
    
    #region Members
    public override double calculateArea()
    {
        return length * width;
    }
    public override string ToString()
    {
        return System.String.Format("This is a Rectangle object called {0} whose length is {1} and width is {2} and area is {3} and of color {4}", name, length, width, calculateArea(), color);
    }
    #endregion
}

class Square : Rectangle
{
    #region Field data
    new protected string color = "White";
    #endregion
    
    #region Properties
    new public string Color
    {
        get { return color; }
        set { color = value; }
    }
    #endregion
    
    #region Constructors
    public Square()
        : base()
    {
        Console.WriteLine("Square called {0} whose length is {1} and width is {2} created", name, length, width);
    }
    public Square(string name)
        : base(name)
    {
        Console.WriteLine("Square called {0} whose length is {1} and width is {2} created", name, length, width);
    }
    public Square(string name, double width)
        : base(name)
    {
        Length = Width = width;
        Console.WriteLine("Square called {0} whose length is {1} and width is {2} created", name, length, width);
    }
    #endregion
    
    #region Members
    public override string ToString()
    {
        return System.String.Format("This is a Square object called {0} whose length is {1} and width is {2} and area is {3} and of color {4}", name, length, width, calculateArea(), color);
    }
    #endregion
}

class Program
{
    static void Main()
    {
        Circle circleA = new Circle();
        Circle circleB = new Circle("Circle-B");
        Circle circleC = new Circle("Circle-C", 5);
        Rectangle rectangleA = new Rectangle();
        Rectangle rectangleB = new Rectangle("Rectangle-B");
        Rectangle rectangleC = new Rectangle("Rectangle-C", 5, 8);
        Square squareA = new Square();
        Square squareB = new Square("Square-B");
        Square squareC = new Square("Square-C", 5);
        
        Console.WriteLine();
        Console.WriteLine(circleA);
        Console.WriteLine(circleB);
        Console.WriteLine(circleC);
        Console.WriteLine(rectangleA);
        Console.WriteLine(rectangleB);
        Console.WriteLine(rectangleC);
        Console.WriteLine(squareA);
        Console.WriteLine(squareB);
        Console.WriteLine(squareC);
        
        Console.WriteLine();
        Console.WriteLine(circleA.Name + "\t" + circleA.Radius + " has area: " + circleA.calculateArea() + "\t" + circleA.Color);
        Console.WriteLine(circleB.Name + "\t" + circleB.Radius + " has area: " + circleB.calculateArea() + "\t" + circleB.Color);
        Console.WriteLine(circleC.Name + "\t" + circleC.Radius + " has area: " + circleC.calculateArea() + "\t" + circleC.Color);
        Console.WriteLine(rectangleA.Name + "\t" + rectangleA.Width + "\t" + rectangleA.Length + " has area: " + rectangleA.calculateArea() + "\t" + rectangleB.Color);
        Console.WriteLine(rectangleB.Name + "\t" + rectangleB.Width + "\t" + rectangleB.Length + " has area: " + rectangleB.calculateArea() + "\t" + rectangleB.Color);
        Console.WriteLine(rectangleC.Name + "\t" + rectangleC.Width + "\t" + rectangleC.Length + " has area: " + rectangleC.calculateArea() + "\t" + rectangleC.Color);
        Console.WriteLine(squareA.Name + "\t" + squareA.Width + " has area: " + squareA.calculateArea() + "\t" + squareA.Color);
        Console.WriteLine(squareB.Name + "\t" + squareB.Width + " has area: " + squareB.calculateArea() + "\t" + squareB.Color);
        Console.WriteLine(squareC.Name + "\t" + squareC.Width + " has area: " + squareC.calculateArea() + "\t" + squareC.Color);
        Console.Read();
    }
}
