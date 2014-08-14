/// <copyright>
/// Copyright ©  2014 Microsoft Corporation. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

#pragma once

#include <Windows.h>
#include <cstdio>

using namespace System;

void wchar2char(const wchar_t * wstr, char* str, int length);

void char2wchar(const char * str, wchar_t * wstr, int lenght);

String^ wchar2mstr(const wchar_t * wstr);

wchar_t * mstr2wchar(String^ mstr);
