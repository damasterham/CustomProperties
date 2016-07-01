using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProperties
{
    //RangeInt<T percentilce type>

        /// <summary>
    /// Default implementation of RangeProperty using T as int and U as float.
    /// This is the tempalte for implementations, so if more should be added add here and branch it out to other implementeations
    /// </summary>
    public class RangeInt : RangeProperty<int, float>//, IPrecantable<float>
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
            {
                Empty();

                if (Prependage != null)
                {
                    int excess = min - amount;
                    Prependage.Subtract(excess);
                }
            }
            else if (amount > max)
            {
                Fill();

                if (Appendage != null)
                {
                    int excess = max - amount;
                    Appendage.Add(excess);
                }
            }
            else
                current = amount;
        }

        protected override void SetCurrent(int amount, bool IsExtensionAllowed = false)
        {
            if (!IsExtensionAllowed)
                SetCurrent(amount);
            else
            {
                if (amount < min)
                {
                    Empty();

                    int excess = min - amount;

                    Prependage = new RangeInt(this.min, excess);

                    Prependage.Subtract(excess);
                }
                else if (amount > max)
                {
                    Fill();

                    int excess = min - amount;

                    Appendage = new RangeInt(excess, this.max);

                    Appendage.Add(excess);
                }
                else
                    current = amount;
            }          
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
        public override void Add(int amount, bool isOverflowAllowed = false)
        {
            if (amount < 0)
                return;

            SetCurrent(current + amount, isOverflowAllowed);
        }

        public override void Subtract(int amount)
        {
            if (amount < 0)
                return;

            SetCurrent(current - amount);

            //Add(amount * -1);
        }
        public override void Subtract(int amount, bool isOverflowAllowed = false)
        {
            if (amount < 0)
                return;

            SetCurrent(current - amount, isOverflowAllowed);

            //Add(amount * -1);
        }

        public override void Fill()
        {
            current = max;

            if (Appendage != null)
            {
                Appendage.Fill();
            }           
        }

        public override void Empty()
        {
            current = min;

            if (Prependage != null)
                Prependage.Empty();
        }

        //protected float GetCurrentPrercent()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
