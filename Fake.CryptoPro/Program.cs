// Fake.CryptoPro\Fake.CryptoPro> dotnet.exe publish .\Fake.CryptoPro.csproj -c Release -o e:\Work
using System.Text;

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

        using (StreamWriter writer = new(OutFilePath, false))
        {
            writer.WriteLine($"{DateTime.Now:f}");
            writer.WriteLine($"{string.Join("; ", args)}");
        }

        if (IsUseUserChoice)
        {
            Console.Write("Тест ожадания пользовательского ввода, введите любой текст и нажминте Enter: ");
            Console.Write("Запрос: ");
            Console.ReadLine();
        }

        Console.WriteLine($"Create file: {OutFilePath}{Environment.NewLine}Success!!!");

    } else Console.WriteLine($"Не верные параметры (нет файла): {InFilePath}");
} else
{
    Console.WriteLine("Не верные параметры, ожидается:");
    Console.WriteLine();
    Console.WriteLine($"{"c:\\Work\\FileName.txt",-20} : путь к обрабатываемогу файлу");
    Console.WriteLine($"{"signature.sig",-20} : имя создаваемого файла, в том же каталоге (опционально)");
    Console.WriteLine($"{"-k",-20} : признак запроса команды (опционально)");
    Console.WriteLine();
    Console.WriteLine($"Примечание:");
    Console.WriteLine($"\tисползовать только полные пути в кавычках");
    Console.WriteLine($"\tисползовать только полные пути в кавычках");
    Console.WriteLine($"\tизбегать кирилических имён");
    Console.WriteLine($"\tизбегать каталогов с пробелами");
    Console.WriteLine($"\tне использовать системные каталоги и корени диска");
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