// HelloWorldClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "..\RpcInterface\SampleRpc_h.h"

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	RPC_STATUS status;

	RPC_CSTR szStringBinding = NULL;

	//Creates a string binding handle.
	//This function is nothing more than a printf.
	//Connection is not done here.
	status = RpcStringBindingCompose(
		NULL,										//UUID to bind to
		reinterpret_cast<RPC_CSTR>("ncacn_ip_tcp"),	//Use TCP/IP protocol
		reinterpret_cast<RPC_CSTR>("localhost"),	//TCP/IP network host
		reinterpret_cast<RPC_CSTR>("4747"),			//Port
		NULL,										//Protocol dependent network options to use
		&szStringBinding);							//String binding output

	if (status)
	{
		exit(status);
	}

	//Validate the format of the string binding handle and converts it to a binding handle
	//Connection is not done here.
	status = RpcBindingFromStringBinding(
		szStringBinding,							//The string binding to validate
		&hSampleRpcBinding);								//Put the result in the implicit binding
													//handle defined in the IDL file.

	if (status)
	{
		exit(status);
	}

	RpcTryExcept
	{
		//Calls the RPC function.
		//The hSampleRpc is used implicitly
		//Connection is done here.
		Output(reinterpret_cast<const char*>("Hello RPC World !"));
	}
	RpcExcept(1)
	{
		cerr << "Runtime reported exception" << RpcExceptionCode() << endl;
	}
	RpcEndExcept

	//Free the memory allocated by a string
	status = RpcBindingFree(
		&hSampleRpcBinding									//String to be freed
	);

	if (status)
	{
		exit(status);
	}

	return 0;
}

//Memory allocation function for RPC
//The runtime use these two functions for allocation/deallocating eough 
//memory to passthe string to the server

void* __RPC_USER midl_user_allocate(size_t size)
{
	return malloc(size);
}

void __RPC_USER midl_user_free(void* p)
{
	free(p);
}

