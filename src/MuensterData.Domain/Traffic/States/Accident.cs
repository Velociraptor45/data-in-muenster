using MuensterData.Domain.Common;

namespace MuensterData.Domain.Traffic.States;

public record Accident(Coordinate Coordinate, int Year, int LightCondition, bool BicycleInvolved, bool CarInvolved,
    bool PedestrianInvolved, bool MotorcycleInvolved, bool TruckInvolved, bool OtherInvolved)
{
    private readonly string[] _colors =
    [
        "#005b79",
        "#aab315",
        "#ff5733",
        "#33ff57",
        "#5733ff",
        "#ff33a1",
        "#33a1ff",
        "#a133ff",
        "#ff8c33",
        "#33ff8c"
    ];

    public double Longitude => Coordinate.Longitude;
    public double Latitude => Coordinate.Latitude;
    public string Color => _colors[Year - 2019];
}