#include "stdafx.h"
#include "CppUnitTest.h"
#include "..\Algorithms\FileOperations.h"
#include <fstream>

using namespace std;
using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest
{
	TEST_CLASS(FileTest)
	{
	public:
		
		/*TEST_METHOD(ReadTest)
		{
			FileOperations fo;
			fo.PrintFile("ReadMe.txt");
		}*/

		TEST_METHOD(FStreamTest)
		{
			ofstream fout("c:\\temp\\init.txt");
			fout << "InitializeChangeNotify" << endl << flush;
			fout.close();
		}

	};
}