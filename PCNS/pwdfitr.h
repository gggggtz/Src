
#ifndef PWDFITR_H
#define PWDFITR_H

# include <stdio.h>
# include <stdlib.h>
# include <time.h>
# include <windows.h>
# include <ntsecapi.h>
# include <wchar.h>
# include <string.h>

#define PSHK_MAX_COMMANDLINE_LEN	256
#define PSHK_PRE_CHANGE				1
#define PSHK_POST_CHANGE			2
#define PSHK_SUCCESS				0
#define PSHK_FAILURE				1

#define PSHK_PROCESS_FLAGS CREATE_NEW_PROCESS_GROUP | CREATE_NO_WINDOW | CREATE_UNICODE_ENVIRONMENT

/* Global static variable that holds config */
/* It is assumed that the LSA is single threaded */
/* All variables are readonly after initialization any */
typedef struct 
{
	int valid;
	int logLevel;
	DWORD maxLogSize;
	wchar_t *postChangeProg;
	wchar_t *postChangeProgArgs;
	wchar_t *preChangeProg;
	wchar_t *preChangeProgArgs;
	wchar_t *logFile;
	wchar_t *workingDir;
	wchar_t *environment;
	wchar_t *environmentStr;
	int priority;
	int processCreateFlags;
	int preChangeProgWait;
	int postChangeProgWait;
	BOOL urlencode;
	BOOL doublequote;
	BOOL inheritParentHandles;
} pshkConfigStruct;

/* registry read functions */
# include "registry.h"

/* log access functions */
# include "logging.h"

/* misc. utility functions */
# include "util.h"

/* undef to enabled URLEncoding/double-quoting of username */
#define RAW_USERNAME
#endif