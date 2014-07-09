// Algorithms.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Algorithms.h"
#include <iostream>

using namespace std;


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

void CAlgorithms::InsertionSort(int* input, int length, bool asc = true)
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
}

void CAlgorithms::SelectionSort(int* input, int length, bool asc = true)
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

void CAlgorithms::MergeSort(int* input, int length, bool asc)
{
	if (length > 1)
	{
		int mid = (length - 1) / 2;
		MergeSort(input, 0, mid, asc);
		MergeSort(input, mid + 1, length - 1, asc);
		Merge(input, 0, mid, length - 1, asc);
	}
}

void CAlgorithms::MergeSort(int* input, int start, int end, bool asc)
{
	if (start == end)
	{
		return;
	}
	else if (end-start ==1)
	{
		if (input[start] > input[end])
		{
			if (asc)
			{
				Swap(input, start, end);
			}
		}
	}
	else
	{
		int mid = (start + end) / 2;
		MergeSort(input, start, mid, asc);
		MergeSort(input, mid + 1, end, asc);
		Merge(input, start, mid, end, asc);
	}
}

void CAlgorithms::Merge(int* input, int start, int mid, int end, bool asc)
{
	int l = mid - start + 1;
	int r = end - mid;
	//No need to merge when either side is empty
	if (l == 0 || r == 0)
	{
		return;
	}
	int* lInput = new int[l];
	for (int i = start; i <= mid; i++)
	{
		lInput[i - start] = input[i];
	}
	int* rInput = new int[r];
	for (int i = mid + 1; i <= end; i++)
	{
		rInput[i - mid - 1] = input[i];
	}
	int i = 0, j = 0;
	for (int k = start; k <= end; k++)
	{
		if (i == l )
		{
			while (j < r)
			{
				input[k++] = rInput[j++];
			}
			break;
		}
		else if (j == r)
		{
			while (i < l)
			{
				input[k++] = lInput[i++];
			}
			break;
		}
		else
		{
			if (asc ? (lInput[i] < rInput[j]) : (lInput[i] > rInput[j]))
			{
				input[k] = lInput[i++];
			}
			else
			{
				input[k] = rInput[j++];
			}
		}
	}
}

void CAlgorithms::Swap(int* input, int i, int j)
{
	int temp = input[i];
	input[i] = input[j];
	input[j] = temp;
}

void CAlgorithms::HeapIfy(int* input, int i, int length, bool maxHeap)
{
	int left = GetLeftChildIndex(i);
	int right = GetRightChildIndex(i);
	//both left and right are out of range
	if (left > length - 1)
	{
		return;
	}
	//right is out of range, left is length - 1
	else if (right > length -1)
	{
		if (maxHeap ? (input[left] > input[i]) : (input[left] < input[i]))
		{
			Swap(input, left, i);
		}
	}
	//both left and right are in the heap
	else
	{
		int key = i;
		if (maxHeap ? (input[left] > input[i]) : input[left] < input[i])
		{
			key = left;
			if (maxHeap ? (input[right] > input[left]) : (input[right] < input[left]))
			{
				key = right;
			}

		}
		else if (maxHeap ? (input[right] > input[i]) : input[right] < input[i])
		{
			key = right;
		}
		if (key != i)
		{
			Swap(input, key, i);
			HeapIfy(input, key, length, maxHeap);
		}
	}
}

void CAlgorithms::BuildHeap(int* input, int length, bool maxHeap)
{
	if (length > 1)
	{
		//all the elements after lastParent are leafs
		int lastParent = GetParentIndex(length - 1);
		//after we adjust all the elements before lastParent, the heap is built
		for (int i = lastParent; i >= 0; i--)
		{
			HeapIfy(input, i, length, maxHeap);
		}
	}
}

void CAlgorithms::HeapSort(int* input, int length, bool asc)
{
	if (length > 1)
	{
		BuildHeap(input, length, asc);
		int i = length - 1;
		while (i > 0)
		{
			Swap(input, i, 0);
			HeapIfy(input, 0, i, asc);
			i--;
		}
	}
}

void CAlgorithms::Print(int* input, int length)
{
	for (int i = 0; i < length; i++)
	{
		cout << input[i] << " - ";
	}
	cout << endl;
}

int CAlgorithms::HeapExtract(int *heap, int length)
{
	if (length > 1)
	{
		bool maxHeap = heap[0] > heap[1];
		int val = heap[0];
		Swap(heap, 0, length - 1);
		HeapIfy(heap, 0, length - 1, maxHeap);
		return val;
	}
	else if (length == 1)
	{
		return heap[0];
	}
	return MININT;
}

void CAlgorithms::HeapAlter(int *heap, int i, int val, int length, bool maxHeap)
{
	if (length > i)
	{
		int oldVal = heap[i];
		heap[i] = val;
		if (length > 1)
		{
			if (maxHeap ? oldVal > val:oldVal < val)
			{
				HeapIfy(heap, i, length, maxHeap);
			}
			else
			{
				while (i > 0)
				{
					int p = GetParentIndex(i);
					if (maxHeap ? heap[p] < heap[i] : heap[p] > heap[i])
					{
						Swap(heap, i, p);
						i = p;
					}
					else
					{
						break;
					}
				}
			}
		}
	}
}

int* CAlgorithms::HeapInsert(int *heap, int length, int val, bool maxHeap)
{
	int* newHeap = new int[length + 1];
	for (int i = 0; i < length; i++)
	{
		newHeap[i] = heap[i];
	}
	newHeap[length] = MININT;
	HeapAlter(newHeap, length, val, length + 1,maxHeap);
	return newHeap;
}

int CAlgorithms::Partion(int* input, int p, int r, bool asc)
{
	//let input[r] be the pivot
	int key = input[r];
	//divide the array into 3 parts for asc
	//p ... i -- <= key
	//i+1 ... j -- > key
	//r -- = key
	int i = p - 1;
	for (int j = p; j < r; j++)
	{
		if (asc ? input[j] <= key : input[j] >= key)
		{
			i++;
			if (i != j)
			{
				Swap(input, i, j);
			}
		}
	}
	Swap(input, i + 1, r);
	return i + 1;
}

void CAlgorithms::QuickSort(int* input, int p, int r, bool asc)
{
	if (p < r)
	{
		int q = Partion(input, p, r, asc);
		QuickSort(input, p, q - 1, asc);
		QuickSort(input, q + 1, r, asc);
	}
}

void CAlgorithms::QuickSort(int* input, int length, bool asc)
{
	QuickSort(input, 0, length - 1,asc);
}