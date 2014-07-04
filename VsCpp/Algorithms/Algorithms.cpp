// Algorithms.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Algorithms.h"


// This is an example of an exported variable
ALGORITHMS_API int nAlgorithms=0;

// This is an example of an exported function.
ALGORITHMS_API int fnAlgorithms(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see Algorithms.h for the class definition
CAlgorithms::CAlgorithms()
{
	return;
}

int* CAlgorithms::InsertionSort(int* input, int length, bool asc = true)
{
	for (int i = 1; i < length; i++)
	{
		int key = input[i];
		int j = i - 1;
		while (j >= 0 && (asc ? input[j] > key:input[j] < key))
		{
			input[j + 1] = input[j];
			j--;
		}
		input[j + 1] = key;
	}

	return input;
}