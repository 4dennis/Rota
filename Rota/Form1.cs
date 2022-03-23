using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rota
{
    public partial class Form1 : Form
    {

        //chkb means check box

        //a couple of errors I couldnt work out, 
        //ex sometimes random chkb would enable themselves when not allowed
        //some thing i would improve is something that doesnt enable chkbs when no possible moves surround it (ie all adjacent squares occupied)






        //global variable here

        //int for number of moves made
        int moves = 1;

        //bool for in progress or not
        bool progress = false;

        //int[] array1 = new int[] { 1, 3, 5, 7, 9 };
        CheckBox[] box;
        //CheckBox[0] = chkb0;








        public Form1()
        {
            InitializeComponent();

            //fill chckb
            box = new CheckBox[] { chkb0, chkb1, chkb2, chkb3, chkb4, chkb5, chkb6, chkb7, chkb8 };

        }




        private void nextplayer()
        {
            //if player one moved

            if ((moves % 2) == 1)
            {
                for(int i = 0; i < box.Length; i++)
                {

                    //Disable p1
                    if (box[i].CheckState == CheckState.Checked)
                    {
                        box[i].Enabled = false;
                    }

                    //enable p2
                    else if (box[i].CheckState == CheckState.Indeterminate) //|| box[i].CheckState == CheckState.Unchecked
                    {
                        box[i].Enabled = true;

                    }
                }


            }

            //if player two moved
            else if ((moves % 2) == 0)
            {
                for (int i = 0; i < box.Length; i++)
                {

                    //Enable p1
                    if (box[i].CheckState == CheckState.Checked)
                    {
                        box[i].Enabled = true;
                    }

                    //disable p2
                    else if (box[i].CheckState == CheckState.Indeterminate) //|| box[i].CheckState == CheckState.Unchecked
                    {
                        box[i].Enabled = false;

                    }
                }

            }

        }



        private void regularbox(int num)
        {
            //determine what to do based on game progress

            if (moves < 6)
            {
                //if first part (placing pieces)

                //starting sequence with number of chkb as input
                startsequence(num);


                //add one to completed moves
                moves++;
            }

            else if (moves == 6)
            {
                //if sixth move
                move_six(num);

                //add one to completed moves
                moves++;
            }

            else if (moves >= 7)
            {
                //if move 7 or beyond

                //if move not in progress
                if (progress == false)
                {
                    //set move to in progress for next action
                    progress = true;


                    //disable and uncheck completley
                    box[num].CheckState = CheckState.Unchecked;
                    box[num].Enabled = false;


                    //disable all checkboxes except right beside and 0

                    for (int i = 0; i < box.Length; i++)
                    {
                        //ex if 4 is clicked, enable 5 (4+1) and 3 (4-1) and 0 
                        if (num + 1 == i || num - 1 == i || i == 0)
                        {
                            box[i].Enabled = true;
                        }
                        //if any of  boxes are check hide them
                        if (box[i].Checked)
                        {
                            box[i].Enabled = false;
                        }
                    }






                }
                else if (progress == true)
                {

                    //if move currently in progress
                    in_progress(num);

                    //check for winner
                    winnercheck();


                    nextplayer();

                    //add one to completed moves
                    moves++;

                }


            }
        }





        private void startsequence(int i)
        {
            //if checked, hide from selection
            box[i].Enabled = false;

            // if player one or two to determine chk (p1) or indetermined check (square)(p2)
            box[i].CheckState = puttingpiece(box[i]);

        }

        private void move_six(int i)
        {
            //if checked, hide from selection
            box[i].Enabled = false;

            // if player one or two (even though only p2 for move 6)
            box[i].CheckState = puttingpiece(box[i]);

            //further instructions
            MessageBox.Show("Now, take turns selecting a piece and moving it to an adjacent, unoccupied square");


            //disable all p2 and empty for next selection, enable p1
            Int_disable();

            //check for winner
            winnercheck();
        }

        private void Int_disable()
        {

            //loop to disable all p2 and empty squares
            //and enable p1 for selection

            for (int i = 0; i < box.Length; i++)
            {
                if (box[i].CheckState == CheckState.Checked)
                {
                    box[i].Enabled = true;
                }
                else if (box[i].CheckState == CheckState.Indeterminate)
                {
                    box[i].Enabled = false;
                }
                else if (box[i].CheckState == CheckState.Unchecked)
                {
                    box[i].Enabled = false;
                }
            }
        }




        private void in_progress(int num)
        {
            //set in progress to no
            progress = false;

            //if p1 move
            if ((moves % 2) == 1)
            {
                //check that box
                box[num].CheckState = CheckState.Checked;

                //set all p1 to disable for next move
                for (int i = 0; i < box.Length; i++)
                {
                    if (box[i].CheckState == CheckState.Checked)
                    {
                        box[i].Enabled = false;

                    }
                    //just incase enable p2
                    else if (box[i].CheckState == CheckState.Indeterminate)
                    {
                        box[i].Enabled = true;

                    }
                }


            }

            //p2
            else if ((moves % 2) == 0)
            {
                //indeterminate check that box
                box[num].CheckState = CheckState.Indeterminate;

                //set all p2 to enable for next move
                for (int i = 0; i < box.Length; i++)
                {
                    if (box[i].CheckState == CheckState.Checked)
                    {
                        box[i].Enabled = true;

                    }
                    //just disable p2
                    else if (box[i].CheckState == CheckState.Indeterminate)
                    {
                        box[i].Enabled = false;

                    }
                }
            }
        }



        private void winnercheck()
        {
            if (box[0].CheckState == CheckState.Checked)
            {
                //if middle square occupied by p1

                for(int i = 1; i < box.Length/2; i++)
                {
                    //check if sorounding squares line up to same player pieces
                    //ex box 2, box 0, & box 6 (2+4) all are p1
                    if (box[i].CheckState == CheckState.Checked && box[i+4].CheckState == CheckState.Checked)
                    {
                        MessageBox.Show("Congrats Player 1 you win!");
                    }

                }


            }
            else if (box[0].CheckState == CheckState.Indeterminate)
            {
                //if middle square occupied by p2

                for (int i = 1; i < box.Length / 2; i++)
                {
                    //check if sorounding squares line up to same player pieces
                    //ex box 3, box 0, & box 7 (3+4) all are p2
                    if (box[i].CheckState == CheckState.Indeterminate && box[i + 4].CheckState == CheckState.Indeterminate)
                    {
                        MessageBox.Show("Congrats Player 2 you win!");
                    }

                }
            }
        }







        private int checkedcount()
        {
            //method to check how many checkboxes checked

            //int to hold howmany checked
            int num = 0;

            //if checkbox is cheked update num
            if (chkb0.Checked) { num++; }
            if (chkb1.Checked) { num++; }
            if (chkb2.Checked) { num++; }
            if (chkb3.Checked) { num++; }
            if (chkb4.Checked) { num++; }
            if (chkb5.Checked) { num++; }
            if (chkb6.Checked) { num++; }
            if (chkb7.Checked) { num++; }
            if (chkb8.Checked) { num++; }

            return num;
        }

        private CheckState puttingpiece( CheckBox chkb )
        {


            if ((moves % 2) == 0) 
            {
                //it is p2 turn
                chkb.CheckState = CheckState.Indeterminate;
                
                

            }
            else if ((moves % 2) == 1)
            {
                //p1
                chkb.CheckState = CheckState.Checked;
                
            }


            //return p1 or p2 
            return chkb.CheckState;

        }








        private void chkb0_CheckedChanged(object sender, EventArgs e)
        {

            //This is a special case for chkb0


            //int to hold which chkb this is
            int num = 0;


            if (moves < 6)
            {
                //refer to comments in regular box method
                startsequence(num);
                moves++;

            }

            else if (moves == 6)
            {
                //refer to comments in regular box method
                move_six(num);
                moves++;

            }
            else if (moves >= 7)
            {
                //refer to comments in regular box method


                //if move not in progress
                if (progress == false)
                {
                    //set move to in progress
                    progress = true;

                    //disable and uncheck completley
                    box[num].CheckState = CheckState.Unchecked;
                    box[num].Enabled = false;


                    //diffrent from regular box method
                    for(int i = 0; i < box.Length; i++)
                    {
                        if (box[i].Checked)
                        {
                            //disable checked boxes
                            box[i].Enabled = false;
                        }
                        else if (box[i].CheckState == CheckState.Unchecked)
                        {
                            //enable uncheked
                            box[i].Enabled |= true;
                        }
                    }




                }
                else if (progress == true)
                {
                    in_progress(num);

                    winnercheck();


                    nextplayer();


                    moves++;

                }


            }





        }





        private void chkb1_CheckedChanged(object sender, EventArgs e)
        {

            //This is a special case for chkb1


            //int to hold which chkb this is
            int num = 1;



            if (moves < 6)
            {

                //refer to comments in regular box method
                startsequence(num);
                moves++;

            }

            else if (moves == 6)
            {
                //refer to comments in regular box method
                move_six(num);
                moves++;

            }
            else if (moves >= 7)
            {
                //refer to comments in regular box method

                //if move not in progress
                if (progress == false)
                {
                    //set move to in progress
                    progress = true;

                    //disable and uncheck completley
                    box[num].CheckState = CheckState.Unchecked;
                    box[num].Enabled = false;



                    //disable all checkboxes except right beside
                    //this is only diffrence from regular box method
                    for (int i = 0; i < box.Length; i++)
                    {
                        if (1 + 1 == i || i == 8 || i == 0)
                        {
                            box[i].Enabled = true;
                        }
                        if (box[i].Checked)
                        {
                            box[i].Enabled = false;
                        }
                    }






                }
                else if (progress == true)
                {
                    //refer to comments in regular box method

                    in_progress(num);

                    winnercheck();

                    nextplayer();

                    moves++;

                }

            }
                    
        }





        private void chkb2_CheckedChanged(object sender, EventArgs e)
        {
            //Same for every regular box
            //method with input of chkb
            regularbox(2);

        }

        private void chkb3_CheckedChanged(object sender, EventArgs e)
        {

            regularbox(3);

        }

        private void chkb4_CheckedChanged(object sender, EventArgs e)
        {

            regularbox(4);

        }

        private void chkb5_CheckedChanged(object sender, EventArgs e)
        {

            regularbox(5);
        }

        private void chkb6_CheckedChanged(object sender, EventArgs e)
        {

            regularbox(6);
        }

        private void chkb7_CheckedChanged(object sender, EventArgs e)
        {

            regularbox(7);
        }

        private void chkb8_CheckedChanged(object sender, EventArgs e)
        {

            //This is a special case for chkb8


            //int to hold which chkb this is
            int num = 8;



            if (moves < 6)
            {
                //refer to comments in regular box method

                startsequence(num);
                moves++;

            }

            else if (moves == 6)
            {
                //refer to comments in regular box method

                move_six(num);
                moves++;

            }
            else if (moves >= 7)
            {
                //refer to comments in regular box method

                //if move not in progress
                if (progress == false)
                {
                    //set move to in progress
                    progress = true;

                    //disable and uncheck completley
                    box[num].CheckState = CheckState.Unchecked;
                    box[num].Enabled = false;



                    //disable all checkboxes except right beside
                    //this is only diffrence to regular box method
                    for (int i = 0; i < box.Length; i++)
                    {
                        if (8 - 1 == i || i == 1 || i == 0)
                        {
                            box[i].Enabled = true;
                        }
                        if (box[i].Checked)
                        {
                            box[i].Enabled = false;
                        }
                    }






                }
                else if (progress == true)
                {
                    //refer to comments in regular box method

                    in_progress(num);

                    winnercheck();

                    nextplayer();


                    moves++;

                }


            }




        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Game instructions
            MessageBox.Show("Welcome to Rota!" +
                " This is a two player game, start by taking alternating turns placing your pieces." +
                " The aim of the game is to get three in a straight line, similar to tic tac toe.");
        }
    }
}
