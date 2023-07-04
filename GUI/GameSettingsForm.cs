using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GUI
{
   public class GameSettingsForm : Form
    {
        private Label m_LabelPlayers;
        private TextBox m_TextBoxPlayer1Name;
        private Label m_LabelPlayer2;
        private TextBox m_TextBoxPlayer2Name;
        private CheckBox m_CheckBoxPlayer2;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelCols;
        private NumericUpDown m_UpDownNumOfRow;
        private NumericUpDown m_UpDownNumOfCols;
        private Button m_ButtonStart;
        private Label m_LabelPlayer1;
        private bool m_SettingsCompleted = false;

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            string projectFolderPath = System.IO.Directory.GetParent(Application.StartupPath).Parent.FullName;
            string iconFilePath = System.IO.Path.Combine(projectFolderPath, "gameLogo.ico");
            Icon = new Icon(iconFilePath);

            m_LabelPlayers = new Label();
            m_LabelPlayer1 = new Label();
            m_TextBoxPlayer1Name = new TextBox();
            m_LabelPlayer2 = new Label();
            m_LabelBoardSize = new Label();
            m_TextBoxPlayer2Name = new TextBox();
            m_CheckBoxPlayer2 = new CheckBox();
            m_LabelRows = new Label();
            m_LabelCols = new Label();
            m_UpDownNumOfRow = new NumericUpDown();
            m_UpDownNumOfCols = new NumericUpDown();
            m_ButtonStart = new Button();
            ((System.ComponentModel.ISupportInitialize)(m_UpDownNumOfRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(m_UpDownNumOfCols)).BeginInit();
            SuspendLayout();
            StartPosition = FormStartPosition.CenterScreen;
            // 
            // m_LabelPlayers
            // 
            m_LabelPlayers.AutoSize = true;
            m_LabelPlayers.Location = new System.Drawing.Point(12, 9);
            m_LabelPlayers.Name = "m_LabelPlayers";
            m_LabelPlayers.Size = new System.Drawing.Size(59, 17);
            m_LabelPlayers.TabIndex = 0;
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelPlayer1
            // 
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Location = new System.Drawing.Point(30, 37);
            m_LabelPlayer1.Name = "m_LabelPlayer1";
            m_LabelPlayer1.Size = new System.Drawing.Size(64, 17);
            m_LabelPlayer1.TabIndex = 1;
            m_LabelPlayer1.Text = "Player 1:";
            // 
            //player 1 Name
            // 
            m_TextBoxPlayer1Name.Location = new System.Drawing.Point(124, 37);
            m_TextBoxPlayer1Name.Name = "textBox1";
            m_TextBoxPlayer1Name.Size = new System.Drawing.Size(136, 22);
            m_TextBoxPlayer1Name.TabIndex = 2;
            m_TextBoxPlayer1Name.TextChanged += M_TextBoxPlayer1Name_TextChanged;
            // 
            // Player 2:
            // 
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.Location = new System.Drawing.Point(54, 69);
            m_LabelPlayer2.Name = "label3";
            m_LabelPlayer2.Size = new System.Drawing.Size(64, 17);
            m_LabelPlayer2.TabIndex = 3;
            m_LabelPlayer2.Text = "Player 2:";
            // 
            // label4
            // 
            m_LabelBoardSize.AutoSize = true;
            m_LabelBoardSize.Location = new System.Drawing.Point(12, 114);
            m_LabelBoardSize.Name = "label4";
            m_LabelBoardSize.Size = new System.Drawing.Size(81, 17);
            m_LabelBoardSize.TabIndex = 6;
            m_LabelBoardSize.Text = "Board Size:";
            // 
            // Player 2 Name
            // 
            m_TextBoxPlayer2Name.Enabled = false;
            m_TextBoxPlayer2Name.Location = new System.Drawing.Point(124, 69);
            m_TextBoxPlayer2Name.Name = "textBox2";
            m_TextBoxPlayer2Name.Size = new System.Drawing.Size(136, 22);
            m_TextBoxPlayer2Name.TabIndex = 4;
            m_TextBoxPlayer2Name.Text = "Computer";
            m_TextBoxPlayer2Name.TextChanged += m_TextBoxPlayer2Name_TextChanged;
            // 
            // checkBox1
            // 
            m_CheckBoxPlayer2.AutoSize = true;
            m_CheckBoxPlayer2.Location = new System.Drawing.Point(30, 70);
            m_CheckBoxPlayer2.Name = "checkBox1";
            m_CheckBoxPlayer2.Size = new System.Drawing.Size(18, 17);
            m_CheckBoxPlayer2.TabIndex = 5;
            m_CheckBoxPlayer2.UseVisualStyleBackColor = true;
            m_CheckBoxPlayer2.CheckedChanged += new System.EventHandler(checkBoxPlayer2_CheckedChanged);
            // 
            // label5
            // 
            m_LabelRows.AutoSize = true;
            m_LabelRows.Location = new System.Drawing.Point(30, 151);
            m_LabelRows.Name = "label5";
            m_LabelRows.Size = new System.Drawing.Size(46, 17);
            m_LabelRows.TabIndex = 7;
            m_LabelRows.Text = "Rows:";
            // 
            // label6
            // 
            m_LabelCols.AutoSize = true;
            m_LabelCols.Location = new System.Drawing.Point(165, 151);
            m_LabelCols.Name = "label6";
            m_LabelCols.Size = new System.Drawing.Size(39, 17);
            m_LabelCols.TabIndex = 8;
            m_LabelCols.Text = "Cols:";
            // 
            // numericUpDown1
            // 
            m_UpDownNumOfRow.Location = new System.Drawing.Point(82, 151);
            m_UpDownNumOfRow.Maximum = 10;
            m_UpDownNumOfRow.Name = "numericUpDown1";
            m_UpDownNumOfRow.Size = new System.Drawing.Size(51, 22);
            m_UpDownNumOfRow.TabIndex = 9;
            m_UpDownNumOfRow.ValueChanged += m_UpDownNumOfRow_ValueChanged;

            // 
            // numericUpDown2
            // 
            m_UpDownNumOfCols.Location = new System.Drawing.Point(210, 151);
            m_UpDownNumOfCols.Maximum = 10;
            m_UpDownNumOfCols.Name = "numericUpDown2";
            m_UpDownNumOfCols.Size = new System.Drawing.Size(49, 22);
            m_UpDownNumOfCols.TabIndex = 10;
            m_UpDownNumOfCols.ValueChanged += M_UpDownNumOfCols_ValueChanged;
            // 
            // button1
            // 
            m_ButtonStart.Location = new System.Drawing.Point(15, 197);
            m_ButtonStart.Name = "button1";
            m_ButtonStart.Size = new System.Drawing.Size(244, 23);
            m_ButtonStart.TabIndex = 11;
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.UseVisualStyleBackColor = true;
            m_ButtonStart.Click += buttomStart_clicked;
            m_ButtonStart.Enabled = false;
            

            // 
            // GameSettingsForm
            // 
            updateUDMinAndMax();
            ClientSize = new System.Drawing.Size(278, 232);
            Controls.Add(m_ButtonStart);
            Controls.Add(m_UpDownNumOfCols);
            Controls.Add(m_UpDownNumOfRow);
            Controls.Add(m_LabelCols);
            Controls.Add(m_LabelRows);
            Controls.Add(m_LabelBoardSize);
            Controls.Add(m_CheckBoxPlayer2);
            Controls.Add(m_TextBoxPlayer2Name);
            Controls.Add(m_LabelPlayer2);
            Controls.Add(m_TextBoxPlayer1Name);
            Controls.Add(m_LabelPlayer1);
            Controls.Add(m_LabelPlayers);
            Name = "GameSettingsForm";
            Text = "Game Settings";
            ((System.ComponentModel.ISupportInitialize)(m_UpDownNumOfRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(m_UpDownNumOfCols)).EndInit();
            ResumeLayout(false);
            PerformLayout();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            FormClosing += GameSettingsForm_FormClosing;

        }

        private void GameSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SettingsCompleted == false)
            {
                Application.Exit();
            }
              
        }

        private void M_UpDownNumOfCols_ValueChanged(object sender, EventArgs e)
        {
            m_UpDownNumOfRow.Value = m_UpDownNumOfCols.Value;
            checkValidion();
        }

        private void m_UpDownNumOfRow_ValueChanged(object sender, EventArgs e)
        {
            m_UpDownNumOfCols.Value = m_UpDownNumOfRow.Value;
            checkValidion();
        }

        private void m_TextBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {
            checkValidion();
        }

        private void M_TextBoxPlayer1Name_TextChanged(object sender, EventArgs e)
        {
            checkValidion();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayer2Name.Enabled = m_CheckBoxPlayer2.Checked;
            m_TextBoxPlayer2Name.Text = m_CheckBoxPlayer2.Checked ? "" : "Computer";
            checkValidion();
        }

        private void buttomStart_clicked(object sender, EventArgs e)
        {
            m_SettingsCompleted = true;
            Close();
        }

        private void checkValidion()
        {
            if (m_TextBoxPlayer1Name.Text.Length != 0 && (m_TextBoxPlayer2Name.Text.Length != 0 || !m_CheckBoxPlayer2.Checked) && m_UpDownNumOfRow.Value != 0 && m_UpDownNumOfCols.Value != 0)
            {
                m_ButtonStart.Enabled = true;
            }
            else
            {
                m_ButtonStart.Enabled = false;
            }
        }

        private void updateUDMinAndMax()
        {
            m_UpDownNumOfRow.Maximum = m_UpDownNumOfCols.Maximum = XO_logics.Game.MaxBoardSize();
            m_UpDownNumOfRow.Minimum = m_UpDownNumOfCols.Minimum = XO_logics.Game.MinBoardSize();
        }

        public void ReturnFormData(out int o_BoardSize, out string o_name1, out string o_name2, out bool o_player2IsHuman)
        {
            o_BoardSize = (int)m_UpDownNumOfRow.Value;
            o_name1 = m_TextBoxPlayer1Name.Text;
            o_name2 = m_TextBoxPlayer2Name.Text;
            o_player2IsHuman = m_CheckBoxPlayer2.Checked == true;
        }

   }
}
