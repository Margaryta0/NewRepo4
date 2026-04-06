using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class Vector
    {
        public double X { get; private set; }  
        public double Y { get; private set; }  

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Довжина вектора (полярна координата r)
        // r = sqrt(x² + y²)
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        // Кут з віссю OX у градусах (полярна координата θ)
        // atan2(y, x) дає кут від осі OX
        public double AngleOX()
        {
            return Math.Atan2(Y, X) * (180.0 / Math.PI);
        }

        // КУТ З ВІССЮ OY — це і є наш КЛЮЧ для хешування
        // Кут з OY = 90° - кут з OX
        // Або через atan2(x, y)
        // Повертаємо в градусах, округлюємо до цілого для хешування
        public double AngleOY()
        {
            return Math.Atan2(X, Y) * (180.0 / Math.PI);
        }

        // Ключ для хеш-таблиці — кут з OY, округлений до цілого
        // Беремо абсолютне значення бо хеш має бути >= 0
        public int Key()
        {
            return Math.Abs((int)Math.Round(AngleOY()));
        }

        // Виведення об'єкта — координати та полярні координати
        public override string ToString()
        {
            return $"Vector({X,6:F2}, {Y,6:F2}) | " +
                   $"r={Length(),6:F2} | " +
                   $"кут OX={AngleOX(),7:F2}° | " +
                   $"кут OY={AngleOY(),7:F2}° | " +
                   $"ключ={Key()}";
        }

        // Перевірка правильності вектора: не нульовий
        public bool IsValid()
        {
            return Length() > 0.0001; // не нульовий вектор
        }
    }
}
