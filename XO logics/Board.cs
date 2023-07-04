using System;

namespace XO_logics
{
    public class Board
    {
        private enum ePossibleLine
        { rows = 0,
            Cols,
            Diagonal,
            SecondaryDiagonal,
            NoLoser,
        }

        private eBoardShape [,] m_Board;
        private byte m_BoardSize;
        private byte m_NumOfTurn = 0;


        public Board(in byte i_size)
        {
            BoardSize = i_size;
            m_Board = new eBoardShape [i_size, i_size];
        }

        public byte NumOfTurn
        {
            get
            {
                return m_NumOfTurn;
            }
            set
            {
                m_NumOfTurn = value;
            }
        }

        public byte BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }

        public static eBoardShape FlipSign(eBoardShape i_shape)
        {
            if (i_shape == eBoardShape.X)
            {
                return eBoardShape.O;
            }
            else
            {
                return eBoardShape.X;
            }
        }

        private bool checkDataRange(in byte i_number)
        {
            return i_number >= 1 && i_number <= m_BoardSize;
        }

        public eBoardShape GetShapeFromMatrix(in byte i_row, in byte i_col)
        {
            return m_Board[i_row, i_col];
        }

        public bool MarkBoard(in byte i_row, in byte i_col, in eBoardShape i_shape)
        {
            const bool v_Valid = true;
            bool result = !v_Valid;
            if (checkDataRange(i_row) && checkDataRange(i_col))
            {
                if (m_Board[i_row - 1, i_col - 1] == eBoardShape.Empty) 
                {
                    m_Board[i_row - 1, i_col - 1] = i_shape;
                    result = v_Valid;
                    NumOfTurn++;
                }
            }
            return result;
        }

        public eGameStatus LookForLoser()
        {
            eGameStatus resultFunction;
            eBoardShape  loser = eBoardShape .Empty;
            ePossibleLine line = 0;
            while (loser == eBoardShape .Empty && line < ePossibleLine.NoLoser)
            {
                switch (line)
                {
                    case ePossibleLine.rows:
                        for (byte j = 0; j < m_BoardSize; j++)
                        {
                            loser = straightRow(j);
                            if (loser != eBoardShape .Empty)
                            {
                                break;
                            }
                        }
                        line++;
                        break;

                    case ePossibleLine.Cols:
                        for (byte j = 0; j < m_BoardSize; j++)
                        {
                            loser = straightCol(j);
                            if (loser != eBoardShape .Empty)
                            {
                                break;
                            }
                        }
                        line++;
                        break;

                    case ePossibleLine.Diagonal:
                        loser = straightDiagonal();
                        line++;
                        break;

                    case ePossibleLine.SecondaryDiagonal:
                        loser = straightSecondaryDiagonal();
                        line++;
                        break;

                }

            }
            if (loser == eBoardShape .X)
            {
                resultFunction = eGameStatus.XLose;
            }
            else if (loser == eBoardShape .O)
            {
                resultFunction = eGameStatus.XWin;
            }
            else if (maxTurnPossible())
            {
                resultFunction = eGameStatus.Tie;
            }
            else
            {
                resultFunction = eGameStatus.NotOver;
            }

            return resultFunction;

        }

        private eBoardShape  straightRow(in byte row)
        {
            eBoardShape  currentShape = m_Board[row, 0];
            if (currentShape != eBoardShape .Empty)
            {
                for (byte j = 1; j < m_BoardSize; ++j)
                {
                    if (m_Board[row, j] != currentShape)
                    {
                        currentShape = eBoardShape .Empty;
                        break;
                    }

                }
            }
            return currentShape;
        }

        private eBoardShape  straightCol(in byte col)
        {
            eBoardShape  currentShape = m_Board[0, col];
            if (currentShape != eBoardShape .Empty)
            {
                for (byte i = 0, j = 1; j < m_BoardSize; ++i, ++j)
                {
                    if (currentShape != m_Board[j, col])
                    {
                        currentShape = eBoardShape .Empty;
                    }
                }
            }
            return currentShape;
        }

        private eBoardShape  straightDiagonal()
        {
            eBoardShape  currentShape = m_Board[0, 0];
            if (currentShape != eBoardShape .Empty)
            {
                for (byte i = 0, j = 1; j < m_BoardSize; ++i, ++j)
                {
                    if (m_Board[i, i] != m_Board[j, j] || currentShape != m_Board[j, j])
                    {
                        currentShape = eBoardShape .Empty;
                    }
                }
            }
            return currentShape;
        }

