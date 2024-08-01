using System;

namespace Converter {
    class MainMenu {
        static string[] temperatureUnits = { "°C", "°F", "K", "°R" };
        static string[] distanceUnits = { "m", "km", "mi", "ft", "yd", "in" };
        static string[] timeUnits = { "s", "min", "h", "d", "wk", "mo", "yr" };
        static string[] pressureUnits = { "Pa", "hPa", "bar", "psi", "atm", "Torr" };

        static void Main(string[] args) {
            Converter();
        }

        static void Converter() {
            Console.WriteLine("What would you like to convert:\n1.Distance\n2.Time\n3.Temperature\n4.Pressure\nEnter the number of your choice below:");
            if (!int.TryParse(Console.ReadLine(), out int choice)) {
                invalidInput();
                Converter();
                return;
            }

            switch (choice) {
                case 1:
                    ConvertDistance();
                    break;
                case 2:
                    ConvertTime();
                    break;
                case 3:
                    ConvertTemperature();
                    break;
                case 4:
                    ConvertPressure();
                    break;
                default:
                    invalidInput();
                    Converter();
                    break;
            }
        }

        static void ConvertDistance() {
            Console.WriteLine("Select the unit you would like to convert from and to (Input the number of the respective unit in the list)");
            Console.WriteLine("From:");
            for (int i = 0; i < distanceUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {distanceUnits[i]}");
            }
            Console.WriteLine($"{distanceUnits.Length + 1}. Exit");
            if (!int.TryParse(Console.ReadLine(), out int fromUnitIndex) || fromUnitIndex < 1 || fromUnitIndex > distanceUnits.Length + 1) {
                invalidInput();
                ConvertDistance();
                return;
            }

            fromUnitIndex--;
            if (fromUnitIndex == distanceUnits.Length) {
                Console.WriteLine("Exiting...");
                Converter();
                return;
            }

            Console.WriteLine("To:");
            for (int i = 0; i < distanceUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {distanceUnits[i]}");
            }
            Console.WriteLine($"{distanceUnits.Length + 1}. Back");
            if (!int.TryParse(Console.ReadLine(), out int toUnitIndex) || toUnitIndex < 1 || toUnitIndex > distanceUnits.Length + 1) {
                invalidInput();
                ConvertDistance();
                return;
            }

            toUnitIndex--;
            if (toUnitIndex == distanceUnits.Length) {
                ConvertDistance();
                return;
            }

            Console.Write("Enter the distance to convert: ");
            if (!double.TryParse(Console.ReadLine(), out double distance)) {
                invalidInput();
                ConvertDistance();
                return;
            }

            double convertedDistance = ConvertUnits(distance, fromUnitIndex, toUnitIndex);
            Console.WriteLine($"The converted distance is: {convertedDistance} {distanceUnits[toUnitIndex]}");
            Converter();
        }

        static double ConvertUnits(double distance, int fromIndex, int toIndex) {
            double[,] distanceConversionRates = {
                // From          | m           | km           | mi            | ft          | yd           | in
                { 1,             0.001,        0.000621371,   3.28084,       1.09361,      39.3701 },    // From meter (m)
                { 1000,          1,            0.621371,      3280.84,       1093.61,      39370.1 },    // From kilometer (km)
                { 1609.34,       1.60934,      1,             5280,          1760,         63360 },      // From mile (mi)
                { 0.3048,        0.0003048,    0.000189394,   1,             0.333333,     12 },         // From foot (ft)
                { 0.9144,        0.0009144,    0.000568182,   3,             1,            36 },         // From yard (yd)
                { 0.0254,        0.0000254,    0.0000157828,  0.0833333,     0.0277778,    1 }           // From inch (in)
            };

            return distance * distanceConversionRates[fromIndex, toIndex];
        }

