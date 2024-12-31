namespace MuensterData.Domain.Traffic.Actions.Accidents;

public record SelectedLightConditionsChangedAction(IReadOnlyCollection<int> Values);
