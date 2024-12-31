namespace MuensterData.Domain.Traffic.Actions.Accidents;
public record SelectedYearsChangedAction(IReadOnlyCollection<int> Values);