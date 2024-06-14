using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscBemaCommands
{
	internal class PaperCut : IPaperCut
	{
		public byte[] Full()
		{
			return new byte[] {27, 'w'.ToByte()};
		}

		public byte[] Partial()
		{
			return new byte[] {27, 'm'.ToByte()};
		}
	}
}