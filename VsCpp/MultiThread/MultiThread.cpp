// MultiThread.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <cstdio>
#include <Windows.h>
#include <process.h>
#include <iostream>

void Silly(void *);

unsigned __stdcall Sillyex(void *);

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	cout << "Main Thread ..." << endl;
	
	_beginthread(Silly, 0, (void*)12);

	unsigned threadID;
	HANDLE hex;

	hex = (HANDLE) _beginthreadex(NULL, 0, Sillyex, (void*)15, CREATE_SUSPENDED, &threadID);

	Silly((void*)-5);
	ResumeThread(hex);

	WaitForSingleObject(hex, INFINITE);

	int i;
	cin >> i;

	return 0;
}

void Silly(void* arg)
{
	cout << "Silly function was passed " << (INT_PTR) arg << endl;
}

unsigned __stdcall Sillyex(void *arg)
{
	cout << "Sillyex function was passed " << (INT_PTR)arg << endl;

	return 0;
}