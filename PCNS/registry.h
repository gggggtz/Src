

#ifndef PKSH_REGISTRY_H
#define PKSH_REGISTRY_H

#define PSHK_REG_KEY L"SYSTEM\\CurrentControlSet\\Control\\Lsa\\pwdfitr"
#define PSHK_REG_VALUE_MAX_LEN	256
#define PSHK_REG_VALUE_MAX_LEN_BYTES PSHK_REG_VALUE_MAX_LEN * sizeof(wchar_t)

pshkConfigStruct pshk_read_registry(void);

#endif