using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using UnityEngine;

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
                Boosts = JsonConvert.DeserializeObject<ObservableCollection<Boost>>(json);
            }
        }
    }

    public async static void Save()
    {
        using (StreamWriter file = new StreamWriter(Application.persistentDataPath + "/Boosts.json"))
        {
            string json = JsonConvert.SerializeObject(Boosts);
            await file.WriteAsync(json);
            file.Dispose();
        }
    }
}
