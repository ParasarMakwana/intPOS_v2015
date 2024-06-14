using SFPOS.Printer.Enums;
using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscBemaCommands
{
	internal class Alignment : IAlignment
	{
		private static byte[] Align(Justifications justification)
		{
			byte align;
			switch (justification)
			{
				case Justifications.Right:
					align = '2'.ToByte();
					break;
				case Justifications.Center:
					align = '1'.ToByte();
					break;
				default:
					align = '0'.ToByte();
					break;
			}

			return new byte[]
			{
				27,
				'a'.ToByte(),
				align
			};
		}

		public byte[] Left()
		{
			return Align(Justifications.Left);
		}

		public byte[] Right()
		{
			return Align(Justifications.Right);
		}

		public byte[] Center()
		{
			return Align(Justifications.Center);
		}
	}
}