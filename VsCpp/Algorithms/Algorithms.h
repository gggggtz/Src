// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the ALGORITHMS_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// ALGORITHMS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef ALGORITHMS_EXPORTS
#define ALGORITHMS_API __declspec(dllexport)
#else
#define ALGORITHMS_API __declspec(dllimport)
#endif

// This class is exported from the Algorithms.dll
class ALGORITHMS_API CAlgorithms {
public:
	CAlgorithms(void);
	// TODO: add your methods here.
	void InsertionSort(int *input, int length, bool asc);

	void SelectionSort(int *input, int length, bool asc);

	int FindMaxSubArray(int *input, int length, int *left, int * right);

	void MergeSort(int* input, int length, bool asc);

private:
	void Swap(int* input, int i, int j);
	int FindMaxSubArrayRecursive(int *input, int low, int high, int *left, int * right);
	int FindMaxCrossingSubArray(int *input, int low, int mid, int high, int *left, int * right);
	void MergeSort(int* input, int start, int end, bool asc);
	void Merge(int* input, int start, int mid, int end, bool asc);
};

extern ALGORITHMS_API int nAlgorithms;

ALGORITHMS_API int fnAlgorithms(void);
