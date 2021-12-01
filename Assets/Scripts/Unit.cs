using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [System.Flags]
    public enum STATE_TYPE
    {
        Ground = 1 << 0,    // (Left Shift ¿¬»êÀÚ)
        Fly    = 1 << 1,
    }
}
