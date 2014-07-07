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
	fopen_s(&pfile, file, "r");
	if (pfile == NULL)
	{
		perror("Error when opening the file");
	}
	else
	{
		int c;
		c = fgetc(pfile);
		while (c != EOF);
		{
			
			printf("%c", c);
		} 
		fclose(pfile);
	}
}

void FileOperations::PrintLastNLines(char* file, int n)
{
	FILE* pfile;	
	int *lineposes = new int[n];
	int lineIndex = 0;
	int totalLines = 0;
	int pos = 0;
	fopen_s(&pfile, file, "r");
	if (pfile == NULL)
	{
		perror("Error when opening the file");
	}
	else
	{
		int c = 0;
		do
		{
			c = fgetc(pfile);
			pos++;
			if (c == '\n' || c == EOF)
			{
				lineposes[lineIndex] = pos;
				totalLines++;
				if (lineIndex < n - 1)
				{
					lineIndex++;
				}
				else
				{
					lineIndex = 0;
				}
			}
		} while (c != EOF);
		//empty file
		if (pos == 0)
		{
			return;
		}
		//entire file has less than n lines
		if (totalLines <= n)
		{
			fsetpos(pfile, 0);
		}
		else
		{
			fpos_t pos = (lineIndex == n - 1) ? 0 : (lineIndex + 1);
			fsetpos(pfile, &pos);
		}
		c = fgetc(pfile);
		while (c != EOF);
		{
			printf("%c", c);
		}
		fclose(pfile);
	}
}