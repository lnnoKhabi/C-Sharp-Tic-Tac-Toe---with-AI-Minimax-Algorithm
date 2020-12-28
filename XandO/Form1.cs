using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace XandO
{
    public partial class Form1 : Form
    {

        private string[] GameState = new string[]{"", "", "",
                                                    "", "", "",
                                                    "", "", "" };
		enum Turn
		{
            Null,
            Draw,
            Human,
            Computer
		}
        Turn CurrentTurn = Turn.Null;
        int[][] WinStates = { 
            new int[]{ 0, 1, 2 }, 
            new int[] { 3, 4, 5 }, 
            new int[] { 6, 7, 8 }, 
            new int[] { 0, 3, 6 }, 
            new int[] { 1, 4, 7 }, 
            new int[] { 2, 5, 8 }, 
            new int[] { 0, 4, 8 }, 
            new int[] { 2, 4, 6 } 
        };
        string Human = "";
        string Computer = "";
        //bool GameOver = false;

        // bool firstMove = true;
        Label[] AllBoxes;
        public Form1 ( )
        {
            InitializeComponent ( );
            AllBoxes = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9 };

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

		private void toolStripButton1_NewGame_Click( object sender, EventArgs e )
		{
            NewGame();

        }
		private void toolStripButton1_Exit_Click( object sender, EventArgs e )
		{
            Application.Exit();

        }
       
        private void NewGame()
		{
            ResetBoard();

            DialogResult XorO = MessageBox.Show("Do you want to go first?", "New Game!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if ( XorO == DialogResult.Yes )
            {
                Human = "X";
                Computer = "O";
                CurrentTurn = Turn.Human;
            }

            else if ( XorO == DialogResult.No )
            {
                Computer = "X";
                Human = "O";
                //CurrentTurn = Turn.Computer;
                //ComputersTurn();

				//first move is random
				Random r = new Random();
				int n = r.Next(0, 9);
				GameState[ n ] = Computer;
				AllBoxes[ n ].Text = Computer;
				CurrentTurn = Turn.Human;
			}
            
        }

        private void ResetBoard()
        {
            for (int i = 0; i < GameState.Length; i++)
            {
                GameState[i] = "";
            }

            foreach ( Label box in AllBoxes)
            {
                box.Text = "";
                box.ForeColor = Color.Black;
            }
        }

        private void Players_Turn(Label label, int index) 
        {
            if ( CurrentTurn == Turn.Human )
            {
                if ( label.Text == string.Empty )//if box is vaccant
                {
                    GameState[ index ] = Human;
                    label.Text = Human;
                    CurrentTurn = Turn.Computer;
                    ComputersTurn();
                }
            }
        }

		private void ComputersTurn()
		{
            if(CurrentTurn == Turn.Computer )
			{
                int bestMove = -2;
                if ( Computer == "O" )
                {
                    int maxEval = int.MaxValue;
                        int shorter_depth = int.MaxValue;
                    for ( int i = 0; i < GameState.Length; i++ )
                    {
                        int depth = 0;
                        if ( string.IsNullOrEmpty(GameState[ i ]) )
                        {
                            GameState[ i ] = Computer;//do next move
                            int eval = Minimax(GameState, true, ref depth);
                            GameState[ i ] = "";//undo the move
                            if ( eval <= maxEval )
                            {
                                maxEval = eval;
                                bestMove = i;
                                if (depth < shorter_depth )
								{
                                    shorter_depth = depth;
                                maxEval = eval;
                                bestMove = i;
								}
                            }
                        }
                    }
                }
				else
				{
                    int maxEval = -int.MaxValue;
                        int shorter_depth = int.MaxValue;
                    for ( int i = 0; i < GameState.Length; i++ )
                    {
                        int depth = 0;
                        if ( string.IsNullOrEmpty(GameState[ i ]) )
                        {
                            GameState[ i ] = Computer;
                            int eval = Minimax(GameState, false, ref depth);
                            GameState[ i ] = "";
                            if ( eval >= maxEval )
                            {
                                maxEval = eval;
                                bestMove = i;
                                if ( depth < shorter_depth )
                                {

                                    shorter_depth = depth;
                                    maxEval = eval;
                                    bestMove = i;
                                }
                            }
                        }
                    }
                }
                try
                {
                    if ( bestMove >= 0 ) 
                    {
                        GameState[ bestMove ] = Computer;
                        AllBoxes[ bestMove ].Text = Computer;
                        CurrentTurn = Turn.Human;
                    }
                }
				catch(Exception e)
				{
					MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				}
                over(GameOver());

            }
		}

		private int Minimax(string[] position, bool maximizing_player, ref int depth)
		{
            Turn WhoWon = GameOver();
            if( WhoWon == Turn.Human || WhoWon == Turn.Computer || WhoWon == Turn.Draw )
			{
				if(WhoWon == Turn.Human )
				{
                    return Human == "O" ? -1 : 1;
				}
                else if ( WhoWon == Turn.Computer )
                {
                    return Computer == "X" ? 1 : -1;
                }
                else if( WhoWon == Turn.Draw )
                {
                    return 0;
                }
            }

			if ( maximizing_player )
			{
                int maxEval = -int.MaxValue;
				for ( int i = 0; i < position.Length; i++ )
				{
                    //depth = 0;
                    if(string.IsNullOrEmpty(position[ i ]) )
                    {
                        position[ i ] = "X";
                        depth += 1;
                        int eval = Minimax(position, false, ref depth );
                        maxEval = Math.Max(maxEval, eval);
                        position[ i ] = "";
					}
				}
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                for ( int i = 0; i < position.Length; i++ )
                {
                    //depth = 0;
                    if ( string.IsNullOrEmpty(position[ i ]) )
                    {
                        position[ i ] = "O";
                        depth += 1;
                        int eval = Minimax(position, true, ref depth);
                        minEval = Math.Min(minEval, eval);
                        position[ i ] = "";
                    }
                }
                return minEval;
            }
        }

        private void over(Turn t )
		{
            CurrentTurn = Turn.Human;
            if ( t != Turn.Null) //if theres a winner
            {
                for ( int i = 0; i < WinStates.Length; i++ )
                {
                    //highlight the winning line
                    //if Human
                    if ( GameState[ WinStates[ i ][ 0 ] ] == Human && GameState[ WinStates[ i ][ 1 ] ] == Human && GameState[ WinStates[ i ][ 2 ] ] == Human )
                    {
                        AllBoxes[ WinStates[ i ][ 0 ] ].ForeColor = Color.Red;
                        AllBoxes[ WinStates[ i ][ 1 ] ].ForeColor = Color.Red;
                        AllBoxes[ WinStates[ i ][ 2 ] ].ForeColor = Color.Red;
                    }
                    //if Computer
                    else if ( GameState[ WinStates[ i ][ 0 ] ] == Computer && GameState[ WinStates[ i ][ 1 ] ] == Computer && GameState[ WinStates[ i ][ 2 ] ] == Computer )
                    {
                        AllBoxes[ WinStates[ i ][ 0 ] ].ForeColor = Color.Red;
                        AllBoxes[ WinStates[ i ][ 1 ] ].ForeColor = Color.Red;
                        AllBoxes[ WinStates[ i ][ 2 ] ].ForeColor = Color.Red;
                    }
                    
                }
                
                string winner = t == Turn.Human ? "You" : "Computer";
                string text = t == Turn.Draw ? $"It's a tie!\nDo you want to play again?" : $"{winner} Won!\nDo you want to play again?";
                DialogResult PLayAgain = MessageBox.Show(text, "Game over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(PLayAgain == DialogResult.Yes )
				{

                    NewGame();
				}
				else
				{
                    CurrentTurn = Turn.Null;
				}

            }
        }

        private Turn GameOver()
		{

            for ( int i = 0; i < WinStates.Length; i++ )
            {
                //check if human won
                if ( GameState[ WinStates[ i ][ 0 ] ] == Human && GameState[ WinStates[ i ][ 1 ] ] == Human && GameState[ WinStates[ i ][ 2 ] ] == Human )
                {
                    return Turn.Human;
                }
                //check if computer won
                else if ( GameState[ WinStates[ i ][ 0 ] ] == Computer && GameState[ WinStates[ i ][ 1 ] ] == Computer && GameState[ WinStates[ i ][ 2 ] ] == Computer )
                {
                    return Turn.Computer;
                }
            }
			//if game is not over and noone has won
			foreach ( string state in GameState )
			{
                if(string.IsNullOrEmpty(state) )
				{
                    return Turn.Null;
				}
			}

			//if game is over and noone has won
            return Turn.Draw;
        }

		private async void toolStripButton_CPUvCPU_Click( object sender, EventArgs e )
		{
            DialogResult cpuvscpu = MessageBox.Show("watch CPU vs CPU?", "New Game!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if ( cpuvscpu == DialogResult.Yes )
            {
                ResetBoard();
                CurrentTurn = Turn.Computer;
                Computer = "X";
                Human = "O";

                //first move is random
                Random r = new Random();
                int n = r.Next(0, 9);
                GameState[ n ] = Computer;
                AllBoxes[ n ].Text = Computer;

                //ComputersTurn();

                for ( int i = 0; i < 8; i++ )
                {
                    //Thread.Sleep(200);
                    await Task.Delay(1500);
                    CurrentTurn = Turn.Computer;
                    Computer = Computer == "X" ? "O" : "X";
                    Human = Computer == "X" ? "O" : "X";
                    ComputersTurn();
                }
            }
        }
	}
}
