#ifndef LOGGING_H
#define LOGGING_H

#define PSHK_LOG_ERROR		1
#define PSHK_LOG_DEBUG		2
#define PSHK_LOG_ALL		3

#define ALLOCATION_ERROR "Cannot allocate memory"
#define CONVERSION_ERROR "Error converting to UTF-8"

/* Open the log file */
/* NOTE: Once the file is opened, it is never closed */
HANDLE pshk_log_open();
void pshk_log_close(HANDLE h);
BOOL pshk_log_write_w(HANDLE h, LPCWSTR s);
BOOL pshk_log_write_a(HANDLE h, LPCSTR s);

#ifdef _DEBUG

/* Use during debuging only */
/* opens, writes, closes */
void pshk_log_debug_log(LPCTSTR s);

#define PSHK_DEBUG_PRINT(x) pshk_log_debug_log(x)

#else

#define PSHK_DEBUG_PRINT(x)

#endif

#endif