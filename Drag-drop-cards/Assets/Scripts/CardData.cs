using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class CardData : ScriptableObject
{
    public PlayerActions playerAction;
    public Sprite artwork;
}
