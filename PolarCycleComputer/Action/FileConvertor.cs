using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Action
{
  public class FileConvertor
  {
    /// <summary>
    /// split text by header data
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string[] SplitString(string text) => text.Split(GetParams(), StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    /// detect line break and return in the form of array
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string[] SplitStringByEnter(string text) => text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    /// detects space in a text and return in the form of array
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string[] SplitStringBySpace(string text) => string.Join(" ", text.Split().Where(x => x != "")).Split(' ');

    /// <summary>
    /// return a header data
    /// </summary>
    /// <returns></returns>
    public string[] GetParams() => new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};
  }
}