        static void ConvertTime() {
            Console.WriteLine("Select the unit you would like to convert from and to.");
            Console.WriteLine("From:");
            for (int i = 0; i < timeUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {timeUnits[i]}");
            }
            Console.WriteLine($"{timeUnits.Length + 1}. Exit");
            if (!int.TryParse(Console.ReadLine(), out int fromUnitIndex) || fromUnitIndex < 1 || fromUnitIndex > timeUnits.Length + 1) {
                invalidInput();
                ConvertTime();
                return;
            }

            fromUnitIndex--;
            if (fromUnitIndex == timeUnits.Length) {
                Console.WriteLine("Exiting...");
                Converter();
                return;
            }

            Console.WriteLine("To:");
            for (int i = 0; i < timeUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {timeUnits[i]}");
            }
            Console.WriteLine($"{timeUnits.Length + 1}. Back");
            if (!int.TryParse(Console.ReadLine(), out int toUnitIndex) || toUnitIndex < 1 || toUnitIndex > timeUnits.Length + 1) {
                invalidInput();
                ConvertTime();
                return;
            }

            toUnitIndex--;
            if (toUnitIndex == timeUnits.Length) {
                ConvertTime();
                return;
            }

            Console.Write("Enter the time to convert: ");
            if (!double.TryParse(Console.ReadLine(), out double time)) {
                invalidInput();
                ConvertTime();
                return;
            }

            double[,] timeConversionRates = {
                // From         | s        | min      | h        | d        | wk       | mo       | yr
                { 1,           1/60.0,   1/3600.0, 1/86400.0, 1/604800.0, 1/2629746.0, 1/31536000.0 }, // From seconds (s)
                { 60,          1,       1/60.0,   1/1440.0, 1/10080.0, 1/43800.0, 1/525600.0 }, // From minutes (min)
                { 3600,        60,      1,        1/24.0,   1/168.0,   1/730.0,   1/8760.0 }, // From hours (h)
                { 86400,       1440,    24,       1,        1/7.0,    1/30.44,   1/365.0 }, // From days (d)
                { 604800,      10080,   168,      7,        1,        1/4.348,   1/52.1429 }, // From weeks (wk)
                { 2629746,     43800,   730,      30.44,    4.348,    1,         12/365.0 }, // From months (mo)
                { 31536000,    525600,  8760,     365,      52.1429,  365/12,    1 } // From years (yr)
            };

            double convertedTime = time * timeConversionRates[fromUnitIndex, toUnitIndex];
            Console.WriteLine($"The converted time is: {convertedTime} {timeUnits[toUnitIndex]}");
            Converter(); 
        }

        static void ConvertTemperature() {
            Console.WriteLine("Select the unit you would like to convert from and to.");
            Console.WriteLine("From:");
            for (int i = 0; i < temperatureUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {temperatureUnits[i]}");
            }
            Console.WriteLine($"{temperatureUnits.Length + 1}. Exit");
            if (!int.TryParse(Console.ReadLine(), out int fromUnitIndex) || fromUnitIndex < 1 || fromUnitIndex > temperatureUnits.Length + 1) {
                invalidInput();
                ConvertTemperature();
                return;
            }

            fromUnitIndex--;
            if (fromUnitIndex == temperatureUnits.Length) {
                Console.WriteLine("Exiting...");
                Converter();
                return;
            }

            Console.WriteLine("To:");
            for (int i = 0; i < temperatureUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {temperatureUnits[i]}");
            }
            Console.WriteLine($"{temperatureUnits.Length + 1}. Back");
            if (!int.TryParse(Console.ReadLine(), out int toUnitIndex) || toUnitIndex < 1 || toUnitIndex > temperatureUnits.Length + 1) {
                invalidInput();
                ConvertTemperature();
                return;
            }

            toUnitIndex--;
            if (toUnitIndex == temperatureUnits.Length) {
                ConvertTemperature();
                return;
            }

            Console.Write("Enter the temperature to convert: ");
            if (!double.TryParse(Console.ReadLine(), out double temperature)) {
                invalidInput();
                ConvertTemperature();
                return;
            }

            double convertedTemperature = 0;

            // Convert from 'fromUnitIndex' to Celsius first
            switch (fromUnitIndex) {
                case 0: // Celsius
                    convertedTemperature = temperature;
                    break;
                case 1: // Fahrenheit to Celsius
                    convertedTemperature = (temperature - 32) * 5.0 / 9.0;
                    break;
                case 2: // Kelvin to Celsius
                    convertedTemperature = temperature - 273.15;
                    break;
                case 3: // Rankine to Celsius
                    convertedTemperature = (temperature - 491.67) * 5.0 / 9.0;
                    break;
            }

            // Now convert from Celsius to 'toUnitIndex'
            switch (toUnitIndex) {
                case 0: // Celsius
                    break;
                case 1: // Celsius to Fahrenheit
                    convertedTemperature = convertedTemperature * 9.0 / 5.0 + 32;
                    break;
                case 2: // Celsius to Kelvin
                    convertedTemperature += 273.15;
                    break;
                case 3: // Celsius to Rankine
                    convertedTemperature = (convertedTemperature + 273.15) * 9.0 / 5.0;
                    break;
            }

            Console.WriteLine($"The converted temperature is: {convertedTemperature} {temperatureUnits[toUnitIndex]}");
            Converter();
        }

