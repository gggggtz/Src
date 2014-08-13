
LPWSTR bool_to_string(BOOL b);
LPWSTR null_to_string(LPWSTR b);
LPWSTR alloc_copy(LPWSTR src, size_t length);
LPWSTR pshk_struct2str();
LPWSTR rawurlencode(LPWSTR src);
LPWSTR doublequote(LPWSTR src);
int pshk_exec_prog(int option, PUNICODE_STRING username, PUNICODE_STRING password);
BOOL password_valid(wchar_t * pwd, int length);
BOOL has_child(wchar_t *str, int len, const wchar_t * child, int clen);