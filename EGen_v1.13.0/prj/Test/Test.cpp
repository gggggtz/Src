// Test.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <fstream>
#include <cstdio>
#include <iostream>

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	string f = "flat_in/Charge.txt";

	ifstream file(f.c_str());

	char t;

	if (file.fail())
	{
		cout << "error" << endl;
	}
	else
	{
		cout << "ok" << endl;
	}

	cin >> t;

	return 0;
}

