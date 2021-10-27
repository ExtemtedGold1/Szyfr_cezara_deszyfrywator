using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Szyfr_cezara_deszyfrywator
{
    class Program
    {

        public static void EncryptTextToFile(string Data, string NameFolder, byte[] Key, byte[] IV)
        {
            FileStream fStream = File.Open(NameFolder, FileMode.OpenOrCreate);
            DES DESalg = DES.Create();

            CryptoStream cStream = new CryptoStream(fStream,
            DESalg.CreateEncryptor(Key, IV),
            CryptoStreamMode.Write);

            StreamWriter sWriter = new StreamWriter(cStream);

            sWriter.WriteLine(Data);

            sWriter.Close();
            cStream.Close();
            fStream.Close();
        }

        public static string DecryptTextFromFile(string NameFolder, byte[] Key, byte[] IV)
        {
            FileStream fStream = File.Open(NameFolder, FileMode.OpenOrCreate);

            DES DESalg = DES.Create();

            CryptoStream cStream = new CryptoStream(fStream, DESalg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            StreamReader SReader = new StreamReader(cStream);

            string val = SReader.ReadLine();

            SReader.Close();
            cStream.Close();
            fStream.Close();
            Console.WriteLine();
            return val;
        }

        static void Main(string[] args)
        {

            int start, stop;

            
            
            byte[] bytes = {0, 0, 0, 0};
            for(int i=0; i < 4; i++)
            {
                for (int j = 0; j < 255; j++)
                {
                    
                    foreach (var x in bytes)
                    {
                        Console.WriteLine(bytes[3] + " " + bytes[2] + " " + bytes[1] + " " + bytes[0]);
                        bytes[i] += 1;
                    }
                }
            }
            Console.Write("Podaj hasło: ");
            DES DESalg = DES.Create("DES");
            start = Environment.TickCount & Int32.MaxValue;
            string orginalData = Console.ReadLine();
            string NameFolder = "DesPlik.txt";


            //szyforwanie
            EncryptTextToFile(orginalData, NameFolder, DESalg.Key, DESalg.IV);
            //deszyforwanie
            string deszyf = DecryptTextFromFile(NameFolder, DESalg.Key, DESalg.IV);

            Console.WriteLine(deszyf);
            stop = Environment.TickCount & Int32.MaxValue;
            Console.WriteLine("Czas pracy " + (stop - start));
        }
    }
}
