@echo off
mode con lines=40 cols=80
color 2f

set sc=F:\Projects\AmilyServerBase\APublish
set des=F:\SangoGai\Server\APublish

set dir1=\��񹤾�
set dir2=\Э�鹤��

set file1=SuperSocket.SocketLuanr.dll

xcopy %sc%%dir1%\*.* %des%%dir1%\ /y
xcopy %sc%%dir2%\*.* %des%%dir2%\ /y

copy %sc%\%file1% %des%\%file1% /y

echo �밴������˳���
@Pause>nul