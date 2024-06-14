using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscPosCommands
{
    public class PaperCut : IPaperCut
    {
        public byte[] Full()
        {
            return new byte[] {29, 'V'.ToByte(), 65,3};
        }

        public byte[] Partial()
        {
            return new byte[] {29, 'V'.ToByte(), 65,3};
        }
    }
}