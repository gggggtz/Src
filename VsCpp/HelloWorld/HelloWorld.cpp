// HelloWorld.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "..\RpcInterface\SampleRpc_h.h"

using namespace std;

RPC_STATUS CALLBACK SecurityCallback(RPC_IF_HANDLE /*hInterface*/, void* /*pBindingHandle*/)
{
	return RPC_S_OK;
}

void Output(const char* szOutput)
{
	cout << szOutput << endl;
}


int _tmain(int argc, _TCHAR* argv[])
{
	RPC_STATUS status;

	//Use the protocol combined with the endpoint for receiving remote procedure calls
	status = RpcServerUseProtseqEp(
		reinterpret_cast<RPC_CSTR>("ncacn_ip_tcp"),	//use TCP/IP protocol
		RPC_C_PROTSEQ_MAX_REQS_DEFAULT,				//Backlog queue length for TCP/IP
		reinterpret_cast<RPC_CSTR>("4747"),			//TCP/IP port to use
		NULL);										//No security

	if (status)
	{
		exit(status);
	}

	//Register the SampleRpc interface
	status = RpcServerRegisterIf2(
		SampleRpc_v1_0_s_ifspec,					//interface to register
		NULL,										//Use the MIDL generated entry-point vector
		NULL,										//Use the MIDL generated entry-point vector
		RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH,		//Forces use of security callback
		RPC_C_LISTEN_MAX_CALLS_DEFAULT,				//Use default number of concurrent calls
		(unsigned)-1,								//Infinite max size of incoming data blocks
		SecurityCallback);							//Naive security callback

	if (status)
	{
		exit(status);
	}

	//Start to listen for remote procedure calls for all registered interfaces
	//This call will not return until RpcMgmtStopServerListening is called

	status = RpcServerListen(
		1,											//Recommended minimum number of threads
		RPC_C_LISTEN_MAX_CALLS_DEFAULT,				//Recommended maximum number of threads
		FALSE);										//Start listening now

	if (status)
	{
		exit(status);
	}

	//Output("Hello Lonely World !");
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