        static void ConvertPressure() {
            Console.WriteLine("Select the unit you would like to convert from and to.");
            Console.WriteLine("From:");
            for (int i = 0; i < pressureUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {pressureUnits[i]}");
            }
            Console.WriteLine($"{pressureUnits.Length + 1}. Exit");
            if (!int.TryParse(Console.ReadLine(), out int fromUnitIndex) || fromUnitIndex < 1 || fromUnitIndex > pressureUnits.Length + 1) {
                invalidInput();
                ConvertPressure();
                return;
            }

            fromUnitIndex--;
            if (fromUnitIndex == pressureUnits.Length) {
                Console.WriteLine("Exiting...");
                Converter();
                return;
            }

            Console.WriteLine("To:");
            for (int i = 0; i < pressureUnits.Length; i++) {
                Console.WriteLine($"{i + 1}. {pressureUnits[i]}");
            }
            Console.WriteLine($"{pressureUnits.Length + 1}. Back");
            if (!int.TryParse(Console.ReadLine(), out int toUnitIndex) || toUnitIndex < 1 || toUnitIndex > pressureUnits.Length + 1) {
                invalidInput();
                ConvertPressure();
                return;
            }

            toUnitIndex--;
            if (toUnitIndex == pressureUnits.Length) {
                ConvertPressure();
                return;
            }

            Console.Write("Enter the pressure to convert: ");
            if (!double.TryParse(Console.ReadLine(), out double pressure)) {
                invalidInput();
                ConvertPressure();
                return;
            }

            double[,] pressureConversionRates = {
                // From          | Pa             | hPa         | bar          | psi          | atm          | Torr
                { 1,             0.01,           1e-5,         0.000145038,   9.86923e-6,    0.00750062 },   // From Pascal (Pa)
                { 100,           1,              0.001,        0.0145038,     0.000986923,   0.750062 },     // From hectopascal (hPa)
                { 1e5,           1000,           1,            14.5038,       0.986923,      750.062 },      // From bar
                { 6894.76,       68.9476,        0.0689476,    1,             0.06804596,    51.7149 },       // From psi
                { 101325,        1013.25,        1.01325,      14.6959,       1,             760 },          // From atm
                { 133.322,       1.33322,        0.00133322,   0.0193368,     0.00131579,    1 }             // From Torr
            };

            double convertedPressure = pressure * pressureConversionRates[fromUnitIndex, toUnitIndex];
            Console.WriteLine($"The converted pressure is: {convertedPressure} {pressureUnits[toUnitIndex]}");
            Converter();
        }

        static void invalidInput() {
            Console.WriteLine("Please enter a valid input.");
        }
    }
}
