<%@ LANGUAGE=VBScript%>
<%
OPTION EXPLICIT
on error resume next

dim con, rs, server_prov, local_host, sql

'processando local - con = 1
'processando remoto - con = 2

local_host = "192.168.137.128"

if request("con") = 1 then
	'servidor local
	server_prov = local_host
else
	'servidor VPN
	server_prov = "5.0.92.157"
end if

set con = server.createobject("ADODB.Connection")
con.open "Provider=MSDASQL.1;Persist Security Info=False;Data Source=fisconet"

'abrindo o RS
set rs  = server.createobject("ADODB.recordset")
%>