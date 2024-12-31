namespace MuensterData.Domain.Traffic.States;

public record AllAccidentFilters(
    IReadOnlyCollection<int> Years,
    IReadOnlyCollection<LightCondition> LightConditions);