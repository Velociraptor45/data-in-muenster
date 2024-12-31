using Fluxor;
using MuensterData.Domain.Traffic.Actions.Accidents;
using MuensterData.Domain.Traffic.States;

namespace MuensterData.Domain.Traffic.Reducers;
public static class AccidentsReducer
{
    [ReducerMethod]
    public static TrafficState OnAllAccidentsLoaded(TrafficState state, AllAccidentsLoadedAction action)
    {
        if (!action.Accidents.Any())
            return state;

        var availableYears = action.Accidents.Select(x => x.Year).Distinct().Order().ToList();

        state = state with
        {
            Accidents = state.Accidents with
            {
                AllAccidents = action.Accidents,
                AllFilters = state.Accidents.AllFilters with
                {
                    Years = availableYears
                },
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    Years = [availableYears[^1]]
                }
            }
        };

        return ApplyFilters(state);
    }

    [ReducerMethod]
    public static TrafficState OnSelectedYearsChanged(TrafficState state, SelectedYearsChangedAction action)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    Years = action.Values.ToArray()
                }
            }
        };

        return ApplyFilters(state);
    }

    [ReducerMethod]
    public static TrafficState OnSelectedLightConditionsChanged(TrafficState state, SelectedLightConditionsChangedAction action)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    LightConditions = action.Values.ToArray()
                }
            }
        };

        return ApplyFilters(state);
    }

    private static TrafficState ApplyFilters(TrafficState state)
    {
        var filters = state.Accidents.ActiveFilters;

        var accidents = state.Accidents.AllAccidents
             .Where(x => filters.Years.Contains(x.Year)
                         && filters.LightConditions.Contains(x.LightCondition)
                         && x.Coordinate is
                         {
                             Longitude: >= 7.502093508911121 and <= 7.740016177368152,
                             Latitude: <= 52.0113100492876 and >= 51.88773057218762
                         })
             .ToList();

        return state with
        {
            Accidents = state.Accidents with
            {
                DisplayedAccidents = accidents
            }
        };
    }
}
