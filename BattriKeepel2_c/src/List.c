#include "List.h"

#ifdef FE_DEBUG
Uint64 allocatedListCount = 0;
Uint64 freedListCount = 0;
#endif

Bool FE_ListCheck(FE_List_impl* list, Byte** data)
{
	return list != NULL && NULL != data && list->isInitialized == TRUE;
}

void* ListGet_imp(FE_List_impl* list, Byte* data, size_t id, size_t dataSize)
{
	if (!FE_ListCheck(list, &data))
	{
		printf("failed to get");
		return NULL;
	}

    if(list->count > id && id >= 0)
    {
        return data + id * dataSize;
    }

	printf("failed to get");
    return NULL;
}

Bool ListInit_impl(FE_List_impl* list, Byte** data)
{
	list->count = 0;
	list->capacity = 0;
	*data = NULL;
	list->isInitialized = TRUE;
	list->head = NULL;

	return TRUE;
}

Bool ListPop_impl(FE_List_impl* list, Byte** data)
{
	if (!FE_ListCheck(list, data))
	{
		printf("failed to pop");
		return FALSE;
	}

	list->count--;

	/*if (list->count == 0)
	{
		FE_MemoryGeneralFree(data);
		return;
	}*/

	/*Byte** temp = *data;
	*data = FE_MemoryGeneralRealloc(*data, (list->count) * list->dataSize);
	if (*data == NULL)
	{
		*data = temp;
		return FALSE;
	}*/

	return TRUE;
}

int64_t ListRemove_impl(FE_List_impl* list, Byte** data, const void* value, size_t dataSize)
{
	if (!FE_ListCheck(list, data) || *data == NULL)
	{
		printf("failed to remove ptr %p", value);
		return -1;
	}

	uint32_t id = 0;
	Bool isFound = FALSE;
	for (; id < list->count; id++)
	{
		isFound = memcmp((*data) + id * dataSize, value, dataSize) == 0;
		if (isFound) break;
	}

	if (!isFound) return -1;

	uint32_t nextId = id + 1;

	memcpy((*data) + dataSize * id, (*data) + dataSize * nextId, ((list->count - 1) - id) * dataSize);

	list->count--;

	return id;
}

Bool ListRemoveAt_impl(FE_List_impl* list, Byte** data, uint32_t id, size_t dataSize)
{
	if (!FE_ListCheck(list, data) || *data == NULL)
	{
		printf("failed to remove at %d", id);
		return FALSE;
	}

	uint32_t nextId = id + 1;

	memcpy((*data) + dataSize * id, (*data) + dataSize * nextId, ((list->count - 1) - id) * dataSize);

	list->count--;

	return TRUE;
}

Bool ListInsert_impl(FE_List_impl* list, Byte** data, const void* value, int32_t position, size_t dataSize)
{
	if (!FE_ListCheck(list, data) || *data == NULL || position > list->count - 1)
	{
		printf("failed to insert ptr %p at %d", value, position);
		return FALSE;
	}

	Byte* temp = *data;

	if (list->capacity < list->count + 1)
	{
		list->capacity = list->count + 1;
		*data = realloc(*data, list->capacity * dataSize);
	}

	if (*data == NULL)
	{
		*data = temp;
		return FALSE;
	}


	//data[dataSize * (list->count - 1)] = NULL;

	size_t bytePos = dataSize * position;

	//for (SizeT i = dataSize * (list->count+1); i > bytePos; i--) //move data to the end of the array
	//{
	//	*((*data) + i) = *((*data) + (i - dataSize));
	//}

	for (size_t i = dataSize * list->count; i > bytePos; i -= dataSize)// it does not iterate on Byte* because it's less optimized du to compiler optimization, pointer calculation, ...
	{
		memcpy((*data) + i, (*data) + (i - dataSize), dataSize);
	}

	list->count++;

	memcpy((*data) + dataSize * position, value, dataSize);

	return TRUE;
}


//Bool FE_ListPush_impl(FE_List_impl* list, Byte** data, const void* value, SizeT dataSize)
//{
//	FE_CORE_ASSERT(list != NULL && data != NULL && list->isInitialized == TRUE, "list or data is null or list hasn't been initialized");
//	if (list == NULL) return FALSE;
//
//	Byte* temp = *data;
//
//	if (list->capacity <= list->count)
//	{
//		if (*data == NULL)
//		{
//			list->capacity += 1;
//			(*data) = FE_MemoryGeneralAlloc(dataSize * list->capacity);
//		}
//		else
//		{
//			list->capacity += list->capacity / 2;
//			(*data) = FE_MemoryGeneralRealloc((*data), dataSize * list->capacity);
//		}
//	}
//
//	if ((*data) == NULL)
//	{
//		(*data) = temp;
//		return FALSE;
//	}
//
//	SizeT lastItemId = list->count;
//	list->count++;
//
//	memcpy((*data) + dataSize * lastItemId, value, dataSize);
//
//	return TRUE;
//}

