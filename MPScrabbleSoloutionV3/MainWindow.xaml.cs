using System;
using System.Windows;
using System.Windows.Controls;

namespace MPScrabbleSoloutionV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // DemoMode will allow you to see the contents on he Scrabble Bag and the Player Tiles.
        private bool DemoMode = false;

        // The grid is the Scrabble Bag.
        private static string[,] grid = new string[10, 10];

        // This activePlayer is used to identify which tiles are displayed.
        private int activePlayer = 1;

        // Create all the Players even if they are not going to be used.
        private Player player1 = new Player();
        private Player player2 = new Player();
        private Player player3 = new Player();
        private Player player4 = new Player();

        // Create a Log
        private ReportLog Report = new ReportLog("ScrabbleGame");

        public MainWindow()
        {
            InitializeComponent();

            // Initialise the Grid\scrabble bag and the players
            InitiliseGame();

        }

        // **********************************
        // **********************************
        /// <summary>
        /// This initlises the Grid with the start letters
        /// </summary>
        public void InitiliseGrid()
        {
            // Alphabet string, may need to change this later if I want to allow for different languages
            string aString = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ__";
            // Convert the String to an array
            char[] letters = aString.ToCharArray();

            int count = 0;

            // Put the Letters into the grid
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    grid[row, column] = letters[count].ToString();
                    count++;
                }
            }

            UpdateLog("Letters add to the Bag", "Grid initialised with letters");
        }

        // **********************************
        /// <summary>
        /// Called when the Generate Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Easter Egg - active DemoMode
            string easterEgg = lbS00.Text + lbS01.Text + lbS02.Text + lbS03.Text + lbS04.Text + lbS05.Text + lbS06.Text + lbS07.Text + lbS08.Text + lbS09.Text;
            if (easterEgg.ToUpper() == "*DEMOMODE*")
            {
                DemoMode = true;
            }

            // generate Random Numbers
            var random = new Random();

            lbS00.Text = random.Next(0, 10).ToString();
            lbS01.Text = random.Next(0, 10).ToString();
            lbS02.Text = random.Next(0, 10).ToString();
            lbS03.Text = random.Next(0, 10).ToString();
            lbS04.Text = random.Next(0, 10).ToString();
            lbS05.Text = random.Next(0, 10).ToString();
            lbS06.Text = random.Next(0, 10).ToString();
            lbS07.Text = random.Next(0, 10).ToString();
            lbS08.Text = random.Next(0, 10).ToString();
            lbS09.Text = random.Next(0, 10).ToString();

            UpdateLog("Generate button pressed", "?");
        }

        // **********************************
        /// <summary>
        /// Called when the Shunt Button is pressed
        /// </summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShunt_Click(object sender, RoutedEventArgs e)
        {
            // Disable the generate button one the shunt button is pressed
            btnGenerate.IsEnabled = false;

            // Shunt the columns down based on the random numbers that were generated.
            ShuntColumns(int.Parse(lbS00.Text), 0);
            ShuntColumns(int.Parse(lbS01.Text), 1);
            ShuntColumns(int.Parse(lbS02.Text), 2);
            ShuntColumns(int.Parse(lbS03.Text), 3);
            ShuntColumns(int.Parse(lbS04.Text), 4);
            ShuntColumns(int.Parse(lbS05.Text), 5);
            ShuntColumns(int.Parse(lbS06.Text), 6);
            ShuntColumns(int.Parse(lbS07.Text), 7);
            ShuntColumns(int.Parse(lbS08.Text), 8);
            ShuntColumns(int.Parse(lbS09.Text), 9);

            DisplayGrid();

            UpdateLog("Shunt button pressed", "Columns shunted according to the Random numbers");
            UpdateLog("?", lbS00.Text + ", " + lbS01.Text + ", " + lbS02.Text + ", " + lbS03.Text + ", " +
                lbS04.Text + ", " + lbS05.Text + ", " + lbS06.Text + ", " + lbS07.Text + ", " + lbS08.Text + ", " +
                lbS09.Text);
        }

        // **********************************
        /// <summary>
        /// Moves a column of the array down
        /// </summary>
        /// <param name="move">number of moves to take</param>
        /// <param name="column">Column to change</param>
        public static void ShuntColumns(int move, int column)
        {
            if (move > 0)
            {
                for (int k = 0; k < move; k++)
                {
                    // store the bottom number of the column
                    string hold = grid[9, column];

                    //shunt the numbers in the column down 1
                    for (int row = 9; row > 0; row--)
                    {
                        grid[row, column] = grid[row - 1, column];
                    }

                    // move the stored number to the top of the column
                    grid[0, column] = hold;
                }
            }
        }

        // **********************************
        /// <summary>
        /// Called when the Shuffle Button is pressed
        /// </summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            // Disable the Shunt button once the Shuffle button is pressed.
            btnShunt.IsEnabled = false;

            // convert the ComboBoxes to numbers
            int sh1 = int.Parse(cbSh1.Text);
            int sh2 = int.Parse(cbSh2.Text);

            // shuffle the Columns and rows based on the first number
            for (int i = sh1 - 1; i < 10; i = i + sh1)
            {
                ShuffleColumns(i);
            }

            for (int i = sh1 - 1; i < 10; i = i + sh1)
            {
                ShuffleRows(i);
            }

            // shuffle the Columns and rows based on the second number
            for (int i = sh2 - 1; i < 10; i = i + sh2)
            {
                ShuffleColumns(i);
            }

            for (int i = sh2 - 1; i < 10; i = i + sh2)
            {
                ShuffleRows(i);
            }

            SortGrid();
            DisplayGrid();

            UpdateLog("Shuffle button pressed", "Columns and Rows Shuffle in a " + cbSh1.Text + " " + cbSh2.Text + " Sequence.");
        }

        // **********************************
        /// <summary>
        /// Take the Column and move to the front of the array
        /// </summary>
        /// <param name="col">Column to move</param>
        public static void ShuffleColumns(int col)
        {
            if (col > 0)
            {
                //  int[] columnSave = new int[10];

                for (int j = 1; j < 10; j = j + col)
                {
                    for (int i = j; i > 0; i--)
                    {
                        MoveColumn(i, i - 1);
                    }
                }
            }
        }

        // **********************************
        /// <summary>
        /// Move the Column
        /// </summary>
        /// <param name="from">from column</param>
        /// <param name="to">to column</param>
        public static void MoveColumn(int from, int to)
        {
            // store column
            string[] columnSave = new string[10];

            // move column
            for (int row = 0; row < 10; row++)
            {
                columnSave[row] = grid[row, from];

                for (int column = from; column > to; column--)
                {
                    grid[row, column] = grid[row, column - 1];
                }

                grid[row, to] = columnSave[row];
            }
        }

        // **********************************
        /// <summary>
        /// Move a row to the front of the array
        /// </summary>
        /// <param name="moves"></param>
        public static void ShuffleRows(int moves)
        {
            if (moves > 0)
            {
                // int[] rowSave = new int[10];

                for (int j = 1; j < 10; j = j + moves)
                {
                    for (int i = j; i > 0; i--)
                    {
                        MoveRow(i, i - 1);
                    }
                }
            }
        }

        // **********************************
        /// <summary>
        /// Move the Row
        /// </summary>
        /// <param name="from">from row</param>
        /// <param name="to">to row</param>
        public static void MoveRow(int from, int to)
        {
            // store column
            string[] rowSave = new string[10];

            // move column
            for (int column = 0; column < 10; column++)
            {
                rowSave[column] = grid[from, column];

                for (int row = from; row > to; row--)
                {
                    grid[row, column] = grid[row - 1, column];
                }

                grid[to, column] = rowSave[column];
            }
        }

        // **********************************
        /// <summary>
        /// Re-display the Grid\Bag of letters on screen
        /// </summary>
        public void DisplayGrid()
        {
            // Sort grid to move all the space to the end

            // Count Letters in the Bag
            int count = CountLetters();

            lbBagCount.Content = "(" + count.ToString() + ")";

            // display the grid
            tbBag.Text = "";

            for (int column = 0; column < 10; column++)
            {
                for (int row = 0; row < 10; row++)
                {
                    if (DemoMode == true)
                    {
                        tbBag.Text += "  " + grid[column, row].ToString();
                    }
                    else
                    {
                        if (grid[column, row].ToString() != " ")
                        {
                            tbBag.Text += "  +";
                        }
                    }
                }
                tbBag.Text += "\n";
            }

            tbBag.Text += "\n";
        }

        // **********************************
        /// <summary>
        /// Sort the Grid\Bag of letters
        /// </summary>
        private void SortGrid()
        {
            if (CountLetters() > 0)
            {
                // Sort the grid, to move all the spaces to the far end.
                int fColumn;
                int fRow;

                // do until there are no more space to move
                do
                {
                    for (int column = 0; column < 10; column++)
                    {
                        for (int row = 0; row < 10; row++)
                        {
                            if (row != 9)
                            {
                                fColumn = column;
                                fRow = row + 1;
                            }
                            else
                            {
                                fColumn = column + 1;
                                fRow = 0;
                            }

                            if (fColumn != 10)
                            {
                                if (grid[column, row] == " ")
                                {
                                    grid[column, row] = grid[fColumn, fRow];
                                    grid[fColumn, fRow] = " ";
                                }
                            }
                            else
                            {
                                if (grid[9, 9] == " ")
                                {
                                    grid[9, 9] = " ";
                                }
                            }
                        }
                    }
                } while (grid[0, 0] == " ");
            }
        }

        // **********************************
        /// <summary>
        /// Count the number of letters in the Grid\Bag
        /// </summary>
        /// <returns>Number of letters</returns>
        public int CountLetters()
        {
            // Count the letter still in the grid and return the number

            int letterCount = 0;

            for (int column = 0; column < 10; column++)
            {
                for (int row = 0; row < 10; row++)
                {
                    if (grid[column, row] != " ")
                    {
                        letterCount++;
                    }
                }
            }
            return letterCount;
        }

        // **********************************
        /// <summary>
        /// Initilise the game at Start and at Reset
        /// </summary>
        public void InitiliseGame()
        {
            // Reset the Grid

            InitiliseGrid();

            // Reset the players and the buttons for the start of the game

            player1.Name = tbPlayer1.Text;
            player2.Name = tbPlayer2.Text;
            player3.Name = tbPlayer3.Text;
            player4.Name = tbPlayer4.Text;

            btnTake3.IsEnabled = false;
            btnTake4.IsEnabled = false;

            activePlayer = 1;

            UpdateLog("Players Initilised", "?");
        }

        // **********************************
        /// <summary>
        /// Player 1 Take Button Routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake1Click(object sender, RoutedEventArgs e)
        {
            string letters = "";

            UpdateLog($"{tbPlayer1.Text} takes {cbTake1.Text}", "?");

            int take = int.Parse(cbTake1.Text);
            if (take != 0)
            {
                // take the letter from the bag and put in the players rack
                if (take <= CountLetters())
                {
                    for (int i = 0; i < take; i++)
                    {
                        letters += grid[0, i];
                        grid[0, i] = " ";
                    }

                    player1.TakeLetters(letters);

                    // re-display the bah and update the player rack
                    SortGrid();
                    DisplayGrid();
                    UpdatePlay1();

                    cbTake1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Not enough letter in the bag!!", "Scrabble Solution", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    UpdateLog("Not enough letter in the bag!!", "?");
                }
            }
        }

        // **********************************
        /// <summary>
        /// Player 2 Take Button Routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake2Click(object sender, RoutedEventArgs e)
        {
            string letters = "";

            UpdateLog($"{tbPlayer2.Text} takes {cbTake2.Text}", "?");

            int take = int.Parse(cbTake2.Text);
            if (take != 0)
            {
                // take the letter from the bag and put in the players rack
                if (take <= CountLetters())
                {
                    for (int i = 0; i < take; i++)
                    {
                        letters += grid[0, i];
                        grid[0, i] = " ";
                    }

                    player2.TakeLetters(letters);

                    // re-display the bah and update the player rack
                    SortGrid();
                    DisplayGrid();
                    UpdatePlay2();

                    cbTake2.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Not enough letter in the bag!!", "Scrabble Solution", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    UpdateLog("Not enough letter in the bag!!", "?");
                }
            }
        }

        // **********************************
        /// <summary>
        /// Player 3 Take Button Routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake3Click(object sender, RoutedEventArgs e)
        {
            string letters = "";

            UpdateLog($"{tbPlayer3.Text} takes {cbTake3.Text}", "?");

            int take = int.Parse(cbTake3.Text);
            if (take != 0)
            {
                // take the letter from the bag and put in the players rack
                if (take <= CountLetters())
                {
                    for (int i = 0; i < take; i++)
                    {
                        letters += grid[0, i];
                        grid[0, i] = " ";
                    }

                    player3.TakeLetters(letters);

                    // re-display the bah and update the player rack
                    SortGrid();
                    DisplayGrid();
                    UpdatePlay3();

                    cbTake3.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Not enough letter in the bag!!", "Scrabble Solution", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    UpdateLog("Not enough letter in the bag!!", "?");
                }
            }
        }

        // **********************************
        /// <summary>
        /// Player 4 Take Button Routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake4Click(object sender, RoutedEventArgs e)
        {
            string letters = "";

            UpdateLog($"{tbPlayer4.Text} takes {cbTake4.Text}", "?");

            int take = int.Parse(cbTake4.Text);
            if (take != 0)
            {
                // take the letter from the bag and put in the players rack
                if (take <= CountLetters())
                {
                    for (int i = 0; i < take; i++)
                    {
                        letters += grid[0, i];
                        grid[0, i] = " ";
                    }

                    player4.TakeLetters(letters);

                    // re-display the bah and update the player rack
                    SortGrid();
                    DisplayGrid();
                    UpdatePlay4();

                    cbTake4.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Not enough letter in the bag!!", "Scrabble Solution", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    UpdateLog("Not enough letter in the bag!!", "?");
                }
            }
        }

        // **********************************
        /// <summary>
        /// Routine to reassign the name of Player 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPlayer1Changed(object sender, TextChangedEventArgs e)
        {
            player1.Name = tbPlayer1.Text;
        }

        // **********************************
        private void tbPlayer1LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateLog("Player 1 name changed to " + tbPlayer1.Text, "?");
        }

        // **********************************
        /// <summary>
        /// Routine to reassign the name of Player 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPlayer2Changed(object sender, TextChangedEventArgs e)
        {
            player2.Name = tbPlayer2.Text;
        }

        // **********************************
        private void tbPlayer2LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateLog("Player 2 name changed to " + tbPlayer2.Text, "?");
        }

        // **********************************
        /// <summary>
        /// Routine to reassign the name of Player 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPlayer3Changed(object sender, TextChangedEventArgs e)
        {
            player3.Name = tbPlayer3.Text;
        }

        // **********************************
        private void tbPlayer3LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateLog("Player 3 name changed to " + tbPlayer3.Text, "?");
        }

        // **********************************
        /// <summary>
        /// Routine to reassign the name of Player 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPlayer4Changed(object sender, TextChangedEventArgs e)
        {
            player4.Name = tbPlayer4.Text;
        }

        // **********************************
        private void tbPlayer4LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateLog("Player 4 name changed to " + tbPlayer4.Text, "?");
        }

        // **********************************
        /// <summary>
        /// Update the Player 1 Array with their letters
        /// </summary>
        public void UpdatePlay1()
        {
            // Update the Player Rack with the correct letters
            if (player1.TotalLetters > 0)
            {
                char[] c = player1.MyLetters.ToCharArray();

                UpdateLog("?", "Player 1 has letters: " + player1.MyLetters);

                for (int i = player1.TotalLetters; i > 0; i--)
                {
                    switch (i)
                    {
                        case 70:
                            UpdateLabel(1, p169, c[69]);
                            break;

                        case 69:
                            UpdateLabel(1, p168, c[68]);
                            break;

                        case 68:
                            UpdateLabel(1, p167, c[67]);
                            break;

                        case 67:
                            UpdateLabel(1, p166, c[66]);
                            break;

                        case 66:
                            UpdateLabel(1, p165, c[65]);
                            break;

                        case 65:
                            UpdateLabel(1, p164, c[64]);
                            break;

                        case 64:
                            UpdateLabel(1, p163, c[63]);
                            break;

                        case 63:
                            UpdateLabel(1, p162, c[62]);
                            break;

                        case 62:
                            UpdateLabel(1, p161, c[61]);
                            break;

                        case 61:
                            UpdateLabel(1, p160, c[60]);
                            break;

                        case 60:
                            UpdateLabel(1, p159, c[59]);
                            break;

                        case 59:
                            UpdateLabel(1, p158, c[58]);
                            break;

                        case 58:
                            UpdateLabel(1, p157, c[57]);
                            break;

                        case 57:
                            UpdateLabel(1, p156, c[56]);
                            break;

                        case 56:
                            UpdateLabel(1, p155, c[55]);
                            break;

                        case 55:
                            UpdateLabel(1, p154, c[54]);
                            break;

                        case 54:
                            UpdateLabel(1, p153, c[53]);
                            break;

                        case 53:
                            UpdateLabel(1, p152, c[52]);
                            break;

                        case 52:
                            UpdateLabel(1, p151, c[51]);
                            break;

                        case 51:
                            UpdateLabel(1, p150, c[50]);
                            break;

                        case 50:
                            UpdateLabel(1, p149, c[49]);
                            break;

                        case 49:
                            UpdateLabel(1, p148, c[48]);
                            break;

                        case 48:
                            UpdateLabel(1, p147, c[47]);
                            break;

                        case 47:
                            UpdateLabel(1, p146, c[46]);
                            break;

                        case 46:
                            UpdateLabel(1, p145, c[45]);
                            break;

                        case 45:
                            UpdateLabel(1, p144, c[44]);
                            break;

                        case 44:
                            UpdateLabel(1, p143, c[43]);
                            break;

                        case 43:
                            UpdateLabel(1, p142, c[42]);
                            break;

                        case 42:
                            UpdateLabel(1, p141, c[41]);
                            break;

                        case 41:
                            UpdateLabel(1, p140, c[40]);
                            break;

                        case 40:
                            UpdateLabel(1, p139, c[39]);
                            break;

                        case 39:
                            UpdateLabel(1, p138, c[38]);
                            break;

                        case 38:
                            UpdateLabel(1, p137, c[37]);
                            break;

                        case 37:
                            UpdateLabel(1, p136, c[36]);
                            break;

                        case 36:
                            UpdateLabel(1, p135, c[35]);
                            break;

                        case 35:
                            UpdateLabel(1, p134, c[34]);
                            break;

                        case 34:
                            UpdateLabel(1, p133, c[33]);
                            break;

                        case 33:
                            UpdateLabel(1, p132, c[32]);
                            break;

                        case 32:
                            UpdateLabel(1, p131, c[31]);
                            break;

                        case 31:
                            UpdateLabel(1, p130, c[30]);
                            break;

                        case 30:
                            UpdateLabel(1, p129, c[29]);
                            break;

                        case 29:
                            UpdateLabel(1, p128, c[28]);
                            break;

                        case 28:
                            UpdateLabel(1, p127, c[27]);
                            break;

                        case 27:
                            UpdateLabel(1, p126, c[26]);
                            break;

                        case 26:
                            UpdateLabel(1, p125, c[25]);
                            break;

                        case 25:
                            UpdateLabel(1, p124, c[24]);
                            break;

                        case 24:
                            UpdateLabel(1, p123, c[23]);
                            break;

                        case 23:
                            UpdateLabel(1, p122, c[22]);
                            break;

                        case 22:
                            UpdateLabel(1, p121, c[21]);
                            break;

                        case 21:
                            UpdateLabel(1, p120, c[20]);
                            break;

                        case 20:
                            UpdateLabel(1, p119, c[19]);
                            break;

                        case 19:
                            UpdateLabel(1, p118, c[18]);
                            break;

                        case 18:
                            UpdateLabel(1, p117, c[17]);
                            break;

                        case 17:
                            UpdateLabel(1, p116, c[16]);
                            break;

                        case 16:
                            UpdateLabel(1, p115, c[15]);
                            break;

                        case 15:
                            UpdateLabel(1, p114, c[14]);
                            break;

                        case 14:
                            UpdateLabel(1, p113, c[13]);
                            break;

                        case 13:
                            UpdateLabel(1, p112, c[12]);
                            break;

                        case 12:
                            UpdateLabel(1, p111, c[11]);
                            break;

                        case 11:
                            UpdateLabel(1, p110, c[10]);
                            break;

                        case 10:
                            UpdateLabel(1, p109, c[9]);
                            break;

                        case 9:
                            UpdateLabel(1, p108, c[8]);
                            break;

                        case 8:
                            UpdateLabel(1, p107, c[7]);
                            break;

                        case 7:
                            UpdateLabel(1, p106, c[6]);
                            break;

                        case 6:
                            UpdateLabel(1, p105, c[5]);
                            break;

                        case 5:
                            UpdateLabel(1, p104, c[4]);
                            break;

                        case 4:
                            UpdateLabel(1, p103, c[3]);
                            break;

                        case 3:
                            UpdateLabel(1, p102, c[2]);
                            break;

                        case 2:
                            UpdateLabel(1, p101, c[1]);
                            break;

                        case 1:
                            UpdateLabel(1, p100, c[0]);
                            break;
                    }
                }
            }
        }

        // **********************************
        /// <summary>
        /// Update the Player 2 Array with their letters
        /// </summary>
        public void UpdatePlay2()
        {
            // Update the Player Rack with the correct letters
            if (player2.TotalLetters > 0)
            {
                char[] c = player2.MyLetters.ToCharArray();

                UpdateLog("?", "Player 2 has letters: " + player2.MyLetters);

                for (int i = player2.TotalLetters; i > 0; i--)
                {
                    switch (i)
                    {
                        case 70:
                            UpdateLabel(2, p269, c[69]);
                            break;

                        case 69:
                            UpdateLabel(2, p268, c[68]);
                            break;

                        case 68:
                            UpdateLabel(2, p267, c[67]);
                            break;

                        case 67:
                            UpdateLabel(2, p266, c[66]);
                            break;

                        case 66:
                            UpdateLabel(2, p265, c[65]);
                            break;

                        case 65:
                            UpdateLabel(2, p264, c[64]);
                            break;

                        case 64:
                            UpdateLabel(2, p263, c[63]);
                            break;

                        case 63:
                            UpdateLabel(2, p262, c[62]);
                            break;

                        case 62:
                            UpdateLabel(2, p261, c[61]);
                            break;

                        case 61:
                            UpdateLabel(2, p260, c[60]);
                            break;

                        case 60:
                            UpdateLabel(2, p259, c[59]);
                            break;

                        case 59:
                            UpdateLabel(2, p258, c[58]);
                            break;

                        case 58:
                            UpdateLabel(2, p257, c[57]);
                            break;

                        case 57:
                            UpdateLabel(2, p256, c[56]);
                            break;

                        case 56:
                            UpdateLabel(2, p255, c[55]);
                            break;

                        case 55:
                            UpdateLabel(2, p254, c[54]);
                            break;

                        case 54:
                            UpdateLabel(2, p253, c[53]);
                            break;

                        case 53:
                            UpdateLabel(2, p252, c[52]);
                            break;

                        case 52:
                            UpdateLabel(2, p251, c[51]);
                            break;

                        case 51:
                            UpdateLabel(2, p250, c[50]);
                            break;

                        case 50:
                            UpdateLabel(2, p249, c[49]);
                            break;

                        case 49:
                            UpdateLabel(2, p248, c[48]);
                            break;

                        case 48:
                            UpdateLabel(2, p247, c[47]);
                            break;

                        case 47:
                            UpdateLabel(2, p246, c[46]);
                            break;

                        case 46:
                            UpdateLabel(2, p245, c[45]);
                            break;

                        case 45:
                            UpdateLabel(2, p244, c[44]);
                            break;

                        case 44:
                            UpdateLabel(2, p243, c[43]);
                            break;

                        case 43:
                            UpdateLabel(2, p242, c[42]);
                            break;

                        case 42:
                            UpdateLabel(2, p241, c[41]);
                            break;

                        case 41:
                            UpdateLabel(2, p240, c[40]);
                            break;

                        case 40:
                            UpdateLabel(2, p239, c[39]);
                            break;

                        case 39:
                            UpdateLabel(2, p238, c[38]);
                            break;

                        case 38:
                            UpdateLabel(2, p237, c[37]);
                            break;

                        case 37:
                            UpdateLabel(2, p236, c[36]);
                            break;

                        case 36:
                            UpdateLabel(2, p235, c[35]);
                            break;

                        case 35:
                            UpdateLabel(2, p234, c[34]);
                            break;

                        case 34:
                            UpdateLabel(2, p233, c[33]);
                            break;

                        case 33:
                            UpdateLabel(2, p232, c[32]);
                            break;

                        case 32:
                            UpdateLabel(2, p231, c[31]);
                            break;

                        case 31:
                            UpdateLabel(2, p230, c[30]);
                            break;

                        case 30:
                            UpdateLabel(2, p229, c[29]);
                            break;

                        case 29:
                            UpdateLabel(2, p228, c[28]);
                            break;

                        case 28:
                            UpdateLabel(2, p227, c[27]);
                            break;

                        case 27:
                            UpdateLabel(2, p226, c[26]);
                            break;

                        case 26:
                            UpdateLabel(2, p225, c[25]);
                            break;

                        case 25:
                            UpdateLabel(2, p224, c[24]);
                            break;

                        case 24:
                            UpdateLabel(2, p223, c[23]);
                            break;

                        case 23:
                            UpdateLabel(2, p222, c[22]);
                            break;

                        case 22:
                            UpdateLabel(2, p221, c[21]);
                            break;

                        case 21:
                            UpdateLabel(2, p220, c[20]);
                            break;

                        case 20:
                            UpdateLabel(2, p219, c[19]);
                            break;

                        case 19:
                            UpdateLabel(2, p218, c[18]);
                            break;

                        case 18:
                            UpdateLabel(2, p217, c[17]);
                            break;

                        case 17:
                            UpdateLabel(2, p216, c[16]);
                            break;

                        case 16:
                            UpdateLabel(2, p215, c[15]);
                            break;

                        case 15:
                            UpdateLabel(2, p214, c[14]);
                            break;

                        case 14:
                            UpdateLabel(2, p213, c[13]);
                            break;

                        case 13:
                            UpdateLabel(2, p212, c[12]);
                            break;

                        case 12:
                            UpdateLabel(2, p211, c[11]);
                            break;

                        case 11:
                            UpdateLabel(2, p210, c[10]);
                            break;

                        case 10:
                            UpdateLabel(2, p209, c[9]);
                            break;

                        case 9:
                            UpdateLabel(2, p208, c[8]);
                            break;

                        case 8:
                            UpdateLabel(2, p207, c[7]);
                            break;

                        case 7:
                            UpdateLabel(2, p206, c[6]);
                            break;

                        case 6:
                            UpdateLabel(2, p205, c[5]);
                            break;

                        case 5:
                            UpdateLabel(2, p204, c[4]);
                            break;

                        case 4:
                            UpdateLabel(2, p203, c[3]);
                            break;

                        case 3:
                            UpdateLabel(2, p202, c[2]);
                            break;

                        case 2:
                            UpdateLabel(2, p201, c[1]);
                            break;

                        case 1:
                            UpdateLabel(2, p200, c[0]);
                            break;
                    }
                }
            }
        }

        // **********************************
        /// <summary>
        /// Update the Player 3 Array with their letters
        /// </summary>
        public void UpdatePlay3()
        {
            // Update the Player Rack with the correct letters

            if (player3.TotalLetters > 0)
            {
                char[] c = player3.MyLetters.ToCharArray();

                UpdateLog("?", "Player 3 has letters: " + player3.MyLetters);

                for (int i = player3.TotalLetters; i > 0; i--)
                {
                    switch (i)
                    {
                        case 40:
                            UpdateLabel(3, p339, c[39]);
                            break;

                        case 39:
                            UpdateLabel(3, p338, c[38]);
                            break;

                        case 38:
                            UpdateLabel(3, p337, c[37]);
                            break;

                        case 37:
                            UpdateLabel(3, p336, c[36]);
                            break;

                        case 36:
                            UpdateLabel(3, p335, c[35]);
                            break;

                        case 35:
                            UpdateLabel(3, p334, c[34]);
                            break;

                        case 34:
                            UpdateLabel(3, p333, c[33]);
                            break;

                        case 33:
                            UpdateLabel(3, p332, c[32]);
                            break;

                        case 32:
                            UpdateLabel(3, p331, c[31]);
                            break;

                        case 31:
                            UpdateLabel(3, p330, c[30]);
                            break;

                        case 30:
                            UpdateLabel(3, p329, c[29]);
                            break;

                        case 29:
                            UpdateLabel(3, p328, c[28]);
                            break;

                        case 28:
                            UpdateLabel(3, p327, c[27]);
                            break;

                        case 27:
                            UpdateLabel(3, p326, c[26]);
                            break;

                        case 26:
                            UpdateLabel(3, p325, c[25]);
                            break;

                        case 25:
                            UpdateLabel(3, p324, c[24]);
                            break;

                        case 24:
                            UpdateLabel(3, p323, c[23]);
                            break;

                        case 23:
                            UpdateLabel(3, p322, c[22]);
                            break;

                        case 22:
                            UpdateLabel(3, p321, c[21]);
                            break;

                        case 21:
                            UpdateLabel(3, p320, c[20]);
                            break;

                        case 20:
                            UpdateLabel(3, p319, c[19]);
                            break;

                        case 19:
                            UpdateLabel(3, p318, c[18]);
                            break;

                        case 18:
                            UpdateLabel(3, p317, c[17]);
                            break;

                        case 17:
                            UpdateLabel(3, p316, c[16]);
                            break;

                        case 16:
                            UpdateLabel(3, p315, c[15]);
                            break;

                        case 15:
                            UpdateLabel(3, p314, c[14]);
                            break;

                        case 14:
                            UpdateLabel(3, p313, c[13]);
                            break;

                        case 13:
                            UpdateLabel(3, p312, c[12]);
                            break;

                        case 12:
                            UpdateLabel(3, p311, c[11]);
                            break;

                        case 11:
                            UpdateLabel(3, p310, c[10]);
                            break;

                        case 10:
                            UpdateLabel(3, p309, c[9]);
                            break;

                        case 9:
                            UpdateLabel(3, p308, c[8]);
                            break;

                        case 8:
                            UpdateLabel(3, p307, c[7]);
                            break;

                        case 7:
                            UpdateLabel(3, p306, c[6]);
                            break;

                        case 6:
                            UpdateLabel(3, p305, c[5]);
                            break;

                        case 5:
                            UpdateLabel(3, p304, c[4]);
                            break;

                        case 4:
                            UpdateLabel(3, p303, c[3]);
                            break;

                        case 3:
                            UpdateLabel(3, p302, c[2]);
                            break;

                        case 2:
                            UpdateLabel(3, p301, c[1]);
                            break;

                        case 1:
                            UpdateLabel(3, p300, c[0]);
                            break;
                    }
                }
            }
        }

        // **********************************
        /// <summary>
        /// Update the Player 4 Array with their letters
        /// </summary>
        public void UpdatePlay4()
        {
            // Update the Player Rack with the correct letters
            if (player4.TotalLetters > 0)
            {
                char[] c = player4.MyLetters.ToCharArray();

                UpdateLog("?", "Player 4 has letters: " + player4.MyLetters);

                for (int i = player4.TotalLetters; i > 0; i--)
                {
                    switch (i)
                    {
                        case 40:
                            UpdateLabel(4, p439, c[39]);
                            break;

                        case 39:
                            UpdateLabel(4, p438, c[38]);
                            break;

                        case 38:
                            UpdateLabel(4, p437, c[37]);
                            break;

                        case 37:
                            UpdateLabel(4, p436, c[36]);
                            break;

                        case 36:
                            UpdateLabel(4, p435, c[35]);
                            break;

                        case 35:
                            UpdateLabel(4, p434, c[34]);
                            break;

                        case 34:
                            UpdateLabel(4, p433, c[33]);
                            break;

                        case 33:
                            UpdateLabel(4, p432, c[32]);
                            break;

                        case 32:
                            UpdateLabel(4, p431, c[31]);
                            break;

                        case 31:
                            UpdateLabel(4, p430, c[30]);
                            break;

                        case 30:
                            UpdateLabel(4, p429, c[29]);
                            break;

                        case 29:
                            UpdateLabel(4, p428, c[28]);
                            break;

                        case 28:
                            UpdateLabel(4, p427, c[27]);
                            break;

                        case 27:
                            UpdateLabel(4, p426, c[26]);
                            break;

                        case 26:
                            UpdateLabel(4, p425, c[25]);
                            break;

                        case 25:
                            UpdateLabel(4, p424, c[24]);
                            break;

                        case 24:
                            UpdateLabel(4, p423, c[23]);
                            break;

                        case 23:
                            UpdateLabel(4, p422, c[22]);
                            break;

                        case 22:
                            UpdateLabel(4, p421, c[21]);
                            break;

                        case 21:
                            UpdateLabel(4, p420, c[20]);
                            break;

                        case 20:
                            UpdateLabel(4, p419, c[19]);
                            break;

                        case 19:
                            UpdateLabel(4, p418, c[18]);
                            break;

                        case 18:
                            UpdateLabel(4, p417, c[17]);
                            break;

                        case 17:
                            UpdateLabel(4, p416, c[16]);
                            break;

                        case 16:
                            UpdateLabel(4, p415, c[15]);
                            break;

                        case 15:
                            UpdateLabel(4, p414, c[14]);
                            break;

                        case 14:
                            UpdateLabel(4, p413, c[13]);
                            break;

                        case 13:
                            UpdateLabel(4, p412, c[12]);
                            break;

                        case 12:
                            UpdateLabel(4, p411, c[11]);
                            break;

                        case 11:
                            UpdateLabel(4, p410, c[10]);
                            break;

                        case 10:
                            UpdateLabel(4, p409, c[9]);
                            break;

                        case 9:
                            UpdateLabel(4, p408, c[8]);
                            break;

                        case 8:
                            UpdateLabel(4, p407, c[7]);
                            break;

                        case 7:
                            UpdateLabel(4, p406, c[6]);
                            break;

                        case 6:
                            UpdateLabel(4, p405, c[5]);
                            break;

                        case 5:
                            UpdateLabel(4, p404, c[4]);
                            break;

                        case 4:
                            UpdateLabel(4, p403, c[3]);
                            break;

                        case 3:
                            UpdateLabel(4, p402, c[2]);
                            break;

                        case 2:
                            UpdateLabel(4, p401, c[1]);
                            break;

                        case 1:
                            UpdateLabel(4, p400, c[0]);
                            break;
                    }
                }
            }
        }

        // **********************************
        /// <summary>
        /// Display only the current players letter unless in DemoMode
        /// </summary>
        /// <param name="cPlayer">Player Number</param>
        /// <param name="lab">Label Name</param>
        /// <param name="letter">Letter to display</param>
        public void UpdateLabel(int cPlayer, Label lab, char letter)
        {
            // If this is the current player or DemoMode is *ON the display the players rack
            if (DemoMode == true || activePlayer == cPlayer)
            {
                lab.Content = letter.ToString();
            }
            else
            {
                lab.Content = "+";
            }
        }

        // **********************************
        /// <summary>
        /// Routine to run when the Print Log Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntPrtLog_Click(object sender, RoutedEventArgs e)
        {
            Report.PrintLog();
        }

        // **********************************
        /// <summary>
        /// Routine to run when the Reset Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntReset_Click(object sender, RoutedEventArgs e)
        {
            Report.Reset();

            UpdateLog("Reset Button Pressed", "?");
        }

        // **********************************
        /// <summary>
        /// Routine to run when the Info Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            new InfoWindow().Show();
        }

        // **********************************
        /// <summary>
        /// Routine to run when the number of player change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbPlayerNbr_Click(object sender, RoutedEventArgs e)
        {
            if (rbPN2.IsChecked == true)
            {
                btnTake3.IsEnabled = false;
                btnTake4.IsEnabled = false;
                UpdateLog("Number of players changed to 2.", "?");
            }
            else if (rbPN3.IsChecked == true)
            {
                btnTake3.IsEnabled = true;
                btnTake4.IsEnabled = false;
                UpdateLog("Number of players changed to 3.", "?");
            }
            else if (rbPN4.IsChecked == true)
            {
                btnTake3.IsEnabled = true;
                btnTake4.IsEnabled = true;
                UpdateLog("Number of players changed to 4.", "?");
            }
        }

        // **********************************
        /// <summary>
        /// Routine to run when the Current Player is change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbCurrentPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (rbCP1.IsChecked == true)
            {
                activePlayer = 1;
                UpdateLog("Current players changed to 1.", "?");
            }
            else if (rbCP2.IsChecked == true)
            {
                activePlayer = 2;
                UpdateLog("Current players changed to 2.", "?");
            }
            else if (rbCP3.IsChecked == true)
            {
                activePlayer = 3;
                UpdateLog("Current players changed to 3.", "?");
            }
            else if (rbCP4.IsChecked == true)
            {
                activePlayer = 4;
                UpdateLog("Current players changed to 4.", "?");
            }
        }

        // **********************************
        /// <summary>
        /// Update the status bar and the writes to the log table
        /// </summary>
        /// <param name="status">Status_Bar Message</param>
        /// <param name="detail">Detail Message</param>
        public void UpdateLog(string status, string detail)
        {
            if (status != "?")
            {
                lblStatusBarText.Text = status;
            }

            Report.AddEntry(status, detail);
        }

        // **********************************
    }
}