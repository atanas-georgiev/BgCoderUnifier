using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BgCoderUnifier
{
    class Program
    {
        static void Main(string[] args)
        {            
            const string outputFile = "BgCoder.txt";

            StringBuilder data = new StringBuilder();
            string line;
            HashSet<string> usings = new HashSet<string>();

            var csFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.cs", SearchOption.AllDirectories);

            foreach (string file in csFiles)
            {
                if (!file.EndsWith("AssemblyInfo.cs"))
                {
                    using (var reader = new StreamReader(file))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!line.TrimStart().StartsWith("using"))
                            {
                                if (!line.TrimStart().StartsWith("namespace") &&
                                    !line.StartsWith("{") &&
                                    !line.StartsWith("}") &&
                                    (line.TrimStart() != string.Empty))
                                {
                                    data.Append(line + Environment.NewLine);
                                }                                
                            }
                            else
                            {
                                usings.Add(line.TrimStart());
                            }
                        }
                    }
                }
            }
            

            using (var writer = new StreamWriter(outputFile, false))
            {
                foreach (string using1 in usings)
                {
                    if (using1.StartsWith("using System"))
                    {
                        writer.WriteLine(using1);
                    }                    
                }

                writer.WriteLine(data.ToString());                
            }
        }
    }
}
