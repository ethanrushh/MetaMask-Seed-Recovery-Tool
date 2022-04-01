using System;
using Nethereum.HdWallet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Program
{
    public static int currentProgress = 0;
    
    private static void Main(string[] args)
    {
        new Thread(UpdateProgress).Start();
        
        string words = "";
        string targetAddress = "";

        try {
            words = System.IO.File.ReadAllText(args[0]).TrimEnd('\n');
            targetAddress = args[1];
        }
        catch {
            Console.WriteLine("Your arguments were invalid. The proper format is:\n\n" + 
            "" + 
            "Descrambler <path to your words> <target address>");
        }
        ValidateMnemonicList(words.Split(' '));

        Console.WriteLine("Beginning descramble attempt. This may take a very long time (up to an hour or more depending on your system). Do not switch off your system or close the application while this program is running, or you'll have to start all over agian.");

        Permute<string>(words.Split(' '), perm =>
        {
            currentProgress++;
            StringBuilder builder = new StringBuilder();

            foreach (string strt in perm)
            {
                builder.Append(strt + ' ');
            }

            string attempt = builder.ToString().TrimEnd();

            if (attempt.Split(' ').Length != 12)
                return;

            try {
                var wallet = new Wallet(attempt, "");
                var account = wallet.GetAccount(0);
                if (account.Address.ToLower() == targetAddress.ToLower()) {
                    Console.WriteLine();
                    Console.WriteLine("Address found! Seed is " + attempt);
                    Console.WriteLine("Private key is: " + BitConverter.ToString(wallet.GetPrivateKey(0)).Replace("-", ""));
                    Environment.Exit(0);
                }
            }
            catch {}
        });

        Console.WriteLine("Words do not match any possible wallet attempt");
        Environment.Exit(0);
    }

    private static void UpdateProgress()
    {
        ProgressBar progressBar = new ProgressBar();
        while (true)
        {
            progressBar.Report((double)currentProgress / 479001600);
            
            Thread.Sleep(16);
        }
    }

    private static void ValidateMnemonicList(IEnumerable<string> words) {
        if (words.ToArray().Length != 12) {
            Console.WriteLine("Your word count isn't 12. You must have exactly 12 words in order for the seed phrase to be successfully unscrambled. If you have the same word twice, enter it twice.");
            Environment.Exit(0);
        }

        List<string> possibleWords = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/Resources/BIP39 Words English.txt").ToList();

        foreach (string s in words) {
            if (possibleWords.Contains(s))
                continue;
            else {
                Console.WriteLine("One of the words you have put in the SeedPhrase.txt document is invalid and isn't a known word in the BIP standard. This application currently has no support for replacing one or two words in the BIP standard, so you're out of luck here. BTCRecover might be able to help. Otherwise, check that you have spelt all the words correctly and try again. The word in question that failed the check is " + s + ". Also ensure there are no spaces before or after the word.");
                Environment.Exit(0);
            }
        }

        Console.WriteLine("Your list passed the validity test. Beginning descramble attempt");
    }

    private static void Output<T>(T[] permutation)
    {
        foreach (T item in permutation)
        {
            Console.Write(item);
            Console.Write(" ");
        }

        Console.WriteLine();
    }

    public static void Permute<T>(T[] items, Action<T[]> output)
    {
        Permute(items, 0, new T[items.Length], new bool[items.Length], output);
    }

    private static void Permute<T>(T[] items, int item, T[] permutation, bool[] used, Action<T[]> output)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (!used[i])
            {
                used[i] = true;
                permutation[item] = items[i];

                if (item < (items.Length - 1))
                {
                    Permute(items, item + 1, permutation, used, output);
                }
                else
                {
                    new Thread(() => output(permutation)).Start();
                }

                used[i] = false;
            }
        }
    }
}
