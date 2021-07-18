using Library.Types;

namespace Library.Block_3.Types
{
    public class Stereobank : Bank
    {
        public Stereobank()
        {
            Name = "Stereobank";
            AvailableCards = new[] {"Black", "White", "Iron"};
        }
    }
}