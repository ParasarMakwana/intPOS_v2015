using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscBemaCommands
{
    internal class InitializePrint : IInitializePrint
    {
        public byte[] Initialize()

        {
            return new byte[] {27, '@'.ToByte()}
                .AddBytes(SetEscBema())
                .AddBytes(SetLineSpace3());
        }

        private static byte[] SetEscBema()
        {
            return new byte[] {29, 249, 53, 0};
        }

        private static byte[] SetLineSpace3(byte range = 20)
        {
            return new byte[] {27, 51, range};
        }
    }
}