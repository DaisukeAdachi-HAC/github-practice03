﻿using System; // Math使うのに必要
using DxLibDLL;
using MyLib; // MyRandomとかInputとか使うのに必要

namespace Chiritori
{
    public class Game
    {
        Chiritori[] chiritories;
        Gomi gomi; // ゴミ
        int[] scores; // 2人分のスコアを入れるための配列

        public void Init()
        {
            Image.Load(); // 画像の読み込み
            MyRandom.Init(); // MyRandomの初期化
            Input.Init(); // Inputの初期化
            chiritories = new Chiritori[2]; // チリトリーの配列を生成
            chiritories[0] = new Chiritori(DX.PAD_INPUT_1, Image.chiritoriGreen); // Player1生成
            chiritories[1] = new Chiritori(DX.PAD_INPUT_2, Image.chiritoriRed); // Player2生成
            scores = new int[2]; // 配列の生成
            gomi = new Gomi(); // ゴミの生成
        }

        public void Update()
        {
            Input.Update(); // Inputの更新

            for (int i = 0; i < chiritories.Length; i++)
            {
                Chiritori chiritori = chiritories[i];

                chiritori.Update(); // チリトリーの更新

                // チリトリーとゴミの距離を調べる
                float deltaX = chiritori.x - gomi.x; // x方向の差分
                float deltaY = chiritori.y - gomi.y; // y方向の差分
                float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY); // 距離
                                                                                      // 距離が50以下なら
                if (distance <= 50)
                {
                    scores[i] += 1; // 吸ったプレイヤーのスコアを加算
                    gomi.ResetPosition(); // ゴミの場所をリセット
                }
            }
        }

        public void Draw()
        {
            DX.DrawGraph(0, 0, Image.woodFloor); // 背景描画
            gomi.Draw(); // ゴミ描画
            for (int i = 0; i < chiritories.Length; i++)
            {
                chiritories[i].Draw();
            }

            // スコアを描画
            for (int i = 0; i < scores.Length; i++)
            {
                DX.DrawString(300 + 60 * i, 20, scores[i].ToString(), DX.GetColor(255, 255, 255));
            }
        }
    }
}