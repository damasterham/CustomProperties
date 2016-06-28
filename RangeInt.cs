using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularSkills.CustomProperties
{
    //RangeInt<T percentilce type>
    public class RangeInt : RangeProperty<int, float>//, IPrecantable<float>
    {
        public RangeInt(int max, int min) : base(max, min) { }

        public RangeInt(int max) : base(max, 0) { }

        protected override void SetMax(int amount)
        {
            if (amount < min)
            {
                throw new ArgumentOutOfRangeException();
            }

            max = amount;

            if (max < current)
            {
                Fill();
            }
        }

        protected override void SetMin(int amount)
        {
            if (amount > max)
            {
                throw new ArgumentOutOfRangeException();
            }

            min = amount;

            if (min > current)
            {
                Empty();
            }
        }

        protected override void SetCurrent(int amount)
        {
            if (amount < min)
                Empty();
            else if (amount > max)
                Fill();
            else
                current = amount;
        }

        // GetCurrentPercet<T>
        protected override float GetCurrentPercent()
        {
            float difference = max - min;
            float offset = min + current;
            return offset / difference;
        }

        public override void Add(int amount)
        {
            if (amount < 0)
                return;

            int cur = current;
            cur += amount;
            SetCurrent(cur);
        }

        public override void Subtract(int amount)
        {
            if (amount < 0)
                return;

            int cur = current;
            cur -= amount;
            SetCurrent(cur);

            //Add(amount * -1);
        }

        public override void Fill()
        {
            current = max;
        }

        public override void Empty()
        {
            current = min;
        }

        //protected float GetCurrentPrercent()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
