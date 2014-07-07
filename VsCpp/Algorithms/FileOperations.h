#pragma once
class FileOperations
{
public:
	FileOperations();
	~FileOperations();

	void PrintFile(char* file);
	void PrintFileBackwards(char* file);
	void PrintLastNLines(char* file, int n);
};

