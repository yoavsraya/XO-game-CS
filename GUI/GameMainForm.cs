using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using XO_logics;

namespace GUI
{
    class GameMainForm : Form
    {
        const bool k_playerHuman = true;
        const bool k_firstplayer = true;

        private Game m_XOLogics;
        private GameSettingsForm m_SettingsForm;
        private Button[ , ] m_BTable;
        private int m_BoardSize;
        private string m_PlayerName1;
        private string m_PlayerName2;
        private bool m_isPlayer2Human;
        Player m_currentPlayer;

        private Label m_LabelPlayer1Name;
        private Label m_LabelPlayer2Name;
        private Label m_LabelnumOfPointsPlayer1;
        private Label m_LabelnumOfPointsPlayer2;
        private FlowLayoutPanel m_ScoreLine;
        private FlowLayoutPanel m_Player1Group;
        private FlowLayoutPanel m_Player2Group;
        private string m_AppIconPath;

        DialogResult m_result;


        public GameMainForm()
        {
            m_SettingsForm = new GameSettingsForm();
            m_SettingsForm.ShowDialog();
            m_SettingsForm.ReturnFormData(out m_BoardSize, out m_PlayerName1, out m_PlayerName2, out m_isPlayer2Human);
            InitializeComponent();

            createGame();
        }

        private void InitializeComponent()
        {
            //string projectFolderPath = System.IO.Directory.GetParent(Application.StartupPath).Parent.FullName;
            //string iconFilePath = System.IO.Path.Combine(projectFolderPath, "gameLogo.ico");
            //Icon = new Icon(iconFilePath);
            //Text = "TicTacToeMisere";
            //m_AppIconPath = @"C:\Users\Yoav\source\repos\c#\B23 Ex05 YoavSraya 207496464 YonatanBrooker 313592420\GUI\gameLogo.ico";
            //Icon = new Icon(m_AppIconPath);
            m_LabelPlayer1Name = new Label();
            m_LabelPlayer2Name = new Label();
            m_LabelnumOfPointsPlayer1 = new Label();
            m_LabelnumOfPointsPlayer2 = new Label();

            m_LabelPlayer1Name.Text = m_PlayerName1 + ":";
            m_LabelPlayer2Name.Text = m_PlayerName2 + ":";

            m_LabelnumOfPointsPlayer1.Text = "0";
            m_LabelnumOfPointsPlayer2.Text = "0";

            Size = new Size(500, 500);
            MaximizeBox = false;
            MinimizeBox = false;

            creatingButtonBoard();
            int formLeft = m_BTable[m_BoardSize - 1 , m_BoardSize - 1].Right + 20;
            int formTop = m_BTable[m_BoardSize - 1, m_BoardSize - 1].Bottom + 60;
            Size = new Size(formLeft, formTop);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            locateLabels();
        }

        private void M_LabelnumOfPoints_TextChanged(object sender, EventArgs e)
        {
            int player1Points = int.Parse(m_LabelnumOfPointsPlayer1.Text);
            int player2Points = int.Parse(m_LabelnumOfPointsPlayer2.Text);

            if (player2Points > player1Points)
            {
                m_Player1Group.Font = new Font(m_LabelnumOfPointsPlayer1.Font, FontStyle.Regular);
                m_Player2Group.Font = new Font(m_LabelnumOfPointsPlayer1.Font, FontStyle.Bold);
            }
            else if(player2Points == player1Points)
            {
                m_Player1Group.Font = new Font(m_LabelnumOfPointsPlayer1.Font, FontStyle.Regular);
                m_Player2Group.Font = new Font(m_LabelnumOfPointsPlayer1.Font, FontStyle.Regular);
            }
            else
            {
                m_Player1Group.Font = new Font(m_LabelnumOfPointsPlayer1.Font, FontStyle.Bold);
                m_Player2Group.Font = new Font(m_LabelnumOfPointsPlayer1.Font, FontStyle.Regular);
            }

        }

        private void locateLabels()
        {
            m_ScoreLine = new FlowLayoutPanel();
            m_Player1Group = new FlowLayoutPanel();
            m_Player2Group = new FlowLayoutPanel();

            m_LabelnumOfPointsPlayer1.TextChanged += M_LabelnumOfPoints_TextChanged;
            m_LabelnumOfPointsPlayer2.TextChanged += M_LabelnumOfPoints_TextChanged;


            m_ScoreLine.FlowDirection = FlowDirection.LeftToRight;
            m_Player1Group.FlowDirection = FlowDirection.LeftToRight;
            m_Player2Group.FlowDirection = FlowDirection.LeftToRight;

            m_LabelPlayer1Name.AutoSize = true;
            m_LabelPlayer2Name.AutoSize = true;
            m_LabelnumOfPointsPlayer1.AutoSize = true;
            m_LabelnumOfPointsPlayer2.AutoSize = true;

            m_Player1Group.Controls.Add(m_LabelPlayer1Name);
            m_Player1Group.Controls.Add(m_LabelnumOfPointsPlayer1);
            m_Player1Group.AutoSize = true;

            m_Player2Group.Controls.Add(m_LabelPlayer2Name);
            m_Player2Group.Controls.Add(m_LabelnumOfPointsPlayer2);
            m_Player2Group.AutoSize = true;

            m_ScoreLine.Controls.Add(m_Player1Group);
            m_ScoreLine.Controls.Add(m_Player2Group);
            m_ScoreLine.AutoSize = true;
            m_ScoreLine.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            m_ScoreLine.Anchor = AnchorStyles.None;
            Controls.Add(m_ScoreLine);

            int labelY = m_BTable[m_BoardSize - 1, m_BoardSize - 1].Bottom + 5;
            int labelX = (ClientSize.Width - m_ScoreLine.Width) / 2;
            m_ScoreLine.Location = new Point(labelX, labelY);
        }

