using System;
namespace XO_logics
{
    public class Game
    {
        public event Action<eGameStatus> gameEndedAction;

        private const bool k_Valid = true;
        private const int k_MaxBoardSize = 10;
        private const int k_MinBoardSize = 3;
        private Board m_GameBoard;
        private Player m_PlayerX = null;
        private Player m_PlayerO = null;
        private eGameStatus m_Status = eGameStatus.NotOver;
        private byte m_BorderSize;

        public Player PlayerX
        {
            get 
            { 
                return m_PlayerX;
            }
        }

        public Player PlayerO 
        {
            get 
            { 
                return m_PlayerO;
            }
        }

        public eGameStatus Status 
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }

        }

        public Board GameBoard 
        {
            get 
            {
                return m_GameBoard;
            }
        } 

        public bool CreatePlayer(string i_name, eBoardShape sign, bool i_isHuman, bool i_isFirst)
        {
            bool signValid;

            if (sign == eBoardShape.X && m_PlayerX == null)
            {
                m_PlayerX = new Player(i_name, sign, i_isHuman);
                signValid = k_Valid;
            }
            else if (sign == eBoardShape.O && m_PlayerO == null)
            {
                m_PlayerO = new Player(i_name, sign, i_isHuman);
                signValid = k_Valid;
            }
            else
            {
                signValid = k_Valid;
            }

            return signValid;
        }

        public bool IfRoundEnded() 
        {
            const bool v_GameFinished = true;
            bool GameOver = !v_GameFinished;
          
            m_Status = m_GameBoard.LookForLoser();

            if (m_Status == eGameStatus.XWin)
            {
                m_PlayerX.NumberOfWinnings++;
                

            }
            else if (m_Status == eGameStatus.XLose)
            {
                m_PlayerO.NumberOfWinnings++;
               
            }
            
            if (m_Status != eGameStatus.NotOver)
            {
                GameOver = v_GameFinished;
                onScoreChanged(m_Status);
            }

            return GameOver;
        }

        public bool PlayPlayerMove(eBoardShape i_shape, byte i_row, byte i_col)
        {
            bool inputValid;
            if (i_row < 1 || i_row > m_BorderSize || i_col < 1 || i_col > m_BorderSize)
            {
                inputValid = !k_Valid;
            }
            else
            {
                inputValid = m_GameBoard.MarkBoard(i_row, i_col, i_shape);
                
            }

            return inputValid;
        }
       
        public bool CreateBoard(in byte i_boardSize)
        {
            bool validInput;

            if (i_boardSize < k_MinBoardSize || i_boardSize > k_MaxBoardSize)
            {
                validInput = !k_Valid;
            }
            else
            {
                m_BorderSize = i_boardSize;
                m_GameBoard = new Board(i_boardSize);
                validInput = k_Valid;
            }
            return validInput;
        }

        public void ClearBoard()
        {
            m_GameBoard.ClearBoard();
        }


        public void GetRandomMove(out byte o_row, out byte o_col)
        {
            byte randomCol = byte.MinValue;
            byte randomRow = byte.MinValue;
            bool randomValid = !k_Valid;
            Random random = new Random();

            while (randomValid != k_Valid)
            {
                randomRow = (byte)random.Next(1, m_BorderSize + 1);
                randomCol = (byte)random.Next(1, m_BorderSize + 1);
                randomValid = m_GameBoard.CheckIfEmpty(randomRow, randomCol);

            }

            o_row = randomRow;
            o_col = randomCol;

        }

        static public int MinBoardSize()
        {
            return k_MinBoardSize;
        }

        static public int MaxBoardSize()
        {
            return k_MaxBoardSize;
        }

        private void onScoreChanged(in eGameStatus i_gameStatus)
        {
            gameEndedAction?.Invoke(i_gameStatus);
        }

    }
}
