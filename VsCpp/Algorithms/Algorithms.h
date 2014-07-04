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
	int * InsertionSort(int *input, int length, bool asc);
};

extern ALGORITHMS_API int nAlgorithms;

ALGORITHMS_API int fnAlgorithms(void);
