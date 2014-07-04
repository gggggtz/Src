#include "stdafx.h"
#include "CppUnitTest.h"
#include "..\Algorithms\Algorithms.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(InsertionSortTest)
		{
			CAlgorithms alg;

			int input1[] = { 8, 4, 2, 6, 9, 3, 7 };			
			int * result1 = alg.InsertionSort(input1, 7,true);			
			Assert::AreEqual(result1[0], 2);
			Assert::AreEqual(result1[1], 3);
			Assert::AreEqual(result1[2], 4);
			Assert::AreEqual(result1[3], 6);
			Assert::AreEqual(result1[4], 7);
			Assert::AreEqual(result1[5], 8);
			Assert::AreEqual(result1[6], 9);

			int input2[] = { 8, 4, 2, 6, 9, 3, 7 };
			int * result2 = alg.InsertionSort(input1, 7,false);
			Assert::AreEqual(result2[0], 9);
			Assert::AreEqual(result2[1], 8);
			Assert::AreEqual(result2[2], 7);
			Assert::AreEqual(result2[3], 6);
			Assert::AreEqual(result2[4], 4);
			Assert::AreEqual(result2[5], 3);
			Assert::AreEqual(result2[6], 2);
		}

		TEST_METHOD(SelectionSortTest)
		{
			CAlgorithms alg;

			int input1[] = { 8, 4, 2, 6, 9, 3, 7 };
			int * result1 = alg.SelectionSort(input1, 7, true);
			Assert::AreEqual(result1[0], 2);
			Assert::AreEqual(result1[1], 3);
			Assert::AreEqual(result1[2], 4);
			Assert::AreEqual(result1[3], 6);
			Assert::AreEqual(result1[4], 7);
			Assert::AreEqual(result1[5], 8);
			Assert::AreEqual(result1[6], 9);

			int input2[] = { 8, 4, 2, 6, 9, 3, 7 };
			int * result2 = alg.SelectionSort(input1, 7, false);
			Assert::AreEqual(result2[0], 9);
			Assert::AreEqual(result2[1], 8);
			Assert::AreEqual(result2[2], 7);
			Assert::AreEqual(result2[3], 6);
			Assert::AreEqual(result2[4], 4);
			Assert::AreEqual(result2[5], 3);
			Assert::AreEqual(result2[6], 2);
		}

	};
}