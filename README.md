# MetaMask Seed Unsrambler
A tool for descrambling a MeatMask, Trust Wallet or any other 12-word BIP39 HD Seed Phrase and/or Finding the Private Key. This was created by me. I am an idiot. Be careful when you use this program, and understand it might work for you and it might not, I won't know. You're using this at your own risk. It was helpful for me, so I decided to publish it for people with the same potential problem. 

Although this is only built for Linux (my host OS), this can be run on anything that supports Omnisharp and VSCode. Yes, this includes Windows and MacOS (with a huge asterisk).

There is currently no multithreading because I'm lazy, however if I get a request to add support for it to speed up unscrambling, let me know and I'll be happy to do so.

Enjoy!

# Usage:
### Linux
To run, you need to first create your seed text document. Place your twelve words in here, with a space inbetween each word. The words MUST be one single line.
Run the application by executing:
`./MetaMask\ Seed\ Unscrambler <scrambled seed phrase file location> <target address>`
