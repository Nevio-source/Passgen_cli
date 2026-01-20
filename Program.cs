using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace myapp
{
    internal class Programm
    {

        static void Banner()
        {
            string bänner = """
                                                                              
                                                                              
                                         .                    ,;L.            
            t                            ;W       ;W        .Gt       f#i EW:        ,ft
            ED.               ..        f#E      f#E       j#W:     .E#t  E##;       t#E
            E#K:             ;W,      .E#f     .E#f      ;K#f      i#W,   E###t      t#E
            E##W;           j##,     iWW;     iWW;     .G#D.      L#D.    E#fE#f     t#E
            E# ##t         G###,    L##Lffi  L##Lffi  j#K;      :K#Wfff;  E#t D#G    t#E
            E#  ##f      :E####,   tLLG##L  tLLG##L ,K#f   ,GD; i##WLLLLt E#t  f#E.  t#E
            E#t ;##D.   ;W#/G##,     ,W#i     ,W#i   j#Wi   E#t  .E#L     E#t   t#K: t#E
            E#ELLE##K: j##/ W##,    j#E.     j#E.     .G#D: E#t    f#E:   E#t    ;#W,t#E
            E#L;;;;;;,G##i,,G##,  .D#j     .D#j         ,K#fK#t     ,WW;  E#t     :K#D#E
            E#t     :K#K:   L##, ,WK,     ,WK,            j###t      .D#; E#t      .E##E
            E#t    ;##D.    L##, EG.      EG.              .G#t        tt ..         G#E
                   ,,,      .,,  ,        ,                  ;;                       fE
            
            Created By
            @Nevio_Pongiluppi

            """;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(bänner);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static async Task<bool> Check_if_pawnd(string passwort)
        {
            string hash = ComputeSHA1Hash(passwort).ToUpperInvariant();

            string five_first = hash.Substring(0, 5);
            string five_least = hash.Substring(5);

            using HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://api.pwnedpasswords.com/range/" + five_first);

            foreach (string line in response.Split('\n'))
            {
                var parts = line.Split(':');
                if (parts.Length != 2)
                {
                    continue;
                }
                if (parts[0].Equals(five_least, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

            }
            return false;
        }


        static string ComputeSHA1Hash(string input) //only for have I been pawnd
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }





        static async Task<string> gethash()
        {
            using HttpClient client = new HttpClient();



            int i = 0;
            while (i < 3)
            {
                try
                {
                    //api rng
                    string json = await client.GetStringAsync("http://78.46.176.247:8080/api");

                    if (string.IsNullOrWhiteSpace(json))
                        throw new Exception("Empty response");

                    using JsonDocument doc = JsonDocument.Parse(json);

                    string? hash = doc.RootElement.GetProperty("sha256").GetString();

                    if (string.IsNullOrWhiteSpace(hash))
                        throw new Exception("sha256 missing or null");

                    byte[] hashFromApibytes = Convert.FromHexString(hash);

                    //system rng
                    byte[] randomData1 = new byte[32];
                    RandomNumberGenerator.Fill(randomData1);
                    byte[] hash_21 = SHA256.HashData(randomData1);





                    using var hmac = new HMACSHA256(hashFromApibytes);
                    byte[] combinedHashBytes = hmac.ComputeHash(hash_21);



                    return BitConverter.ToString(combinedHashBytes).Replace("-", "");
                }
                catch
                {
                    i++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("API error → retrying in 500ms...");
                    Console.ForegroundColor = ConsoleColor.White;
                    await Task.Delay(500); // Short pause to prevent spaming 
                }
            }
            byte[] randomData = new byte[32];
            RandomNumberGenerator.Fill(randomData);
            byte[] hash_2 = SHA256.HashData(randomData);
            return BitConverter.ToString(hash_2).Replace("-", "");
        }

        static async Task Main(string[] args)
        {
            Console.Title = "PassGen";


        string alphabet =
            "abcdefghijklmnopqrstuvwxyz" +
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "0123456789" +
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        // 2. Unicode-Zeichen (BMP, Surrogates ausgelassen)
        string unicodeChars =
            "\u00A0\u00A1\u00A2\u00A3\u00A4\u00A5\u00A6\u00A7\u00A8\u00A9" +
            "\u00AA\u00AB\u00AC\u00AD\u00AE\u00AF\u00B0\u00B1\u00B2\u00B3" +
            "\u00B4\u00B5\u00B6\u00B7\u00B8\u00B9\u00BA\u00BB\u00BC\u00BD" +
            "\u00BE\u00BF\u00C0\u00C1\u00C2\u00C3\u00C4\u00C5\u00C6\u00C7" +
            "\u00C8\u00C9\u00CA\u00CB\u00CC\u00CD\u00CE\u00CF\u00D0\u00D1" +
            "\u00D2\u00D3\u00D4\u00D5\u00D6\u00D7\u00D8\u00D9\u00DA\u00DB" +
            "\u00DC\u00DD\u00DE\u00DF\u00E0\u00E1\u00E2\u00E3\u00E4\u00E5" +
            "\u00E6\u00E7\u00E8\u00E9\u00EA\u00EB\u00EC\u00ED\u00EE\u00EF" +
            "\u00F0\u00F1\u00F2\u00F3\u00F4\u00F5\u00F6\u00F7\u00F8\u00F9" +
            "\u00FA\u00FB\u00FC\u00FD\u00FE\u00FF";

        string lexicon = "";


            while (true)
            {
                Console.Clear();

                Banner();
                Console.Write("Choose your password length");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("(default 20)");
                Console.ResetColor();
                Console.WriteLine("Note: input cannot be higher than 200");
                Console.WriteLine();
                Console.Write(">");
                string lengthInput = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Use Unicode symbols ?");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("(Default N)");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("(Y/N)>");
                string unicodeyn = Console.ReadLine();

                if (lengthInput == "Nevio_Pongiluppi")
                {
                    try
                    {
                        System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=8Pc0AEbfnBM");
                    }
                    catch
                    {
                        System.Diagnostics.Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://www.youtube.com/watch?v=8Pc0AEbfnBM",
                            UseShellExecute = true
                        });
                    }

                }
                //Check input
                int.TryParse(lengthInput, out int length_re);
                if (length_re == 0)
                {
                    length_re = 20;
                }
                else if (length_re < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR cannot be negativ");
                    await Task.Delay(500);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Setting now on Default (20)");
                    await Task.Delay(500);
                    Console.WriteLine("You think your smart dont you ?");
                    await Task.Delay(1000);
                    Console.ResetColor();
                    length_re = 20;
                }
                else if (length_re > 200)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR user input higher than 200");
                    await Task.Delay(500);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Setting user input to 200");
                    Console.ResetColor();
                    length_re = 200;
                }
                string hash = await gethash();
                byte[] bytes = Convert.FromHexString(hash);

                // Hash verlängern, wenn nicht genug Bytes vorhanden
                while (bytes.Length < length_re)
                {
                    hash += await gethash();
                    bytes = Convert.FromHexString(hash);
                }

                //set lexicon
                if (unicodeyn == "y" || unicodeyn == "Y")
                {
                     lexicon = alphabet + unicodeChars;
                }

                else
                {
                     lexicon = alphabet;
                }

                //add symbols to passwort ouput
                List<char> passwordChars = new List<char>();
                for (int i = 0; i < length_re; i++)
                {
                    int index = bytes[i] % lexicon.Length;
                    passwordChars.Add(lexicon[index]);
                }

                string password = new string(passwordChars.ToArray());
                string border_ascci = new string('-', length_re);


                Console.WriteLine("Generated password:");
                Console.WriteLine();
                Console.WriteLine(border_ascci);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(password);
                Console.ResetColor();
                Console.WriteLine(border_ascci);

                if (await Check_if_pawnd(password))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Password got leaked :/ please change !!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Password not leaked :) ");
                    Console.ResetColor();
                }

                Console.ResetColor();
                Console.ResetColor();
                Console.WriteLine("\nPress any key to generate a new password...");
                Console.ReadKey(); // **Warte auf Benutzer**, bevor es neu startet
            }
        }
    }
}
