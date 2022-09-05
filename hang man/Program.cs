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

            List<ConsoleKey> menuOptions = new() { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Escape };
            List<string> states = new() { "\r\n\r\n\r\n\r\n\r\n", "\r\n\r\n\r\n\r\n╩", "\r\n║\r\n║\r\n║\r\n╩", "╔═╗\r\n║\r\n║\r\n║\r\n╩", "╔═╗\r\n║ ◯\r\n║\r\n║\r\n╩ ", "╔═╗\r\n║ ◯\r\n║ │\r\n║   \r\n╩ ", "╔═╗\r\n║ ◯\r\n║ │\r\n║ ┴\r\n╩", "╔═╗\r\n║ ◯\r\n║ ┼\r\n║ ┴\r\n╩\r"
};
            List<string> customWordList = new();

            while (1+1 == 2)
            {
                Console.Clear();
                int decidedOption;

                MainMenu(menuOptions, out decidedOption);

                while (playing)
                {
                    int wrongGuesses = 0;
                    int rightGuesses = 0;

                    bool won = false;

                    string chosenWord = "ERROR";

                    switch (decidedOption)
                    {
                        case 1:
                            chosenWord = GiveRandomWord();
                            break;
                        case 2:
                            chosenWord = SelectWord();
                            break;
                    }

                    List<string> guessedLetters = new();

                    while (wrongGuesses < states.Count)
                    {
                        Console.Clear();

                        DisplayWord(chosenWord, guessedLetters);

                        Console.WriteLine(states[wrongGuesses]);

                        List<string> wrongLetters = new();

                        wrongLetters.AddRange(guessedLetters.Where(l => !chosenWord.Contains(l)));

                        foreach (var letter in wrongLetters)
                            Console.Write($"{letter} ");

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
                                guess = Console.ReadKey(true).Key.ToString();

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
                                wrongGuesses++;

                            guessedLetters.Add(guess.ToString());
                        }
                    }

                    if (!won)
                        Console.WriteLine($"\nYou Lose! The word was {chosenWord}. Want to try again? Y/N");

                    if (ContinueCheck() == "N")
                        playing = false;
                }
            }

            
        }

        public static string GiveRandomWord()
        {
            List<string> wordList = new() { "PEN", "BOAT", "HOME", "BUSS", "SCHOOL", "MACHINE", "CAR", "PLANE", "MOUSE", "DOG", "CAT", "JAZZ", "CONSISTENT", "HAPPY", "SAD", "ANGRY" };

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

        /// <summary>
        /// Used to check if one wants to continue
        /// </summary>
        /// <returns>string of either Y or N</returns>
        public static string ContinueCheck()
        {
            string? awnser = Console.ReadKey(true).Key.ToString();

            while (awnser.ToUpper() != "Y" && awnser.ToUpper() != "N")
            {
                awnser = Console.ReadKey(true).Key.ToString();
            }

            return awnser;
        }

        public static string SelectWord()
        {
            bool selectedWord = false;
            string? whatWord = "";

            while (!selectedWord)
            {
                Console.Clear();
                Console.WriteLine("Write a word");
                whatWord = Console.ReadLine();

                if (whatWord != null || whatWord != "")
                {
                    Console.WriteLine("Do you want to choose this word? Y/N");

                    ConsoleKey input = Console.ReadKey(true).Key;

                    while (input != ConsoleKey.Y && input != ConsoleKey.N)
                    {
                        input = Console.ReadKey(true).Key;
                    }

                    if (input == ConsoleKey.Y)
                    {
                        selectedWord = true;
                    }

                }
            }

            return whatWord.ToUpper();
        }

        public static void MainMenu(List<ConsoleKey> validOptions, out int userAwnser)
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Play with random word");
            Console.WriteLine("2. Choose word to play with");

            ConsoleKey awnser = Console.ReadKey(true).Key;

            while (!validOptions.Any(o => o == awnser))
                awnser = Console.ReadKey(true).Key;

            int tempVar = 0;

            switch (awnser)
            {
                case ConsoleKey.D1:
                    tempVar = 1;
                    break;
                case ConsoleKey.D2:
                    tempVar = 2;
                    break;
                case ConsoleKey.Escape:
                    Console.WriteLine("Thank you, come again :)");
                    Environment.Exit(0);
                    break;
            }

            userAwnser = tempVar;
        }
    }
}
