using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if player 1 has the turn
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;


        #endregion
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
             
        }
        #endregion

        /// <summary>
        /// Starts a new game and clears everything
        /// </summary>
        private void NewGame() 
        {
            //Create new blank array
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure player 1 is current player
            mPlayer1Turn = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            // Make sure the game hasn't finished
            mGameEnded = false;
        }

       /// <summary>
       /// Handles a button click event
       /// </summary>
       /// <param name="sender">The button that was clicked</param>
       /// <param name="e"> The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded) 
            {
                NewGame();
                return;
            }

            //Cast the sender to a button
            var button = (Button)sender;

            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Prevent clicked squares from changing
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Send button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change 0 to green

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;


            //Change player turn after setting X or O                      
            mPlayer1Turn ^= true;

            CheckForWinner();
            {

            }
        }
        /// <summary>
        /// Checks if there is a winner of TicTacToe
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private void CheckForWinner()
        {
            #region Horizontal wins
            //Check for horizontal wins

            //Row 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Row 2

            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            { //Game ends
            mGameEnded = true;

            //Highlight winning cells in green
            Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            
            //Row 3
            if (mResults[4] != MarkType.Free && (mResults[4] & (mResults[5] & mResults[6])) == mResults[4])
            { //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical wins
            // Check for Vertical wins

            //Column 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            
            //Column 2
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Column 3
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal wins
            // Diagonal wins

            // Top left to bottom righ

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Bottom left to top right           
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[4] & mResults[2]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region Draw
            // Game ended without winner
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //Game ends
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;

                });

                #endregion
            }
        }
    }
}
