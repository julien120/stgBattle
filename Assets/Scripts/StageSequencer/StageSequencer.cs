using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

[CreateAssetMenu(menuName = "StageSequencer")]
public class StageSequencer : ScriptableObject
{
    [SerializeField] private string filename = "";

    public enum CommandType
    {
        SETSPEED,
        PUTENEMY
    }
    //文字列と列挙体の一致、対応表
    static readonly Dictionary<string, CommandType> commandlist =
        new Dictionary<string, CommandType>()
        {
            {"SETSPEED",CommandType.SETSPEED },
            {"PUTENEMY",CommandType.PUTENEMY },
        };

    /// <summary>
    /// ファイルから読み込んだ内容をそれぞれ対応する変数に格納するstruct変数
    /// stageDataに引数で持たせることができる
    /// </summary>
    public struct StageData
    {
        public readonly float eventPos;
        public readonly CommandType command;
        public readonly float arg1, arg2;
        public readonly uint arg3;
        public StageData(float _eventpos, string _command, float _x, float _y, uint _type)
        {
            eventPos = _eventpos;
            command = commandlist[_command];
            arg1 = _x;
            arg2 = _y;
            arg3 = _type;
        }
    }

    StageData[] stageDatas;

    private int stagedataidx = 0;

    [SerializeField] Enemy[] enemyPrefabs = default;

    public void Load()
    {
        //名前から番号を逆引きする辞書作成
        var revarr = new Dictionary<string, uint>();
        for (uint i = 0; i < enemyPrefabs.Length; ++i)
        {
            revarr.Add(enemyPrefabs[i].name, i);
        }

        var stagecsvdata = new List<StageData>();
        var csvdata = Resources.Load<TextAsset>(filename).text;
        //文字列読み込み1行,区切りずつに
        StringReader sr = new StringReader(csvdata);
        while (sr.Peek() != -1)
        {
            var line = sr.ReadLine();
            var cols = line.Split(',');
            if (cols.Length != 5) continue; //5項目なければwhile脱出

            stagecsvdata.Add(
                new StageData(
                    float.Parse(cols[0]),
                    cols[1],
                    float.Parse(cols[2]),
                    float.Parse(cols[3]),
                    revarr.ContainsKey(cols[4]) ? revarr[cols[4]] : 0
                    //三項演算子でscvの名前データから辞書と照合する内容を参照
                    )
                );

            //Debug.Log(line);
        }
        stageDatas = stagecsvdata.OrderBy(item => item.eventPos).ToArray();
    }

    public void Reset()
    {
        stagedataidx = 0;
    }

    public void Step(float _stageProgressTime)
    {
        while (stagedataidx < stageDatas.Length && stageDatas[stagedataidx].eventPos <= _stageProgressTime)
        {
            switch (stageDatas[stagedataidx].command)
            {
                case CommandType.SETSPEED:
                    StageController.Instance.stageSpeed = stageDatas[stagedataidx].arg1;
                    break;

                case CommandType.PUTENEMY:
                    var enmtmp = Instantiate(enemyPrefabs[stageDatas[stagedataidx].arg3]);
                    enmtmp.transform.parent = StageController.Instance.enemyPool;
                    enmtmp.transform.localPosition = new Vector3(stageDatas[stagedataidx].arg1, 0, stageDatas[stagedataidx].arg2);
                    break;
            }
            ++stagedataidx;
        }
    }

}
