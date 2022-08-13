using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    IEnumerator ObjectMove(Vector2 initPos);
}
