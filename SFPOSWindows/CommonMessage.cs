using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOSWindows
{
    public static class CommonMessage
    {
        public const string labeledPrice = "This product is labeled price, Please insert price in the product UPC Code.";
        public const string TareWeightExceeds = "Tare weight exceeds product gross weight.";
        public const string WeightExceeds = "Scale weight overweight.";
        public const string WeightUnderZero = "Scale Weight Under Zero.";
        public const string WeightUnstable = "There is nothing on scale, please place item on scale";// Scale Weight Unstable.";
        public const string nothingOnScale = "There is nothing on scale, please place item on scale";
        public const string passwordDuplicate = "This password has already existed with another user, Please insert different password.";
        public const string tareWeightMoreThen = "Tare weight should not more than 30 lb.";
    }
}
