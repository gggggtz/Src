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
	};
}