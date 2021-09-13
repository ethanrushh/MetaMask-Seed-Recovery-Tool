# MetaMask Seed Unsrambler
A tool for descrambling a MetaMask, Trust Wallet or any other 12-word BIP39 HD Seed Phrase and/or Finding the Private Key. Be careful when you use this program, and understand it might work for you and it might not, I won't know. You're using this at your own risk (which is minimal). It was helpful for me, so I decided to publish it for people with the same potential problem.

Although this is only built for Linux (my host OS), this can be run on anything that supports Omnisharp and VSCode. Yes, this includes Windows and MacOS (with a huge asterisk).

There is currently no multithreading because I'm lazy, however if I get a request to add support for it to speed up unscrambling, let me know and I'll be happy to do so.

Enjoy!

# What this *can* do
If you've got a scrambled seed phrase (meaning out of order), this can help. Sometimes, MetaMask will internally store a corrupted version of your seed phrase. I've seen it happen, and I'm sure it will happen again. If that happens, this probably can help. This is also useful if you wrote down your seed phrase in the wrong order, which I've also seen happen.

# What this *can't* do
If you've got the wrong words in your seed phrase, nobody can help with that. A lost seed phrase is a lost seed phrase. Maybe brute forcing with GPU acceleration is possible, but its a dire effort and you've probably lost your funds. This program can not help with flat out incorrect words.

# Usage:
### Linux
To run, you need to first create your seed text document. Place your twelve words in here, with a space inbetween each word. The words MUST be one single line.
Run the application by executing:
`./MetaMask\ Seed\ Unscrambler <scrambled seed phrase file location> <target address>`

### Windows
Sorry, but I refuse to use Windows for development unless someone is threatening me with a fate worse than death. If you need to use Windows to run this program, you'll need to compile it yourself. Open it up in Visual Studio Code and restore the NuGet packages. It's not too difficult.

### MacOS
I haven't compiled this for MacOS purely because I don't have access to a Mac at the moment. The same steps can be applied here as Windows.

# Donations
If you find this program useful, please consider chipping me a little bit of your recovered coin...

### BSC/ETH/MATIC
0x2F50B7A73D7065a672bB0eb97fB4e3DAb391CC23

Thanks!
