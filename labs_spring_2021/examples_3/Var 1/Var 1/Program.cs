using System.Collections.Generic;
using System.IO;
using System.Linq;
char[,] matrix;
var path="/home/serhiy/Документы/GitHub/Introduction-To-Programming/labs_spring_2021/examples_3/Labirint.csv";
int rows= File.ReadAllLines(path).Length, columns=0;
using (StreamReader reader = new StreamReader(path))
{
   string a = reader.ReadLine();
   for (int i = 0; i < a.Length; i++)
   { 
      if (a[i] == ' ' && a[i + 1] == ' ' && a[i-1]=='X')
         break;
      ++columns;
   }
   matrix = new char [rows, columns];
   reader.BaseStream.Position = 0;
   reader.ReadToEnd();
   reader.BaseStream.Position = 0;
   for (int i = 0; i < rows; ++i)
   {
      for (int j = 0; j < columns; ++j)
      {
         matrix[i, j] = Convert.ToChar(reader.Read());
      }
      reader.ReadLine();
   }
}

for (int i = 0; i < rows; ++i)
{
   for (int j = 0; j < columns; j++)
   {
      Console.Write($"{matrix[i,j]}");
   }
   Console.WriteLine();
}