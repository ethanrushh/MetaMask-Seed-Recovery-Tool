using System;
using System.Threading.Tasks;
using ReactiveUI;
using System.Windows.Input;
using Avalonia.Controls;
using HD_Wallet_Recoverer_GUI.Computation;
using HD_Wallet_Recoverer_GUI.Utils;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace HD_Wallet_Recoverer_GUI.ViewModels;

public class MainWindowViewModel : WordBoxStorage
{
    private WordComputationEngine _computationEngine;

    public Window ThisWindow;

    private string _targetWalletAddress;
    public string TargetWalletAddress
    {
        get => _targetWalletAddress;
        set => this.RaiseAndSetIfChanged(ref _targetWalletAddress, value);
    }

    private string _foundSeedPhrase = "Seed phrase not found.";
    public string FoundSeedPhrase
    {
        get => _foundSeedPhrase;
        set => this.RaiseAndSetIfChanged(ref _foundSeedPhrase, value);
    }
    
    private int _currentStepIdx = 1;
    public ICommand NextStepCommand { get; set; }
    
    private bool _stepOneVisible = true;
    public bool StepOneVisible
    {
        get => _stepOneVisible;
        set => this.RaiseAndSetIfChanged(ref _stepOneVisible, value);
    }
    private bool _stepTwoVisible;
    public bool StepTwoVisible
    {
        get => _stepTwoVisible;
        set => this.RaiseAndSetIfChanged(ref _stepTwoVisible, value);
    }
    private bool _stepThreeVisible;
    public bool StepThreeVisible
    {
        get => _stepThreeVisible;
        set => this.RaiseAndSetIfChanged(ref _stepThreeVisible, value);
    }
    private bool _stepFourVisible;
    public bool StepFourVisible
    {
        get => _stepFourVisible;
        set => this.RaiseAndSetIfChanged(ref _stepFourVisible, value);
    }
    private bool _stepFiveVisible;
    public bool StepFiveVisible
    {
        get => _stepFiveVisible;
        set => this.RaiseAndSetIfChanged(ref _stepFiveVisible, value);
    }

    private bool _eulaAgreeChecked;
    public bool EulaAgreeChecked
    {
        get => _eulaAgreeChecked;
        set => this.RaiseAndSetIfChanged(ref _eulaAgreeChecked, value);
    }

    private string _permutationCompletionText = "Completed: 0%";
    public string PermutationCompletionText
    {
        get => _permutationCompletionText;
        set => this.RaiseAndSetIfChanged(ref _permutationCompletionText, value);
    }
    
    
    public MainWindowViewModel()
    {
        _computationEngine = new WordComputationEngine("Resources/words.txt");
        Console.WriteLine("DEBUG: Successfully imported words list");
        
        NextStepCommand = ReactiveCommand.Create(NextStep);
    }
    

    private void NextStep()
    {
        _currentStepIdx++;

        SetAllStepsInvisible();

        if (_currentStepIdx == 1)
            StepOneVisible = true;
        if (_currentStepIdx == 2)
            StepTwoVisible = true;
        if (_currentStepIdx == 3)
        {
            if (!HDWallet.ValidateAddress(TargetWalletAddress))
            {
                var walletBox = MessageBoxManager
                    .GetMessageBoxStandard("Invalid Wallet", "The wallet address you entered is invalid. Enter a valid wallet address and try again.");
                walletBox.ShowWindowDialogAsync(ThisWindow).ConfigureAwait(false);
                
                StepTwoVisible = true;
                _currentStepIdx--;
                
                return;
            }
            
            if (_computationEngine.ValidateWordList(GetAllWords()))
            {
                Task.Factory.StartNew(() =>
                {
                    var (found, result) = _computationEngine.FindAddressFromWords(GetAllWords(), TargetWalletAddress,
                        x => PermutationCompletionText = x);

                    SetAllStepsInvisible();
                    
                    if (!found)
                    {
                        StepFiveVisible = true;
                        return;
                    }

                    FoundSeedPhrase = string.Join(" ", result);
                    StepFourVisible = true;
                });
                StepThreeVisible = true;
                return;
            }
            
            var wordsBox = MessageBoxManager
                .GetMessageBoxStandard("Invalid words", "One or more of the words you entered were invalid. Please check your spelling or alter the word list found in Resources/words.txt and try again.");
            wordsBox.ShowWindowDialogAsync(ThisWindow).ConfigureAwait(false);
            
            StepTwoVisible = true;
            _currentStepIdx--;
        }

        if (_currentStepIdx == 4)
            StepFourVisible = true;
        
        return;
        

        void SetAllStepsInvisible()
        {
            StepOneVisible = false;
            StepTwoVisible = false;
            StepThreeVisible = false;
            StepFourVisible = false;
            StepFiveVisible = false;
        }
    }
}
