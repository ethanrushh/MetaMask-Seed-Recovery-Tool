using System.Collections.Generic;
using HD_Wallet_Recoverer_GUI.ViewModels;
using ReactiveUI;

namespace HD_Wallet_Recoverer_GUI.Utils;

// This is crude af but it works so
public class WordBoxStorage : ViewModelBase
{
    // This absolutely sucks ass but I don't really see an alternative that doesn't suck even more
    // Reflection is too slow and unreliable. Bad idea with UI components.
    // For now this will do. I don't understand Avalonia well enough to come up with an alternative
    // solution at 1:44AM.
    // Update - figured out a better solution but I cbf to implement it, this works.
    // Update - I figured out an even better way using user components but I REALLY cbf to implement that
    // TODO Clean this up
    
    private string word1;
    public string Word1 { get => word1; set => this.RaiseAndSetIfChanged(ref word1, value); }
    
    private string word2;
    public string Word2  { get => word2; set => this.RaiseAndSetIfChanged(ref word2, value); }
    private string word3;
    public string Word3  { get => word3; set => this.RaiseAndSetIfChanged(ref word3, value); }
    
    private string word4;
    public string Word4  { get => word4; set => this.RaiseAndSetIfChanged(ref word4, value); }
    
    private string word5;
    public string Word5  { get => word5; set => this.RaiseAndSetIfChanged(ref word5, value); }
    
    private string word6;
    public string Word6  { get => word6; set => this.RaiseAndSetIfChanged(ref word6, value); }
    
    private string word7;
    public string Word7  { get => word7; set => this.RaiseAndSetIfChanged(ref word7, value); }
    
    private string word8;
    public string Word8  { get => word8; set => this.RaiseAndSetIfChanged(ref word8, value); }
    
    private string word9;
    public string Word9  { get => word9; set => this.RaiseAndSetIfChanged(ref word9, value); }
    
    private string word10;
    public string Word10 { get => word10; set => this.RaiseAndSetIfChanged(ref word10, value); }
    
    private string word11;
    public string Word11 { get => word11; set => this.RaiseAndSetIfChanged(ref word11, value); }
    
    private string word12;
    public string Word12 { get => word12; set => this.RaiseAndSetIfChanged(ref word12, value); }


    protected IEnumerable<string> GetAllWords()
        => new[] { Word1, Word2, Word3, Word4, Word5, Word6, Word7, Word8, Word9, Word10, Word11, Word12 };
}
