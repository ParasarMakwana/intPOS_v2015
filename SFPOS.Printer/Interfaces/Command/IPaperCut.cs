namespace SFPOS.Printer.Interfaces.Command
{
	internal interface IPaperCut
	{
		byte[] Full();
		byte[] Partial();
	}
}