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

        }

        static bool CheckDir(string dp)
        {
            if (dp != "")
            {
                if (Directory.Exists(dp))
                {
                    try
                    {
                        foreach (var f in Directory.GetFiles(dp))
                        {
                            if (DateTime.Now - System.IO.File.GetLastWriteTime(f) > TimeSpan.FromMinutes(30))
                            {
                                File.Delete(f);
                            }
                            else { Console.WriteLine($"Файл {f} был модифицирован менее 30 минут назад"); }
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
                            if (DateTime.Now - System.IO.File.GetLastWriteTime(d) > TimeSpan.FromMinutes(30))
                            {
                                Directory.Delete(d, true);
                            }
                            else { Console.WriteLine($"Папка {d} был модифицирована менее 30 минут назад"); }
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