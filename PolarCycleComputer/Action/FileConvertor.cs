using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Action
{
  public class FileConvertor
  {
    public string[] SplitString(string text) => text.Split(GetParams(), StringSplitOptions.RemoveEmptyEntries);

    public string[] SplitStringByEnter(string text) => text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

    public string[] SplitStringBySpace(string text) => string.Join(" ", text.Split().Where(x => x != "")).Split(' ');

    public string[] GetParams() => new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};
  }
}
