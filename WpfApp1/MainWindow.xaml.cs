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
                MessageBox.Show("Введите сообщение");
            else if (k.Text == "")
                MessageBox.Show("Введите ключ");
            else if (k.Text.Length<8)
                MessageBox.Show("Длина ключа должна быть равна 8");
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
                MessageBox.Show("Зашифруйте или вставьте зашифрованный текст");
            else if (k.Text == "")
                MessageBox.Show("Введите ключ");
            else if (k.Text.Length < 8)
                MessageBox.Show("Длина ключа должна быть равна 8");
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

        private void kGen_Click(object sender, RoutedEventArgs e)
        {
            if(k.Text=="")
                MessageBox.Show("Введите ключ");
            else if (k.Text.Length < 8)
                MessageBox.Show("Длина ключа должна быть равна 8");
            else
            {
                char[] ch = k.Text.ToCharArray();
                key = Encoding.Default.GetBytes(k.Text);
                byte[][] kg = Safer.GetKey(key);
                description.Text =
                    "В симметричном блочном алгоритме шифрования Safer длина ключа составляет 64 бита." +
                    "\nКлюч K[1] в ASCII:\n";
                foreach (var c in ch)
                    description.Text += c + "\t";
                description.Text += "\n";
                foreach (var c in key)
                    description.Text += c + "\t";
                description.Text += "\n\n";
                description.Text +=
                    "Первый ключ шифрования K[1] является  секретным ключом, задаваемым пользователем. Каждый последующий ключ шифрования " +
                    "K[i+1] получается из предыдущего K[i] посредством побайтового циклического сдвига на 3 и побайтовым сложением по модулю 256 с константой этапа B[i]. " +
                    "Значения B хранятся в специальных таблицах.\n" +
                    "Количество ключей будет зависеть от количества раундов и будет равно 2r. Так, как в алгоритме Safer количество раундов равно 6, то, следовательно, " +
                    "количество ключей будет равно 12.\n";
                description.Text += "Сгенерированные ключи\n";
                for (int i = 0; i < 12; i++)
                {
                    description.Text += "K["+$"{i+1}"+"]: " + kg[i][0] + "  " + kg[i][1] + "  " + kg[i][2] + "  " + kg[i][3] + "  "
                                        + kg[i][4] + "  " + kg[i][5] + "  " + kg[i][6] + "  " + kg[i][7] + "  ";
                    description.Text += "\n";
                }
            }
        }

        private void eAlg_Click(object sender, RoutedEventArgs e)
        {
            if (message.Text == "")
                MessageBox.Show("Введите сообщение");
            else if (k.Text == "")
                MessageBox.Show("Введите ключ");
            else if (k.Text.Length < 8)
                MessageBox.Show("Длина ключа должна быть равна 8");
            else
            {
                string m=message.Text;
                key = Encoding.Default.GetBytes(k.Text);
                int[] c = new int[8];
                byte[][] eK = Safer.GetKey(key);
                while (m.Length % 8 != 0)
                {
                    m += " ";
                }
                string[] es = Safer.encryptShow(Encoding.Default.GetBytes(m.Substring(0, 8)), key);
                description.Text = "Алгоритм шифрования.\n" +
                                   "Входной блок размером 64 бит делится на 8 частей длиной по одному байту. Таким же образом делятся ключи K[2r-1] и K[2r]. Начальные значения первых двух подблоков: \n";
                byte[] h = Encoding.Default.GetBytes(m.Substring(0, 2));
                description.Text += Convert.ToString(h[0],2)+"  "+Convert.ToString(h[1],2)+"\n";
                description.Text += "Соответствующие подблоки входного текста и ключа K[2i - 1]" +
                " либо складываются по модулю 2 - для подблоков 1, 4, 5, 8, либо складываются по модулю 256 - для подблоков 2, 3, 6, 7.\n";
                for (int i = 0; i < 8; i++)
                {
                    c[i] = Convert.ToInt32(es[i]);
                }
                description.Text += "Для первого подблока:\n"+Convert.ToString(h[0], 2)+" XOR "+ Convert.ToString(eK[0][0], 2)+" = "+Convert.ToString(Convert.ToByte(c[0]),2)+"\n";
                description.Text += "Для второго подблока:\n" + Convert.ToString(h[1], 2) +" + " + Convert.ToString(eK[0][1], 2)+ " MOD256 "+ " = " + Convert.ToString(Convert.ToByte(c[3]), 2)+"\n";
                description.Text += "Далее результаты сложения проходят через так называемые S-блоки. Их содержимое представляет собой одну из нелинейных операций:\n" +
                                    "y = 45^x mod 257 - для подблоков 1, 4, 5, 8;\n" +
                                    "у = log45 x - для подблоков 2, 3, 6, 7, \n" +
                                    "где x - входной байт, y - выходной.\n" +
                                    "Поскольку каждый раз рассчитывать результаты этих операций в практических реализациях алгоритма весьма неудобно, как правило используются специально составляемые таблицы для " +
                                    "получения результатов их действия.\n";
                description.Text += "Значение первого подблока после прохождения через S-блок:\n" + Convert.ToString(Convert.ToByte(c[1]), 2);
                description.Text += "\nЗначение второго подблока после прохождения через S-блок:\n" + Convert.ToString(Convert.ToByte(c[4]), 2);
                description.Text += "\nНад результатами предыдущего действия производится операция, аналогичная п.1," +
                                    " с той лишь разницей, что используется второй подключ, а операции XOR и сложения по модулю 256 меняются местами.";
                description.Text += "\nДля первого подблока:\n" + Convert.ToString(c[1], 2) + " + " + Convert.ToString(eK[1][0], 2) + " MOD256 = " + Convert.ToString(Convert.ToByte(c[2]), 2) + "\n";
                description.Text += "Для второго подблока:\n" + Convert.ToString(c[4], 2) + " XOR " + Convert.ToString(eK[1][1], 2) + " = " + Convert.ToString(Convert.ToByte(c[5]), 2) + "\n";
                description.Text += "Полученные байты проходят через многоуровневую систему преобразований, взаимно складываясь в различном порядке. Это эквивалентно трём уровням псевдопреобразований Адамара(PHT)." +
                                    "Каждое преобразование действует таким образом, что при входных байтах a1 и a2 на выходе получим:\n" +
                                    "b1 = (2*a1 + a2) mod 256;\n" +
                                    "b2 = (a1 + a2 ) mod 256.\n";
                description.Text += "Значение первого подблока после PHT преобразований: \n" + Convert.ToString(Convert.ToByte(c[6]), 2)+"\n"; 
                description.Text += "Значение второго подблока после PHT преобразований: \n" + Convert.ToString(Convert.ToByte(c[7]), 2)+"\n";
                description.Text += "Далее, действия повторяются, пока не пройдут все 6 раундов шифрования.";
            }

        }

        private void dAlg_Click(object sender, RoutedEventArgs e)
        {
            description.Text = "Расшифрование производятся в обратном порядке, но при этом операции заменяются обратными. Так, операции сложения по модулю 256 заменяются операциями вычитания, а сложение по" +
                                " модулю 2 выполняется точно так же, как и при зашифровании. Операции 45^x mod 257 и log45 x меняются местами." +
                                " Псевдопреобразования Адамара заменяются обратными(IPHT), действующими следующим образом: \n" +
                                "a1 = (b1 - b2) mod 256;\n" +
                                "a2 = (-b1 + 2*b2) mod 256.";
        }
    }
}
