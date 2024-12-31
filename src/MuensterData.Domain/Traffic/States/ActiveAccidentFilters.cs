namespace MuensterData.Domain.Traffic.States;

public record ActiveAccidentFilters(
    int[] Years,
    int[] LightConditions);