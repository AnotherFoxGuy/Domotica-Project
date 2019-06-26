cmake_minimum_required(VERSION 3.14)
project(CliTest VERSION 1.0 LANGUAGES CSharp)

add_executable(CliTest
	main.cs
	../App/Project_Green/ArdunoRest.cs
	packages.config
)

set_property(TARGET CliTest PROPERTY DOTNET_TARGET_FRAMEWORK_VERSION "v4.7.2")

set_property(TARGET CliTest PROPERTY VS_DOTNET_REFERENCES
    "Microsoft.CSharp"
	"${CMAKE_BINARY_DIR}/packages/YamlDotNet.6.1.1/lib/net20/YamlDotNet.dll"
    "Newtonsoft.Json"
    "System"
    "System.Core"
    "System.Data"
    "System.Data.DataSetExtensions"
    "System.Net.Http"
    "System.Runtime.Serialization"
    "System.Xml"
    "System.Xml.Linq"
    "WindowsBase"
)

set(CMAKE_CSharp_FLAGS "/langversion:latest")

configure_file(${CMAKE_SOURCE_DIR}/packages.config
${CMAKE_BINARY_DIR}/packages.config COPYONLY)

find_program(NUGET nuget)
add_custom_target(nuget-restore
  COMMAND ${NUGET} restore ${CMAKE_BINARY_DIR}/${PROJECT_NAME}.sln
)

add_dependencies(${PROJECT_NAME} nuget-restore)