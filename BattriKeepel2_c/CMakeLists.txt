cmake_minimum_required(VERSION 3.15)
project(BattriKeepel2)

set(CMAKE_C_COMPLER MSVC)
set(CMAKE_C_STANDARD 17)

set(CMAKE_EXPORT_COMPILE_COMMANDS "ON" CACHE STRING "export compile commands.")
message("==== Feur export compile commands set to ${CMAKE_EXPORT_COMPILE_COMMANDS} ====")

if(CMAKE_EXPORT_COMPILE_COMMANDS)
    message("==== Feur create .clang file to store 'CompilationDatabase: build/' ====")
    file(WRITE ".clang"
        "CompileFlags:
            CompilationDatabase: build/"
    )
endif()

add_library(BattriKeepel2 SHARED
    src/core.h
    src/ECS.c
    src/ECS.h
    src/ECSSystems.c
    src/ECSSystems.h
    src/ByteFlag.c
    src/ByteFlag.h
    src/List.c
    src/List.h
)

set_target_properties(BattriKeepel2 PROPERTIES
    OUTPUT_NAME BattriKeepel2
    ARCHIVE_OUTPUT_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/bin/BattriKeepel2"
    LIBRARY_OUTPUT_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/bin/BattriKeepel2"
    RUNTIME_OUTPUT_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/bin/BattriKeepel2"
)

target_include_directories(BattriKeepel2 PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/src
)

target_compile_definitions(BattriKeepel2 PRIVATE
    BK2_EXPORTS
    ASSERT
)
