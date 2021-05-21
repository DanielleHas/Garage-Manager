using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    [Serializable]
    public class ValueOutOfRangeException : Exception
    {
        float m_MaxValue;
        float m_MinValue;

        /*
         * Constructor for this exception
        */
        internal ValueOutOfRangeException(float i_MaxValue, float i_MinValue) : base()
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}