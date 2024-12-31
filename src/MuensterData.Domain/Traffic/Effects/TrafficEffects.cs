﻿using Fluxor;
using MuensterData.Domain.Common.Ports;
using MuensterData.Domain.Traffic.Actions;
using MuensterData.Domain.Traffic.Actions.Accidents;
using MuensterData.Domain.Traffic.States;

namespace MuensterData.Domain.Traffic.Effects;

public class TrafficEffects
{
    private readonly ICsvReader _csvReader;

    public TrafficEffects(IState<TrafficState> state, ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }

    [EffectMethod(typeof(TrafficPageEnteredAction))]
    public Task HandleTrafficPageEnteredAction(IDispatcher dispatcher)
    {
        var accidents = _csvReader.LoadAccidents().ToList();

        dispatcher.Dispatch(new AllAccidentsLoadedAction(accidents));

        return Task.CompletedTask;
    }

}
