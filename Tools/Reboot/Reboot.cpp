// Reboot.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
#include <WinBase.h>
#include <WinUser.h>
#include <cstdio>
#include <iostream>

using namespace std;

void Reboot(UINT flags)
{
	try
	{
		cout << "Enter Main ... " << endl;

		HANDLE hToken;
		TOKEN_PRIVILEGES stTokenPrivilege;

		//Get Current Process's Token with TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY privilege
		if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken))
		{
			cout << "OpenProcessToken Error : " << GetLastError() << endl;
			return ;
		}

		//Get the LUID (local unique identifier) 
		if (!LookupPrivilegeValue(NULL, SE_SHUTDOWN_NAME, &stTokenPrivilege.Privileges[0].Luid))
		{
			cout << "LookupPrivilegeValue Error : " << GetLastError() << endl;
			return ;
		}

		stTokenPrivilege.PrivilegeCount = 1;
		stTokenPrivilege.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

		if (!AdjustTokenPrivileges(hToken, FALSE, &stTokenPrivilege, 0, NULL, NULL))
		{
			cout << "AdjustTokenPrivileges Error : " << GetLastError() << endl;
			return ;
		}

		if (hToken != NULL)
		{
			CloseHandle(hToken);
		}

		BOOL result = ExitWindowsEx(flags, 0);
		cout << result << endl;

		if (result == 0)
		{
			DWORD error = GetLastError();
			cout << error << endl;
		}

		cout << "Rebooting ... " << endl;
	}

	catch (exception ex)
	{
		cout << ex.what() << endl;
	}
}

void Usage()
{
	cout << "Reboot -r -f(optional) : " << endl;
	cout << "-f : force reboot " << endl;
}

int _tmain(int argc, _TCHAR* argv[])
{
	wchar_t* r = L"-r";
	wchar_t* f = L"-f";
	if (argc >= 2)
	{
		if(*argv[1] == *r)
		{
			UINT flags = EWX_REBOOT;
			if (argc == 3 && *argv[2] == *f)
			{
				flags |= EWX_FORCE;
			}
			Reboot(flags);
		}
		else
		{
			Usage();
		}
	}
	else
	{
		Usage();
	}
	return 0;
}


