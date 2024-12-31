using MuensterData.Domain.Traffic.States;

namespace MuensterData.Domain.Traffic.Actions.Accidents;
public record AllAccidentsLoadedAction(IReadOnlyCollection<Accident> Accidents);