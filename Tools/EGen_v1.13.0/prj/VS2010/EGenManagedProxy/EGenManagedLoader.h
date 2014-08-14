/// <copyright>
/// Copyright ©  2014 Microsoft Corporation. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

#pragma once

#include <string>

#include "../../../inc/EGenLoader_stdafx.h"
#include "../../../InputFiles/inc/DataFileManager.h"

#include <cstdio>
#include <cassert>
#include <fstream>
#include <set>
#include <map>
#include <iostream>
#include <algorithm>
#include <string>
#include <vector>

#include <windows.h>

#include <sql.h>
#include <sqlext.h>
#include <odbcss.h>

// TODO: reference additional headers your program requires here
using namespace std;

#include "../../../inc/EGenTables_stdafx.h"
#include "../../../inc/EGenBaseLoader_stdafx.h"
#include "../../../inc/EGenGenerateAndLoad_stdafx.h"

// Include one or more load types.
//#include "../../../inc/NullLoad_stdafx.h"
#include "../../../inc/FlatFileLoad_stdafx.h"
//#include "../../../inc/win/ODBCLoad_stdafx.h"
//#include "../../../inc/custom/CustomLoad_stdafx.h"

// Generic Error Codes
#define ERROR_BAD_OPTION 1
#define ERROR_INPUT_FILE 2
#define ERROR_INVALID_OPTION_VALUE 3


using namespace System;

namespace EGenManagedProxy {

	public ref class EGenManagedLoader
	{
	public:
		EGenManagedLoader();
		~EGenManagedLoader();

	private:

	};

}
