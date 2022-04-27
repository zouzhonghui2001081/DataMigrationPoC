using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T parameter)
        {
            Parameter = parameter;
        }

        public T Parameter { get; set; }
    }

    public class EventArgs<T1, T2> : EventArgs
    {
        public EventArgs(T1 parameter1, T2 parameter2)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }

        public T1 Parameter1 { get; set; }
        public T2 Parameter2 { get; set; }
    }

    public class EventArgs<T1, T2, T3> : EventArgs
    {
        public EventArgs(T1 parameter1, T2 parameter2, T3 parameter3)
        {
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Parameter3 = parameter3;
        }

        public T1 Parameter1 { get; set; }
        public T2 Parameter2 { get; set; }
        public T3 Parameter3 { get; set; }
    }
}