using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WheelReward))]
public class WheelRewardEditor : PropertyDrawer
{
    public WheelRewardType RewardType { get; set; }

    public List<Item> GainerItems { get; set; } = new List<Item>();
    public List<Item> ProteinItems { get; set; } = new List<Item>();

    private const string GAINER_RESOURCES_PATH = "Shop/GeinerShop";
    private const string PROTEIN_RESOURCES_PATH = "Shop/ProteinShop";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GainerItems = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>(GAINER_RESOURCES_PATH).text);
        ProteinItems = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>(PROTEIN_RESOURCES_PATH).text);

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        property.FindPropertyRelative("rewardType").enumValueIndex = (int)(WheelRewardType)Enum.Parse(typeof(WheelRewardType),
            EditorGUI.EnumPopup(new Rect(position.x, position.y, 100, position.height), (Enum)Enum.Parse(typeof(WheelRewardType), ((WheelRewardType)property.FindPropertyRelative("rewardType").enumValueIndex).ToString())).ToString());

        switch (property.FindPropertyRelative("rewardType").enumValueIndex)
        {
            case 0:
                if (!string.IsNullOrEmpty(property.FindPropertyRelative("reward").stringValue)
                    && GainerItems.FirstOrDefault(x => x.Name == property.FindPropertyRelative("reward").stringValue) != null)
                {
                    property.FindPropertyRelative("reward").stringValue = GainerItems[EditorGUI.Popup(new Rect(position.x + 100, position.y, 50, position.height),
                        GainerItems.IndexOf(GainerItems.First(x => x.Name == property.FindPropertyRelative("reward").stringValue)),
                        GainerItems.Select(x => x.Name).ToArray())].Name;
                }
                else
                {
                    property.FindPropertyRelative("reward").stringValue = GainerItems[0].Name;
                }


                property.FindPropertyRelative("count").intValue = 
                    EditorGUI.IntField(new Rect(position.x + 150, position.y, 50, position.height), property.FindPropertyRelative("count").intValue);
                break;
            case 1:

                if (!string.IsNullOrEmpty(property.FindPropertyRelative("reward").stringValue) 
                    && ProteinItems.FirstOrDefault(x => x.Name == property.FindPropertyRelative("reward").stringValue) != null)
                {
                    property.FindPropertyRelative("reward").stringValue = ProteinItems[EditorGUI.Popup(new Rect(position.x + 100, position.y, 50, position.height),
                        ProteinItems.IndexOf(ProteinItems.First(x => x.Name == property.FindPropertyRelative("reward").stringValue)),
                        ProteinItems.Select(x => x.Name).ToArray())].Name;
                }
                else
                {
                    property.FindPropertyRelative("reward").stringValue = ProteinItems[0].Name;
                }


                property.FindPropertyRelative("count").intValue =
                    EditorGUI.IntField(new Rect(position.x + 150, position.y, 50, position.height), property.FindPropertyRelative("count").intValue);

                break;
            case 2:
                property.FindPropertyRelative("reward").stringValue = "Coin";

                property.FindPropertyRelative("count").intValue =
                    EditorGUI.IntField(new Rect(position.x + 100, position.y, 50, position.height), property.FindPropertyRelative("count").intValue);
                break;
            case 3:
                property.FindPropertyRelative("reward").stringValue = "Dollars";

                property.FindPropertyRelative("count").intValue =
                    EditorGUI.IntField(new Rect(position.x + 100, position.y, 50, position.height), property.FindPropertyRelative("count").intValue);
                break;
            default:
                break;
        }

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
