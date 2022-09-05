using System;
using System.Linq;

namespace Code
{
    class MainProgram
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool playing = true;

            List<string> menuOptions = new() { "1", "2", "3", "4"};
            List<string> states = new() { "\r\n\r\n\r\n\r\n\r\n", "\r\n\r\n\r\n\r\n╩", "\r\n║\r\n║\r\n║\r\n╩", "╔═╗\r\n║\r\n║\r\n║\r\n╩", "╔═╗\r\n║ ◯\r\n║\r\n║\r\n╩ ", "╔═╗\r\n║ ◯\r\n║ │\r\n║   \r\n╩ ", "╔═╗\r\n║ ◯\r\n║ │\r\n║ ┴\r\n╩", "╔═╗\r\n║ ◯\r\n║ ┼\r\n║ ┴\r\n╩\r\n"
};
            List<string> customWordList = new();

            while (playing)
            {
                string chosenWord = GiveRandomWord();

                int wrongGuesses = 0;
                int rightGuesses = 0;

                bool won = false;

                List<string> guessedLetters = new();

                while (wrongGuesses < states.Count)
                {
                    Console.Clear();

                    DisplayWord(chosenWord, guessedLetters);

                    Console.WriteLine(states[wrongGuesses]);

                    if (chosenWord.Length == rightGuesses)
                    {
                        won = true;
                        Console.WriteLine("You won! Want to try again? Y/N");
                        break;
                    }
                    if (!won)
                    {
                        string? guess = Console.ReadKey(true).Key.ToString();

                        while (guessedLetters.Any(l => l.ToString() == guess))
                        {
                            guess = Console.ReadKey(true).Key.ToString();
                        }

                        bool isGuessInWord = false;
                        foreach (char letter in chosenWord)
                        {
                            if (letter.ToString() == guess)
                            {
                                rightGuesses++;
                                isGuessInWord = true;
                            }
                        }

                        if (!isGuessInWord)
                        {
                            wrongGuesses++;
                        }

                        guessedLetters.Add(guess.ToString());
                    }
                }

                if (!won)
                    Console.WriteLine("You Lose! Want to try again? Y/N");

                if (ContinueCheck() == "N")
                {
                    playing = false;
                }
            }

            Console.WriteLine("Thank you, come again :)");
        }

        public static string GiveRandomWord()
        {
            List<string> wordList = new() { "PEN", "BOAT", "HOME", "BUSS", "SCHOOL" };

            return wordList[new Random().Next(0, wordList.Count)];
        }

        /// <summary>
        /// Displays the letters in the hidden word if players have guessed them
        /// </summary>
        /// <param name="wordToDisplay">The word that's to be displayed</param>
        /// <param name="guessedLetters">The letters which have been guessed</param>
        public static void DisplayWord(string wordToDisplay, List<string> guessedLetters)
        {
            foreach (char chr in wordToDisplay)
            {
                if (guessedLetters.Any(l => l == chr.ToString()))
                    Console.Write($"{chr} ");
                else
                    Console.Write("_ ");
            }
            Console.Write("\n");
        }

        public static string ContinueCheck()
        {
            string? awnser = Console.ReadKey(true).Key.ToString();

            while (awnser.ToUpper() != "Y" && awnser.ToUpper() != "N")
            {
                awnser = Console.ReadKey(true).Key.ToString();
            }

            return awnser;
        }
    }
}
