using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscPosCommands
{
    public class InitializePrint : IInitializePrint
    {
        public byte[] Initialize()
        {
            return new byte[] {27, '@'.ToByte()};
        }
    }
}