﻿using SFPOS.Printer.Extensions;
using SFPOS.Printer.Interfaces.Command;

namespace SFPOS.Printer.EscPosCommands
{
    public class FontWidth : IFontWidth
    {
        public byte[] Normal()
        {
            return new byte[] {27, '!'.ToByte(), 0};
        }

        public byte[] DoubleWidth2()
        {
            return new byte[] {29, '!'.ToByte(), 16};
        }

        public byte[] DoubleWidth3()
        {
            return new byte[] {29, '!'.ToByte(), 32};
        }

        public byte[] DoubleWidth4()
        {
            return new byte[] {27, '!'.ToByte(), 12};
        }
    }
}