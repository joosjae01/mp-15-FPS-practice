using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeData : MonoBehaviour
{
    // [ PLAYER INTERACT ]
    [SerializeField] public KeyCode _interactionKey = KeyCode.E;

    // [ PLAYER MOVEMENT ]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    //[SerializeField] private KeyCode _slideKey = KeyCode.C;
    //[SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;

    // [ PLAYER WEAPON ]
    [SerializeField] public KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] public KeyCode _reloadKey = KeyCode.R;
    [SerializeField] public KeyCode _grenadeKey = KeyCode.Alpha3;

    // [ PLAYER UTILITIES] 
    //[SerializeField] public KeyCode _OpenMenuKey = KeyCode.Escape;
}