        private eBoardShape straightSecondaryDiagonal()
        {
            eBoardShape  currentShape = m_Board[0, m_BoardSize - 1];
            if (currentShape != eBoardShape .Empty)
            {
                for (byte i = 0, j = 1; j < m_BoardSize; ++i, ++j)
                {
                    if (m_Board[i, m_BoardSize - (i + 1)] != m_Board[j, (byte)(m_BoardSize - (j + 1))] || currentShape != m_Board[j, (byte)(m_BoardSize - (j + 1))])
                    {
                        currentShape = eBoardShape .Empty;
                    }
                }
            }
            return currentShape;
        }

        public void ClearBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_Board[i, j] = eBoardShape.Empty;
                }
            }
            NumOfTurn = 0;
        }

        private bool maxTurnPossible()
        {
            return m_NumOfTurn == (m_BoardSize * m_BoardSize);
        }

        public bool CheckIfEmpty(in byte row, in byte col)
        {
            return m_Board[row - 1, col - 1] == eBoardShape.Empty;
        }

        public void BestMove(Player i_AIPlayer, out byte row, out byte col)
        {
            // AI to make its turn
            short bestScore = short.MaxValue;
            short score;
            row = 0;
            col = 0;
            for (byte i = 0; i < m_BoardSize; i++)
            {
                for (byte j = 0; j < m_BoardSize; j++)
                {
                    // Is the spot available?
                    if (m_Board[i, j] == eBoardShape.Empty)
                    {
                        score = 0;
                        m_Board[i, j] = i_AIPlayer.MyShape;
                        NumOfTurn++;
                        minimax(0, false, i_AIPlayer, ref score);
                        m_Board[i, j] = eBoardShape.Empty;
                        NumOfTurn--;
                        if (score < bestScore)
                        {
                            bestScore = score;
                            row = (byte)(i + 1);
                            col = (byte)(j + 1);
                        }
                    }
                }
            }
        }

        private short checkIfAIPlayerLoser(eGameStatus i_result, eBoardShape i_AIPlayerShape)
        {
            if (i_result == eGameStatus.XLose)
            {
                return i_AIPlayerShape == eBoardShape.X ? (short)1 : (short)-1;
            }
            else if (i_result == eGameStatus.XWin)
            {
                return i_AIPlayerShape == eBoardShape.X ? (short)-1 : (short)1;
            }
            else
            {
                return 0;
            }
        }

        private void minimax(int depth, bool isMinimizing, Player i_AIPlayer, ref short io_score)
        {
            eGameStatus result = LookForLoser();
            if (result != eGameStatus.NotOver)
            {
                io_score += checkIfAIPlayerLoser(result, i_AIPlayer.MyShape);
                return;
            }

            if (isMinimizing)
            {
                short bestScore = short.MaxValue;
                for (byte i = 0; i < m_BoardSize; i++)
                {
                    for (byte j = 0; j < m_BoardSize; j++)
                    {
                        // Is the spot available?
                        if (m_Board[i, j] == eBoardShape.Empty)
                        {
                            m_Board[i, j] = i_AIPlayer.MyShape;
                            NumOfTurn++;
                            minimax(depth + 1, false, i_AIPlayer, ref io_score);
                            m_Board[i, j] = eBoardShape.Empty;
                            NumOfTurn--;
                            io_score = Math.Min(io_score, bestScore);
                        }
                    }
                }
            }
            else
            {
                short bestScore = short.MinValue;
                for (byte i = 0; i < m_BoardSize; i++)
                {
                    for (byte j = 0; j < m_BoardSize; j++)
                    {
                        // Is the spot available?
                        if (m_Board[i, j] == eBoardShape.Empty)
                        {
                            m_Board[i, j] = FlipSign(i_AIPlayer.MyShape);
                            NumOfTurn++;
                            minimax(depth + 1, true, i_AIPlayer, ref io_score);
                            m_Board[i, j] = eBoardShape.Empty;
                            NumOfTurn--;
                            io_score = Math.Max(io_score, bestScore);
                        }
                    }
                }
            }
        }

    }
}
