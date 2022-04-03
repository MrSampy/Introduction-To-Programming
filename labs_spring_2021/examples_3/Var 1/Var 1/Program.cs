using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

void print_matrix(char [,] m) 
{ 
   for (int i = 0; i < m.GetUpperBound(0)+1; ++i)
   {
      for (int j = 0; j < m.GetUpperBound(1) + 1; ++j)
      {
         Console.Write(m[i,j]);
      }
      Console.WriteLine();
   }
}
char[,] matrix;
var coordinates = new List<int[]>();
var path="/home/serhiy/Документы/GitHub/Introduction-To-Programming/labs_spring_2021/examples_3/Labirint.csv";
int rows= File.ReadAllLines(path).Length, columns=0;
using (StreamReader reader = new StreamReader(path))
{
   string a = reader.ReadLine();
   reader.BaseStream.Position = 0;
   for (int i = 0; i < a.Length; i++)
   { 
      if (a[i] == ' ' && a[i + 1] == ' ' && a[i-1]=='X')
         break;
      ++columns;
   }
   matrix = new char [rows, columns];
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
for (int i = 0; i < 2; i++)
{
   while (true)
   {  if(Convert.ToBoolean(i))
         Console.Write("Enter coordinates of end point: ");
      else
          Console.Write("Enter coordinates of start point: ");
      var input = Console.ReadLine().Split(' ');
      if (input[0] == null || input[1] == null)
      {
         Console.WriteLine("Try again!(Null numbers)");
         continue;
      }
      int row_cor = Convert.ToInt32(input[0]), colm_cor = Convert.ToInt32(input[1]);
      if (row_cor >= rows-1 || colm_cor >= columns-1 || colm_cor%2==1 || colm_cor<=0 || row_cor<=0 || matrix[row_cor, colm_cor] == 'X')
      {
         Console.WriteLine("Try again!(wrong coordinates)");
         continue;
      }
      if (!Convert.ToBoolean(i))
      {
         coordinates.Add(new int[] {row_cor, colm_cor, 0});
         matrix[row_cor, colm_cor] = 'S';
      }
      else
      {
         coordinates.Add(new int[] {row_cor, colm_cor, 0});
         matrix[row_cor, colm_cor] = 'F';
      }

      break;
   }
   Console.WriteLine();
}
