using System.Collections.Generic;
using System.Linq;
using Nethereum.HdWallet;
using Nethereum.Util;

namespace HD_Wallet_Recoverer_GUI.Utils;

public static class HDWallet
{
    public static bool ValidateAddress(string address)
        => AddressUtil.Current.IsValidEthereumAddressHexFormat(address);

    public static bool GetAddressFromSeedPhrase(IEnumerable<string> seedPhrase, out string address)
    {
        try
        {
            var thisAttempt = string.Join(" ", seedPhrase.Where(x => !string.IsNullOrEmpty(x)));
            var wallet = new Wallet(thisAttempt, "");

            address = wallet.GetAccount(0).Address;

            return true;
        }
        catch
        {
            address = "";
            return false;
        }
    }
}
