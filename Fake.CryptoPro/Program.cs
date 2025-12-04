// Fake.CryptoPro\Fake.CryptoPro> dotnet.exe publish .\Fake.CryptoPro.csproj -c Release -o e:\Work
using System.Security.Cryptography;
using System.Text;

string CalculateMD5(string filename)
{
    using (var md5 = MD5.Create())
    {
        using (var stream = File.OpenRead(filename))
        {
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}

Console.OutputEncoding = Encoding.UTF8;
string InFilePath = string.Empty;
string OutFilePath = string.Empty;
bool IsUseUserChoice = false;

if (args.Length >= 1)
{
    foreach (var el in args)
    {
        if (string.Equals(el.Trim().ToLower(), "-k"))
            IsUseUserChoice = true;
        else if (string.IsNullOrWhiteSpace(InFilePath))
            InFilePath = el.Trim();
        else OutFilePath = el.Trim();
    }

    if (File.Exists(InFilePath))
    {
        if (string.IsNullOrEmpty(OutFilePath))
            OutFilePath = "signature.sig";

        OutFilePath = Path.Combine(Path.GetDirectoryName(InFilePath), OutFilePath);


        string consoleOutput = string.Empty;
        if (IsUseUserChoice)
        {
            Console.WriteLine("Тест ожидания пользовательского ввода, введите любой текст нажмите Enter и закройте CLIApp");
            Console.Write("Запрос: ");
            consoleOutput = Console.ReadLine();
        }

        using (StreamWriter writer = new(OutFilePath, false))
        {
            writer.WriteLine($"{"Дата:",-12}{DateTime.Now:f}");
            writer.WriteLine($"{"Параметры:",-12}{string.Join("; ", args)}");
            writer.WriteLine($"{"Консоль:",-12}{consoleOutput}");
            writer.WriteLine($"{"MD5:",-12}{CalculateMD5(InFilePath)}");
        }

        Console.WriteLine($"Create file: {OutFilePath}{Environment.NewLine}Success!!!");

    } else Console.WriteLine($"Не верные параметры (нет файла): {InFilePath}");
} else
{
    Console.WriteLine("Не верные параметры, ожидается:");
    Console.WriteLine();
    Console.WriteLine($"{"c:\\Work\\FileName.txt",-20} : путь к обрабатываемому файлу");
    Console.WriteLine($"{"signature.sig",-20} : имя создаваемого файла, в том же каталоге (опционально)");
    Console.WriteLine($"{"-k",-20} : признак запроса команды (опционально)");
    Console.WriteLine();
    Console.WriteLine($"Примечание:");
    Console.WriteLine($"\tиспользовать только полные пути в кавычках");
    Console.WriteLine($"\tиспользовать только полные пути в кавычках");
    Console.WriteLine($"\tизбегать кириллических имён");
    Console.WriteLine($"\tизбегать каталогов с пробелами");
    Console.WriteLine($"\tне использовать системные каталоги и корни диска");
    Console.WriteLine();
    Console.WriteLine($"Пример:");
    Console.WriteLine();
    Console.WriteLine($"\tFake.CryptoPro.exe \"c:\\work\\file.txt\" => в каталоге \"c:\\work\" будет создан файл signature.sig");
    Console.WriteLine();
    Console.WriteLine($"\tFake.CryptoPro.exe \"c:\\work\\file.txt\" \"message.sign\", в каталоге \"c:\\work\" будет создан файл message.sign");
    Console.WriteLine();
    Console.WriteLine($"\tFake.CryptoPro.exe \"c:\\work\\file.txt\" \"message.sign\" -k, в каталоге \"c:\\work\" будет создан файл message.sign и запрошены параметры консоли");
    Console.ReadKey();
}