using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace MGIS350_Project2
{
    [SuppressMessage(@"ReSharper", @"InconsistentNaming")]
    public static class Constants
    {
        private static readonly IList<KeyValuePair<string, double>> _large = new ReadOnlyCollection<KeyValuePair<string, double>>(list: new List<KeyValuePair<string, double>>
            {
                new KeyValuePair<string,double>(key: @"Dough", value: 4),
                new KeyValuePair<string,double>(key: @"Cheese", value: 16),
                new KeyValuePair<string,double>(key: @"Sauce", value: 10),
                new KeyValuePair<string,double>(key: @"ExtraCheese", value: 16),
                new KeyValuePair<string,double>(key: @"Pepperoni", value: 8),
                new KeyValuePair<string,double>(key: @"Mushrooms", value: 6),
                new KeyValuePair<string,double>(key: @"Sausage", value: 6)

            }
        );


        private static readonly IList<KeyValuePair<string, double>> _medium = new ReadOnlyCollection<KeyValuePair<string, double>>(list: new List<KeyValuePair<string, double>>
            {
                new KeyValuePair<string,double>(key: @"Dough", value: 3),
                new KeyValuePair<string,double>(key: @"Cheese", value: 12),
                new KeyValuePair<string,double>(key: @"Sauce", value: 7.5),
                new KeyValuePair<string,double>(key: @"ExtraCheese", value: 12),
                new KeyValuePair<string,double>(key: @"Pepperoni", value: 6),
                new KeyValuePair<string,double>(key: @"Mushrooms", value: 4.5),
                new KeyValuePair<string,double>(key: @"Sausage", value: 4.5)

            }
        );

        private static readonly IList<KeyValuePair<string, double>> _small = new ReadOnlyCollection<KeyValuePair<string, double>>(list: new List<KeyValuePair<string, double>>
            {
                new KeyValuePair<string,double>(key: @"Dough", value: 2),
                new KeyValuePair<string,double>(key: @"Cheese", value: 8),
                new KeyValuePair<string,double>(key: @"Sauce", value: 5),
                new KeyValuePair<string,double>(key: @"ExtraCheese", value: 8),
                new KeyValuePair<string,double>(key: @"Pepperoni", value: 4),
                new KeyValuePair<string,double>(key: @"Mushrooms", value: 3),
                new KeyValuePair<string,double>(key: @"Sausage", value: 3)

            }
        );

        private static readonly IList<KeyValuePair<string, string>> _units = new ReadOnlyCollection<KeyValuePair<string, string>>(list: new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(key: @"Dough", value: @"lbs"),
            new KeyValuePair<string, string>(key: @"Cheese", value: @"oz"),
            new KeyValuePair<string, string>(key: @"Sauce", value: @"oz"),
            new KeyValuePair<string, string>(key: @"Pepperoni", value: @"oz"),
            new KeyValuePair<string, string>(key: @"Mushrooms", value: @"oz"),
            new KeyValuePair<string, string>(key: @"Sausage", value: @"oz")
        }); 

        public static IList<KeyValuePair<string, double>> Large() { return _large; }
        public static IList<KeyValuePair<string, double>> Medium() { return _medium; }
        public static IList<KeyValuePair<string, double>> Small() { return _small; }
        public static IList<KeyValuePair<string, string>> Units { get { return _units; } } 


    }

}
