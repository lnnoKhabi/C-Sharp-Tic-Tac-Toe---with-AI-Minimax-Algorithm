using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace XandO
{
    public partial class Form1 : Form
    {
        private string[] BoxContents = new string[]{"", "", "",
                                                    "", "", "",
                                                    "", "", "" };

        bool YourTurn = false;

        bool NewGame = true;
        public Form1 ( )
        {
            InitializeComponent ( );
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void Label2_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        
        private void Label3_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void Label4_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void Label5_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void Label6_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void Label7_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void Label8_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));

        }
        private void Label9_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            Players_Turn(lbl, Convert.ToInt16(lbl.TabIndex));
        }
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

                ResetBoard();
            DialogResult PlayFirstOrNot = MessageBox.Show("Would you like to play first?","New Game!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (PlayFirstOrNot == DialogResult.Yes)
            {
                YourTurn = true;
                NewGame = false;
            }

            else if(PlayFirstOrNot == DialogResult.No)
            {
                NewGame = false;

                Computers_Turn();//computer plays
            }
        }

        private void ResetBoard()
        {
            for (int i = 0; i < BoxContents.Length; i++)
            {
                BoxContents[i] = "";
            }
            Label[] AllBoxes = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9 };
            
            foreach (Label box in AllBoxes)
            {
                box.Text = "";
            }
        }

        private void Players_Turn(Label label, int index) 
        {
            if (YourTurn && IsGameOver() == false && string.IsNullOrEmpty(Winner()))//if its human turn to play
            {
                if (label.Text == string.Empty)//if box is vaccant
                {
                    BoxContents[index] = "X";
                    label.Text = "X";
                    Computers_Turn();
                }
            }
            else
            {
                ResultDialog();
            }
        }

        private void Computers_Turn()//Ai for the computer
        {
            if (IsGameOver() == false && string.IsNullOrEmpty(Winner()))
            {


                //Start Ai logic***************************************************
                Label[] AllBoxes = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9 };
                List<Label> EmptyBoxes = new List<Label>();

                foreach (Label box in AllBoxes)
                {
                    if (box.Text == string.Empty)
                    {
                        EmptyBoxes.Add(box);
                    }
                }
                BoxContents[Convert.ToInt16(EmptyBoxes[0].TabIndex)] = "O";
                EmptyBoxes[0].Text = "O";
                
                //end Ai logic*****************************************************

                YourTurn = true;

                //check if pc won
                if (!string.IsNullOrEmpty(Winner()))
                {
                    ResultDialog();
                }
            }
            else
            {
                ResultDialog();
            }
        }

        private bool IsGameOver()
        {
            bool over = true;
            foreach (string item in BoxContents)
            {
                if (string.IsNullOrEmpty(item))
                {
                    over = false;
                    break;
                }
            }

            return over;
        }

        private string Winner()
        {
            string won = "";

            int[,] States = { { 0, 1, 2 }, { 3, 4, 5 }, {6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {

                    string w = HasWon(States[i,0], States[i,1], States[i,2] );
                if (!string.IsNullOrEmpty(w))
                {
                    won = w[0].ToString() + " Won!";
                    break;
                }
            }

            if (string.IsNullOrEmpty(won) && IsGameOver() == true)
            {
                won = "It's a tie.";
            }
            return won;
        }
        private string HasWon(int a,int b,int c)
        {
            string won = "";
            if($"{BoxContents[a]}{BoxContents[b]}{BoxContents[c]}" == "OOO" || $"{BoxContents[a]}{BoxContents[b]}{BoxContents[c]}" == "XXX")
            {
                won = BoxContents[a];
            }
            return won;
        }
        private void ResultDialog()
        {
            if (NewGame == false)
            {
                DialogResult GameOver = MessageBox.Show($"{Winner()}.\nPlease start a new game.", "Game Over!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }
    }
}
