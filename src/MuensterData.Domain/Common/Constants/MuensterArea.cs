namespace MuensterData.Domain.Common.Constants;

public static class MuensterArea
{

    public static readonly Syncfusion.Blazor.Maps.Coordinate TopLeft = new()
    {
        Longitude = 7.516008380126951,
        Latitude = 52.05571934590725,
    };

    public static readonly Syncfusion.Blazor.Maps.Coordinate BottomRight = new()
    {
        Longitude = 7.740016177368152,
        Latitude = 51.870048583779656,
    };

    public static readonly List<Syncfusion.Blazor.Maps.Coordinate> MuensterCityAreaPoints = [
        TopLeft,
        new() { Longitude = BottomRight.Longitude, Latitude = TopLeft.Latitude },
        BottomRight,
        new() { Longitude = TopLeft.Longitude, Latitude = BottomRight.Latitude },
    ];
}
