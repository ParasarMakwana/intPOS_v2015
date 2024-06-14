using SFPOS.Printer.Enums;

namespace SFPOS.Printer.Interfaces.Command
{
	internal interface IQrCode
	{
		byte[] Print(string qrData);
		byte[] Print(string qrData, QrCodeSize qrCodeSize);
	}
}