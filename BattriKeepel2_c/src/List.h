#pragma once
#include "Core.h"

typedef struct FE_List_impl {
    size_t count;
    size_t capacity;
    Bool isInitialized;
    void* head;
} FE_List_impl;

//FE_List must be freed with FE_ListClear(a)
#define FE_List(type)  struct { FE_List_impl impl; type* data; }
//with 'fe_list' an FE_List(type)
#define FE_ListGet(fe_list, id) ListGet_imp(&(fe_list).impl, (Byte*)fe_list.data, id, sizeof(*(fe_list).data))

//Put list as a parameter
#define FE_ListParameterPtr(type) FE_List_impl*
#define FE_ListFromParameterPtr(parameter) { *parameter, parameter->head }
////Convert list parameter to FE_List(type)
//#define FE_ListParameterRead(newVariable, parameterPtrVariableName, type) FE_List(type) newVariable = { impl_list_##parameterPtrVariableName, data_list_##parameterPtrVariableName }


//with 'fe_list' an FE_List(type)
#define FE_ListInit(fe_list) BK2_CORE_ASSERT(ListInit_impl(&(fe_list).impl, (Byte**)&(fe_list).data), "failed to init fe_list")
//with 'fe_list' an FE_List(type)
#define FE_ListPop(fe_list) BK2_CORE_ASSERT(ListPop_impl(&(fe_list).impl, (Byte**)&(fe_list).data), "failed to pop fe_list")
//with 'fe_list' an FE_List(type); 'id' the item id to remove
#define FE_ListRemoveAt(fe_list, id) BK2_CORE_ASSERT(ListRemoveAt_impl(&(fe_list).impl, (Byte**)&(fe_list).data, id, sizeof(*(fe_list).data)), "failed to removeAt fe_list")
//with 'fe_list' an FE_List(type); 'value' the value to remove; don't work with char*; return index of the item removed
#define FE_ListRemove(fe_list, value) ListRemove_impl(&(fe_list).impl, (Byte**)&(fe_list).data, (const void*)&value, sizeof(*(fe_list).data))
//with 'fe_list' an FE_List(type); 'value' type of 'type'; 'position' is Uint32
#define FE_ListInsert(fe_list, value, position) BK2_CORE_ASSERT(ListInsert_impl(&(fe_list).impl, (Byte**)&(fe_list).data, (const void*)&value, position, sizeof(*(fe_list).data)), "failed to insert fe_list")

//with 'fe_list' an FE_List(type); 'type' same type of *fe_list.data; 'value' type of 'type'; 'position' is Uint32
#define FE_ListInsertValue(fe_list, type, value, position) { \
type temp = value; \
ListInsert_impl(&(fe_list).impl, (Byte**)&(fe_list).data, (const void*)&value, position, sizeof(*(fe_list).data)) \
}


//with 'fe_list' an FE_List(type); 'value' type of 'type'
#define FE_ListPush(fe_list, value)  BK2_CORE_ASSERT(ListPushArray_impl(&(fe_list).impl, (Byte**)&(fe_list).data, (const void*)&value, 1, sizeof(*(fe_list).data)), "failed to push fe_list")

