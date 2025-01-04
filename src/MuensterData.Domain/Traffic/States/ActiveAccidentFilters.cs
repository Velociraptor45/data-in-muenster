namespace MuensterData.Domain.Traffic.States;

public record ActiveAccidentFilters(
    int[] Years,
    int[] LightConditions,
    bool WithBicycle,
    bool WithCar,
    bool WithPedestrian,
    bool WithMotorcycle,
    bool WithTruck,
    bool WithOther,
    bool OnlyMuensterCityArea);