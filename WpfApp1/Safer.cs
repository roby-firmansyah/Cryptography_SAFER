using System;
using System.Security.Cryptography;
using System.Text;

namespace WpfApp1
{
    public static class Safer
    {
        
        public static string LeftShift(string array, int i)
        {
            string save = array.Substring(0, i);
            array = array.Remove(0, i);
            array += save;
            return array;
        }
        public static int BitToInt(char[] array)
        {
            int result = 0;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                result += Int32.Parse(array[i].ToString()) * (int)Math.Pow(2, array.Length - 1 - i);
            }
            return result;
        }
        public static byte[][] GetKey(byte[] key)
        {
            byte[][] k = new byte[12][]
            {
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0,0,0,0,0,0,0,0},
                new byte[]{ 0, 0, 0, 0, 0, 0, 0, 0 }
            }; ;
            char[] b = new char[12];
            b[0] = '1';
            b[1] = '3';
            b[2] = '0';
            b[3] = '2';
            b[4] = '9';
            b[5] = '0';
            b[6] = '4';
            b[7] = 'F';
            b[8] = '2';
            b[9] = 'E';
            b[10] = '7';
            b[11] = '2';
            k[0] = key;
            byte[] prev = new byte[8];
            for (int i = 0; i < key.Length; i++)
                prev[i] = key[i];
            for (int i = 1; i < 12; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    string shift = LeftShift(Convert.ToString(prev[j], 2), 3);
                    prev[j] = Convert.ToByte(BitToInt(shift.ToCharArray()));
                    uint buint = Convert.ToUInt32(Convert.ToByte(b[i]));
                    k[i][j] = Convert.ToByte(Convert.ToUInt32(prev[j] + buint) % 256U);
                }
            }
            return k;
        }
        public static byte XOR(byte text, byte key)
        {
            return Convert.ToByte(Convert.ToInt32(text) ^ Convert.ToInt32(key));
        }
        public static byte Mod256(byte text, byte key)
        {
            return Convert.ToByte((Convert.ToInt32(text) + Convert.ToInt32(key)) % 256);
        }
        public static byte DiffMod256(byte text, byte key)
        {
            int int32_1 = Convert.ToInt32(text);
            int int32_2 = Convert.ToInt32(key);
            if (int32_1 < int32_2)
                int32_1 += 256;
            return Convert.ToByte(int32_1 - int32_2 % 256);
        }
        public static byte E(byte text)
        {
            return Convert.ToByte(new int[256]
            {
                1, 45, 226, 147, 190, 69, 21, 174, 120, 3, 135, 164, 184, 56, 207, 63, 8, 103, 9, 148, 235, 38, 168, 107, 189, 24, 52, 27,
                187, 191, 114, 247, 64, 53, 72, 156, 81, 47, 59, 85, 227, 192, 159, 216, 211, 243, 141, 177, (int) byte.MaxValue, 167, 62,
                220, 134, 119, 215, 166, 17, 251, 244, 186, 146, 145, 100, 131, 241, 51, 239, 218, 44, 181, 178, 43, 136, 209, 153, 203,
                140, 132, 29, 20, 129, 151, 113, 202, 95, 163, 139, 87, 60, 130, 196, 82, 92, 28, 232, 160, 4, 180, 133, 74, 246, 19, 84,
                182, 223, 12, 26, 142, 222, 224, 57, 252, 32, 155, 36, 78, 169, 152, 158, 171, 242, 96, 208, 108, 234, 250, 199, 217, 0,
                212, 31, 110, 67, 188, 236, 83, 137, 254, 122, 93, 73, 201, 50, 194, 249, 154, 248, 109, 22, 219, 89, 150, 68, 233, 205,
                230, 70, 66, 143, 10, 193, 204, 185, 101, 176, 210, 198, 172, 30, 65, 98, 41, 46, 14, 116, 80, 2, 90, 195, 37, 123, 138,
                42, 91, 240, 6, 13, 71, 111, 112, 157, 126, 16, 206, 18, 39, 213, 76, 79, 214, 121, 48, 104, 54, 117, 125, 228, 237, 128,
                106, 144, 55, 162, 94, 118, 170, 197, (int) sbyte.MaxValue, 61, 175, 165, 229, 25, 97, 253, 77, 124, 183, 11, 238, 173, 75,
                34, 245, 231, 115, 35, 33, 200, 5, 225, 102, 221, 179, 88, 105, 99, 86, 15, 161, 49, 149, 23, 7, 58, 40
            }[Convert.ToInt32(text)]);
        }
        public static byte L(byte text)
        {
            return Convert.ToByte(new int[256]
            {
                128, 0, 176, 9, 96, 239, 185, 253, 16, 18, 159, 228, 105, 186, 173, 248, 192, 56, 194, 101, 79, 6, 148, 252, 25, 222,
                106, 27, 93, 78, 168, 130, 112, 237, 232, 236, 114, 179, 21, 195, (int) byte.MaxValue, 171, 182, 71, 68, 1, 172, 37,
                201, 250, 142, 65, 26, 33, 203, 211, 13, 110, 254, 38, 88, 218, 50, 15, 32, 169, 157, 132, 152, 5, 156, 187, 34, 140,
                99, 231, 197, 225, 115, 198, 175, 36, 91, 135, 102, 39, 247, 87, 244, 150, 177, 183, 92, 139, 213, 84, 121, 223, 170,
                246, 62, 163, 241, 17, 202, 245, 209, 23, 123, 147, 131, 188, 189, 82, 30, 235, 174, 204, 214, 53, 8, 200, 138, 180,
                226, 205, 191, 217, 208, 80, 89, 63, 77, 98, 52, 10, 72, 136, 181, 86, 76, 46, 107, 158, 210, 61, 60, 3, 19, 251,
                151, 81, 117, 74, 145, 113, 35, 190, 118, 42, 95, 249, 212, 85, 11, 220, 55, 49, 22, 116, 215, 119, 167, 230, 7, 219,
                164, 47, 70, 243, 97, 69, 103, 227, 12, 162, 59, 28, 133, 24, 4, 29, 41, 160, 143, 178, 90, 216, 166, 126, 238, 141,
                83, 75, 161, 154, 193, 14, 122, 73, 165, 44, 129, 196, 199, 54, 43, (int) sbyte.MaxValue, 67, 149, 51, 242, 108, 104,
                109, 240, 2, 40, 206, 221, 155, 234, 94, 153, 124, 20, 134, 207, 229, 66, 184, 64, 120, 45, 58, 233, 100, 31, 146, 144,
                125, 57, 111, 224, 137, 48
            }[Convert.ToInt32(text)]);
        }
        public static byte[] IPHT(byte x1, byte x2)
        {
            int int32_1 = Convert.ToInt32(x1);
            int int32_2 = Convert.ToInt32(x2);
            int num1 = -int32_1 + 2 * int32_2;
            if (num1 < 0)
                num1 += 256;
            int num2 = int32_1 - int32_2;
            if (num2 < 0)
                num2 += 256;
            int num3 = num1 % 256;
            int num4 = num2 % 256;
            byte[] numArray = new byte[2]
            {
                (byte) 0, Convert.ToByte(num3)
            };
            numArray[0] = Convert.ToByte(num4);
            return numArray;
        }
        public static byte[] PHT(byte x1, byte x2)
        {
            int int32_1 = Convert.ToInt32(x1);
            int int32_2 = Convert.ToInt32(x2);
            int num1 = (2 * int32_1 + int32_2) % 256;
            int num2 = (int32_1 + int32_2) % 256;
            return new byte[2]
            {
                Convert.ToByte(num1), Convert.ToByte(num2)
            };
        }
        public static byte[] execPHTStage(byte[] x, int round)
        {
            byte[] numArray1 = new byte[8];
            byte[] numArray2 = new byte[2];
            byte[] numArray3 = PHT(x[0], x[1]);
            if (round != 0)
            {
                numArray1[0] = numArray3[0];
                numArray1[4] = numArray3[1];
            }
            else
                Array.Copy((Array)numArray3, 0, (Array)numArray1, 0, 2);
            byte[] numArray4 = PHT(x[2], x[3]);
            if (round != 0)
            {
                numArray1[1] = numArray4[0];
                numArray1[5] = numArray4[1];
            }
            else
                Array.Copy((Array)numArray4, 0, (Array)numArray1, 2, 2);
            byte[] numArray5 = PHT(x[4], x[5]);
            if (round != 0)
            {
                numArray1[2] = numArray5[0];
                numArray1[6] = numArray5[1];
            }
            else
                Array.Copy((Array)numArray5, 0, (Array)numArray1, 4, 2);
            byte[] numArray6 = PHT(x[6], x[7]);
            if (round != 0)
            {
                numArray1[3] = numArray6[0];
                numArray1[7] = numArray6[1];
            }
            else
                Array.Copy((Array)numArray6, 0, (Array)numArray1, 6, 2);
            return numArray1;
        }
        public static byte[] execIPHTStage(byte[] x, int round)
        {
            byte[] numArray1 = new byte[8];
            byte[] numArray2 = new byte[2];
            byte[] numArray3 = IPHT(x[0], x[1]);
            if (round != 0)
            {
                numArray1[0] = numArray3[0];
                numArray1[2] = numArray3[1];
            }
            else
                Array.Copy((Array)numArray3, 0, (Array)numArray1, 0, 2);
            byte[] numArray4 = IPHT(x[2], x[3]);
            if (round != 0)
            {
                numArray1[4] = numArray4[0];
                numArray1[6] = numArray4[1];
            }
            else
                Array.Copy((Array)numArray4, 0, (Array)numArray1, 2, 2);
            byte[] numArray5 = IPHT(x[4], x[5]);
            if (round != 0)
            {
                numArray1[1] = numArray5[0];
                numArray1[3] = numArray5[1];
            }
            else
                Array.Copy((Array)numArray5, 0, (Array)numArray1, 4, 2);
            byte[] numArray6 = IPHT(x[6], x[7]);
            if (round != 0)
            {
                numArray1[5] = numArray6[0];
                numArray1[7] = numArray6[1];
            }
            else
                Array.Copy((Array)numArray6, 0, (Array)numArray1, 6, 2);
            return numArray1;
        }
        public static string[] encryptShow(byte[] text, byte[] key)
        {
            string[] st = new string[8];
            byte[] encryptedMessage = new byte[8];
            byte[][] keys = GetKey(key);
            for (int round = 0; round < 6; round++)
            {
                for (int index = 0; index < 8; index++)
                {
                    if (round == 0)
                    {
                        if (index == 0 || index == 3 || index == 4 || index == 7)
                        {
                            encryptedMessage[index] = XOR(text[index], keys[round * 2][index]);
                            if (index < 2)
                                st[0] = encryptedMessage[index].ToString();
                            encryptedMessage[index] = E(encryptedMessage[index]);
                            if (index < 2)
                                st[1] = encryptedMessage[index].ToString();
                            encryptedMessage[index] = Mod256(encryptedMessage[index], keys[round * 2 + 1][index]);
                            if (index < 2)
                                st[2] = encryptedMessage[index].ToString();
                        }
                        else
                        {
                            encryptedMessage[index] = Mod256(text[index], keys[round * 2][index]);
                            if (index < 2 && round == 0)
                                st[3] = encryptedMessage[index].ToString();
                            encryptedMessage[index] = L(encryptedMessage[index]);
                            if (index < 2 && round == 0)
                                st[4] = encryptedMessage[index].ToString();
                            encryptedMessage[index] = XOR(encryptedMessage[index], keys[round * 2 + 1][index]);
                            if (index < 2 && round == 0)
                                st[5] = encryptedMessage[index].ToString();
                        }
                    }
                    else
                    {
                        if (index == 0 || index == 3 || index == 4 || index == 7)
                        {
                            encryptedMessage[index] = XOR(encryptedMessage[index], keys[round * 2][index]);
                            encryptedMessage[index] = E(encryptedMessage[index]);
                            encryptedMessage[index] = Mod256(encryptedMessage[index], keys[round * 2 + 1][index]);
                        }
                        else
                        {
                            encryptedMessage[index] = Mod256(encryptedMessage[index], keys[round * 2][index]);
                            encryptedMessage[index] = L(encryptedMessage[index]);
                            encryptedMessage[index] = XOR(encryptedMessage[index], keys[round * 2 + 1][index]);
                        }
                    }
                }
                byte[] x = new byte[8];
                Array.Copy((Array)encryptedMessage, 0, (Array)x, 0, 8);
                Array.Copy((Array)execPHTStage(execPHTStage(execPHTStage(x, 1), 2), 0), 0, (Array)encryptedMessage, 0, 8);
                if (round == 0)
                {
                    st[6] = encryptedMessage[0].ToString();
                    st[7] = encryptedMessage[1].ToString();
                }
            }

            return st;
        }
        public static byte[] encrypt(byte[] text, byte[] key)
        {
            byte[] encryptedMessage = new byte[8];
            byte[][] keys = GetKey(key);
            for (int round = 0; round < 6; round++)
            {
                for (int index = 0; index < 8; index++)
                {
                    if (round == 0)
                    {
                        if (index == 0 || index == 3 || index == 4 || index == 7)
                        {
                            encryptedMessage[index] = XOR(text[index], keys[round * 2][index]);
                            encryptedMessage[index] = E(encryptedMessage[index]);
                            encryptedMessage[index] = Mod256(encryptedMessage[index], keys[round * 2 + 1][index]);
                        }
                        else
                        {
                            encryptedMessage[index] = Mod256(text[index], keys[round * 2][index]);
                            encryptedMessage[index] = L(encryptedMessage[index]);
                            encryptedMessage[index] = XOR(encryptedMessage[index], keys[round * 2 + 1][index]);
                        }
                    }
                    else
                    {
                        if (index == 0 || index == 3 || index == 4 || index == 7)
                        {
                            encryptedMessage[index] = XOR(encryptedMessage[index], keys[round * 2][index]);
                            encryptedMessage[index] = E(encryptedMessage[index]);
                            encryptedMessage[index] = Mod256(encryptedMessage[index], keys[round * 2 + 1][index]);
                        }
                        else
                        {
                            encryptedMessage[index] = Mod256(encryptedMessage[index], keys[round * 2][index]);
                            encryptedMessage[index] = L(encryptedMessage[index]);
                            encryptedMessage[index] = XOR(encryptedMessage[index], keys[round * 2 + 1][index]);
                        }
                    }
                }
                byte[] x = new byte[8];
                Array.Copy((Array)encryptedMessage, 0, (Array)x, 0, 8);
                Array.Copy((Array)execPHTStage(execPHTStage(execPHTStage(x, 1), 2), 0), 0, (Array)encryptedMessage, 0, 8);
            }

            return encryptedMessage;
        }
        public static byte[] decrypt(byte[] encrMess, byte[] key)
        {
            byte[] help = encrMess;
            byte[] decryptedMessage = new byte[8];
            decryptedMessage = encrMess;
            byte[][] keys = GetKey(key);
            for (int round = 5; round >= 0; round--)
            {
                byte[] x = new byte[8];
                Array.Copy((Array)help, 0, (Array)x, 0, 8);
                Array.Copy((Array)execIPHTStage(execIPHTStage(execIPHTStage(x, 1), 2), 0), 0, (Array)decryptedMessage, 0, 8);
                for (int index = 0; index < 8; index++)
                {
                    if (index == 0 || index == 3 || index == 4 || index == 7)
                    {
                        decryptedMessage[index] = DiffMod256(decryptedMessage[index], keys[round * 2 + 1][index]);
                        decryptedMessage[index] = L(decryptedMessage[index]);
                        decryptedMessage[index] = XOR(decryptedMessage[index], keys[round * 2][index]);
                    }
                    else
                    {
                        decryptedMessage[index] = XOR(decryptedMessage[index], keys[round * 2 + 1][index]);
                        decryptedMessage[index] = E(decryptedMessage[index]);
                        decryptedMessage[index] = DiffMod256(decryptedMessage[index], keys[round * 2][index]);
                    }
                }

                help = decryptedMessage;
            }
            return decryptedMessage;
        }
    }
}
