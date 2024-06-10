/* Дан двумерный массив.
   732
   496
   185
   
   Отсортировать данные в нем по возрастанию.
   123
   456
   789

   Вывести результат на печать. */



namespace Seminar_2._Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = { { 7, 3, 2 }, { 4, 9, 6 }, { 1, 8, 5 } };

            // int[] testLine = {  0, 1, 2, 3, 9, 6, 1, 8 };

            int[] linearArray = new int[matrix.Length];
            int rows = matrix.GetLength(0);

            Console.WriteLine("Исходный массив:\n");
            printMatrix(matrix);

            linearArray = convertMatrixToLine(matrix);

            linearArray = mergeSort(linearArray);

            // linearArray = testLine;

            try
            {
                matrix = convertLineToMatrix(linearArray, rows);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка входных данных: {ex.Message}");
                Environment.Exit(0);
            }
            
            Console.WriteLine("\nОтсортированный массив:\n");
            printMatrix(matrix);
        }
        private static void printMatrix(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                Console.Write("[");
                for (int j = 0; j < a.GetLength(1) - 1; j++)
                    Console.Write($"{a[i, j]} ");
                Console.WriteLine($"{a[i, a.GetLength(1) - 1]}]");
            }
        }
        private static int[] convertMatrixToLine(int[,] matrix)
        {
            int[] linearArray = new int[matrix.GetLength(0) * matrix.GetLength(1)]; ;
            int i = 0;
            foreach (var item in matrix)
                linearArray[i++] = item;
            return linearArray;
        }

        private static int[,] convertLineToMatrix(int[] linearArray, int rows)
        {
            int columns = 0;
            if ( linearArray.Length % rows == 0 )
                columns = linearArray.Length / rows;
            else
                throw new Exception("Данный линейный массив невозожно преобразовать в двумерный!");

            int[,] matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = linearArray[i * columns + j];
            
            return matrix;
        }

        // Сортировка Слиянием -----------------------------------------------------------------------------------------------
        static int[] mergeSort(int[] arg)
        {

            if (arg.Length <= 1)
                return arg;
            else
            {
                int middle = arg.Length / 2;                            // Делим входящий массив пополам
                int[] left = new int[middle];                           // Создаём Левый подмассив, размером как половина входящего
                int[] right = new int[arg.Length - middle];             // Создаём Правый подмассив, на оставшуюся часть. Она может быть не равна левой, т.к. входящий может иметь не чётное количество членов
                int[] result = new int[arg.Length];                     // Создаём Реультирующий новый массив длной, как входящий.
                Array.Copy(arg, left, middle);                          // Копируем в Левый подмассив левую часть входящего массива
                Array.Copy(arg, middle, right, 0, arg.Length - middle); // Копируем в Правый подмассив оставшуюся часть входящего массива
                left = mergeSort(left);                                 // Рекурсия! Отправляем Левый подмассив в сотрировку слиянием
                right = mergeSort(right);                               // Рекурсия! Отправляем Правый подмассив в сотрировку слиянием
                result = merge(left, right);                            // "Склеиваем" отсортированный Левый подмассив с отсортированным Правым подмассивом
                return result;                                          // Возвращаем отсортированный массив
            }
        }

        static int[] merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int indexLeft = 0;
            int indexRight = 0;
            int indexResult = 0;
            while (indexLeft < left.Length && indexRight < right.Length)    // Продолжаем пока какой-нибудь из индексов не дойдёт до правого края
            {
                if (left[indexLeft] <= right[indexRight])
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                }
                else
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                }
                indexResult++;
            }
            while (indexLeft < left.Length)                 // Добиваем результирующий массив неиспользованными значениями из Левого подмассива, если они есть
            {
                result[indexResult] = left[indexLeft];
                indexLeft++;
                indexResult++;
            }
            while (indexRight < right.Length)               // Добиваем результирующий массив неиспользованными значениями из Правого подмассива, если они есть
            {
                result[indexResult] = right[indexRight];
                indexRight++;
                indexResult++;
            }
            return result;
        }
    }
}

