using MuensterData.Domain.Common.Ports;
using MuensterData.Domain.Traffic.States;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace MuensterData.Infrastructure.DataLoading;
public class CsvReader : ICsvReader
{
    private static readonly CultureInfo EnglishCulture = new CultureInfo("en-en");
    private const string One = "1";

    public IEnumerable<Accident> LoadAccidents()
    {
        var executionDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var filePath = Path.Combine(executionDirectory!, "data/unfaelle-muenster.csv");
        var rows = File.ReadAllLines(filePath, Encoding.UTF8).ToList();

        const int longRowIndex = 20;
        const int latRowIndex = 21;
        const int yearRowIndex = 4;
        const int lightConditionRowIndex = 11;
        const int withBicycleRowIndex = 12;
        const int withCarRowIndex = 13;
        const int withPedestrianRowIndex = 14;
        const int withMotorcycleRowIndex = 15;
        const int withTruckRowIndex = 16;
        const int withOtherRowIndex = 16;

        foreach (var row in rows.Skip(1))
        {
            var columns = row.Split(',');

            var longitude = double.Parse(columns[longRowIndex], EnglishCulture);
            var latitude = double.Parse(columns[latRowIndex], EnglishCulture);
            var year = int.Parse(columns[yearRowIndex]);
            var lightCondition = int.Parse(columns[lightConditionRowIndex]);
            var withBicycle = columns[withBicycleRowIndex] == One;
            var withCar = columns[withCarRowIndex] == One;
            var withPedestrian = columns[withPedestrianRowIndex] == One;
            var withMotorcycle = columns[withMotorcycleRowIndex] == One;
            var withTruck = columns[withTruckRowIndex] == One;
            var withOther = columns[withOtherRowIndex] == One;

            yield return new Accident(new(longitude, latitude), year, lightCondition, withBicycle, withCar,
                withPedestrian, withMotorcycle, withTruck, withOther);
        }
    }
}
