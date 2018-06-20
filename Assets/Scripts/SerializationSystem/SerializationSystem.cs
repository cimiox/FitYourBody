using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class SerializationSystem
{
    public static string PathToProperties { get; set; }
    private static object threadLock = new object();

    public async static void Save(PlayerAttributes.Properties playerProperties)
    {
        string json = JsonConvert.SerializeObject(playerProperties);
        await Task.Run(() =>
        {
            lock (threadLock)
            {
                using (StreamWriter writer = new StreamWriter(GetPath(), false, Encoding.UTF8))
                {
                    writer.WriteAsync(json);
                }
            }
        });
    }

    public static T Load<T>() where T : new()
    {
        if (File.Exists(GetPath()))
        {
            using (StreamReader reader = new StreamReader(GetPath()))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }
        else
            return new T();
    }

    private static string GetPath()
    {
        return string.Format("{0}/Properties.fyb", PathToProperties);
    }
}
