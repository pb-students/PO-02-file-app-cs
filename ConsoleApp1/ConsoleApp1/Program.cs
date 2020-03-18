using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Realizacja();
        }

        public static void Realizacja()
        {
            ///Otwieranie pliku
            string path, pathOut;
            while(true)
            {
                path = Console.ReadLine();
                pathOut = path + ".max.txt";
                path += ".txt";
                if (!File.Exists(path))
                    Console.WriteLine("Plik o podanej nazwie nie istnieje ( " + path + " )");
                else
                    break;
            }
            StreamReader streamIn = new StreamReader(path);
            List<string[]> lines = new List<string[]>();

            ///Odczytywanie pliku
            string line_;
            while ((line_ = streamIn.ReadLine()) != null)
                lines.Add(line_.Split(' '));
            streamIn.Close();

            ///Sprawdzanie największej skuteczności
            float highestEfficiency = float.MinValue;
            List<int> highestEfficiencyIndexes = new List<int>();

            int i = 0;
            float efficiency_ = 0f;
            foreach (string[] line in lines)
            {
                ///Na mojej maszynie nie wiem czemu ale Convert.String() nie przyjmuje '.' jako znaku dziesiętnego stąd te .Replace()                ///
                efficiency_ = Convert.ToSingle(line[line.Length - 1].Replace('.', ','));
                if (efficiency_ > highestEfficiency)
                {
                    highestEfficiency = efficiency_;
                    highestEfficiencyIndexes.Clear();
                    highestEfficiencyIndexes.Add(i);
                }
                else if(efficiency_ == highestEfficiency)
                    highestEfficiencyIndexes.Add(i);

                i++;
            }

            ///Tworzenie pliku wyspecyfikowanego w zadaniu
            bool bulionKontrolny = false;
            StreamWriter streamOut = new StreamWriter(pathOut);
            foreach(int index in highestEfficiencyIndexes)
            {
                //Console.WriteLine("Jestem tutaj kilka razy, albo raz, bo ja wiem?!");
                string[] suspect = lines[index];
                if(suspect[0].Length > 3 && suspect[1].EndsWith("ski"))
                {
                    //Console.WriteLine("\n" + lines[index]);
                    //Console.WriteLine(Anonimize(suspect[0]) + Anonimize(suspect[1]) + suspect[2]);
                    streamOut.WriteLine(Anonimize(suspect[0]) + " " + Anonimize(suspect[1]) + " " + suspect[2]);
                    bulionKontrolny = true;
                }
            }
            if (!bulionKontrolny)
                streamOut.WriteLine("");
            streamOut.Close();

            Console.WriteLine("Program wykonał się pomyślnie, rezultat jest w pliku: " + pathOut + "\n");
        }

        public static string Anonimize(string input)
        {
            char[] output = input.ToCharArray();
            for (int i = 1; i < output.Length - 3; i++)
                output[i] = '*';
            return new string(output);
        }
    }
}
