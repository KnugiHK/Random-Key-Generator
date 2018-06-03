using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using mpfstudio;

namespace RandomKeyGenerator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

            private static RNGCryptoServiceProvider KeyGenCore = new RNGCryptoServiceProvider();
            public static string GetUniqueKey(int maxSize)
            {
                
                char[] charactors = new char[81];
                charactors = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890#^,.?!|&_~@$%/\=+-*".ToCharArray();
                byte[] data = new byte[1];
                try
                {
                    KeyGenCore.GetNonZeroBytes(data);
                    data = new byte[maxSize];
                    KeyGenCore.GetNonZeroBytes(data); 
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Computer do not have enough memory for calculating or save the results.", "Out Of Memory");
                }
                StringBuilder ResultKey = new StringBuilder(maxSize);
                foreach (byte b in data)
                {
                    ResultKey.Append(charactors[b % (charactors.Length)]);
                }
                return ResultKey.ToString();
            } 
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int parsedValue))
            {
                MessageBox.Show("Key Lenght must be integer", "Warning");
                return;
            }
            else
            {
                //int Keylenght = int.Parse(textBox2.Text);
                //string Key = GetUniqueKey(Keylenght);
                richTextBox1.Text = GetUniqueKey(int.Parse(textBox2.Text));
                Clipboard.SetText(richTextBox1.Text);
            }
        }
    }
}
