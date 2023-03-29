using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string DirPath = "";
            do
            {
                Console.WriteLine("Введите путь до папки для очистки:");
                DirPath = @"" + Console.ReadLine();
            } while (CheckDir(DirPath));

            foreach (var d in Directory.GetDirectories(DirPath))
            {
                if (DateTime.Now - System.IO.File.GetLastWriteTime(d) > TimeSpan.FromMinutes(30))
                {
                    Directory.Delete(d, true);
                }

            }

            foreach (var f in Directory.GetFiles(DirPath))
            {
                if (DateTime.Now - System.IO.File.GetLastWriteTime(f) > TimeSpan.FromMinutes(30))
                {
                    File.Delete(f);
                }
            }
        }

        static bool CheckDir(string dp)
        {
            if (dp != "")
            {
                if (Directory.Exists(dp))
                {
                    try
                    {
                        foreach (var d in Directory.GetFiles(dp))
                        {
                            File.Delete(d);
                        }
                    } catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("У вас нет доступа к данной папке");
                        return true;
                    }

                    try
                    {
                        foreach (var d in Directory.GetDirectories(dp))
                        {
                            Directory.Delete(d, true);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("У вас нет доступа к данному файлу");
                        return true;
                    }

                    return false;
                }
                else { Console.WriteLine("Введенная папка не существует"); return true; }
            }
            else { return true; }
            
        }

    }
}