#pragma once

#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <memory.h>
#include <assert.h>

#define BK2_DLLEXPORT __declspec(dllexport)
#define BK2_DLLIMPORT __declspec(dllimport)
#define EXTERN extern

#ifdef BK2_EXPORTS
#   define BK2_API BK2_DLLEXPORT
#else
#   define BK2_API BK2_DLLIMPORT
#endif

#ifdef ASSERT
#   define BK2_CORE_ASSERT(x, msg) do {if(!(x)) { assert(x); printf(msg); } } while(0)
#else
#   define BK2_CORE_ASSERT(x, msg)
#endif

#define BIT(x) 1 << x

typedef char Byte;
typedef Byte Bool;

#define TRUE 1
#define FALSE 0
