namespace SFPOS.Printer.Interfaces.Command
{
	internal interface IAlignment
	{
		byte[] Left();
		byte[] Right();
		byte[] Center();
	}
}