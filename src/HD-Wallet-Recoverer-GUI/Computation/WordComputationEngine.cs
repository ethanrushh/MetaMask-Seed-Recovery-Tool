using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HD_Wallet_Recoverer_GUI.Utils;

namespace HD_Wallet_Recoverer_GUI.Computation;

public class WordComputationEngine
{
    private string[] _wordList;
    
    public WordComputationEngine(string wordListPath)
    {
        try
        {
            _wordList = File.ReadAllLines(wordListPath);

            if (_wordList.Length == 0)
                throw new ArgumentException("Invalid file path, empty file.");
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Failed to load word list, see inner exception", ex);
        }
    }
    
    public bool ValidateWordList(IEnumerable<string> words)
        => words.All(word => _wordList.Contains(word));

    public (bool, string[]) FindAddressFromWords(IEnumerable<string> words, string targetAddress, Action<string> setOutputText)
    {
        ulong completedPermutations = 0;
        var correctPermutation = new string[12];
        var foundPermutation = false;
        
        Mathematics.Permutations.ForAllPermutation(words.ToArray(), thisPermutation =>
        {
            completedPermutations++;
            // Completed: {completedPermutations} / 479001600 combinations
            // Removed for performance.
            // Should probably be a progress bar
            setOutputText($"{(((decimal)completedPermutations / 479001600) * 100).ToString("N", new NumberFormatInfo() {NumberDecimalDigits = 4})}%");
            
            if (!HDWallet.GetAddressFromSeedPhrase(thisPermutation, out var resultingAddress)) return false;

            if (!string.Equals(resultingAddress, targetAddress, StringComparison.CurrentCultureIgnoreCase))
                return false;

            correctPermutation = thisPermutation;
            foundPermutation = true;
            
            return true;
        });

        return (foundPermutation, correctPermutation);
    }
}
