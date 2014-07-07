#ifdef ALGORITHMS_EXPORTS
#define ALGORITHMS_API __declspec(dllexport)
#else
#define ALGORITHMS_API __declspec(dllimport)
#endif

#pragma once
class ALGORITHMS_API FileOperations
{
public:
	FileOperations();
	~FileOperations();

	void PrintFile(char* file);
	void PrintLastNLines(char* file, int n);
};

