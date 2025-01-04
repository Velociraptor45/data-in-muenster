using Fluxor;
using MuensterData.Domain.Common.Constants;
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
                },
                MapSettings = state.Accidents.MapSettings with
                {
                    IsOpen = false,
                    MarkerOpacity = 0.4d,
                    ShowMuensterCityArea = false
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
                        && (!filters.OnlyMuensterCityArea
                            || (
                                x.Coordinate.Longitude <= MuensterArea.BottomRight.Longitude
                                && x.Coordinate.Longitude >= MuensterArea.TopLeft.Longitude
                                && x.Coordinate.Latitude >= MuensterArea.BottomRight.Latitude
                                && x.Coordinate.Latitude <= MuensterArea.TopLeft.Latitude))
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

    [ReducerMethod]
    public static TrafficState OnMapMarkerOpacityChanged(TrafficState state, MapMarkerOpacityChangedAction action)
    {
        return state with
        {
            Accidents = state.Accidents with
            {
                MapSettings = state.Accidents.MapSettings with
                {
                    MarkerOpacity = action.NewOpacityValue
                }
            }
        };
    }

    [ReducerMethod(typeof(ToggleMapSettingsVisibilityAction))]
    public static TrafficState OnToggleMapSettingsVisibility(TrafficState state)
    {
        return state with
        {
            Accidents = state.Accidents with
            {
                MapSettings = state.Accidents.MapSettings with
                {
                    IsOpen = !state.Accidents.MapSettings.IsOpen
                }
            }
        };
    }

    [ReducerMethod(typeof(ToggleOnlyMuensterCityAreaAction))]
    public static TrafficState OnToggleOnlyMuensterCityArea(TrafficState state)
    {
        state = state with
        {
            Accidents = state.Accidents with
            {
                ActiveFilters = state.Accidents.ActiveFilters with
                {
                    OnlyMuensterCityArea = !state.Accidents.ActiveFilters.OnlyMuensterCityArea
                }
            }
        };

        return ApplyFilters(state);
    }

    [ReducerMethod(typeof(ToggleShowMuensterCityAreaAction))]
    public static TrafficState OnToggleShowMuensterCityArea(TrafficState state)
    {
        return state with
        {
            Accidents = state.Accidents with
            {
                MapSettings = state.Accidents.MapSettings with
                {
                    ShowMuensterCityArea = !state.Accidents.MapSettings.ShowMuensterCityArea
                }
            }
        };
    }
}
