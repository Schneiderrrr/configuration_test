using System.Xml;

namespace ToConfiguration
{
    // Product
    public class Configuration
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    // Concrete products
    public class ConfigurationFromXML : Configuration { }
    public class ConfigurationFromCSV : Configuration { }


    // Creator
    public abstract class Configurator
    {
        public abstract Configuration GetConfiguration(string source);
    }
    // Concrete creators
    public class ConfiguratorFromXML : Configurator
    {
        public override Configuration GetConfiguration(string source) 
        {
            Configuration config = new ConfigurationFromXML();
            XmlDocument xmlDoc = new();
            xmlDoc.Load(source);
            config.Name = xmlDoc.GetElementsByTagName("name")[0]!.InnerText;
            config.Description = xmlDoc.GetElementsByTagName("description")[0]!.InnerText;

            return config;
        }
    }
    public class ConfiguratorFromCSV : Configurator
    {
        public override Configuration GetConfiguration(string source)
        {
            Configuration config = new ConfigurationFromCSV();
            using (StreamReader sr = new(source))
            {
                string[] text = sr.ReadToEnd().Split(';');
                config.Name = text[0];
                config.Description = text[1];
            }

            return config;
        }
    }

    class Program
    {
        public static void Main(string[] args) 
        { 
            var directory = new DirectoryInfo(Environment.CurrentDirectory + @"\objects");
            if (!directory.Exists)
                directory.Create();

            FileInfo[] files = directory.GetFiles();
            if (files.Length == 0)
                Console.WriteLine($"В папке {directory.FullName} нет конфигураций");

            foreach (var file in files)
            {
                Console.WriteLine($"Файл конфигурации: {file.Name}\n");
                var fileExt = file.Extension;
                Configurator? configurator;
                switch (fileExt)
                {
                    case ".xml":
                        configurator = new ConfiguratorFromXML();
                        break;
                    case ".csv":
                        configurator = new ConfiguratorFromCSV();
                        break;
                    default:
                        Console.WriteLine("Неизвестное расширение файла\n\n");
                        continue;
                }

                try
                {
                    Configuration configuration = configurator!.GetConfiguration(file.FullName); 
                    Console.WriteLine($"Объект класса Configuration: \nName = {configuration.Name};\nDescription = {configuration.Description}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось получить конфигурацию.\nОшибка: {ex.Message}");
                }
                
                Console.WriteLine("\n\n");
            }
        }
    }

}