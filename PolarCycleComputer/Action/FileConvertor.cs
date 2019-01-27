using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Action
{
  public class FileConvertor
  {
    public string[] SplitString(string text)
    {
      var splitString = GetParams();

      var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

      return splittedText;
    }

    public string[] SplitStringByEnter(string text)
    {
      return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }

    public string[] SplitStringBySpace(string text)
    {
      var formattedText = string.Join(" ", text.Split().Where(x => x != ""));
      return formattedText.Split(' ');
    }

    public string[] GetParams()
    {
      return new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};
    }
  }
}
