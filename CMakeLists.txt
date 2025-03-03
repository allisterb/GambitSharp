﻿# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required (VERSION 3.8)

# Enable Hot Reload for MSVC compilers if supported.
if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

project ("gambit" VERSION "0.1.0" LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED True)

SET (GAMBIT_DIR ext/gambit)

set (GAMBIT_SRC 
    ${GAMBIT_DIR}/src/core/dvector.cc
    ${GAMBIT_DIR}/src/core/function.cc
    ${GAMBIT_DIR}/src/core/integer.cc
    ${GAMBIT_DIR}/src/core/matrix.cc
    ${GAMBIT_DIR}/src/core/pvector.cc
    ${GAMBIT_DIR}/src/core/rational.cc
    ${GAMBIT_DIR}/src/core/sqmatrix.cc
    ${GAMBIT_DIR}/src/core/tinyxml.cc
    ${GAMBIT_DIR}/src/core/tinyxmlerror.cc
    ${GAMBIT_DIR}/src/core/tinyxmlparser.cc
    ${GAMBIT_DIR}/src/core/vector.cc
    ${GAMBIT_DIR}/src/games/behavmixed.cc
    ${GAMBIT_DIR}/src/games/behavpure.cc
    ${GAMBIT_DIR}/src/games/behavspt.cc
    ${GAMBIT_DIR}/src/games/file.cc
    ${GAMBIT_DIR}/src/games/game.cc
    ${GAMBIT_DIR}/src/games/gameagg.cc
    ${GAMBIT_DIR}/src/games/gamebagg.cc
    ${GAMBIT_DIR}/src/games/gameexpl.cc
    ${GAMBIT_DIR}/src/games/gametable.cc
    ${GAMBIT_DIR}/src/games/gametree.cc
    ${GAMBIT_DIR}/src/games/nash.cc
    ${GAMBIT_DIR}/src/games/stratpure.cc
    ${GAMBIT_DIR}/src/games/stratspt.cc
    ${GAMBIT_DIR}/src/games/writer.cc
    ${GAMBIT_DIR}/src/games/agg/agg.cc
    ${GAMBIT_DIR}/src/games/agg/bagg.cc
    ${GAMBIT_DIR}/src/games/agg/trie_map.cc
    ${GAMBIT_DIR}/src/solvers/enummixed/enummixed.cc
    ${GAMBIT_DIR}/src/solvers/enummixed/clique.cc
    ${GAMBIT_DIR}/src/solvers/enummixed/lrsenum.cc
    
    ${GAMBIT_DIR}/src/solvers/linalg/basis.cc
    ${GAMBIT_DIR}/src/solvers/linalg/btableau.cc
    ${GAMBIT_DIR}/src/solvers/linalg/basis.cc
    ${GAMBIT_DIR}/src/solvers/linalg/lemketab.cc
    ${GAMBIT_DIR}/src/solvers/linalg/lhtab.cc
    ${GAMBIT_DIR}/src/solvers/linalg/lpsolve.cc
    ${GAMBIT_DIR}/src/solvers/linalg/lptab.cc
    ${GAMBIT_DIR}/src/solvers/linalg/ludecomp.cc
    ${GAMBIT_DIR}/src/solvers/linalg/tableau.cc

    ${GAMBIT_DIR}/src/solvers/lrs/lrslib.c
    ${GAMBIT_DIR}/src/solvers/lrs/lrsmp.c
    ${GAMBIT_DIR}/src/solvers/lrs/lrsnashlib.c
)

# this is the "object library" target: compiles the sources only once
add_library(objlib OBJECT ${GAMBIT_SRC})

# shared libraries need PIC
set_property(TARGET objlib PROPERTY POSITION_INDEPENDENT_CODE 1)

target_include_directories (objlib PUBLIC 
    ${GAMBIT_DIR}/src
)

add_library(gambit SHARED $<TARGET_OBJECTS:objlib>)
add_library(gambit_static STATIC $<TARGET_OBJECTS:objlib>)
#add_library(gambit SHARED ${GAMBIT_SRC})


