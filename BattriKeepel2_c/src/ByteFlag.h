#pragma once
#include "Core.h"
#include "List.h"

typedef struct { FE_List_impl impl; Byte* data; } FE_ByteFlag;

void FE_FlagInit(FE_ByteFlag* flag);
void FE_FlagDestroy(FE_ByteFlag* flag);
void FE_FlagReset(FE_ByteFlag* flag);

void FE_FlagSet(FE_ByteFlag* flag, uint64_t newFlag);
Bool FE_FlagExist(FE_ByteFlag* flag, uint64_t searchedFlag);
void FE_FlagRemove(FE_ByteFlag* flag, uint64_t flagToRemove);

void FE_FlagPrint(FE_ByteFlag* flag);
