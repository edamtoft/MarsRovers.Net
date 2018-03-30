using MarsRovers.Net.Types;

namespace MarsRovers.Net
{
  public interface IMap
  {
    void ReportLocation(ITrackedRover rover);
  }
}