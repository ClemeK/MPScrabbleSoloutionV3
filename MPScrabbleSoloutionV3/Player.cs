namespace MPScrabbleSoloutionV3
{
    internal class Player
    {
        // Private Properties
        private string myLetters;
        private string name;
        private int totalLetters;

        public Player()
        {
        }
        // **********************************
        // **********************************
        public string MyLetters
        {
            get { return myLetters; }
        }
        // **********************************
        public string Name
        {
            get { return name; }
            set { name = Name; }
        }
        // **********************************
        public int TotalLetters
        {
            get { return totalLetters; }
        }
        // **********************************
        public void ResetPlayer()
        {
            myLetters = "";
            totalLetters = 0;
        }
        // **********************************
        public void TakeLetters(string letters)
        {
            myLetters += letters;
            totalLetters = myLetters.Length;
        }
        // **********************************
    }
}