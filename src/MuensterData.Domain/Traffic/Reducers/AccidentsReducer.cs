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

    [ReducerMethod(typeof(WithBicycleToggled))]
    public static TrafficState OnWithBicycleToggled(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    WithBicycle = !state.Accidents.ActiveFilters.WithBicycle
                }
            }
        };
        return ApplyFilters(state);
    }

    [ReducerMethod(typeof(WithCarToggled))]
    public static TrafficState OnWithCarToggled(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    WithCar = !state.Accidents.ActiveFilters.WithCar
                }
            }
        };
        return ApplyFilters(state);
    }

    [ReducerMethod(typeof(WithPedestrianToggled))]
    public static TrafficState OnWithPedestrianToggled(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    WithPedestrian = !state.Accidents.ActiveFilters.WithPedestrian
                }
            }
        };
        return ApplyFilters(state);
    }

    [ReducerMethod(typeof(WithMotorcycleToggled))]
    public static TrafficState OnWithMotorcycleToggled(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    WithMotorcycle = !state.Accidents.ActiveFilters.WithMotorcycle
                }
            }
        };
        return ApplyFilters(state);
    }

    [ReducerMethod(typeof(WithTruckToggled))]
    public static TrafficState OnWithTruckToggled(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    WithTruck = !state.Accidents.ActiveFilters.WithTruck
                }
            }
        };
        return ApplyFilters(state);
    }

    [ReducerMethod(typeof(WithOtherToggled))]
    public static TrafficState OnWithOtherToggled(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    WithOther = !state.Accidents.ActiveFilters.WithOther
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
                        }
                        && (!filters.WithBicycle || x.BicycleInvolved)
                        && (!filters.WithCar || x.CarInvolved)
                        && (!filters.WithPedestrian || x.PedestrianInvolved)
                        && (!filters.WithMotorcycle || x.MotorcycleInvolved)
                        && (!filters.WithTruck || x.TruckInvolved)
                        && (!filters.WithOther || x.OtherInvolved)
            )
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
