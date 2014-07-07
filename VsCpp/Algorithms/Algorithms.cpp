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

int CAlgorithms::FindMaxSubArray(int *input, int length, int *left, int * right)
{
	*left = 0;
	*right = 0;
	if (length == 1)
	{
		return input[0];
	}
	else if (length > 1)
	{	
		//Divide and conquer
		int mid = length / 2;

		return FindMaxSubArrayRecursive(input, 0, length - 1, left, right);		
	}
	return 0;
}

int CAlgorithms::FindMaxSubArrayRecursive(int *input, int low, int high, int *left, int * right)
{
	int sum = 0;
	*left = low;
	*right = high;
	if (high == low)
	{
		sum = input[low];
	}
	else
	{
		int mid = (high + low) / 2;

		int left1, right1, sum1;
		sum1 = FindMaxSubArrayRecursive(input, low, mid, &left1, &right1);

		int left2, right2, sum2;
		sum2 = FindMaxSubArrayRecursive(input, mid + 1, high, &left2, &right2);

		int left3, right3, sum3;
		sum3 = FindMaxCrossingSubArray(input, low, mid, high, &left3, &right3);

		if (sum1 > sum2)
		{
			if (sum1 > sum3)
			{
				*left = left1;
				*right = right1;
				sum = sum1;
			}
			else
			{
				*left = left3;
				*right = right3;
				sum = sum3;
			}
		}
		else
		{
			if (sum2 > sum3)
			{
				*left = left2;
				*right = right2;
				sum = sum2;
			}
			else
			{
				*left = left3;
				*right = right3;
				sum = sum3;
			}
		}
	}
	return sum;
}

int CAlgorithms::FindMaxCrossingSubArray(int *input, int low, int mid, int high, int *left, int * right)
{
	int leftsum = MININT, rightsum = MININT, sum = 0;

	if (low == mid)
	{
		*left = low;
		leftsum = 0;
	}
	else
	{
		sum = 0;
		//Needs to include input[mid] in one of the two branches
		//Otherwise, if input[mid-1] is negtive, we will count that number
		for (int i = mid; i >= low; i--)
		{
			sum += input[i];
			if (sum > leftsum)
			{
				leftsum = sum;
				*left = i;
			}
		}
	}

	if (mid == high)
	{
		*right = high;
		rightsum = 0;
	}
	else
	{
		sum = 0;
		for (int i = mid + 1; i <= high; i++)
		{
			sum += input[i];
			if (sum > rightsum)
			{
				rightsum = sum;
				*right = i;
			}
		}
	}
	return leftsum + rightsum;
}

void CAlgorithms::Swap(int* input, int i, int j)
{
	int temp = input[i];
	input[i] = input[j];
	input[j] = temp;
}