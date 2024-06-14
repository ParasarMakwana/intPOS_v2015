namespace SFPOS.Printer.Interfaces.Command
{
    internal interface IDrawer
    {
        byte[] LowPaper();
        byte[] Open();
    }
}