#pragma once

#include "Core.h"
#include "List.h"
#include "ByteFlag.h"

typedef int64_t FE_EntityID;
typedef int64_t FE_EntityComponentTypeID;
typedef int64_t FE_EntityComponentID;

typedef struct FE_Entity
{
	FE_EntityID entityUuid;
	FE_ByteFlag byteFlag;
} FE_Entity;

typedef struct FE_EntityComponentList
{
	const size_t compSize;
	size_t count;
	void* dataList;
} FE_EntityComponentList;

typedef struct FE_EntityRegistry
{
	FE_List(FE_Entity) entities;
	FE_List(FE_EntityComponentTypeID) compUuids;
	FE_List(FE_List(FE_EntityID)) compEntityUuids;
	FE_List(FE_EntityComponentList) dataList;
} FE_EntityRegistry;

EXTERN BK2_API void FE_EntityCreateRegistry(FE_EntityRegistry* registry);
EXTERN BK2_API void FE_EntityDestroyRegistry(FE_EntityRegistry* registry);
EXTERN BK2_API FE_EntityID FE_EntityCreate(FE_EntityRegistry* registry);
EXTERN BK2_API void FE_EntityDestroy(FE_EntityRegistry* registry, FE_EntityID uuid);
EXTERN BK2_API FE_EntityComponentTypeID FE_EntityCreateComponentType(FE_EntityRegistry* registry, size_t compSize);
EXTERN BK2_API void FE_EntityDestroyComponentType(FE_EntityRegistry* registry, FE_EntityComponentTypeID componentUuid);
EXTERN BK2_API void* FE_EntityAttachComp(FE_EntityRegistry* registry, FE_EntityID entityUuid, FE_EntityComponentTypeID componentUuid);
EXTERN BK2_API void FE_EntityDetachComp(FE_EntityRegistry* registry, FE_EntityID entityUuid, FE_EntityComponentTypeID componentUuid);

EXTERN BK2_API FE_Entity* FE_EntityQueryFromID(const FE_EntityRegistry* registry, FE_EntityID uuid);
EXTERN BK2_API FE_EntityComponentList* FE_EntityComponentListQueryFromID(const FE_EntityRegistry* registry, FE_EntityComponentTypeID uuid);
EXTERN BK2_API void* FE_EntityComponentQueryFromID(const FE_EntityRegistry* registry, FE_EntityID entityUuid, FE_EntityComponentTypeID componentUuid);
EXTERN BK2_API FE_EntityComponentID FE_EntityComponentIDQueryFromID(const FE_EntityRegistry* registry, FE_EntityID entityUuid, FE_EntityComponentTypeID componentUuid);

EXTERN BK2_API Bool FE_EntityHasComponent(const FE_EntityRegistry* registry, FE_EntityID entityUuid, FE_EntityComponentTypeID componentUuid);
EXTERN BK2_API void FE_EntityPrintEntityCompFlags(const FE_EntityRegistry* registry, FE_EntityID entityUuid);
