
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\tXX====== Tournament Sort Algorithm ======XX");
            Console.WriteLine("\nMade By:\nMuhammad Umair Khalid  (17b-005-se)\nShahrukh Ghazi  (17b-065-se)\n");
            Sort A = new Sort();
            A.InputUnsortedArray(); // Input unsorted array
            A.TournamentSort(); // 1 transfer of non-sorted array from the InputUnsortedarray method and 2 transfer of the sorted array to the sortedarray method
            A.Output(); // output sorted array
            Console.ReadLine();
        }
    }

    static class Constants
    {
        public static readonly Int32 N = 18; // number of array elements
    }
    class Sort
    {   // read the constant of the number of elements in the array
        private Int32[] A = new Int32[Constants.N + 1];

        // built pyramid heap / binary tree from source array
        private void Initialize(Int32[] tree, Int32 size)
        {   // Initialize the remaining leaves of the original array
            Int32 j = 1, k;
            while (j <= Constants.N)
            {
                tree[size + j - 1] = A[j]; j++;
            }
            // Calculation of the upper level of the tree, where the level above the leaves is processed separately.           
            for (j = size + Constants.N; j <= 2 * size - 1; j++) tree[j] = Int32.MinValue;
            j = size; // size from the Tournament Sort method
            while (j <= 2 * size - 1)
            {
                if (tree[j] >= tree[j + 1]) tree[j / 2] = j;// shift of array elements with lower values to the beginning
                else tree[j / 2] = j + 1;
                j += 2;
            }
            // compiling a binary tree from the remaining 50% of the raw elements of the original array
            k = size / 2; // allows you to define a smaller element in each tournament pair of the same level N / 2 
            while (k > 1)
            {
                j = k;
                while (j <= 2 * k - 1)
                {
                    if (tree[tree[j]] >= tree[tree[j + 1]])
                        tree[j / 2] = tree[j];
                    else tree[j / 2] = tree[j + 1];
                    j += 2;
                }
                k /= 2;
            }
        }

        private void Readjust(Int32[] tree, ushort i)
        {
            unchecked// assignment to any element of its maximum value when overloaded in case there is not enough allocated number of indexes of sort nodes
            {
                ushort j;
                // shift of all odd numbers of the lowest-extreme level of the pyramidal heap above even ones on the principle 1 <2 for subsequent faster sort
                if ((i % 2) != 0) tree[i / 2] = i - 1;
                else tree[i / 2] = i + 1;
                // Last stage of compiling a binary tree from the remaining 25% of raw elements of the original array
                i /= 2;
                while (i > 1)
                { // 
                    if ((i % 2) != 0) j = (ushort)(i - 1);
                    else j = (ushort)(i + 1);
                    if (tree[tree[i]] > tree[tree[j]]) tree[i / 2] = tree[i];
                    else tree[i / 2] = tree[j];
                    i /= 2;
                }
            }
        }
        public void TournamentSort()
        {  // sorting the constructed pyramidal heap / binary tree using the pyramid-tournament sorting method 
            unchecked
            {
                const Int32 size = 128; // The number of leaves needed in a full binary tree, as the value of the size variable is at least 2, at most N.
                Int32[] tree = new Int32[256]; // for 2 variants of each of the 128 leaves of the full binary tree 
                Int32 k;
                ushort i;

                Initialize(tree, size);
                // Now after the tree has been built, the operation of moving the element represented by the root to the next position with a lower index in the array is repeated and the tree is shifted.
                for (k = Constants.N; k >= 2; k--)
                {
                    i = (ushort)tree[1]; // i - the index of the node with the sheet corresponding to the root.
                    A[k] = tree[i]; // Move the element referenced by the root to position k.
                    tree[i] = Int32.MinValue;
                    Readjust(tree, i); // Redirecting the tree in accordance with the new content tree [i].
                }
                A[1] = tree[tree[1]];
            }
        }

        public void InputUnsortedArray()
        {
            Random r = new Random(); // assignment of random values to the array elements
            Console.WriteLine("Unsorted array");
            for (Int32 i = 1; i <= Constants.N; i++)
            {
                A[i] = r.Next(0, 100);
                Console.Write("{0} ", A[i]);
            }
            Console.WriteLine();
        }

        public void Output()
        {
            Console.WriteLine("Sorted Array");
            for (int i = 1; i <= Constants.N; i++)
            {
                Console.Write("{0} ", A[i]);

            }
        }
    }
}