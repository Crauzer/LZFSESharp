using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZFSESharp;
using System.IO;

namespace LZFSESharp.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.WriteLine("Usage: -compress <input file path> <output file path>\n" +
                                  "       -decompress <input file path> <output file path>");
                return;
            }

            OperationType operationType = args[0] == "-compress" ? OperationType.Compress : OperationType.Decompress;
            if(operationType == OperationType.Compress)
            {
                File.WriteAllBytes(args[2], LZFSE.Compress(File.ReadAllBytes(args[1])));
            }
            else
            {
                File.WriteAllBytes(args[2], LZFSE.Decompress(File.ReadAllBytes(args[1])));
            }
        }
    }

    public enum OperationType
    {
        Compress,
        Decompress
    }
}
