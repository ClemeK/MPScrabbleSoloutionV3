using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MPScrabbleSoloutionV3
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {

            InitializeComponent();

            FlowDocument mcFlowDoc = new FlowDocument();
            // Create a paragraph with text  
            Paragraph para = new Paragraph();
            para.Inlines.Add(new Bold(new Run("Application\n")));
            para.Inlines.Add(new Run("This Application was designed to allow up to 4 people to play scrabble over a conference call.\n\n"));
            
            para.Inlines.Add(new Bold(new Run("How to Use\n")));
            para.Inlines.Add(new Run("1. Each play needs to download a copy of the application.\n"));
            para.Inlines.Add(new Run("2. Select the number of players.\n"));
            para.Inlines.Add(new Run("3. Select which play you are going to be (you can change the name of the players).\n"));
            para.Inlines.Add(new Run("4. Player 1 can then press the Generate button (only Player 1 has this button enabled), this will create a list of 10 random number that Player 1 can pass to the other Players.\n"));
            para.Inlines.Add(new Run("5. The other Player can then put the number in to their application (where the zeros are).\n"));
            para.Inlines.Add(new Run("6. When everyone is ready, ALL Player can press the Shunt button (This will lock the Generate button for Player 1).\n"));
            para.Inlines.Add(new Run("7. All players need to decide what the Shuffle Number need to be (by default they are 2, 3). If the decision is made to change these, ALL Players need to do this.\n"));

            para.Inlines.Add(new Run("\nThe letter bag should now be sorted on all machine the same way.\n"));

            para.Inlines.Add(new Run("8. Each play can take it in turn to select from the dropdown the number of letters they want and press the Take button, this will remove the letters from the Letter Bag and put them on the Player area, Only the Current Player will see their tiles.\n"));
            para.Inlines.Add(new Run("9. When a Player take a go, they will need to inform the other player to what tiles their have used and where they have gone on the board.\n"));
            para.Inlines.Add(new Run("10. Once all players have taken a turn you can Re-shuffle if you wish or just take another turn until the bag become empty.\n"));
            para.Inlines.Add(new Run("11. Once you have completed the game, there is a Print log button so you can see what tile were pick in what order.\n"));
            para.Inlines.Add(new Run("12. There is a reset button so you can have another.\n\n"));

            para.Inlines.Add(new Bold(new Run("Background\n")));
            para.Inlines.Add(new Run("I build this application after watch a YouTube video by Matt Parker (https://www.youtube.com/watch?v=JaXo_i3ktwM&t=970s) in which one of his fellow YouTuber need a way to play scrabble over a conference call but not use a game app. The idea was to mix the tile at both ends of the call in the same way and take them for each play and be able to place the tiles on their own boards.\n\n"));
            para.Inlines.Add(new Run("After watching the video, I thought this would be a ‘nice little’ project to try and code. The code was fairly easy, so I decided to add a screen to make life easy for the players, this proved more of a challenge.\n\n"));

            para.Inlines.Add(new Bold(new Run("Easter Egg\n")));
            para.Inlines.Add(new Run("If you want to see the program work (Shunt and Shuffle) type the ‘*demomode*’ over the zeros after the Generate button and then press the Generate button (This only works for Player 1). This was design for test purposes.\n\n"));

            // Add the paragraph to blocks of paragraph  
            mcFlowDoc.Blocks.Add(para);
            // Set contents  
            rtbInfoBox.Document = mcFlowDoc;
            
                        
        }
        // ****************************
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
