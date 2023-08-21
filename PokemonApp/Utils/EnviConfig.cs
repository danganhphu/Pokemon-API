namespace PokemonApp.Utils
{
    public class EnviConfig
    {
        public static string DevConnectionString { get; private set; }

        public static void Config(IConfiguration configuration)
            => DevConnectionString = configuration.GetConnectionString("DevConnection");
    }
}