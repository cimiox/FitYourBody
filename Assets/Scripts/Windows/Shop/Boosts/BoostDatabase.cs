using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using UnityEngine;
using System.Linq;

[Serializable]
public static class BoostDatabase
{
    public static ObservableCollection<Boost> Boosts { get; private set; } = new ObservableCollection<Boost>();

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Boosts.json"))
        {
            using (StreamReader file = new StreamReader(Application.persistentDataPath + "/Boosts.json"))
            {
                string json = file.ReadToEnd();
                var newBoosts = JsonConvert.DeserializeObject<ObservableCollection<Boost>>(json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                newBoosts.ToList().ForEach(x =>
                {
                    if (x.BoostTimer.EndTime > DateTime.Now)
                    {
                        Boosts.Add(x);
                    }
                });
            }
        }
    }

    public static void Save()
    {
        using (StreamWriter file = new StreamWriter(Application.persistentDataPath + "/Boosts.json"))
        {
            string json = JsonConvert.SerializeObject(Boosts, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            file.Write(json);
        }
    }
}
