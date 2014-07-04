#include "stdafx.h"
#include "CppUnitTest.h"
#include "..\Algorithms\Algorithms.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(TestMethod1)
		{
			int input[] = { 8, 4, 2, 6, 9, 3, 7 };
			CAlgorithms alg;
			alg.InsertionSort(input, 7);

			Assert::AreEqual(5, 5);
		}

	};
}