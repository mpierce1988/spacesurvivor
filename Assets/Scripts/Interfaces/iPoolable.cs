using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPoolable 
{
    public void ReturnToPool();
    public void SetReturnToPoolAction(Action<GameObject> returnToPoolAction);
}
