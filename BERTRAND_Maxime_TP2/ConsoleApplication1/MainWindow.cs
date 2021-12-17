using System;

namespace BERTRAND_Maxime_TP2_ST2TRD
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // the "??" operator checks for nullability and value all at once.
            // because ConvertCheckBox.IsChecked is of type __bool ?__ which
            // is a nullable boolean, so it can either be true, false or null
            var toDecrypt = ConvertCheckBox.IsChecked ?? false;
            var inputText = InputTextBox.Text;
            var inpputKey = InputKey.Text;
            var encryptionmethod = EncryptionComboBox.Text;


            if (toDecrypt)
            {
                OutputTextBox.Text = $"{inputText} is gibberish and should be decrypted using {encryptionmethod}";
            }
            else
            {
                OutputTextBox.Text = $"{inputText} was written as an input to be encrypted using {encryptionmethod}";
            }

            if (encryptionmethod == "Caesar")
            {
                OutputTextBox.Text = Caesar.Code(inputText, toDecrypt , inpputKey);
            }

            if (encryptionmethod == "Caesar2")
            {
                OutputTextBox.Text = Caesar2.Code(inputText, toDecrypt, inpputKey);
            }
            
        }

        public object ConvertCheckBox { get; }

        public object OutputTextBox { get; set; }
    }

    // This class is not instantiated because it is static. 
    // You might not be able to do this so easily...
    // And each class should have its own file !
    internal static class Caesar
    {
        public static string Code(string inputText, bool toDecrypt, string KeyString)
        {
            // Ternary operator - Google it
            return toDecrypt ? Decrypt(inputText, KeyString) : Encrypt(inputText, KeyString);
        }

        private static bool TestKey(int Key)
        {
            var isNumber = true;
            if (Key == 0)
            {
                MessageBox.Equals(object)("Don't try this ! :)..Caesar's key should be only number different de 0");
                isNumber = false;
            }

            return isNumber;
        }

        public static object MessageBox { get; set; }


        private static string Encrypt(string inputText, string keyString, bool inversed = false)
        {
            
            int.TryParse(keyString, out var key);
            
            var alphabetUp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var alphabetLow = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var encryptedMessage = "";

            if (TestKey(key) == false)
            {
                return "Sorry it's not possible to have a negative key";
            }


            var rotation = key < 0 ? ((key % 26) + 26) % 26 : key % 26;
            //bool IsUpper = true; 

            if (inversed == true)
            {
                rotation = rotation - 2 * rotation;
            }
            

            foreach (var t in inputText)
            {
                if (char.IsLetter(t))
                {
                    var verif = Char.IsUpper(t);

                    char[] alphabet = null;
                    if (verif == true)
                    {
                        alphabet = alphabetUp;
                    }
                    else
                    {
                        alphabet = alphabetLow;
                    }

                    if (t == ' ')
                    {
                        encryptedMessage += ' ';
                    }

                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (t == alphabet[j])
                        {
                            encryptedMessage += alphabet[(j + rotation + 26) % 26];
                        }
                    }
                }
                else
                {
                    encryptedMessage += t;
                }
            }
            
            
            return $"{encryptedMessage} encrypted with Caesar !";
        }

        private static string Decrypt(string inputText, string keyString)
        {
            var output = Encrypt(inputText, keyString, true);
            return $"{output} was decrypted with Caesar !";
        }
    }

    internal static class Caesar2
    {
        
        private static bool TestKey(string Key)
        {
            var isNumber = false;
            foreach (var t in Key)
            {
                if (!char.IsLetter(t))
                {
                    MessageBox.Show("Don't try this ! :).. Caesar2 key should be a string not a number");
                    isNumber = true;
                }
            }
            

            return isNumber;
        }

        public static object MessageBox { get; set; }

        public static string Code(string inputText, bool toDecrypt, string keyString)
        {
            // Ternary operator - Google it
            return toDecrypt ? Decrypt(inputText, keyString) : Encrypt(inputText, keyString);
        }

        private static int Mod(int x, int y)
        {
            return (x % y + y) % y;
        }
        
        
        
        private static string Encrypt(string inputText, string keyString, bool inversed = true)
        {
            
           
            
            var encryptedMessage = "";
            var alphabetUp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var alphabetLow = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            var counterNonLetter = 0;

             if (TestKey(keyString) == true)
             {
                 return "Sorry it's not possible to have number key !";
             }


            for (var i = 0; i < inputText.Length; i++)
            {
                if (char.IsLetter(inputText[i]))
                {
                    var isUpper = char.IsUpper(inputText[i]);
                    var offSet = isUpper ? 'A' : 'a';
                    var indexKey = (i - counterNonLetter) % keyString.Length;
                    var k = (isUpper ? char.ToUpper(keyString[indexKey]) : char.ToLower(keyString[indexKey])) - offSet;

                    k = inversed ? k : -k;
                    var letter = (char) ((Mod(((inputText[i] + k) - offSet), 26)) + offSet);

                    encryptedMessage += letter;

                }
                else
                {
                    encryptedMessage += inputText[i];
                    ++counterNonLetter;
                }
            }

            return encryptedMessage;
        }

        private static string Decrypt(string inputText, string keyString, bool inversed = false)
        {
            return Encrypt(inputText, keyString, false );
        }
        
    }

}