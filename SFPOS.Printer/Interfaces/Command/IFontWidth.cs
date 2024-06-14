namespace SFPOS.Printer.Interfaces.Command
{
    internal interface IFontWidth
    {
        byte[] Normal();
        byte[] DoubleWidth2();
        byte[] DoubleWidth3();
        byte[] DoubleWidth4();
        
    }
}