//#define FE_ListPush(fe_list, value) FE_ListPush_impl(&fe_list.impl, (Byte**)&fe_list.data, (const void*)&value, sizeof(*fe_list.data))
//with 'fe_list' an FE_List(type); 'type' same type of *fe_list.data; 'value' type of 'type';
#define FE_ListPushValue(fe_list, type, value) { \
type temp = value; \
BK2_CORE_ASSERT(ListPushArray_impl(&(fe_list).impl, (Byte**)&(fe_list).data, (const void*)&temp, 1, sizeof(*(fe_list).data)), "failed to push value fe_list"); \
}
//with 'fe_list' an FE_List(type); 'value' type of 'type'
#define FE_ListPushArray(fe_list, arrayDataPtr, countToPush) BK2_CORE_ASSERT(ListPushArray_impl(&(fe_list).impl, (Byte**)&(fe_list).data, (const void*)arrayDataPtr, countToPush, sizeof(*(fe_list).data)), "failed to push array fe_list")
//with 'fe_list' an FE_List(type); 'value' type of 'type' it reserve a supplement : list.capacity += amount
#define FE_ListReserve(fe_list, amount) BK2_CORE_ASSERT(ListReserve_impl(&(fe_list).impl, (Byte**)&(fe_list).data, amount, sizeof(*(fe_list).data)), "failed to reserve fe_list")
//with 'fe_list' an FE_List(type); 'value' type of 'type' it reserve a supplement : list.capacity += amount and list.count += amount
#define FE_ListResize(fe_list, amount) BK2_CORE_ASSERT(ListResize_impl(&(fe_list).impl, (Byte**)&(fe_list).data, amount, sizeof(*(fe_list).data)), "failed to resize fe_list")
//with 'fe_list' an FE_List(type);
#define FE_ListClear(fe_list) BK2_CORE_ASSERT(ListClear_impl(&(fe_list).impl, (Byte**)&(fe_list).data), "failed to clear fe_list")
//with 'fe_list' an FE_List(type);
#define FE_ListRemoveDuplicate(fe_list) BK2_CORE_ASSERT(ListRemoveDuplicate_impl(&(fe_list).impl, (Byte**)&(fe_list).data, sizeof(*(fe_list).data)), "failed to remove duplicate fe_list")
//with 'fe_listA' an FE_List(type); 'fe_listB' an FE_List(type);
#define FE_ListEqual(fe_listA, fe_listB) ListEqual_impl(&(fe_listA).impl, &(fe_listB).impl, (Byte**)&(fe_listA).data, (Byte**)&(fe_listB).data, sizeof(*(fe_list).data))

//with 'fe_list' an FE_List(type);
#define FE_ListRemoveAll(fe_list) BK2_CORE_ASSERT(ListRemoveAll_impl(&(fe_list).impl), "failed to set count")

//with 'fe_list' an FE_List(type);
#define FE_ListPrint(fe_list) ListPrint_impl(&(fe_list).impl, (Byte**)&(fe_list).data, sizeof(*(fe_list).data))


Bool ListInit_impl(FE_List_impl* list, Byte** data);
void* ListGet_imp(FE_List_impl* list, Byte* data, size_t id, size_t dataSize);

Bool ListPop_impl(FE_List_impl* list, Byte** data);
int64_t ListRemove_impl(FE_List_impl* list, Byte** data, const void* value, size_t dataSize);
Bool ListRemoveAt_impl(FE_List_impl* list, Byte** data, uint32_t id, size_t dataSize);

Bool ListInsert_impl(FE_List_impl* list, Byte** data, const void* value, int32_t position, size_t dataSize);

//Bool FE_DECL FE_ListPush_impl(FE_List_impl* list, Byte** data, const void* value, size_t dataSize);
Bool ListPushArray_impl(FE_List_impl* list, Byte** data, const void* arrayData, size_t countToPush, size_t dataSize);

Bool ListReserve_impl(FE_List_impl* list, Byte** data, size_t amount, size_t dataSize);
Bool ListResize_impl(FE_List_impl* list, Byte** data, size_t amount, size_t dataSize);
Bool ListClear_impl(FE_List_impl* list, Byte** data);

Bool ListRemoveDuplicate_impl(FE_List_impl* list, Byte** data, size_t dataSize);

Bool ListEqual_impl(FE_List_impl* listA, FE_List_impl* listB, Byte** dataA, Byte** dataB, size_t dataSize);

Bool ListRemoveAll_impl(FE_List_impl* list);

void ListPrint_impl(const FE_List_impl* list, const Byte* data, size_t dataSize);
void ListPrintReport();
