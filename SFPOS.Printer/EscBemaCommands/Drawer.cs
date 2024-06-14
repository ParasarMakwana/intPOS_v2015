using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscBemaCommands
{
	internal class Drawer : IDrawer
	{
		public byte[] Open()
		{
			return new byte[] {27, 'v'.ToByte(), 140};
		}
	}
}