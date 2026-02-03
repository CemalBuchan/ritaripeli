using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ritaripeli
{
    internal class Reppu
    {
        public List<Tavara> itemsInBag = new List<Tavara>();

        public float maxItemsInBag = 10;

        public float maxWeight = 30;
        public float currentWeight = 0;

        public float maxVolume = 20;
        public float currentVolume = 0;

        public float leftWeight()
        {
            float leftWeight = maxWeight - currentWeight;


            return leftWeight;
        }
        public float leftVolume()
        {
            float leftVolume = maxVolume - currentVolume;


            return leftVolume;
        }

        public bool leftCount()
        {
            float leftCount = maxItemsInBag - itemsInBag.Count;

            if (leftCount != 0)
            {
                return true;
            }
            else
                return false;


        }


        public void addToBag(Tavara item)
        {
            bool weightFit = leftWeight() > item.Weight;
            bool volumeFit = leftVolume() > item.Volume;

            if (leftCount())
            {
                if (weightFit && volumeFit)
                {
                    currentWeight = currentWeight + item.Weight;
                    currentVolume = currentVolume + item.Volume;
                    itemsInBag.Add(item);
                    Console.WriteLine(item.Name + " added");
                    Console.WriteLine(" ");

                }
                else if (weightFit && !volumeFit)
                {
                    Console.WriteLine("Only the weight fits; there is not enough volume.");
                }
                else if (!weightFit && volumeFit)
                {
                    Console.WriteLine("Only the volume fits; the weight limit is exceeded.");
                }
                else
                {
                    Console.WriteLine("Neither the weight nor the volume fits.");
                }
            }
            else
                Console.WriteLine("There is no space left."); Console.WriteLine(" ");






        }
    }
}
