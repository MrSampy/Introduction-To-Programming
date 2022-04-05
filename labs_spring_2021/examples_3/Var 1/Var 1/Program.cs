﻿using System.Collections.Generic;
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

void print_coord_List(List<int[]> coord)
{
   for (int i = 0; i < coord.Count; i++)
   {  Console.Write($"{i+1})Coordinate: ( ");
      for (int j = 0; j < coord[i].Length; j++)
      {
         Console.Write($"{coord[i][j]} ");
      }
      Console.WriteLine(")");
   }
}

bool isContains(List<int[]> coord, int[] temparr)
{
   for (int i = 0; i < coord.Count; i++)
      if (coord[i][0] == temparr[0] && coord[i][1] == temparr[1])
         return true;
   return false;
}

char[,] matrix;
List<int[]> all_coordinates = new List<int[]>(), final_coordinates = new List<int[]>();
var path="/home/serhiy/Документы/GitHub/Introduction-To-Programming/labs_spring_2021/examples_3/Labirint.csv";
int rows = File.ReadAllLines(path).Length, columns = 0;
int[] end_coord=new int[2];
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
         all_coordinates.Add(new int[] {row_cor, colm_cor, 0});
         matrix[row_cor, colm_cor] = 'S';
      }
      else
      {
         end_coord[0]=row_cor;
         end_coord[1] = colm_cor;
         matrix[row_cor, colm_cor] = 'F';
      }

      break;
   }
   Console.WriteLine();
}
int tempint=0,counter=1,lenbgth_of_arr = all_coordinates.Count;
bool tempbool = false, isskip=false;
for (int i = 0; i < all_coordinates.Count; i++)
{
   for (int j = 0; j < 4; j++)
   {
      int[] temparr = new int[3];
      for (int k = 0; k < temparr.Length; k++)
      {
         temparr[k] = all_coordinates[i][k];
      }
      switch (j)
      {
         case 0:
            ++temparr[0];
            break;
          case 1:
            --temparr[0];
            break;
          case 2:
            temparr[1]+=2;
             break;
          case 3:
             temparr[1]-=2;
            break;
       }
      if (temparr[0] < 0 || temparr[0] >= rows || temparr[1] < 0 || temparr[1] >= columns || isContains(all_coordinates, temparr) || matrix[temparr[0], temparr[1]] == 'X')
         continue;
      temparr[2] = counter;
      if (temparr[0].CompareTo(end_coord[0])==0 && temparr[1].CompareTo(end_coord[1])==0)
      {  all_coordinates.Add(temparr);
         tempbool = true;
         break;
      }
      if (!tempbool)
      {
         all_coordinates.Add(temparr);
      }
   }

   if (!Convert.ToBoolean(tempint))
   {
      tempint = all_coordinates.Count - lenbgth_of_arr;
      lenbgth_of_arr = all_coordinates.Count;
      ++counter;

   }
   --tempint;
   if (tempbool)
       break;
}
int[] temparray = all_coordinates[all_coordinates.Count - 1];
for (int i = all_coordinates.Count - 2; i > 0; --i)
{
   if (all_coordinates[i][2] < temparray[2] && (Math.Abs(temparray[0] - all_coordinates[i][0]) == 1 ||
                                                Math.Abs(temparray[1] - all_coordinates[i][1]) == 2))
   {
      temparray = all_coordinates[i];
      final_coordinates.Insert(0,temparray);
   }
}
for(int i=0;i<final_coordinates.Count;++i)
   matrix[final_coordinates[i][0],final_coordinates[i][1]]=Convert.ToChar(i+97); 
print_matrix(matrix);