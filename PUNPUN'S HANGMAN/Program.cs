using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUNPUN_HANGMAN
{
    class Program
    {
        static bool startscreen = true, gamescreen = false, answer = false, gameoverscreen = false, gamecompletescreen = false;
        static bool errorinput = false, wordtransform = false, guesscheck = false, alreadyguess = false, correct = false;
        static string categoryfiledirectory = "", categoryfilename = "";
        static string[] wordarray = new string[] { "" }, hintarray = new string[] { "" };
        static string scoretext = "", wronglefttext = "", wrongguessed = "", wrongguessedtext = "", wordnumbertext = "", word1 = "", word2 = "", word3 = "", word4 = "", hint = "";
        static char guess = ' ';
        static int score = 0, wrongleft = 7, wordnumber = 1, underscore = 0;

        static void Main(string[] args)
        {
            Console.WindowWidth = 65;
            Console.WindowHeight = 35;
            while (true)
            {
                if (startscreen)
                {
                    reset();
                    wrongleft = 0;
                    Console.Clear();
                    Name();
                    Rule();
                    Left_img();
                    wrongleft = 7;
                    categoryfilename = Selectcategory();
                    if (categoryfilename == "error")
                    {
                        continue;
                    }
                    else
                    {
                        startscreen = false;
                        gamescreen = true;
                    }
                }
                else if (gamescreen)
                {
                    if (guess=='@')
                    {
                        gamescreen = false;
                        gameoverscreen = true;
                        continue;
                    }
                    wordtransform = false;
                    wrongguessed = "";
                    wrongleft = 7;
                    correct = false;
                    Categoryfileread();
                    while (true)
                    {
                        Console.Clear();
                        word1 = wordarray[wordnumber - 1];
                        word2 = word3;
                        word3 = "";
                        word4 = "";
                        guesscheck = false;
                        alreadyguess = false;
                        if (!wordtransform)
                        {
                            underscore = 0;
                            for (int i = 0; i < word1.Length; i++)
                            {
                                char letter1 = word1[i];
                                if ((letter1 >= 65 && letter1 <= 90) || (letter1 >= 97 && letter1 <= 122))
                                {
                                    word3 = word3 + "_";
                                    underscore += 1;
                                }
                                else
                                {
                                    word3 = word3 + letter1.ToString();
                                }
                            }
                            wordtransform = true;
                        }
                        else
                        {
                            underscore = 0;
                            for (int i = 0; i < word1.Length; i++)
                            {
                                char letter1 = word1[i];
                                char letter2 = word2[i];
                                if (letter2 == '_')
                                {
                                    if (((guess >= 65 && guess <= 90) || (guess >= 97 && guess <= 122)) && (guess == letter1 || guess == letter1 + 32 || guess == letter1 - 32))
                                    {
                                        word3 = word3 + letter1.ToString();
                                        guesscheck = true;
                                    }
                                    else
                                    {
                                        underscore += 1;
                                        word3 = word3 + "_";
                                    }
                                }
                                else
                                {
                                    if (guess == letter1 || guess == letter1 + 32 || guess == letter1 - 32)
                                    {
                                        alreadyguess = true;
                                    }
                                    word3 = word3 + letter2.ToString();
                                }
                            }
                            if (guesscheck)
                            {
                                score += 1;
                            }
                            else if (((guess >= 65 && guess <= 90) || (guess >= 97 && guess <= 122)) && !alreadyguess)
                            {
                                wrongleft -= 1;
                                if (!wrongguessed.Contains(guess))
                                {
                                    wrongguessed = wrongguessed + guess.ToString() + " ";
                                }
                            }
                        }

                        if (underscore == 0)
                        {
                            correct = true;
                        }
                        else if (wrongleft == 0)
                        {
                            answer = true;
                        }

                        Name();
                        Console.WriteLine("");
                        scoretext = "SCORE: " + score.ToString();
                        wronglefttext = "WRONG GUESSES LEFT: " + wrongleft.ToString();
                        Console.SetCursorPosition((Console.WindowWidth - scoretext.Length) / 2, Console.CursorTop);
                        Console.WriteLine(scoretext);
                        Console.SetCursorPosition((Console.WindowWidth - wronglefttext.Length) / 2, Console.CursorTop);
                        Console.WriteLine(wronglefttext);
                        wrongguessedtext = "WRONG GUESSED LETTERS: " + wrongguessed;
                        Console.SetCursorPosition((Console.WindowWidth - (wrongguessedtext.Length - 2)) / 2, Console.CursorTop);
                        Console.Write(wrongguessedtext);
                        Console.WriteLine("");

                        Left_img();
                        Console.SetCursorPosition((Console.WindowWidth - categoryfilename.Length) / 2, Console.CursorTop);
                        Console.WriteLine(categoryfilename);
                        Console.WriteLine("");
                        wordnumbertext = "WORD #" + wordnumber.ToString();
                        Console.SetCursorPosition((Console.WindowWidth - wordnumbertext.Length) / 2, Console.CursorTop);
                        Console.WriteLine(wordnumbertext);
                        hint = "HINT: " + hintarray[wordnumber - 1];
                        Console.SetCursorPosition((Console.WindowWidth - hint.Length) / 2, Console.CursorTop);
                        Console.WriteLine(hint);
                        Console.WriteLine("");

                        foreach (char letter in word3)
                        {
                            word4 = word4 + letter.ToString() + " ";
                        }
                        Console.SetCursorPosition((Console.WindowWidth - (word4.Length - 1)) / 2, Console.CursorTop);
                        Console.WriteLine(word4);
                        guess = Enterguess();
                        if (guess == '>')
                        {
                            break;
                        }
                        else if (guess == '@')
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (wordnumber == wordarray.Length)
                    {
                        gamescreen = false;
                        gamecompletescreen = true;
                        continue;
                    }
                    else
                    {
                        wordnumber += 1;
                    }
                }
                else if (gameoverscreen)
                {
                    Console.Clear();
                    Name();
                    Console.WriteLine("");
                    string gameovertext = "<<<<<<<<<< GAME OVER T_T >>>>>>>>>>";
                    Console.SetCursorPosition((Console.WindowWidth - gameovertext.Length) / 2, Console.CursorTop);
                    Console.WriteLine(gameovertext);
                    Console.WriteLine("");
                    string yourscore = "YOUR SCORE: " + score.ToString();
                    Console.SetCursorPosition((Console.WindowWidth - yourscore.Length) / 2, Console.CursorTop);
                    Console.WriteLine(yourscore);
                    Console.WriteLine("");
                    string pressentertext = "PRESS ENTER TO BACK TO MAIN MANU";
                    Console.SetCursorPosition((Console.WindowWidth - pressentertext.Length) / 2 - 1, Console.CursorTop);
                    Console.Write(pressentertext);
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        gameoverscreen = false;
                        startscreen = true;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (gamecompletescreen)
                {
                    Console.Clear();
                    Name();
                    Console.WriteLine("");
                    string gamecompletetext = "<<<<<<<<<< GAME COMPLETE :-) >>>>>>>>>>";
                    Console.SetCursorPosition((Console.WindowWidth - gamecompletetext.Length) / 2, Console.CursorTop);
                    Console.WriteLine(gamecompletetext);
                    Console.WriteLine("");
                    string yourscore = "YOUR SCORE: " + score.ToString();
                    Console.SetCursorPosition((Console.WindowWidth - yourscore.Length) / 2, Console.CursorTop);
                    Console.WriteLine(yourscore);
                    Console.WriteLine("");
                    string pressentertext = "PRESS ENTER TO BACK TO MAIN MANU";
                    Console.SetCursorPosition((Console.WindowWidth - pressentertext.Length) / 2 - 1, Console.CursorTop);
                    Console.Write(pressentertext);
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        gameoverscreen = false;
                        startscreen = true;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        static void Name()
        {
            var name = new[]
            {
                "========================",
                "    PUNPUN'S HANGMAN    ",
                "========================"
            };
            Console.WriteLine("");
            foreach (string line in name)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                Console.WriteLine(line);
            }
        }

        static void Rule()
        {
            var rule = new[]
            {
                " TRY TO GUESS THE SECRET WORD ONE LATTER AT A TIME ",
                "TO WIN, SPELL THE WORD BEFORE YOUR HANGMAN IS HUNG!",
            };
            Console.WriteLine("");
            foreach (string line in rule)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                Console.WriteLine(line);
            }
        }

        static string Selectcategory()
        {
            DirectoryInfo categoryfolder = new DirectoryInfo("../../Category/");
            FileInfo[] files = categoryfolder.GetFiles("*.txt");
            string[] categoryname = new string[files.Length];
            int categoryindex = 0;
            foreach (FileInfo line in files)
            {
                categoryname[categoryindex] = Path.GetFileNameWithoutExtension(line.Name);
                categoryindex += 1;
            }
            Array.Sort(categoryname);

            Console.WriteLine("");
            string categorymenu = "----- SELECT CATEGORY : -----";
            Console.SetCursorPosition((Console.WindowWidth - categorymenu.Length) / 2, Console.CursorTop);
            Console.WriteLine(categorymenu);
            int categorynumber = 1;
            string categorylist = "";
            foreach (string line in categoryname)
            {
                categorylist = categorynumber + " - " + line;
                Console.SetCursorPosition((Console.WindowWidth - categorylist.Length) / 2, Console.CursorTop);
                Console.WriteLine(categorylist);
                categorynumber += 1;
            }
            Console.WriteLine("");
            if (errorinput)
            {
                string errortext = "*** ERROR INPUT ***";
                Console.SetCursorPosition((Console.WindowWidth - errortext.Length) / 2, Console.CursorTop);
                Console.Write(errortext);
            }
            Console.WriteLine("");
            string select = "==> ";
            Console.SetCursorPosition((Console.WindowWidth - (select.Length + 1)) / 2 - 1, Console.CursorTop);
            Console.Write(select);
            bool inputisnumber = false;
            int selection;
            inputisnumber = int.TryParse(Console.ReadLine().ToString(), out selection);
            if (!inputisnumber || selection < 1 || selection > (categoryname.Length))
            {
                errorinput = true;
                return "error";
            }
            else
            {
                errorinput = false;
                return categoryname[selection - 1];
            }
        }

        static void Left_img()
        {
            var left = new[] { "" };
            var left7 = new[]
            {
                "*------------------*",
                "|                  |",
                "|                  |",
                "|                  |",
                "|                  |",
                "|                  |",
               @"|                  |",
               @"|                  |",
                "|                  |",
                "|                  |",
                "*------------------*"
            };
            var left6 = new[]
            {
                "*------------------*",
                "|                  |",
                "|                  |",
                "|                  |",
                "|                  |",
                "|                  |",
               @"|                  |",
               @"|                  |",
                "| _______          |",
                "|                  |",
                "*------------------*"
            };
            var left5 = new[]
            {
                "*------------------*",
                "|                  |",
                "|    |             |",
                "|    |             |",
                "|    |             |",
                "|    |             |",
               @"|    |             |",
               @"|    |             |",
                "| ___|___          |",
                "|                  |",
                "*------------------*"
            };
            var left4 = new[]
            {
                "*------------------*",
                "|    _________     |",
                "|    |             |",
                "|    |             |",
                "|    |             |",
                "|    |             |",
               @"|    |             |",
               @"|    |             |",
                "| ___|___          |",
                "|                  |",
                "*------------------*"
            };
            var left3 = new[]
            {
                "*------------------*",
                "|    _________     |",
                "|    |       |     |",
                "|    |       O     |",
                "|    |             |",
                "|    |             |",
               @"|    |             |",
               @"|    |             |",
                "| ___|___          |",
                "|                  |",
                "*------------------*"
            };
            var left2 = new[]
            {
                "*------------------*",
                "|    _________     |",
                "|    |       |     |",
                "|    |       O     |",
                "|    |       |     |",
                "|    |       |     |",
               @"|    |             |",
               @"|    |             |",
                "| ___|___          |",
                "|                  |",
                "*------------------*"
            };
            var left1 = new[]
            {
                "*------------------*",
                "|    _________     |",
                "|    |       |     |",
                "|    |       O     |",
                "|    |   >---|---< |",
                "|    |       |     |",
               @"|    |             |",
               @"|    |             |",
                "| ___|___          |",
                "|                  |",
                "*------------------*"
            };
            var left0 = new[]
            {
                "*------------------*",
                "|    _________     |",
                "|    |       |     |",
                "|    |       O     |",
                "|    |   >---|---< |",
                "|    |       |     |",
               @"|    |      / \    |",
               @"|    |    _/   \_  |",
                "| ___|___          |",
                "|                  |",
                "*------------------*"
            };

            switch (wrongleft)
            {
                case 7: left = left7; break;
                case 6: left = left6; break;
                case 5: left = left5; break;
                case 4: left = left4; break;
                case 3: left = left3; break;
                case 2: left = left2; break;
                case 1: left = left1; break;
                default: left = left0; break;
            }

            Console.WriteLine("");
            foreach (string line in left)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                Console.WriteLine(line);
            }
            Console.WriteLine("");
        }
        static void Categoryfileread()
        {
            categoryfiledirectory = "../../Category/" + categoryfilename + ".txt";
            string[] categoryfileline = File.ReadAllLines(categoryfiledirectory);
            wordarray = new string[categoryfileline.Length];
            hintarray = new string[categoryfileline.Length];
            int index = 0;
            foreach (string line in categoryfileline)
            {
                string[] split = line.Split(new[] { ", Hint : " }, StringSplitOptions.None);
                wordarray[index] = split[0];
                hintarray[index] = split[1];
                index += 1;
            }
        }
        static char Enterguess()
        {
            Console.WriteLine("");
            if (answer)
            {
                string lose = "xxxxxxxx YOU LOSE! xxxxxxxx";
                Console.SetCursorPosition((Console.WindowWidth - lose.Length) / 2, Console.CursorTop);
                Console.WriteLine(lose);
                string answeris = "THE ANSWER IS " + word1;
                Console.SetCursorPosition((Console.WindowWidth - answeris.Length) / 2, Console.CursorTop);
                Console.WriteLine(answeris);
                string pressentertext = "PRESS ENTER TO GO NEXT";
                Console.SetCursorPosition((Console.WindowWidth - pressentertext.Length) / 2 - 1, Console.CursorTop);
                Console.Write(pressentertext);
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    return '@';
                }
                else
                {
                    return '*';
                }
            }
            else if (!correct)
            {
                if (guess == '*')
                {
                    string errortext = "*** ERROR INPUT ***";
                    Console.SetCursorPosition((Console.WindowWidth - errortext.Length) / 2, Console.CursorTop);
                    Console.Write(errortext);
                }
                if (alreadyguess)
                {
                    string alreadyguesstext = @"*** SORRY, YOU ALREADY GUESSED '" + guess.ToString() + @"' ***";
                    Console.SetCursorPosition((Console.WindowWidth - alreadyguesstext.Length) / 2, Console.CursorTop);
                    Console.Write(alreadyguesstext);
                }
                Console.WriteLine("");
                string guesstext = "ENTER YOUR GUESS ==> ";
                Console.SetCursorPosition((Console.WindowWidth - (guesstext.Length + 1)) / 2 - 1, Console.CursorTop);
                Console.Write(guesstext);
                bool inputischar = false;
                inputischar = char.TryParse(Console.ReadLine().ToString(), out guess);
                if (!inputischar || guess < 65 || (guess > 90 && guess < 97) || guess > 122)
                {
                    return '*';
                }
                else
                {
                    return guess;
                }
            }
            else
            {
                string correcttext = "######## THAT'S CORRECT ########";
                Console.SetCursorPosition((Console.WindowWidth - correcttext.Length) / 2, Console.CursorTop);
                Console.WriteLine(correcttext);
                string pressentertext = "PRESS ENTER TO GO NEXT";
                Console.SetCursorPosition((Console.WindowWidth - pressentertext.Length) / 2 - 1, Console.CursorTop);
                Console.Write(pressentertext);
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    return '>';
                }
                else
                {
                    return '*';
                }
            }
        }
        static void reset()
        {
            answer = false;
            wordtransform = false; guesscheck = false; alreadyguess = false; correct = false;
            categoryfiledirectory = ""; categoryfilename = "";
            wordarray = new string[] { "" }; hintarray = new string[] { "" };
            scoretext = ""; wronglefttext = ""; wrongguessed = ""; wrongguessedtext = ""; wordnumbertext = ""; word1 = ""; word2 = ""; word3 = ""; word4 = ""; hint = "";
            guess = ' ';
            score = 0; wrongleft = 7; wordnumber = 1; underscore = 0;
        }
    }
}

