namespace XO_logics
{
    public class Player
    {
        private readonly string r_Name;
        private readonly bool r_IsHuman;
        private eBoardShape m_MyShape;
        private short m_NumberOfWinnings = 0;

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public short NumberOfWinnings
        {
            get
            {
                return m_NumberOfWinnings;
            }

            set
            {
                m_NumberOfWinnings = value;
            }
        }

        public eBoardShape MyShape
        {
            get
            {
                return m_MyShape;
            }

            set
            {
                m_MyShape = value;
            }
        }

        public bool IsHuman
        {
            get
            {
                return r_IsHuman;
            }
        }

        public Player(string i_name, eBoardShape i_shape, bool i_isHuman)
        {
            r_Name = i_name;
            m_MyShape = i_shape;
            r_IsHuman = i_isHuman;
        }
    }
}
