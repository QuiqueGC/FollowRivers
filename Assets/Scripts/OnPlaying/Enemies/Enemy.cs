using System;
using UnityEngine;

//serializable para que aparezca en el inspector
//scriptable para que puedan crearse instancias en el inspector
[Serializable] [CreateAssetMenu(fileName = "New Enemy", menuName = "New Enemy")] public class Enemy : ScriptableObject
{
    [SerializeField] private float hP;
    [SerializeField] private float speed;
    [SerializeField] private float visualRangeMax;
    [SerializeField] private float visualRangeMin;
    [SerializeField] private float damage;

    public float HP { get => hP; set => hP = value; }
    public float Speed { get => speed; set => speed = value; }
    public float VisualRangeMax { get => visualRangeMax; set => visualRangeMax = value; }
    public float Damage { get => damage; set => damage = value; }
    public float VisualRangeMin { get => visualRangeMin; set => visualRangeMin = value; }
}



