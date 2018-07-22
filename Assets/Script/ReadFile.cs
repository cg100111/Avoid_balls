using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadFile {

    public List<List<int>> Get_file_info()
    {
        return Transform(Read_file());
    }

    private string[] Read_file()
    {
        string path = Application.streamingAssetsPath + "/Resources/level_info.txt";
        string txt = File.ReadAllText(path);
        return txt.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
    }

    private List<List<int>> Transform(string[] file_content)
    {
        List<List<int>> level_info = new List<List<int>>();
        string[] cutting_flag = new string[] { " ", "　", "  " };
        foreach(string row in file_content)
        {
            string[] info = row.Split(cutting_flag, System.StringSplitOptions.RemoveEmptyEntries);
            if (info[0] == "#")
                continue;
            level_info.Add(new List<int>());
            foreach(string data in info)
                level_info[level_info.Count - 1].Add(int.Parse(data));
        }
        return level_info;
    }
}
