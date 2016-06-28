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

        public RangeProperty(T max, T min)
        {        
            this.min = min;
            this.max = max;
            Fill();
        }

        //public T Current { get { return current; } }

        public T Max
        {
            get { return max; }
            set { SetMax(value); }
        }

        public T Min
        {
            get { return min; }
            set { SetMin(value); }
        }

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

        public U CurrentPercent
        {
            get
            {
               return GetCurrentPercent();
            }
        }
        public abstract void Add(T amount);
        public abstract void Subtract(T amount);
        public abstract void Fill();
        public abstract void Empty();
    }

}

