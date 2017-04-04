using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FileToByteArray
{
    internal class Program
    {
        private static string MakeDataString(string name, byte[] data)
        {
            var sb = new StringBuilder();
            sb.AppendLine("        public static byte[] " + name + " => Convert.FromBase64String(\""+ Convert.ToBase64String(data) + "\");");
            // Below commented code makes it a byte array while the above is for a base64 string
            //sb.Append("        public static readonly byte[] " + name + " = {");
            //for (var i = 0; i < data.Length; i++)
            //{
            //    if (i % 16 == 0)
            //    {
            //        sb.AppendLine();
            //        sb.Append("            ");
            //    }
            //    sb.AppendFormat("0x{0:X02}, ", data[i]);
            //}
            //sb.AppendLine();
            //sb.AppendLine("        };");
            return sb.ToString();
        }

        private static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                if (!File.Exists(arg))
                    continue;
                var sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("namespace LexiconLMS");
                sb.AppendLine("{");
                sb.AppendLine("    partial class DocumentSeedData");
                sb.AppendLine("    {");

                // Get filename without extension
                var fname = Path.GetFileNameWithoutExtension(arg);
                // Replace anything that isn't a letter or number with _
                fname = Regex.Replace(fname, "[^a-zA-Z0-9]", "_");

                sb.AppendLine(MakeDataString(fname, File.ReadAllBytes(arg)));

                sb.AppendLine("    }");
                sb.AppendLine("}");
                File.WriteAllText(fname + ".cs", sb.ToString());
            }
        }
    }
}
