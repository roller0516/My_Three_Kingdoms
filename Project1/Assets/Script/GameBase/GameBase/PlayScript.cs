using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{

    public Text maintext;

    public Text choice1_text;
    public Text choice2_text;
    public Text choice3_text;

    public GameObject btn_choice1;
    public GameObject btn_choice2;
    public GameObject btn_choice3;


    TableInfo_play table_data;

    // Start is called before the first frame update
    void Start()
    {
        Load_TableInfo_play();


        table_data = Get_TableInfo_play("scene1");

        //print("" + table_data.maintxt);


        //Display("scene1");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Display(string _scene )
    {
        table_data = Get_TableInfo_play(_scene);
        if (table_data == null)
            print("error table      " + _scene );

        string _main = table_data.maintxt;
        string _c1 = table_data.collect1_txt;
        string _c2 = table_data.collect2_txt;
        string _c3 = table_data.collect3_txt;

        maintext.text = _main;
        choice1_text.text = _c1;
        choice2_text.text = _c2;
        choice3_text.text = _c3;

        if (_c1 =="0")   btn_choice1.SetActive(false);
        else            btn_choice1.SetActive(true);

        if (_c2 == "0") btn_choice2.SetActive(false);
        else btn_choice2.SetActive(true);

        if (_c3 == "0") btn_choice3.SetActive(false);
        else btn_choice3.SetActive(true);

    }

    public void OnClick_c1()
    {
        if (table_data.collect1_result == "0") return;
        if (table_data.collect1_result == "Clear")
        {
            Game_clear();
            return;
        }

            Display(table_data.collect1_result);
    }

    public void OnClick_c2()
    {
        if (table_data.collect1_result == "0") return;

        Display(table_data.collect2_result);
    }

    public void OnClick_c3()
    {
        if (table_data.collect1_result == "0") return;

        Display(table_data.collect3_result);
    }



    void Game_clear()
    {
        print("Game_clear");

    }

















    // system -----------------------------------------------------------------------

    public TableInfo_play[] kInfo_play = null;

    public TableInfo_play Get_TableInfo_play(string _scene)
    {
        foreach (TableInfo_play kInfo in kInfo_play)
        {
            if (kInfo != null)
                if (kInfo.scene == _scene) return kInfo;
        }
        return null;
    }

    void Load_TableInfo_play()
    {
        if (kInfo_play != null) return;

        // 문장사용할때 - CR,LF 사용시 - // 엑셀 유니코드 TXT        
        string txtFilePath = "EX_data";                     
        TextAsset ta = LoadTextAsset(txtFilePath);
        List<string> line = LineSplit(ta.text);

        //string txtFilePath = "EX_data";                     
        //TextAsset ta = LoadTextAsset(txtFilePath);
        //String[] line = ta.text.Split('\n'); // line Split

        TableInfo_play[] kInfo = new TableInfo_play[line.Count];
        for (int i = 0; i < line.Count; i++)
        {
            //Console.WriteLine("line : " + line[i]);
            if (line[i] == null) continue;
            if (i == 0) continue; 	// Title skip

            string[] Cells = line[i].Split("\t"[0]);	// cell split, tab
            if (Cells[0] == "") continue;
            //for (int j = 0; j < Cells.Length; j++)    System.Diagnostics.Trace.WriteLine(" LoadDataTable : " + Cells[j], ">>>>>");

            kInfo[i - 1] = new TableInfo_play();
            //kInfo[i - 1].iID = int.Parse(Cells[0]);

            kInfo[i - 1].scene = Cells[0];                      print("line : " + Cells[0]);
            kInfo[i - 1].maintxt = Cells[1];            
            kInfo[i - 1].collect1_txt = Cells[2];
            kInfo[i - 1].collect1_result = Cells[3];
            kInfo[i - 1].collect1_effect_type = Cells[4];
            kInfo[i - 1].collect1_effect_value = int.Parse(Cells[5]);
            kInfo[i - 1].collect2_txt = Cells[6];
            kInfo[i - 1].collect2_result = Cells[7];
            kInfo[i - 1].collect2_effect_type = Cells[8];
            kInfo[i - 1].collect2_effect_value = int.Parse(Cells[9]);
            kInfo[i - 1].collect3_txt = Cells[10];
            kInfo[i - 1].collect3_result = Cells[11];
            kInfo[i - 1].collect3_effect_type = Cells[12];
            kInfo[i - 1].collect3_effect_value = int.Parse(Cells[13]);
        }
        kInfo_play = kInfo;
    }


    TextAsset LoadTextAsset(string _txtFile)
    {
        TextAsset ta;
        ta = Resources.Load( _txtFile) as TextAsset; //폴더
        return ta;
    }


    //엑셀 유니코드 TXT 저장, "" 가 있는 경우, 콤마 CR LF등 사용시. //느림.
    public List<string> LineSplit(string text)
    {
        //Console.WriteLine("LineSplit " + text.Length);

        char[] text_buff = text.ToCharArray();

        List<string> lines = new List<string>();

        int linenum = 0;
        bool makecell = false;

        StringBuilder sb = new StringBuilder("");

        for (int i = 0; i < text.Length; i++)
        {
            char c = text_buff[i];
            //int value = Convert.ToInt32(c); Console.WriteLine(String.Format("{0:x4}", value) + " " + c.ToString());

            if (c == '"')
            {
                char nc = text_buff[i + 1];
                if (nc == '"') { i++; } //next char
                else
                {
                    if (makecell == false) { makecell = true; c = nc; i++; } //next char
                    else { makecell = false; c = nc; i++; } //next char
                }
            }

            //0x0a : LF ( Line Feed : 다음줄로 캐럿을 이동 '\n')
            //0x0d : CR ( Carrage Return : 캐럿을 제일 처음으로 복귀 )			    
            if (c == '\n' && makecell == false)
            {
                char pc = text_buff[i - 1];
                if (pc != '\n')	//file end
                {
                    lines.Add(sb.ToString()); sb.Remove(0, sb.Length);
                    linenum++;
                }
            }
            else if (c == '\r' && makecell == false)
            {
            }
            else
            {
                sb.Append(c.ToString());
            }
        }

        return lines;
    }
}

public class TableInfo_play //: MonoBehaviour
{
    //public int iID = 0;

    public string scene;
    public string maintxt;
    public string collect1_txt;
    public string collect1_result;
    public string collect1_effect_type;
    public int collect1_effect_value;
    public string collect2_txt;
    public string collect2_result;
    public string collect2_effect_type;
    public int collect2_effect_value;
    public string collect3_txt;
    public string collect3_result;
    public string collect3_effect_type;
    public int collect3_effect_value;

}
