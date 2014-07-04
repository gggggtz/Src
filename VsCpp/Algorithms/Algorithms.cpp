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

int* CAlgorithms::SelectionSort(int* input, int length, bool asc = true)
{
	for (int i = 0; i < length; i++)
	{
		int k = i;
		for (int j = i; j < length; j++)
		{
			if (asc ? input[j] < input[k] : input[j] > input[k])
			{
				k = j;
			}
		}
		if (k != i)
		{
			Swap(input, k, i);
		}
	}
	return input;
}

void CAlgorithms::Swap(int* input, int i, int j)
{
	int temp = input[i];
	input[i] = input[j];
	input[j] = temp;
}