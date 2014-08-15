/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

#include "stdafx.h"
#include "Utility.h"
#include <vcclr.h>

using namespace System;

void wchar2char(const wchar_t * wstr, char* str, int length)
{
	WideCharToMultiByte(CP_ACP, 0, wstr, -1, str, length, NULL, NULL);
}

void char2wchar(const char * str, wchar_t * wstr, int length)
{
	MultiByteToWideChar(CP_ACP, 0, str, length, wstr, length/sizeof(wstr[0]));
}

String^ wchar2mstr(const wchar_t * wstr)
{
	return gcnew String(wstr);
}

wchar_t * mstr2wchar(String^ mstr)
{
	pin_ptr<const wchar_t> ptr = PtrToStringChars(mstr);
	wchar_t * result = const_cast<wchar_t *>(ptr);
	return result;
}