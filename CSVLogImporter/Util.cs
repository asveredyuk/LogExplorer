using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSVLogImporter
{
    public static class Util
    {
        public static string ComputeMD5Hash(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static string ComputeFileMD5Hash(string fpath)
        {
            using (Stream stream = File.OpenRead(fpath))
            {
                return ComputeMD5Hash(stream);
            }
        }

        public static string ComputeMD5Hash(byte[] arr)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(arr);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static string ComputeStringMD5Hash(string str)
        {
            byte[] arr = Encoding.UTF8.GetBytes(str);
            return ComputeMD5Hash(arr);
        }
        public static string GetEmptyMD5Hash()
        {
            return ComputeMD5Hash(new byte[0]);
        }
        public static string GenProgressBar(int percentage, int size)
        {
            char[] arr = new char[size];
            for (int i = 0; i < size; i++)
            {
                int percentageNow = (i + 1) * 100 / size;
                if (percentageNow <= percentage)
                    arr[i] = '=';
                else
                    arr[i] = ' ';
            }
            return new string(arr);
        }
        public static string ExtendedProgressBar(int percentage, int size)
        {
            string progr = GenProgressBar(percentage, size);
            return $"[{progr}]{percentage}%";
        }

        public static string ExtendedProgressBar(long done, long total, int size)
        {
            int percentage = (int)(done * 100 / total);
            return ExtendedProgressBar(percentage, size);
        }

        public static void Progress(long done, long total)
        {
            Console.Write(ExtendedProgressBar(done, total, 50) + "\r");
        }

        public static void ProgressEachFraction(long done, long total, long fraction)
        {
            if (done % (total / fraction) == 0 || done == total)
                Progress(done, total);
        }
        public static void ProgressEach(long done, int total, long each)
        {
            if (done % each == 0 || done == total)
            {
                Progress(done, total);
            }
        }

        public static string MemConverter(long mem)
        {
            if (mem < 1024)
                return mem + " B";
            mem /= 1024;
            if (mem < 1024)
                return mem + " KB";
            mem /= 1024;
            if (mem < 1024)
                return mem + " MB";
            mem /= 1024;
            return mem + " GB";

        }
    }
}
