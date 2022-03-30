using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
Console.OutputEncoding = System.Text.Encoding.Default;
Console.Write("Write a path to the directory:");
string dirpath = Console.ReadLine();
if (Directory.Exists(dirpath))
{   var list_of_avvmarks = new List<double>();
    var list_of_names = new List<string>();
    string[] pathToFiles = Directory.GetFiles(dirpath, "*.csv");
    for (var i = 0; i < pathToFiles.Length; i++)
    {
        using (StreamReader reader = new StreamReader(pathToFiles[i]))
        {
            int amount_of_stud = Convert.ToInt32(reader.ReadLine());
            for (var j = 0; j < amount_of_stud; j++)
            {    
                string[] temps = reader.ReadLine().Split(new char[] {','});
                if(Convert.ToBoolean(temps[temps.Length - 1]))
                    continue;
                list_of_names.Add(temps[0]);
                double summ = 0;
                for (var k = 1; k < temps.Length - 1; k++)
                    summ += Convert.ToDouble(temps[k]);
                list_of_avvmarks.Add(summ / (temps.Length - 2));
            }
        }
    }
    dirpath += "/rating.csv";
    for (int i = 1; i < list_of_avvmarks.Count; ++i)
    {
        for (int j = 0; j < list_of_avvmarks.Count-i; ++j)
        {
            if (list_of_avvmarks[j] < list_of_avvmarks[j + 1])
            {
                string temp1=list_of_names[j];
                double temp2 = list_of_avvmarks[j];
                list_of_avvmarks[j] = list_of_avvmarks[j + 1];
                list_of_names[j] = list_of_names[j + 1];
                list_of_avvmarks[j + 1] = temp2;
                list_of_names[j + 1] = temp1;
            }
        }
    }
    using (StreamWriter writer = new StreamWriter(dirpath,false))
    {
        writer.WriteLine("Rating:");
        writer.WriteLine("  With scholarship:");
        for (int i = 0; i < list_of_avvmarks.Count; i++)
        {
            writer.WriteLine($"  {i+1}){list_of_names[i]} has average mark:{list_of_avvmarks[i]}.");
            if (i + 1 == Convert.ToInt32(list_of_avvmarks.Count * 0.4))
            {
                writer.WriteLine("  ----------------------------");
                writer.WriteLine("  Without scholarship:");
            }
        }
        writer.WriteLine($"Minimum score for scholarship: {list_of_avvmarks[Convert.ToInt32(list_of_avvmarks.Count * 0.4)-1]}");
    }
}


