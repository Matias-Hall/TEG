using System;

namespace TEG
{
    public struct TEGColor
    {
        public int Id { get; }
        public TEGColor(int color)
        {
            Id = color;
        }
        public static TEGColor Black { get => new TEGColor(0); }
        public static TEGColor Red { get => new TEGColor(1); }
        public static TEGColor Yellow { get => new TEGColor(2); }
        public static TEGColor Green { get => new TEGColor(3); }
        public static TEGColor Blue { get => new TEGColor(4); }
        public static TEGColor Orange { get => new TEGColor(5); }

        public static implicit operator string(TEGColor c)
        {
            return c.Id switch
            {
                0 => "Black",
                1 => "Red",
                2 => "Yellow",
                3 => "Green",
                4 => "Blue",
                5 => "Orange",
                _ => "Error at conversion",
            };
        }
        public static TEGColor FromString(string name)
        {
            return name switch
            {
                "Black" => Black,
                "Red" => Red,
                "Yellow" => Yellow,
                "Green" => Green,
                "Blue" => Blue,
                "Orange" => Orange,
                _ => throw new ArgumentOutOfRangeException("Not a valid color name."),
            };
        }
        public override string ToString()
        {
            return this;
        }
        public static explicit operator TEGColor(int i)
        {
            return new TEGColor(i);
        }
    }
}
