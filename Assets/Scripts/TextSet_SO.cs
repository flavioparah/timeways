using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Localization;

[CreateAssetMenu(fileName ="New Text Set", menuName = "Text Set")]
public class TextSet_SO : ScriptableObject
{
    public StringTableCollection table;

}
