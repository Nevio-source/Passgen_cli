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
                "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~" +
                "\u00A7\u00B1\u00A3\u20AC\u00A5\u00A2\u00A9\u00AE\u2122\u00B5\u00B6\u2022\u00B0\u2260\u2248" +
                "\u00F7\u00D7\u221E\u2264\u2265" +
                "\u00BF\u00A1\u00DF" +
                "äöüÄÖÜáàâãåæœØøÅÆŒÐðÞþñÑçÇ" +
                "\u00AC\u00A6\u2013\u2014\u2026\u2020\u2021\u2030" +
                "\u00A4";



            while (true)
            {
                Console.Clear();

                Banner();
                Console.WriteLine("Choose your password length (default 20)");
                Console.WriteLine("Note: input cannot be higher than 200");
                Console.WriteLine();
                Console.Write(">");
                string lengthInput = Console.ReadLine();

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

                List<char> passwordChars = new List<char>();
                for (int i = 0; i < length_re; i++)
                {
                    int index = bytes[i] % alphabet.Length;
                    passwordChars.Add(alphabet[index]);
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
