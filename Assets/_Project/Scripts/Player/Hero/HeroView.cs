using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroView : MonoBehaviour
{
    public MoveType currentMove = MoveType.Up;

    // Start is called before the first frame update
    void Start()
    {
        RandomColor();
    }

    private void RandomColor()
    {
        Color randomColor = UnityEngine.Random.ColorHSV();
        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material.color = randomColor;
        }
    }

    
    void Update()
    {

    }

    public void Move(MoveType type)
    {
        currentMove = type;
        Vector2 dir = new Vector2();
        Quaternion rotateTo = new Quaternion();
        switch (type)
        {
            case MoveType.Up:
                dir = Vector2.up;
                rotateTo = Quaternion.Euler(0, 0, 0);
                break;
            case MoveType.Down:
                dir = Vector2.down;
                rotateTo = Quaternion.Euler(0, 180, 0);
                break;
            case MoveType.Left:
                dir = Vector2.left;
                rotateTo = Quaternion.Euler(0, 270, 0);
                break;
            case MoveType.Right:
                dir = Vector2.right;
                rotateTo = Quaternion.Euler(0, 90, 0);
                break;
        }
        Vector3 newPosition = transform.position + new Vector3(dir.x, 0, dir.y);
        transform.position = newPosition;
        transform.rotation = rotateTo;
    }
}