Bool ListPushArray_impl(FE_List_impl* list, Byte** data, const void* arrayData, size_t sizeToPush, size_t dataSize)
{
	if (!FE_ListCheck(list, data) || arrayData == NULL || sizeToPush <= 0)
	{
		printf("failed to push array fe_list");
		return FALSE;
	}

	Byte* temp = *data;

	if (list->capacity <= list->count - 1 + sizeToPush)
	{
		if (*data == NULL)
		{
			list->capacity += sizeToPush;
			(*data) = malloc(dataSize * list->capacity);
			list->head = *data;
#ifdef FE_DEBUG
			allocatedListCount++;
#endif
		}
		else
		{
			list->capacity += sizeToPush + llabs((int64_t)(list->capacity / 2 - sizeToPush));
			(*data) = realloc((*data), list->capacity * dataSize);
		}
	}

	if ((*data) == NULL)
	{
		(*data) = temp;
		return FALSE;
	}

	memcpy((*data) + dataSize * list->count, arrayData, dataSize * sizeToPush);
	list->count += sizeToPush;

	return TRUE;
}


Bool ListReserve_impl(FE_List_impl* list, Byte** data, size_t amount, size_t dataSize)
{
	if (!FE_ListCheck(list, data))
	{
		printf("failed to reserve fe_list");
		return FALSE;
	}

	list->capacity += amount;

	Byte* temp = *data;

#ifdef FE_DEBUG
	if(*data == NULL) allocatedListCount++;
#endif

	if ((*data) == NULL)
	{
		(*data) = malloc(dataSize * list->capacity);
		list->head = *data;
	}
	else
	{
		(*data) = realloc((*data), dataSize * list->capacity);
	}

	if ((*data) == NULL)
	{
		(*data) = temp;
		return FALSE;
	}

	return TRUE;
}

Bool ListResize_impl(FE_List_impl* list, Byte** data, size_t amount, size_t dataSize)
{
	if (!FE_ListCheck(list, data) || list->count >= amount)
	{
		printf("failed to resize fe_list");
		return FALSE;
	}

	Bool result = ListReserve_impl(list, data, amount, dataSize);
	if (!result) return result;

	memset((*data) + list->count * dataSize, 0, dataSize * amount);
	list->count += amount;

	return result;
}

Bool ListClear_impl(FE_List_impl* list, Byte** data)
{
	if (!FE_ListCheck(list, data))
	{
		printf("failed to clear fe_list");
		return FALSE;
	}

	list->capacity = 0;
	list->count = 0;
	if (*data != NULL)
	{
		free(*data);

#ifdef FE_DEBUG
	freedListCount++;
#endif
	}
	return TRUE;
}

Bool ListRemoveDuplicate_impl(FE_List_impl* list, Byte** data, size_t dataSize)
{
	if (!FE_ListCheck(list, data) || *data == NULL || list->count < 2)
	{
		printf("failed to remove duplicate");
		return FALSE;
	}

	Bool isSame;
	for (uint32_t i = 0; i < list->count; i++)
	{
		for (uint32_t y = i + 1; y < list->count; y++)
		{
			isSame = TRUE;

			isSame = memcmp((*data) + i * dataSize, (*data) + y * dataSize, dataSize) == 0;
			/*for (Uint32 b = 0; b < dataSize; b++) for loop equivalent (barely) to memcmp
			{
				if ((*data)[i * dataSize + b] != (*data)[y * dataSize + b])
				{
					isSame = FALSE;
					break;
				}
			}*/

			if (isSame)
			{
				ListRemoveAt_impl(list, data, y, dataSize);
			}
		}
	}

	return TRUE;
}

Bool ListEqual_impl(FE_List_impl* listA, FE_List_impl* listB, Byte** dataA, Byte** dataB, size_t dataSize)
{
	if (listA != NULL && listB != NULL && listA->isInitialized == TRUE && listB->isInitialized == TRUE)
	{
		printf("failed to verify equal fe_list");
		return FALSE;
	}

	if (listA == NULL || listB == NULL) return FALSE;

	if (listA->count != listB->count) return FALSE;
	if (dataA == dataB) return TRUE;

	for (size_t i = 0; i < listA->count; i++)
	{
		if ((*dataA)[i] != (*dataB)[i]) return FALSE;
	}

	return TRUE;
}

Bool ListRemoveAll_impl(FE_List_impl* list)
{
	list->count = 0;
	return TRUE;
}

void ListPrint_impl(const FE_List_impl* list, const Byte* data, size_t dataSize)
{
	for (size_t i = 0; i < list->count; i++)
	{
		printf("%d", data[dataSize * i]);
	}
}

void ListPrintReport()
{
#ifdef FE_DEBUG
	FE_CORE_LOG_SUCCESS("list report | alloc : %lld        free : %lld", allocatedListCount, freedListCount);

	if (allocatedListCount == freedListCount)
	{
		if (allocatedListCount < freedListCount)
		{
			FE_CORE_LOG_SUCCESS("too much FE_List freed !");
		}
		else
		{
			FE_CORE_LOG_SUCCESS("no memory leak in FE_List detected ! ");
		}
	}
	else
	{
		FE_CORE_LOG_WARNING("memory leak detected");
	}
#endif
}
