using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // "O", "X", ""のいずれか
        private string[] board = new string[9];
        private Boolean isPlayerX;
        private int turnsCount;

        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = "";
            }
            drawButtons();
            turnsCount = 0;
            isPlayerX = true;
            statusText.Text = "現在の手番 : " + (isPlayerX ? "X" : "O");
        }
        private void drawButtons() {
            Button btn;
            for (int i = 0; i < board.Length; i++)
            {
                string buttonName = "btn" + i.ToString();
                btn = (Button)Controls.Find(buttonName, true)[0];

                btn.Text = board[i];
            }
        }

        private string CalculateWinner(string[] board)
        {
            int[,] lines = new int[8,3] 
                {
                    { 0, 1, 2},
                    { 3, 4, 5},
                    { 6, 7, 8},
                    { 0, 3, 6},
                    { 1, 4, 7},
                    { 2, 5, 8},
                    { 0, 4, 8},
                    { 2, 4, 6}
                };

            for(int i = 0; i < lines.GetLength(0); i++)
            {
                if(
                    board[lines[i, 0]] != "" &&
                    board[lines[i,0]] == board[lines[i,1]] &&
                    board[lines[i,0]] == board[lines[i,2]]
                )
                {
                    return board[lines[i, 0]];
                }
            }

            return "";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int btnIndex = int.Parse(btn.Name.Replace("btn", ""));

            if (board[btnIndex] != "" || CalculateWinner(board) != "" || turnsCount == 9)
            {
                return;
            }

            turnsCount++;
            board[btnIndex] = isPlayerX ? "X" : "O";
            drawButtons();

            isPlayerX = !isPlayerX;
            string winner = CalculateWinner(board);
            if(winner != "")
            {
                statusText.Text = winner + "の勝ち！！";
                return;
            }

            if(turnsCount == 9)
            {
                statusText.Text = "引き分け！！";
                return;
            }

            statusText.Text = "現在の手番 : " + (isPlayerX ? "X" : "O");
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            init();
        }
    }
}
