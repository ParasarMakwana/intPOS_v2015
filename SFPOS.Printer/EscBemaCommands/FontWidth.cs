using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscBemaCommands
{
	public class FontWidth : IFontWidth
	{
		public byte[] Normal()
		{
			return new byte[]
			{
				27,
				'W'.ToByte(),
				0,
				27,
				'd'.ToByte(),
				0
			};
		}

		public byte[] DoubleWidth2()
		{
			return new byte[]
			{
				27,
				'W'.ToByte(),
				1,
				27,
				'd'.ToByte(),
				1
			};
		}

		public byte[] DoubleWidth3()
		{
			return new byte[]
			{
				29,
				'!'.ToByte(),
				32
			};
		}
	}
}