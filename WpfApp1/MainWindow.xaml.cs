using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string mess = "";
        public string decrMess = "";
        public byte[] key;
        public string help = "";
        public byte[][] enc;
        public int p;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void encrypt_Click(object sender, RoutedEventArgs e)
        {
            em.Text = "";
            if (message.Text == "" )
                MessageBox.Show("Enter your message");
            else if (k.Text == "")
                MessageBox.Show("Enter the key");
            else if (k.Text.Length<8)
                MessageBox.Show("Key length must be 8");
            else
            {
                mess = message.Text;
                while (mess.Length % 8 != 0)
                {
                    mess += " ";
                }
                byte[][] encryptedMessage = new byte[mess.Length / 8][];
                p = mess.Length / 8;
                key = Encoding.Default.GetBytes(k.Text);
                for (int i = 0, j = 0; i < mess.Length; i += 8, j++)
                {
                    help = mess.Substring(i, 8);
                    byte[] encr = Encoding.Default.GetBytes(help);
                    encryptedMessage[j] = Safer.encrypt(encr, key);
                    em.Text += Encoding.Default.GetString(encryptedMessage[j]);
                    enc = encryptedMessage;
                }
                help = "";
            }
        }

        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (em.Text == "")
                MessageBox.Show("Encrypt or paste encrypted text");
            else if (k.Text == "")
                MessageBox.Show("Enter the key");
            else if (k.Text.Length < 8)
                MessageBox.Show("Key length must be 8");
            else
            {
                key = Encoding.Default.GetBytes(k.Text);
                byte[][] decryptedMessage = new byte[em.Text.Length / 8][];
                for (int i = 0, j = 0; j < decryptedMessage.Length; i += 8, j++)
                {
                    decryptedMessage[j] = Safer.decrypt(Encoding.Default.GetBytes(em.Text.Substring(i, 8)), key);
                    help = Encoding.Default.GetString(decryptedMessage[j]);
                    decrMess += help;
                }
                dm.Text = decrMess;
            }
        }

        
    }
}
