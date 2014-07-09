#include "stdafx.h"
#include "CppUnitTest.h"
#include "..\Algorithms\Algorithms.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest
{		
	TEST_CLASS(SortTest)
	{
	public:
		
		TEST_METHOD(InsertionSortTest)
		{
			CAlgorithms alg;

			int input1[] = { 8, 4, 2, 6, 9, 3, 7 };			
			alg.InsertionSort(input1, 7,true);			
			Assert::AreEqual(input1[0], 2);
			Assert::AreEqual(input1[1], 3);
			Assert::AreEqual(input1[2], 4);
			Assert::AreEqual(input1[3], 6);
			Assert::AreEqual(input1[4], 7);
			Assert::AreEqual(input1[5], 8);
			Assert::AreEqual(input1[6], 9);

			int input2[] = { 8, 4, 2, 6, 9, 3, 7 };
			alg.InsertionSort(input2, 7, false);
			Assert::AreEqual(input2[0], 9);
			Assert::AreEqual(input2[1], 8);
			Assert::AreEqual(input2[2], 7);
			Assert::AreEqual(input2[3], 6);
			Assert::AreEqual(input2[4], 4);
			Assert::AreEqual(input2[5], 3);
			Assert::AreEqual(input2[6], 2);
		}

		TEST_METHOD(SelectionSortTest)
		{
			CAlgorithms alg;

			int input1[] = { 8, 4, 2, 6, 9, 3, 7 };
			alg.SelectionSort(input1, 7, true);
			Assert::AreEqual(input1[0], 2);
			Assert::AreEqual(input1[1], 3);
			Assert::AreEqual(input1[2], 4);
			Assert::AreEqual(input1[3], 6);
			Assert::AreEqual(input1[4], 7);
			Assert::AreEqual(input1[5], 8);
			Assert::AreEqual(input1[6], 9);

			int input2[] = { 8, 4, 2, 6, 9, 3, 7 };
			alg.SelectionSort(input2, 7, false);
			Assert::AreEqual(input2[0], 9);
			Assert::AreEqual(input2[1], 8);
			Assert::AreEqual(input2[2], 7);
			Assert::AreEqual(input2[3], 6);
			Assert::AreEqual(input2[4], 4);
			Assert::AreEqual(input2[5], 3);
			Assert::AreEqual(input2[6], 2);
		}

		TEST_METHOD(MaxSubArrayTest)
		{
			CAlgorithms alg;
			int input[] = { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };
			int left, right;
			int sum = alg.FindMaxSubArray(input, 16, &left, &right);

			Assert::AreEqual(sum, 43);
			Assert::AreEqual(left, 7);
			Assert::AreEqual(right, 10);
		}

		TEST_METHOD(MergeSortTest)
		{
			CAlgorithms alg;

			int input1[] = { 8, 4, 2, 6, 9, 3, 7, 1, 10, 5 };
			alg.MergeSort(input1, 10, true);
			Assert::AreEqual(input1[0], 1);
			Assert::AreEqual(input1[1], 2);
			Assert::AreEqual(input1[2], 3);
			Assert::AreEqual(input1[3], 4);
			Assert::AreEqual(input1[4], 5);
			Assert::AreEqual(input1[5], 6);
			Assert::AreEqual(input1[6], 7);
			Assert::AreEqual(input1[7], 8);
			Assert::AreEqual(input1[8], 9);
			Assert::AreEqual(input1[9], 10);

			int input2[] = { 8, 4, 2, 6, 9, 3, 7, 1, 10, 5 };
			alg.SelectionSort(input2, 10, false);
			Assert::AreEqual(input2[0], 10);
			Assert::AreEqual(input2[1], 9);
			Assert::AreEqual(input2[2], 8);
			Assert::AreEqual(input2[3], 7);
			Assert::AreEqual(input2[4], 6);
			Assert::AreEqual(input2[5], 5);
			Assert::AreEqual(input2[6], 4);
			Assert::AreEqual(input2[7], 3);
			Assert::AreEqual(input2[8], 2);
			Assert::AreEqual(input2[9], 1);
		}

		TEST_METHOD(HeapSortTest)
		{
			CAlgorithms alg;

			int input1[] = { 8, 4, 2, 6, 9, 3, 7, 1, 10, 5 };
			alg.HeapSort(input1, 10, true);
			Assert::AreEqual(input1[0], 1);
			Assert::AreEqual(input1[1], 2);
			Assert::AreEqual(input1[2], 3);
			Assert::AreEqual(input1[3], 4);
			Assert::AreEqual(input1[4], 5);
			Assert::AreEqual(input1[5], 6);
			Assert::AreEqual(input1[6], 7);
			Assert::AreEqual(input1[7], 8);
			Assert::AreEqual(input1[8], 9);
			Assert::AreEqual(input1[9], 10);

			int input11[] = { 8, 4, 2, 6, 9, 3, 7, 1, 10, 5 };
			alg.BuildHeap(input11, 10, false);

			Assert::AreEqual(alg.HeapExtract(input11, 10), 1);
			Assert::AreEqual(alg.HeapExtract(input11, 9), 2);
			Assert::AreEqual(alg.HeapExtract(input11, 8), 3);
			Assert::AreEqual(alg.HeapExtract(input11, 7), 4);
			Assert::AreEqual(alg.HeapExtract(input11, 6), 5);
			Assert::AreEqual(alg.HeapExtract(input11, 5), 6);
			Assert::AreEqual(alg.HeapExtract(input11, 4), 7);
			Assert::AreEqual(alg.HeapExtract(input11, 3), 8);
			Assert::AreEqual(alg.HeapExtract(input11, 2), 9);
			Assert::AreEqual(alg.HeapExtract(input11, 1), 10);

			int input2[] = { 8, 4, 2, 6, 9, 3, 7, 1, 10, 5 };
			alg.HeapSort(input2, 10, false);
			Assert::AreEqual(input2[0], 10);
			Assert::AreEqual(input2[1], 9);
			Assert::AreEqual(input2[2], 8);
			Assert::AreEqual(input2[3], 7);
			Assert::AreEqual(input2[4], 6);
			Assert::AreEqual(input2[5], 5);
			Assert::AreEqual(input2[6], 4);
			Assert::AreEqual(input2[7], 3);
			Assert::AreEqual(input2[8], 2);
			Assert::AreEqual(input2[9], 1);

			int input22[] = { 8, 4, 2, 6, 9, 3, 7, 1, 10, 5 };
			alg.BuildHeap(input22, 10, true);

			int p = input22[3];

			//parents of input22[3]
			int p1 = input22[1];
			int p2 = input22[0];

			//childs of input22[3]
			int c1 = input22[7];
			int c2 = input22[8];

			alg.HeapAlter(input22, 3, 11, 10, true);

			Assert::AreEqual(input22[0], 11);
			Assert::AreEqual(input22[1], p2);
			Assert::AreEqual(input22[3], p1);

			alg.HeapAlter(input22, 3, -1, 10, true);

			Assert::AreEqual(input22[3], c1 > c2 ? c1 : c2);
			Assert::AreEqual(input22[7], c1 < c2 ? c1 : -1);
			Assert::AreEqual(input22[8], c2 < c1 ? c2 : -1);

			int* result = alg.HeapInsert(input22, 10, p, true);
			alg.HeapSort(result, 11, true);

			Assert::AreEqual(result[0], -1);
			Assert::AreEqual(result[1], 1);
			Assert::AreEqual(result[2], 2);
			Assert::AreEqual(result[3], 3);
			Assert::AreEqual(result[4], 4);
			Assert::AreEqual(result[5], 5);
			Assert::AreEqual(result[6], 6);
			Assert::AreEqual(result[7], 7);
			Assert::AreEqual(result[8], 8);
			Assert::AreEqual(result[9], 10);
			Assert::AreEqual(result[10], 11);
		}
	};
}