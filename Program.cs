char[] letter = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

if (args.Length > 1) // wurden argumente übergeben?
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    if (args[0] == "ecrypt:") { Console.WriteLine("*** Encrypt ***"); }
    else if (args[0] == "decrypt") { Console.WriteLine("*** Decrypt ***"); }
    Console.ResetColor();

    string text = string.Empty;
    do
    {
        text = Console.ReadLine()!;
        if (text == "exit") { break; }
        string result;
        int shift;
        if (!int.TryParse(args[1], out shift))
        {
            Console.WriteLine("Falsche Argumente übergeben!");
            return;
        }
        switch (args[0])
        {
            case "encrypt": result = Encryption(text, shift); break;
            case "decrypt": result = Decryption(text, shift); break;
            default: result = "Falsche Argumente übergeben!"; break;
        }
        Console.WriteLine(result);
    } while (true);
}
else
{
    Console.WriteLine("Keine Argumente übergeben! Try again ;-)");
}



#region Ceaser Cipher

string Decryption(string input, int shift)
{
    int index = 0;
    string output = string.Empty;

    while (input != string.Empty)
    {
        if (!Char.IsLetter(input[0])) { output += input[0]; }
        else if (char.IsUpper(input[0]))
        {
            index = Array.IndexOf(letter, char.ToLower(input[0])) - shift;

            if (index < 0) { output += char.ToUpper(letter[25 - index * -1 + 1]); }
            else { output += char.ToUpper(letter[index]); }
        }
        else if (!(char.IsUpper(input[0])))
        {
            index = Array.IndexOf(letter, char.ToLower(input[0])) - shift;

            if (index < 0) { output += letter[25 - index * -1 + 1]; }
            else output += letter[index];
        }
        input = input.Substring(1);
    }
    return output;
}
string Encryption(string input, int shift)
{
    int index = 0;
    string output = string.Empty;

    while (input != string.Empty)
    {
        if (!Char.IsLetter(input[0])) { output += input[0]; }
        else if (char.IsUpper(input[0]))
        {
            index = Array.IndexOf(letter, char.ToLower(input[0])) + shift;

            if (index > 25) { output += char.ToUpper(letter[index - 25 - 1]); }
            else output += char.ToUpper(letter[Array.IndexOf(letter, char.ToLower(input[0])) + shift]);
        }
        else if (!(char.IsUpper(input[0])))
        {
            index = Array.IndexOf(letter, char.ToLower(input[0])) + shift;

            if (index > 25) { output += letter[index - 25 - 1]; }
            else output += letter[Array.IndexOf(letter, input[0]) + shift];
        }

        input = input.Substring(1);
    }
    return output;
}
#endregion

#region Vigenère Cipher
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("**** Viginere Cipher ****");
Console.ResetColor();

Console.WriteLine("Enter the keyword: ");
string enteredKeyword = Console.ReadLine()!;
string resultingKeyword = string.Empty;
Console.WriteLine("Enter the plaintext: ");
string plaintext = Console.ReadLine()!;

int i = 0;
int counter = 0;

if (enteredKeyword.Length < plaintext.Length)
{
    while (resultingKeyword.Length < plaintext.Length)
    {
        if (plaintext[counter] == ' ') { resultingKeyword += ' '; }
        else { resultingKeyword += enteredKeyword[i]; i++; }

        counter++;
        if (i == enteredKeyword.Length) { i = 0; }
    }
}

plaintext = VigenereEncryption(resultingKeyword, plaintext);
Console.WriteLine("Encryption Vigenère:  " + plaintext);

string VigenereEncryption(string keyword, string plaintext)
{
    string output = string.Empty;
    int index = 0;
    int shift = 0;

    while (plaintext != string.Empty)
    {
        shift = Array.IndexOf(letter, keyword[0]);
        index = Array.IndexOf(letter, char.ToLower(plaintext[0])) + shift;

        if (!Char.IsLetter(plaintext[0])) { output += plaintext[0]; }
        else if (char.IsUpper(plaintext[0]))
        {
            if (index > 25) { output += char.ToUpper(letter[index - 25 - 1]); }
            else { output += char.ToUpper(letter[index]); }
        }
        else if (!char.IsUpper(plaintext[0]))
        {
            if (index > 25) { output += letter[index - 25 - 1]; }
            else output += letter[index];
        }

        plaintext = plaintext.Substring(1);
        keyword = keyword.Substring(1);

        index = 0; shift = 0;
    }

    return output;
}

#endregion