using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProperties
{
    public abstract class RangeProperty<T, U>
    {
        protected T current;
        protected T min;
        protected T max;

        /// <summary>
        /// Constructs a RangePropery with Max and Min values
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        public RangeProperty(T max, T min)
        {        
            this.min = min; // Initial value needed, can't use SetMin since max not being set would cancel it out
            SetMax(max);
            Fill();
        }

        //public T Current { get { return current; } }

        /// <summary>
        /// Get or Set the maximum value of the range. The Max value must be larger than the Min value.
        /// </summary>
        public T Max
        {
            get { return max; }
            set { SetMax(value); }
        }

        /// <summary>
        /// Get or Set the minimum value of the range. The Min value must be smaller than the Max value.
        /// </summary>
        public T Min
        {
            get { return min; }
            set { SetMin(value); }
        }

        /// <summary>
        /// Get or Set the current value within Min and Max range.
        /// </summary>
        public T Current
        {
            get
            {

                return current;
            }
            set
            {
                SetCurrent(value);
            }
        }

        
        protected abstract void SetMin(T amount);
        protected abstract void SetMax(T amount);
        protected abstract void SetCurrent(T amount);
        protected abstract U GetCurrentPercent();

       
        /// <summary>
        /// Add an amount to the Current value
        /// </summary>
        /// <param name="amount"></param>
        public abstract void Add(T amount);

        /// <summary>
        /// Subtract an amount from the Current value
        /// </summary>
        /// <param name="amount"></param>
        public abstract void Subtract(T amount);
        /// <summary>
        /// Set the Current value to Max
        /// </summary>
        public abstract void Fill();

        /// <summary>
        /// Set the Current value to Min;
        /// </summary>
        public abstract void Empty();

        /// <summary>
        /// Get the Current value as a percentage
        /// </summary>
        public U CurrentPercent
        {
            get
            {
                return GetCurrentPercent();
            }
        }

        /// <summary>
        /// Returns true if Current is equal to Min
        /// </summary>
        /// <returns>Returns true if Current is equal to Min</returns>
        public bool IsEmpty
        {
            get
            {
                return min.Equals(current);
            }
        }

        /// <summary>
        /// Returns true if Current is equal to Max
        /// </summary>
        /// <returns>Returns true if Current is equal to Max</returns>
        public bool IsFull
        {
            get
            {
                return max.Equals(current);
            }
        }
    }

}

