using MuensterData.Domain.Traffic.States;

namespace MuensterData.Domain.Common.Ports;
public interface ICsvReader
{
    IEnumerable<Accident> LoadAccidents();
}
