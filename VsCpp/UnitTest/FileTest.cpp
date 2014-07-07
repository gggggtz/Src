#include "stdafx.h"
#include "CppUnitTest.h"
#include "..\Algorithms\FileOperations.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest
{
	TEST_CLASS(FileTest)
	{
	public:
		
		TEST_METHOD(ReadTest)
		{
			FileOperations fo;
			fo.PrintFile("ReadMe.txt");
		}

	};
}