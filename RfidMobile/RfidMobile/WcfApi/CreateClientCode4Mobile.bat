cd %~dp0
rem generate client proxy classes with the wsdl
"C:\Program Files (x86)\Microsoft.NET\SDK\CompactFramework\v3.5\bin\NetCFSvcUtil.exe" ^
http://localhost:8106/RFID.svc
pause