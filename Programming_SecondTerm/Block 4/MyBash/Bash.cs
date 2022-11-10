using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashAnalogue
{
    internal class MyBash
    {
        private int LastRes;

        private List<KeyValue<string, string>> LVar = new();

        
        private void RunCommand(string[] input)
        {
            List<string> output = new();

            switch (input[0])
            {
                case "pwd":
                    string pwdResult = Directory.GetCurrentDirectory();
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        Console.WriteLine(pwdResult);
                        Console.WriteLine();
                        LastRes = 0;
                    }
                    else
                    {
                        output.Add(pwdResult);
                        input = input.Skip(1).ToArray();
                        FileManager(input, output);
                    }
                    output = default;
                    break;
                case "cat":
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        Console.WriteLine("Bash: cat: требуются какие либо аргументы\n");
                        LastRes = 1;
                    }
                    else if (input.Length == 2)
                    {
                        string path = @input[1];
                        try
                        {
                            using (StreamReader sr = new StreamReader(path))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    Console.WriteLine(line);
                                }
                            }
                            Console.WriteLine();
                            LastRes = 0;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(@$"Bash: {path}: Такого файла или каталога нет");
                            Console.WriteLine();
                            LastRes = 1;
                        }
                    }
                    else
                    {
                        string path = @input[1];
                        try
                        {
                            using (StreamReader sr = new StreamReader(path))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    output.Add(line);
                                }
                            }
                            input = input.Skip(2).ToArray();
                            FileManager(input, output);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(@$"Bash: {path}: Такого файла или каталога нет");
                            Console.WriteLine();
                            LastRes = 1;
                        }
                    }
                    output = default;
                    break;
                case "echo":
                    string outputString = "";
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        Console.WriteLine();
                        LastRes = 0;
                    }
                    else if (input.Length == 2)
                    {
                        if (input[1] == "$?")
                        {
                            Console.WriteLine(LastRes);
                            Console.WriteLine();
                            LastRes = 0;
                        }
                        else if (input[1][0] == '$' && input[1].Length > 1)
                        {
                            string id = input[1].TrimStart('$');
                            string text = "";
                            bool findFlag = false;

                            foreach (var item in LVar)
                            {
                                if (item.Id == id)
                                {
                                    text = item.Text;
                                    findFlag = true;
                                }
                            }

                            if (findFlag)
                            {
                                Console.WriteLine(text);
                                Console.WriteLine();
                                LastRes = 0;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                LastRes = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine(input[1]);
                            Console.WriteLine();
                            LastRes = 0;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < input.Length; i++)
                        {
                            if (input[i] == "$?")
                            {
                                input[i] = LastRes.ToString();
                            }
                            else if (input[i][0] == '$' && input[i].Length > 1)
                            {
                                string id = input[i].TrimStart('$');
                                string text = "";
                                bool findFlag = false;

                                foreach (var item in LVar)
                                {
                                    if (item.Id == id)
                                    {
                                        text = item.Text;
                                        findFlag = true;
                                    }
                                }

                                if (findFlag)
                                {
                                    input[i] = text;
                                }
                                else
                                {
                                    input[i] = "";
                                }
                            }
                            else if (input[i] == ">" || input[i] == "<" || input[i] == ">>")
                            {
                                for (int j = 1; j < i; j++)
                                {
                                    outputString = outputString + input[j];
                                    outputString = outputString + " ";
                                }
                                input = input.Skip(i).ToArray();
                                output.Add(outputString);
                                break;
                            }
                        }
                        if (input[0] == ">" || input[0] == "<" || input[0] == ">>")
                        {
                            FileManager(input, output);
                        }
                        else
                        {
                            for (int i = 1; i < input.Length; i++)
                            {
                                Console.Write(input[i]);
                                Console.Write(" ");
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            LastRes = 0;
                        }
                    }
                    output = default;
                    break;
                case "true":
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        LastRes = 0;
                        Console.WriteLine();
                    }
                    else
                    {
                        for (int i = 1; i < input.Length; i++)
                        {
                            if (input[i] == ">" || input[i] == "<" || input[i] == ">>")
                            {
                                input = input.Skip(i).ToArray();
                                output.Add("");
                                break;
                            }
                        }
                        if (input[0] == ">" || input[0] == "<" || input[0] == ">>")
                        {
                            FileManager(input, output);
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                    LastRes = 0;
                    output = default;
                    break;
                case "false":
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        LastRes = 1;
                        Console.WriteLine();
                    }
                    else
                    {
                        for (int i = 1; i < input.Length; i++)
                        {
                            if (input[i] == ">" || input[i] == "<" || input[i] == ">>")
                            {
                                input = input.Skip(i).ToArray();
                                output.Add("");
                                break;
                            }
                        }
                        if (input[0] == ">" || input[0] == "<" || input[0] == ">>")
                        {
                            FileManager(input, output);
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                    LastRes = 1;
                    output = default;
                    break;
                case "wc":
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        Console.WriteLine("Bash: wc: требуются какие либо аргументы\n");
                        LastRes = 1;
                    }
                    else if (input.Length == 2)
                    {
                        int linesCount = 0;
                        int wordsCount = 0;
                        long bytesCount = 0;
                        string path = @input[1];
                        try
                        {
                            bytesCount = new FileInfo(path).Length;
                            using (StreamReader sr = new StreamReader(path))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    linesCount++;
                                    line = line.Trim();
                                    string[] parsedLine = line.Split();
                                    parsedLine = parsedLine.Where(val => val != "").ToArray();
                                    wordsCount += parsedLine.Length;
                                }
                            }
                            Console.Write(linesCount + " " + wordsCount + " " + bytesCount);
                            Console.WriteLine("\n");
                            LastRes = 0;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(@$"Bash: {path}: Такого файла или каталога нет");
                            Console.WriteLine();
                            LastRes = 1;
                        }
                    }
                    else
                    {
                        int linesCount = 0;
                        int wordsCount = 0;
                        long bytesCount = 0;
                        string path = @input[1];
                        try
                        {
                            bytesCount = new FileInfo(path).Length;
                            using (StreamReader sr = new StreamReader(path))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    linesCount++;
                                    line = line.Trim();
                                    string[] parsedLine = line.Split();
                                    parsedLine = parsedLine.Where(val => val != "").ToArray();
                                    wordsCount += parsedLine.Length;
                                }
                            }
                            string outputData = linesCount.ToString() + " " + wordsCount.ToString() + " " + bytesCount.ToString();
                            output.Add(outputData);
                            input = input.Skip(2).ToArray();
                            FileManager(input, output);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(@$"Bash: {path}: Такого файла или каталога нет");
                            Console.WriteLine();
                            LastRes = 1;
                        }
                    }
                    output = default;
                    break;
                case "scr":
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        Console.WriteLine("Bash: scr: требуются какие либо аргументы\n");
                        LastRes = 1;
                    }
                    else
                    {
                        string path = @input[1];
                        try
                        {
                            using (StreamReader sr = new StreamReader(path))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    line = line.Trim().ToLower();

                                    string[] fileCommand = line.Split();
                                    fileCommand = fileCommand.Where(val => val != "").ToArray();
                                    int pInputLength = fileCommand.Length;
                                    List<int> connectorsIndexes = new List<int>();

                                    if (fileCommand.Length > 0)
                                    {
                                        if (fileCommand[0] == "&&" || fileCommand[0] == "||" || fileCommand[0] == ";")
                                        {
                                            Console.WriteLine("Bash: синтаксическая ошибка рядом с неожиданным токеном\n");
                                            LastRes = 1;
                                            continue;
                                        }
                                        else if (fileCommand[^1] == "&&" || fileCommand[^1] == "||" || fileCommand[^1] == ";")
                                        {
                                            Console.WriteLine("Bash: синтаксическая ошибка рядом с неожиданным токеном\n");
                                            LastRes = 1;
                                            continue;
                                        }
                                    }

                                    for (int i = 0; i < pInputLength; i++)
                                    {
                                        if (fileCommand[i] == "&&" || fileCommand[i] == "||" || fileCommand[i] == ";")
                                        {
                                            connectorsIndexes.Add(i);
                                        }
                                    }

                                    if (connectorsIndexes.Count > 0)
                                    {
                                        int connectorsIndexesCount = connectorsIndexes.Count;
                                        int previousConnectorIndex = default;

                                        while (connectorsIndexes.Count > 0)
                                        {
                                            if (connectorsIndexes.Count == connectorsIndexesCount)
                                            {
                                                if (connectorsIndexesCount == 1)
                                                {
                                                    string[] firstCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                    RunCommand(firstCommand);
                                                    string[] lastCommand = fileCommand.Skip(connectorsIndexes[0] + 1).ToArray();

                                                    if (fileCommand[connectorsIndexes[0]] == "&&")
                                                    {
                                                        if (LastRes == 0)
                                                        {
                                                            RunCommand(lastCommand);
                                                        }
                                                    }
                                                    else if (fileCommand[connectorsIndexes[0]] == "||")
                                                    {
                                                        if (LastRes == 1)
                                                        {
                                                            RunCommand(lastCommand);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        RunCommand(lastCommand);
                                                    }
                                                    connectorsIndexes.RemoveAt(0);
                                                }
                                                else
                                                {
                                                    string[] firstCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                    RunCommand(firstCommand);
                                                    previousConnectorIndex = connectorsIndexes[0];
                                                    connectorsIndexes.RemoveAt(0);
                                                }
                                            }
                                            else
                                            {
                                                if (connectorsIndexes.Count == 1)
                                                {
                                                    if (fileCommand[previousConnectorIndex] == "&&")
                                                    {
                                                        if (LastRes == 0)
                                                        {
                                                            string[] nextCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                            nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                                            RunCommand(nextCommand);
                                                        }
                                                    }
                                                    else if (fileCommand[previousConnectorIndex] == "||")
                                                    {
                                                        if (LastRes == 1)
                                                        {
                                                            string[] nextCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                            nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                                            RunCommand(nextCommand);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string[] nextCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                        nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                                        RunCommand(nextCommand);
                                                    }
                                                    string[] lastCommand = fileCommand.Skip(connectorsIndexes[0] + 1).ToArray();

                                                    if (fileCommand[(connectorsIndexes[0])] == "&&")
                                                    {
                                                        if (LastRes == 0)
                                                        {
                                                            RunCommand(lastCommand);
                                                        }
                                                    }
                                                    else if (fileCommand[(connectorsIndexes[0])] == "||")
                                                    {
                                                        if (LastRes == 1)
                                                        {
                                                            RunCommand(lastCommand);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        RunCommand(lastCommand);
                                                    }
                                                    connectorsIndexes.RemoveAt(0);
                                                }
                                                else
                                                {
                                                    if (fileCommand[previousConnectorIndex] == "&&")
                                                    {
                                                        if (LastRes == 0)
                                                        {
                                                            string[] nextCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                            nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                                            RunCommand(nextCommand);
                                                        }
                                                    }
                                                    else if (fileCommand[previousConnectorIndex] == "||")
                                                    {
                                                        if (LastRes == 1)
                                                        {
                                                            string[] nextCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                            nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                                            RunCommand(nextCommand);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string[] nextCommand = fileCommand.Take(connectorsIndexes[0]).ToArray();
                                                        nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                                        RunCommand(nextCommand);
                                                    }
                                                    previousConnectorIndex = connectorsIndexes[0];
                                                    connectorsIndexes.RemoveAt(0);

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        RunCommand(fileCommand);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(@$"Bash: {path}: Такого файла или каталога нет");
                            Console.WriteLine();
                            LastRes = 1;
                        }
                    }
                    output = default;
                    break;
                default:
                    input = CheckOperators(input);
                    if (input == null)
                    {
                    }
                    else if (input.Length == 1)
                    {
                        if (input[0][0] == '$' && input[0].Length > 1)
                        {
                            bool findFlag = false;
                            string id = input[0].TrimStart('$');
                            string textCommand = default;

                            foreach (var item in LVar)
                            {
                                if (item.Id == id)
                                {
                                    textCommand = item.Text;
                                    findFlag = true;
                                }
                            }

                            if (findFlag)
                            {
                                string[] command = { textCommand };
                                RunCommand(command);
                            }
                            else
                            {
                                LastRes = 0;
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Bash: {input[0]}: command not found\n");
                            LastRes = 1;
                        }
                    }
                    else
                    {
                        if (input[1] != "=")
                        {
                            Console.WriteLine($"Bash: {input[0]}: command not found\n");
                            LastRes = 1;
                        }
                        else
                        {
                            if (input.Length == 2)
                            {
                                bool changeFlag = false;

                                for (int i = 0; i < LVar.Count; i++)
                                {
                                    if (LVar[i].Id == input[0])
                                    {
                                        LVar[i].Text = "";
                                        changeFlag = true;
                                    }
                                }
                                if (changeFlag)
                                {
                                }
                                else
                                {
                                    LVar.Add(new KeyValue<string, string>(input[0], ""));
                                }
                            }
                            else
                            {
                                bool changeFlag = false;

                                for (int i = 0; i < LVar.Count; i++)
                                {
                                    if (LVar[i].Id == input[0])
                                    {
                                        LVar[i].Text = input[2];
                                        changeFlag = true;
                                    }
                                }
                                if (changeFlag)
                                {
                                }
                                else
                                {
                                    LVar.Add(new KeyValue<string, string>(input[0], input[2]));
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    break;
            }
        }

        public void Run()
        {
            string[] pInput;

            while (true)
            {
                Console.Write("$ ");
                string input = Console.ReadLine();
                input = input.Trim().ToLower();
                pInput = input.Split();
                pInput = pInput.Where(val => val != "").ToArray();
                int pInputLength = pInput.Length;
                List<int> connectorsIndexes = new List<int>();

                if (pInput.Length > 0)
                {
                    if (pInput[0] == "&&" || pInput[0] == "||" || pInput[0] == ";")
                    {
                        Console.WriteLine("Bash: синтаксическая ошибка рядом с неожиданным токеном\n");
                        LastRes = 1;
                        continue;
                    }
                    else if (pInput[^1] == "&&" || pInput[^1] == "||" || pInput[^1] == ";")
                    {
                        Console.WriteLine("Bash: синтаксическая ошибка рядом с неожиданным токеном\n");
                        LastRes = 1;
                        continue;
                    }
                }

                for (int i = 0; i < pInputLength; i++)
                {
                    if (pInput[i] == "&&" || pInput[i] == "||" || pInput[i] == ";")
                    {
                        connectorsIndexes.Add(i);
                    }
                }

                if (connectorsIndexes.Count > 0)
                {
                    int connectorsIndexesCount = connectorsIndexes.Count;
                    int previousConnectorIndex = default;

                    while (connectorsIndexes.Count > 0)
                    {
                        if (connectorsIndexes.Count == connectorsIndexesCount)
                        {
                            if (connectorsIndexesCount == 1)
                            {
                                string[] firstCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                RunCommand(firstCommand);
                                string[] lastCommand = pInput.Skip(connectorsIndexes[0] + 1).ToArray();

                                if (pInput[connectorsIndexes[0]] == "&&")
                                {
                                    if (LastRes == 0)
                                    {
                                        RunCommand(lastCommand);
                                    }
                                }
                                else if (pInput[connectorsIndexes[0]] == "||")
                                {
                                    if (LastRes == 1)
                                    {
                                        RunCommand(lastCommand);
                                    }
                                }
                                else
                                {
                                    RunCommand(lastCommand);
                                }
                                connectorsIndexes.RemoveAt(0);
                            }
                            else
                            {
                                string[] firstCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                RunCommand(firstCommand);
                                previousConnectorIndex = connectorsIndexes[0];
                                connectorsIndexes.RemoveAt(0);
                            }
                        }
                        else
                        {
                            if (connectorsIndexes.Count == 1)
                            {
                                if (pInput[previousConnectorIndex] == "&&")
                                {
                                    if (LastRes == 0)
                                    {
                                        string[] nextCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                        nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                        RunCommand(nextCommand);
                                    }
                                }
                                else if (pInput[previousConnectorIndex] == "||")
                                {
                                    if (LastRes == 1)
                                    {
                                        string[] nextCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                        nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                        RunCommand(nextCommand);
                                    }
                                }
                                else
                                {
                                    string[] nextCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                    nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                    RunCommand(nextCommand);
                                }
                                string[] lastCommand = pInput.Skip(connectorsIndexes[0] + 1).ToArray();

                                if (pInput[(connectorsIndexes[0])] == "&&")
                                {
                                    if (LastRes == 0)
                                    {
                                        RunCommand(lastCommand);
                                    }
                                }
                                else if (pInput[(connectorsIndexes[0])] == "||")
                                {
                                    if (LastRes == 1)
                                    {
                                        RunCommand(lastCommand);
                                    }
                                }
                                else
                                {
                                    RunCommand(lastCommand);
                                }
                                connectorsIndexes.RemoveAt(0);
                            }
                            else
                            {
                                if (pInput[previousConnectorIndex] == "&&")
                                {
                                    if (LastRes == 0)
                                    {
                                        string[] nextCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                        nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                        RunCommand(nextCommand);
                                    }
                                }
                                else if (pInput[previousConnectorIndex] == "||")
                                {
                                    if (LastRes == 1)
                                    {
                                        string[] nextCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                        nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                        RunCommand(nextCommand);
                                    }
                                }
                                else
                                {
                                    string[] nextCommand = pInput.Take(connectorsIndexes[0]).ToArray();
                                    nextCommand = nextCommand.Skip(previousConnectorIndex + 1).ToArray();
                                    RunCommand(nextCommand);
                                }
                                previousConnectorIndex = connectorsIndexes[0];
                                connectorsIndexes.RemoveAt(0);

                            }
                        }
                    }
                }
                else
                {
                    RunCommand(pInput);
                }
            }
        }



        private string[] CheckOperators(string[] command)
        {
            if (command[^1] == ">" || command[^1] == "<" || command[^1] == ">>")
            {
                Console.WriteLine("Bash: синтаксическая ошибка рядом с неожиданным токеном 'newline'\n");
                command = null;
                LastRes = 1;
                return command;
            }
            return command;
        }

        private void FileManager(string[] input, List<string> output)
        {
            bool writeF = false;
            while (input != null && input.Length > 0)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ">" && input.Length > (i + 1))
                    {
                        string fullPath = @input[i + 1]; 

                        if (File.Exists(fullPath)) 
                        {
                            using (StreamWriter writer = new StreamWriter(fullPath, false))
                            {
                                foreach (var item in output)
                                {
                                    writer.WriteLine(item);
                                }
                            }
                            input = input.Skip(i + 2).ToArray();
                            LastRes = 0;
                            writeF = true;
                            Console.WriteLine();
                            break;
                        }
                        else if (!File.Exists(fullPath))
                        {
                            try
                            {
                                using (StreamWriter writer = new StreamWriter(fullPath, false))
                                {
                                    foreach (var item in output)
                                    {
                                        writer.WriteLine(item);
                                    }
                                }
                                input = input.Skip(i + 2).ToArray();
                                LastRes = 0;
                                writeF = true;
                                Console.WriteLine();
                                break;

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(@$"Bash: {fullPath}: Такого файла или каталога нет");
                                Console.WriteLine();
                                input = null;
                                LastRes = 1;
                                break;
                            }
                        }
                    }
                    else if (input[i] == ">>" && input.Length > (i + 1))
                    {
                        string fullPath = @input[i + 1];

                        if (File.Exists(fullPath))
                        {
                            using (StreamWriter writer = new StreamWriter(fullPath, true))
                            {
                                foreach (var item in output)
                                {
                                    writer.WriteLine(item);
                                }
                            }
                            input = input.Skip(i + 2).ToArray();
                            LastRes = 0;
                            writeF = true;
                            Console.WriteLine();
                            break;
                        }
                        else if (!File.Exists(fullPath)) 
                        {
                            try
                            {
                                using (StreamWriter writer = new StreamWriter(fullPath, true))
                                {
                                    foreach (var item in output)
                                    {
                                        writer.WriteLine(item);
                                    }
                                }
                                input = input.Skip(i + 2).ToArray();
                                LastRes = 0;
                                writeF = true;
                                Console.WriteLine();
                                break;

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(@$"Bash: {fullPath}: Такого файла или каталога нет");
                                Console.WriteLine();
                                input = null;
                                LastRes = 1;
                                break;
                            }
                        }
                    }
                    else if (input[i] == "<" && input.Length > (i + 1))
                    {
                        if (!writeF)
                        {
                            foreach (var item in output)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                            input = null;
                            LastRes = 0;
                            break;
                        }
                        else
                        {
                            input = null; 
                            break;
                        }
                    }
                    else
                    {
                        if (!writeF)
                        {
                            foreach (var item in output)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                            input = null;
                            LastRes = 0;
                            break;
                        }
                        else
                        {
                            input = null; 
                            break;
                        }
                    }
                }
                continue;
            }
        }
    }
    internal class KeyValue<Key, Value>
    {
        public Key Id { get; set; }
        public Value Text { get; set; }
        public KeyValue() { }

        public KeyValue(Key key, Value valune)
        {
            this.Id = key;
            this.Text = valune;
        }
    }
}
