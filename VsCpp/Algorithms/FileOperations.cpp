#include "stdafx.h"
#include "FileOperations.h"
#include <iostream>
#include <fstream>
#include <cstdio>
#include <cstdlib>

using namespace std;

FileOperations::FileOperations()
{
}


FileOperations::~FileOperations()
{
}

void FileOperations::PrintFile(char* file)
{
	FILE* pfile;
	fopen_s(&pfile,"example.txt","r");
	if (pfile == NULL)
	{
		perror("Error when opening the file");
	}
	else
	{
		int c;
		do
		{
			c = fgetc(pfile);
			printf("%c", c);
		} while (c != EOF);
		fclose(pfile);
	}
}
void FileOperations::PrintFileBackwards(char* file)
{

}
void FileOperations::PrintLastNLines(char* file, int n)
{

}