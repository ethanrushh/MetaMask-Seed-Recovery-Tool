using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.MetaMaskRecoverer
{
    public partial class MainWindow : Window
    {
        private const string WordsPath = "BIP39 Words.txt";
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Exit(object? sender, RoutedEventArgs e) => Environment.Exit(0);

        private async void Go_Clicked(object? sender, RoutedEventArgs e)
        {
            MainPage.IsVisible = false;
            WorkingScreen.IsVisible = true;

            await Task.Delay(1000);

            var result = Worker.AttemptRecovery(TbSeedPhrase.Text.Split(' '), TbWalletAddress.Text);

            WorkingScreen.IsVisible = false;

            SuccessScreen.IsVisible = result.Item1;
            FailureScreen.IsVisible = !result.Item1;

            // If we succeeded
            if (result.Item1)
            {
                TbOutputBox.Text = result.Item2;
            }
        }
        
        private async void ValidateSeedPhrase_Clicked(object? sender, RoutedEventArgs e)
        {
            if (TbSeedPhrase is null)
                return;
            if (TbSeedPhrase!.Text is "")
                return; // TODO Popup
            
            if (!File.Exists(WordsPath))
                throw new Exception(
                    "You are missing a necessary file for this to run. Please check that the BIP39 Words.txt is in the same directory as the program this is being run from or in the same path if explicitly specified");

            var words = await File.ReadAllLinesAsync(WordsPath);

            if (words.Length == 0)
                throw new Exception("Words list was empty and therefore corrupted");

            if (VerifyAllExist(words, TbSeedPhrase.Text.Split(' ')))
            {
                await SetButtonTextForTime(5000, "Verified ✅");
                BtGo.IsEnabled = TbWalletAddress.Text is not null;
            }
            else
                await SetButtonTextForTime(5000, "Incorrect ❌");
        }

        private async Task SetButtonTextForTime(int milliseconds, string message)
        {
            var preContent = (string)BtCheckValidity.Content;
            BtCheckValidity.Content = message;

            await Task.Delay(milliseconds);

            BtCheckValidity.Content = preContent;
        }

        private static bool VerifyAllExist(IEnumerable<string> wordList, params string[] words) => words.All(wordList.Contains);
    }
}
