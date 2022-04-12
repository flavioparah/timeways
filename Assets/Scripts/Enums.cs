using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum WreckageType { normal, fast, dangerous };
    public enum Connection { none, door, elevator, ulises, antenna };

    public enum Screen { connection, map, ulises, suit };

    public enum Interaction { zoomOutButton, Ulises, ExitHatch };

    public enum PuzzleMove { up, down, left, right };

}
