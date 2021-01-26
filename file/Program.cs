using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace file
{
    class Program
    {
        //файл каталог
        static void Main(string[] args)
        {
            // Запись текста в файл
            File.WriteAllText("1.txt", "Hello, world!");

			//класс, для работы с каталогами - System.IO.Directory

			// Путь относительно "текущей директории"
			Console.WriteLine(Environment.CurrentDirectory);
            // Обычно это директория, в которой была запущена ваша программа

            // размещение запущенного exe-файла
            Console.WriteLine(Assembly.GetExecutingAssembly().Location);

            // Сформировать абсолютный путь по относительному
            Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "1.txt"));

            // Записать строки в файл, разделив их символом конца строки (\r\n для Windows и \n для Linux и MacOS)
            File.WriteAllLines("2.txt", new[] { "Hello", "world" });

            // Записать в файл массив байтов в бинарном виде:
            File.WriteAllBytes("3.dat", new byte[10240]);

            // Чтение данных из файла
            string text = File.ReadAllText("1.txt");
            string[] lines = File.ReadAllLines("2.txt");
            byte[] bytes = File.ReadAllBytes("3.dat");

            // Все файлы в текущей директории (точка в пути означает текущую директорию)
            foreach (var file in Directory.GetFiles("."))
                Console.WriteLine(file);

			//Методом Encoding.UTF8.GetBytes(string s) можно преобразовать строку s в массив байтов согласно кодировке UTF-8.
			Console.WriteLine(Encoding.UTF8.GetBytes("БЩФzw!").Length); // 9

		}

		//кодирование
		static void WriteAndRead(string text, Encoding encoding)
		{
			Console.WriteLine("{0}, encoding {1}", text, encoding.EncodingName);
			//записать в файл некий текст
			//Альтернативы - WriteAllLines (записывает массив строк) или WriteAllBytes (массив байт)
			File.WriteAllText("temp.txt", text, encoding);

			//прочитать массив байт
			//Альтернативы аналогично тексту
			var bytes = File.ReadAllBytes("temp.txt");
			foreach (var e in bytes)
				Console.Write("  {0} ", (char)e);
			Console.WriteLine();
			foreach (var e in bytes)
				Console.Write("{0:D3} ", e);
			Console.WriteLine();
			//В С# есть явное преобразование типа byte в char. 
		}
		public static void Main2()
		{
			//Английский язык и базовые символы одинаковы во всех кодировках
			//но при сохранении текста в кодировке UTF добавляется специальный маркер файла, по которому текстовые редакторы определяют кодировку текста
			WriteAndRead("Hello!", Encoding.ASCII);
			WriteAndRead("Hello!", Encoding.UTF8);

			//Русские буквы нельзя сохранять в ASCII
			WriteAndRead("Привет!", Encoding.ASCII);

			//Можно попробовать в кодировке локали, но этого лучше не делать:
			//В этом случае файл не самодостаточен, для его прочтения нужно знать какая кодировка у вас в локали
			WriteAndRead("Привет!", Encoding.Default);

			//UTF-8 - лучшее решение!
			//Русские буквы кодируются в ней двумя байтами
			WriteAndRead("Привет!", Encoding.UTF8);
			//А китайские иероглифы - уже тремя
			WriteAndRead("你好!", Encoding.UTF8);
		}
	}
}
