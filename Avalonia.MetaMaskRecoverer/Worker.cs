using System;
using System.Collections.Generic;
using System.Linq;
using Erdcsharp.Domain;

namespace Avalonia.MetaMaskRecoverer;

public class Worker
{
    private static bool _finishedPermute = false;
    
    public static Tuple<bool, string, string> AttemptRecovery(IEnumerable<string> words, string walletAddress)
    {
        _finishedPermute = false;
        var result = new Tuple<bool, string, string>(false, "", "");
        
        var enumerable = words as string[] ?? words.ToArray();

        Permute(enumerable, perm =>
        {
            if (perm.Length != enumerable.Length) return;
            
            var thisAttempt = string.Join(" ", perm.Where(x => !string.IsNullOrEmpty(x)));
            try
            {
                var wallet = Wallet.DeriveFromMnemonic(thisAttempt);

                if (!string.Equals(wallet.GetAccount().Address.ToString(), walletAddress,
                        StringComparison.CurrentCultureIgnoreCase)) return;
                
                Console.WriteLine("Found at " + thisAttempt);
                    
                result = new Tuple<bool, string, string>(true, thisAttempt, wallet.GetPrivateKey().ToString() ?? "");
                _finishedPermute = true;

            }
            catch (Exception ex) { Console.WriteLine($"Failed to assess {thisAttempt} because {ex.Message}"); }
        });
        /*
        Console.WriteLine(wordCombinations.Count());
        
        foreach (var combination in wordCombinations)
            if (combination.Count == enumerable.Length)
            {
                var thisAttempt = string.Join(" ", combination.Where(x => !string.IsNullOrEmpty(x)));
                try
                {
                    var wallet = new Wallet(thisAttempt, "");

                    if (string.Equals(wallet.GetAddresses(1)[0], walletAddress,
                            StringComparison.CurrentCultureIgnoreCase))
                        return new Tuple<bool, string, string>(true, thisAttempt, wallet.GetPrivateKey(0).ToString() ?? "");
                }
                catch (Exception ex) { Console.WriteLine($"Failed to assess {thisAttempt} because {ex.Message}"); }
            }*/

        return result;
    }
    
    private static void Permute<T>(T[] items, Action<T[]> output)
    {
        Permute(items, 0, new T[items.Length], new bool[items.Length], output);
    }

    private static void Permute<T>(T[] items, int item, T[] permutation, bool[] used, Action<T[]> output)
    {
        for (var i = 0; i < items.Length; ++i)
        {
            if (_finishedPermute)
                return;
            
            if (used[i]) continue;
            
            used[i] = true;
            permutation[item] = items[i];

            if (item < items.Length - 1)
                Permute(items, item + 1, permutation, used, output);
            else
                output(permutation);

            used[i] = false;
        }
    }
}
