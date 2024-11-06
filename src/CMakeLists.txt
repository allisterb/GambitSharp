# CMakeList.txt : Top-level CMake project file, do global configuration
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

SET (GAMBIT_DIR ../ext/gambit)

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

)
add_library(gambit SHARED ${GAMBIT_SRC})

target_include_directories (gambit PUBLIC 
    ${GAMBIT_DIR}/src
)
