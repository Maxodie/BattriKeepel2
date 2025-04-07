#include "ByteFlag.h"
#include "Core.h"

void FE_FlagInit(FE_ByteFlag* flag)
{
	FE_ListInit(*flag);
	FE_ListPushValue(*flag, Byte, 0);
}

void FE_FlagDestroy(FE_ByteFlag* flag)
{
	FE_ListClear(*flag);
}

void FE_FlagReset(FE_ByteFlag* flag)
{
	memset(flag->data, 0, flag->impl.count); // Byte is 1
}

void FE_FlagSet(FE_ByteFlag* flag , uint64_t newFlag)
{
	BK2_CORE_ASSERT(flag->impl.isInitialized, "ByteFlag has not been initialized");

	uint64_t flagBit = newFlag / 8;

	if (flag->impl.count <= flagBit)
	{
		uint64_t amountToAdd = flagBit - (flag->impl.count - 1);

		Byte arr[] = {0};
		FE_ListPushArray(*flag, arr, amountToAdd);
		memset(flag->data + flag->impl.count - amountToAdd, 0, amountToAdd);
	}

	flag->data[flagBit] |= BIT(newFlag % 8);
}

Bool FE_FlagExist(FE_ByteFlag* flag, uint64_t searchedFlag)
{
	BK2_CORE_ASSERT(flag->impl.isInitialized, "ByteFlag has not been initialized");

	uint64_t flagBit = searchedFlag / 8;

	if (flag->impl.count < flagBit)
	{
		return FALSE;
	}

	return flag->data[flagBit] & BIT(searchedFlag % 8);
}

void FE_FlagRemove(FE_ByteFlag* flag, uint64_t flagToRemove)
{
	BK2_CORE_ASSERT(flag->impl.isInitialized, "ByteFlag has not been initialized");

	uintptr_t flagBit = flagToRemove / 8;

	if (flag->impl.count < flagBit)
	{
		return;
	}

	flag->data[flagBit] &= ~BIT(flagToRemove % 8);
}

void FE_FlagPrint(FE_ByteFlag* flag)
{
	char* result = malloc(flag->impl.count * 8 * 2);
	size_t bitCount = 0;

	for (size_t i = 0; i < (flag->impl.count * 8) * 2 - 1; i++)
	{
		if (i % 2 == 0)
		{
			result[i] =  ((flag->data[bitCount / 8] & BIT(bitCount % 8)) ? 1  : 0)  + '0';
			bitCount++;
		}
		else
		{
			result[i] = '-';
		}
	}

	result[(flag->impl.count * 8) * 2 - 1] = '\0';

	printf("%s", result);
	free(result);
}
