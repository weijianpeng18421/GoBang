using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ChessType chessColor = ChessType.Black;

    void FixedUpdate()
    {
        if (chessColor == ChessBoard.Instacne.turn && ChessBoard.Instacne.timer > 0.05f)
        {
            PlayChess();
        }

    }

    void PlayChess()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print((int)(pos.x + 7.5f) + "------------" + (int)(pos.y + 7.5f));
            ChessBoard.Instacne.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) });
            ChessBoard.Instacne.timer = 0;
        }
    }
}
