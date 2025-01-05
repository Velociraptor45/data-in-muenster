namespace MuensterData.Domain.Traffic.States;
public record Accidents(
    IReadOnlyCollection<Accident> AllAccidents,
    IReadOnlyCollection<Accident> DisplayedAccidents,
    AllAccidentFilters AllFilters,
    ActiveAccidentFilters ActiveFilters,
    MapSettings MapSettings,
    bool SourcesVisible);