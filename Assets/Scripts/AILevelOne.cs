using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILevelOne : Player //集成Player,AI可以看成一个玩家
{
    Dictionary<string, float> toScore = new Dictionary<string, float>();
    private float[,] score = new float[15, 15];

    private void Start()
    {
        /*
         *                                                            打分机制的AI
         * 和判断输赢一样横竖斜上斜下检测整个棋盘的所有位置。假如AI为黑棋,检测整个棋盘时如果在四个方向（上下左右）上有相邻的一个黑棋，则整个
         * 点的类型为“aa”（有两个黑棋相连的情况），此时判断“aa”类型为它打分，如果这个类型再往左为白棋则终止左边的查找，开始查找这个类型
         * 的右边，假如右边是个空格子则用“aa_”表示,给“aa_”类型打分为50分，“_aa”和“aa_”想同所以也赋值为50分，“_aa_”表示“aa”类型左
         * 右均为空格子，分值肯定要高于“_aa”和“aa_”类型，所有给打100分，分值由自己决定，分值越高，该类型优先级越高。后面“aaa”为三个黑棋
         * 相连的情况和“aa”类似，但是“aaa”的分值肯定要高于“aa”的分值。以此类推。"aaaaa"是可以连接成五个黑棋了也就是可以赢了所以优先级最
         * 高分数也给最高分。
         */
        toScore.Add("aa_", 50);
        toScore.Add("_aa", 50);
        toScore.Add("_aa_", 100);

        toScore.Add("aaa_", 500);
        toScore.Add("_aaa", 500);
        toScore.Add("_aaa_", 1000);

        toScore.Add("aaaa_", 5000);
        toScore.Add("_aaaa", 5000);
        toScore.Add("_aaaa_", 10000);

        toScore.Add("aaaaa", float.MaxValue);//只有能连成五个黑棋时考虑这种情况，因为这样可以赢，二三四个黑棋的时候不考虑这种情况是因为左右被白棋堵死后就没意义，
        toScore.Add("aaaaa_", float.MaxValue);
        toScore.Add("_aaaaa", float.MaxValue);
        toScore.Add("_aaaaa_", float.MaxValue);
    }

    public void CheckOneLien(int[] pos, int[] offset)
    {
        string str = "a";
        //从原点向右移动判断
        for (int i = offset[0], j = offset[1];
            pos[0] + i >= 0 && pos[0] + i < 15 && pos[1] + j >= 0 && pos[1] + j < 15;
            i += offset[0], j += offset[1])
        {
            if (ChessBoard.Instacne.grid[pos[0] + i, pos[1] + j] == (int)chessColor)
            {
                str = str + "a";
            }
            else if (ChessBoard.Instacne.grid[pos[0] + i, pos[1] + j] == 0)
            {
                str = str + "_";
                break;
            }
            else
            {
                break;
            }
        }
        //从原点向左移动判断
        for (int i = -offset[0], j = -offset[1];
            pos[0] + i >= 0 && pos[0] + i < 15 && pos[1] + j >= 0 && pos[1] + j < 15;
            i -= offset[0], j -= offset[1])
        {
            if (ChessBoard.Instacne.grid[pos[0] + i, pos[1] + j] == (int)chessColor)
            {
                str = "a" + str;
            }
            else if (ChessBoard.Instacne.grid[pos[0] + i, pos[1] + j] == 0)
            {
                str = "_" + str;
                break;
            }
            else
            {
                break;
            }
        }
        if (toScore.ContainsKey(str))  //ContainsKey判断字典中的key中是否存在和str一样的
        {
            score[pos[0], pos[1]] += toScore[str];
        }
    }

    public void SetScore(int[] pos)
    {
        score[pos[0], pos[1]] = 0;
        CheckOneLien(pos, new int[2] { 1, 0 });
        CheckOneLien(pos, new int[2] { 0, 1 });
        CheckOneLien(pos, new int[2] { 1, 1 });
        CheckOneLien(pos, new int[2] { 1, -1 });
    }
    public override void PlayChess()
    {
        if (ChessBoard.Instacne.chessStack.Count == 0)                  //如果AI先开始下棋
        {
            ChessBoard.Instacne.PlayChess(new int[2] { 7, 7 });
            ChessBoard.Instacne.timer = 0;
            return;
        }

        float maxScore = 0;
        int[] maxPos = new[] { 0, 0 };
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (ChessBoard.Instacne.grid[i, j] == 0)
                {
                    SetScore(new int[2] { i, j });
                    if (score[i, j] >= maxScore)
                    {
                        maxPos[0] = i;
                        maxPos[1] = j;
                        maxScore = score[i, j];
                    }
                }
            }
        }

        if (ChessBoard.Instacne.PlayChess(maxPos))
        {
            ChessBoard.Instacne.timer = 0;
        }
    }
}
