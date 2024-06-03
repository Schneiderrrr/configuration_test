using System;

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
        public abstract Configuration GetConfiguration();
    }
    // Concrete creators
    public class ConfiguratorFromXML : Configurator
    {
        public override Configuration GetConfiguration() 
        { 
            return new ConfigurationFromXML();
        }
    }
    public class ConfiguratorFromCSV : Configurator
    {
        public override Configuration GetConfiguration()
        {
            return new ConfigurationFromCSV();
        }
    }

    class Program
    {
        public static void Main(string[] args) 
        { 
            
        }
    }

}