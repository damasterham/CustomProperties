using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProperties
{
    /// <summary>
    /// Default implementation of RangeProperty using T as int and U as float.
    /// This is the tempalte for implementations, so if more should be added add here and branch it out to other implementeations
    /// </summary>
    public class RangeIntWithoutAppendages : RangeProperty<int, float>//, IPrecantable<float>
    {
        public bool BreakPointFlag = false;

        /// <inheritdoc/>  // Need Sandcastle or NDoc for this
        /// <remarks>
        /// Woop!
        /// </remarks>
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

        protected override void SetCurrent(int amount, bool isOverflowAllowed)
        {
            throw new NotImplementedException();
        }

        public override float RangeAmount()
        {
            return max - min;
        }
        // GetCurrentPercent<T>
        protected override float GetCurrentPercent() //Must be fixed!!!!!
        {
            /// Harder to caluculate Current percentage when Min is negative
            /// Should find some mathimaticl formel or rule wich solves this
            int offset = min + current;
            float percent = (Min + offset) / RangeAmount();
            if (percent < 0)
                return percent + 1;
            else
                return percent;
        }

        protected override void SetCurrentPercent(float percent) // Can use a RangeFloat
        {
            float min = 0;
            float max = 1;

            if (!(percent < min) && !(percent > max))
            {
                // Add Min for offset, so if Min is negative the Current value is correct 
                // 25% of the range -100 -> 100 is -50 | 100 - -100 = 200 | 200 * 0.25 = 50 (!-50) | 200 * 0.5 + -100 = -50
                Current = Min + (int)(RangeAmount() * percent);
            }
        }

        public override void Add(int amount)
        {
            if (amount < 0)
                return;

            SetCurrent(current + amount);
        }
        public override void Add(int amount, bool no = false)
        {
            Add(amount);
        }


        public override void Subtract(int amount)
        {
            if (amount < 0)
                return;

            SetCurrent(current - amount);

            //Add(amount * -1);
        }
        public override void Subtract(int amount, bool no = false)
        {
            Subtract(amount);
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