        private void creatingButtonBoard()
        {
            m_BTable = new Button[m_BoardSize, m_BoardSize];
         
            for (byte row = 0; row < m_BoardSize; row++)
            {
                for (byte col = 0; col < m_BoardSize; col++)
                {
                    m_BTable[row, col] = new Button();
                    Controls.Add(m_BTable[row, col]);
                    m_BTable[row, col].Width = m_BTable[row, col].Height = 50;
                    m_BTable[row, col].Top = 10 + (row * 60);
                    m_BTable[row, col].Left = 10 + (col * 60);
                    m_BTable[row, col].Click += bottomTable_Click;
                    m_BTable[row, col].TabStop = false;

                }

            }

        }

        private void bottomTable_Click(object sender, EventArgs e)
        {
            Button currentBottom = sender as Button;
            byte col = (byte)(((currentBottom.Left - 10) / 60) + 1);
            byte row = (byte)(((currentBottom.Top - 10) / 60) + 1);

            updateMove(row , col );

            if (!m_XOLogics.IfRoundEnded())
            {

                switchPlayer();
                if (!m_currentPlayer.IsHuman)
                {
                    if (m_BoardSize > 3)
                    {
                        m_XOLogics.GetRandomMove(out row, out col);
                    }
                    else
                    {
                        m_XOLogics.GameBoard.BestMove(m_currentPlayer, out row, out col);
                    }

                    updateMove(row, col);
                    switchPlayer();
                }
                m_XOLogics.IfRoundEnded();
            }
        }

        private void updateMove(in byte i_row, in byte i_col)
        {
            m_XOLogics.PlayPlayerMove(m_currentPlayer.MyShape, i_row, i_col);
            m_BTable[i_row - 1, i_col - 1].Text = m_currentPlayer.MyShape.ToString();
            m_BTable[i_row - 1, i_col - 1].Enabled = false;
        }

        private void switchPlayer()
        {
            if (m_currentPlayer.MyShape == eBoardShape.X)
            {
                m_currentPlayer = m_XOLogics.PlayerO;
            }
            else
            {
                m_currentPlayer = m_XOLogics.PlayerX;
            }


        }

        private void createGame()
        {
            m_XOLogics = new Game();
            m_XOLogics.gameEndedAction += M_XOLogics_gameEndedAction;
            createPlayers(m_PlayerName1, m_PlayerName2, m_isPlayer2Human);
            m_XOLogics.CreateBoard((byte)m_BoardSize);
            m_currentPlayer = m_XOLogics.PlayerX;

        }

        private void M_XOLogics_gameEndedAction(eGameStatus obj)
        {
            string gameSatatus;
            string caption;
         

            if (obj == eGameStatus.XWin)
            {
                m_LabelnumOfPointsPlayer1.Text = m_XOLogics.PlayerX.NumberOfWinnings.ToString();
                gameSatatus = $"The winner is {m_XOLogics.PlayerX.Name}!";
                caption = "A win!";
            }
            else if (obj == eGameStatus.XLose)
            {
                m_LabelnumOfPointsPlayer2.Text = m_XOLogics.PlayerO.NumberOfWinnings.ToString();
                gameSatatus = $" the winner is {m_XOLogics.PlayerO.Name}!";
                caption = "A win!";
            }
            else
            {
                gameSatatus = "Tie!";
                caption = "A Tie!";
            }

            m_result = MessageBox.Show($"{gameSatatus}{Environment.NewLine}would you like to play another round?", caption, MessageBoxButtons.YesNo);
            if (m_result == DialogResult.No)
            {
                Environment.Exit(0);
            }
            else
            {
                clearGame();
            }

        }

        private void clearGame()
        {
            m_XOLogics.ClearBoard();

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if(m_BTable[i, j].Enabled == false)
                    {
                        m_BTable[i, j].Text = string.Empty;
                        m_BTable[i, j].Enabled = true;
                    }

                }
            }

            m_currentPlayer = m_XOLogics.PlayerX;

        }

        private void createPlayers(in string i_name1, in string i_name2, in bool i_player2IsHuman)
        {
            m_XOLogics.CreatePlayer(i_name1, eBoardShape.X, k_playerHuman, k_firstplayer);
            m_XOLogics.CreatePlayer(i_name2, eBoardShape.O, i_player2IsHuman, !k_firstplayer);
        }
    }
}
