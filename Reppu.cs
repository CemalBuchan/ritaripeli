using ritaripeli;

internal class Reppu
{
    private List<Tavara> itemsInBag = new List<Tavara>();

    public IReadOnlyList<Tavara> Tavarat => itemsInBag.AsReadOnly();

    public float MaxItemsInBag { get; } = 10;
    public float MaxWeight { get; } = 30;
    public float MaxVolume { get; } = 20;

    public float CurrentWeight { get; private set; } = 0;
    public float CurrentVolume { get; private set; } = 0;

    public float LeftWeight() => MaxWeight - CurrentWeight;
    public float LeftVolume() => MaxVolume - CurrentVolume;
    public float LeftCount() => MaxItemsInBag - itemsInBag.Count;

    public bool LisaaTavara(Tavara item)
    {
        if (LeftCount() <= 0)
        {
            Console.WriteLine("There is no space left in the bag.");
            return false;
        }

        if (item.Weight > LeftWeight() && item.Volume > LeftVolume())
        {
            Console.WriteLine("Neither the weight nor the volume fits.");
            return false;
        }
        else if (item.Weight > LeftWeight())
        {
            Console.WriteLine("Weight limit exceeded, cannot add item.");
            return false;
        }
        else if (item.Volume > LeftVolume())
        {
            Console.WriteLine("Volume limit exceeded, cannot add item.");
            return false;
        }

        itemsInBag.Add(item);
        CurrentWeight += item.Weight;
        CurrentVolume += item.Volume;

        Console.WriteLine($"{item.Name} added to the bag.");
        return true;
    }

    public bool PoistaTavara(Tavara item)
    {
        if (itemsInBag.Remove(item))
        {
            CurrentWeight -= item.Weight;
            CurrentVolume -= item.Volume;
            return true;
        }
        return false;
    }
}
