using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscPosCommands
{
    public class Drawer : IDrawer
    {
        public byte[] LowPaper()
        {
            return new byte[] { 29, 114, 49 };
        }

        public byte[] Open()
        {
            return new byte[] {27, 112, 0, 60, 120};
        }
    }
}