namespace TravelPal.Classes
{
    public interface iPackingListItem
    {
        public string Name { get; set; }

        public string GetInfo()
        {
            return Name;
        }
    }

    public class TravelDocument : iPackingListItem
    {
        public string Name { get; set; }
        public bool Required { get; set; }

        public TravelDocument(string name, bool required)
        {
            Name = name;
            Required = required;
        }

        public string GetInfo()
        {
            if (Required)
            {
                return Name + " Required!";
            }
            else
            {
                return Name + " Optional!";
            }
        }

    }

    public class OtherItem : iPackingListItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public OtherItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string GetInfo()
        {
            return Name + " " + Quantity;
        }
    }
}
