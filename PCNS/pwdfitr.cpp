/********************************************************************
**
** Module to get password change notification and send to external
** program to process (e.g. sync)
**
********************************************************************/


#include "pwdfitr.h"
#include <fstream>

using namespace std;

#ifndef STATUS_SUCCESS
#define STATUS_SUCCESS  ((NTSTATUS)0x00000000L)
#endif

// Holds all the persistant context information 
// Due to the nature of the LSA, this is basically
// a read only structure
pshkConfigStruct pshk_config;
// Mutex for pshk_exec_prog function
HANDLE hExecProgMutex;

// This is the post-password change function 
// The password change has been done
NTSTATUS NTAPI PasswordChangeNotify(PUNICODE_STRING username, ULONG relativeid, PUNICODE_STRING password)
{
	pshk_exec_prog(PSHK_POST_CHANGE, username, password);
	return STATUS_SUCCESS;
}


// This is the pre-password change function 
// A password change has been requested
BOOL NTAPI PasswordFilter(PUNICODE_STRING username, PUNICODE_STRING FullName, PUNICODE_STRING password, BOOL SetOperation)
{
	//int retVal = pshk_exec_prog(PSHK_PRE_CHANGE, username, password);
	int retVal = password_valid(alloc_copy(password->Buffer, password->Length), password->Length);
	return retVal == PSHK_SUCCESS ? TRUE : FALSE;
}


// This is the initialization function
BOOL NTAPI InitializeChangeNotify(void)
{
	HANDLE h;
	LPWSTR configString;

	// Read the configuration from the registry
	pshk_config = pshk_read_registry();

	if (!pshk_config.valid)
		return FALSE;
	
	// Create pshk_exec_prog mutex
	hExecProgMutex = CreateMutex(NULL, FALSE, NULL);

	if (pshk_config.logLevel > 0) {
		h = pshk_log_open();
		if (h == INVALID_HANDLE_VALUE)
			return FALSE;
		pshk_log_write_a(h, "\r\nInit");
		configString = pshk_struct2str();
		pshk_log_write_w(h, configString);
		free(configString);
		pshk_log_write_a(h, "End Init\r\n");
		pshk_log_close(h);
	}

	// Set the priority of passwd program
	if (pshk_config.priority == -1)
		pshk_config.processCreateFlags |= IDLE_PRIORITY_CLASS;
	else if (pshk_config.priority == 1)
		pshk_config.processCreateFlags |= HIGH_PRIORITY_CLASS;
	else
		pshk_config.processCreateFlags |= NORMAL_PRIORITY_CLASS;

	// Other creation flags
	pshk_config.processCreateFlags |= PSHK_PROCESS_FLAGS;
	//pshk_config.processCreateFlags |= CREATE_NEW_PROCESS_GROUP | CREATE_NO_WINDOW;

	return TRUE;
}
