using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeed
{
    float CurrentSpeed { get; }
    float StartSpeed { get; }
    float MaxSpeed { get; }
}
