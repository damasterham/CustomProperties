using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProperties
{
    /// <summary>
    /// Base Class 
    /// </summary>
    /// <typeparam name="T">Meant for structs such as int, float or double</typeparam>
    /// <typeparam name="U">Meant for structs such as int, float or double</typeparam> 
    public abstract class RangeProperty<T, U> where T : struct where U : struct
    {
        private T nullValue; // The null value of type, so pretty much 0 without writing its 0

        protected T current;
        protected T min;
        protected T max;
        private bool incrementFlag;
        protected T increment;

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

        private RangeProperty<T,U>  appendage;
        private RangeProperty<T,U> prependage;

        public RangeProperty<T,U> Prependage
        {
            get { return prependage; }
            set { prependage = value; }
        }

        public RangeProperty<T,U>  Appendage // This will add a whole lot of more contextual math, so All mutator methods would have to implement Appedage checks
        {
            get { return appendage; }
            set { appendage = value; }
        }


        protected abstract void SetMin(T amount);
        protected abstract void SetMax(T amount);
        protected abstract void SetCurrent(T amount);
        protected abstract void SetCurrent(T amount, bool isOverflowAllowed);
       //protected abstract U Difference(); // Changed to public U RangeAmount
        protected abstract U GetCurrentPercent();
        protected abstract void SetCurrentPercent(U percent);

        public abstract U RangeAmount();

        /// <summary>
        /// Add an amount to the Current value
        /// </summary>
        /// <param name="amount"></param>
        public abstract void Add(T amount);
        public abstract void Add(T amount, bool isOverflowAllowed);

        /// <summary>
        /// Subtract an amount from the Current value
        /// </summary>
        /// <param name="amount"></param>
        public abstract void Subtract(T amount);
        public abstract void Subtract(T amount, bool isOverflowAllowed);

        /// <summary>
        /// Set the Current value to Max
        /// </summary>
        public abstract void Fill();

        /// <summary>
        /// Set the Current value to Min;
        /// </summary>
        public abstract void Empty();

        /// <summary>
        /// Increments Current value by IncremenValue
        /// </summary>
        public void Increment()
        {
            // Could maybe contain a check iff null/0 but Generic Type is complicated...
            if (incrementFlag)
                Add(increment);
            else
                throw new ArgumentNullException("IncrementalValue is 0, incrementing or decrementing would do nothing. Set the IncrementValue to use Increment() and Decrement()");

        }

        /// <summary>
        /// Decrements Current value by IncrementValue
        /// </summary>
        public void Decrement()
        {
            if (incrementFlag)
                Subtract(increment);
            else
                throw new ArgumentNullException("IncrementalValue is 0, incrementing or decrementing would do nothing. Set the IncrementValue to use Increment() and Decrement()");
        }

        /// <summary>
        /// Get or Set the Current value as a percentage. 
        /// </summary>
        public U CurrentPercent
        {
            get
            {
                return GetCurrentPercent();
            }
            set
            {
                SetCurrentPercent(value);
            }
        }

        /// <summary>
        /// Get or Set the Increment value. The Increment value is used if you consitily want to increment or decremet the Current value using Increment() and Decrement() 
        /// </summary>
        public T IncrementValue
        {
            get { return increment; }
            set {
                    if (!nullValue.Equals(value))// All number structs initialize to 0 so by checking if the value passed is equal to the nullValue it's check if 0
                    {
                        increment = value;
                        incrementFlag = true;
                    }
                   
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

