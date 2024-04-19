namespace Ex03.GarageLogic.Exceptions
{
    internal class ValueOutOfRangeException : Exception
    {
        internal float MaxValue { get; set; }
        internal float MinValue { get; set; }
        private string m_CustomMessage;

        public override string Message
        {
            get { return m_CustomMessage; }
        }

        public ValueOutOfRangeException()
        {
            m_CustomMessage = "The value you are trying to use is out of range.";
        }

        public ValueOutOfRangeException(string i_Message)
        {
            m_CustomMessage = i_Message;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
            m_CustomMessage = $"You should enter a value between {i_MinValue} - {i_MaxValue}";
        }
    }
}