; Main constants - define following constants as you want them displayed in your installation wizard
!define PRODUCT_NAME "DoAnQLPM"
!define PRODUCT_VERSION "1.00"
!define PRODUCT_PUBLISHER "UIT"
!define PRODUCT_WEB_SITE "https://github.com/NamBang/SE104_I23_Nhom_4_CEMEN_QLPMT"

; Following constants you should never change
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"


!include "MUI.nsh"
!define MUI_ABORTWARNING
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"
!define MUI_WELCOMEFINISHPAGE_BITMAP "UIT.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "UIT.bmp"

;header image
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_BITMAP "header.bmp"
; Wizard pages
!insertmacro MUI_PAGE_WELCOME
; Note: you should create License.txt in the same folder as this file, or remove following line.
!insertmacro MUI_PAGE_LICENSE "License.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES

# These indented statements modify settings for MUI_PAGE_FINISH
    !define MUI_FINISHPAGE_NOAUTOCLOSE
    !define MUI_FINISHPAGE_RUN
    !define MUI_FINISHPAGE_RUN_CHECKED
    !define MUI_FINISHPAGE_RUN_TEXT "Launch ${PRODUCT_NAME}"
    !define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"
	
	!define MUI_FINISHPAGE_SHOWREADME
	!define MUI_FINISHPAGE_SHOWREADME_CHECKED
	!define MUI_FINISHPAGE_SHOWREADME_TEXT "Create Desktop Shortcut"
	!define MUI_FINISHPAGE_SHOWREADME_FUNCTION "CreateDesktopShortcut"
	
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"


; Replace the constants bellow to hit suite your project
Name "DoAnQLPM"
OutFile "Setup DoAnQLPM_v${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\DoAnQLPM"
ShowInstDetails show
ShowUnInstDetails show

; Following lists the files you want to include, go through this list carefully!
Section "MainSection" 
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "..\bin\Release\DoAnQLPM.exe"
  File "..\bin\Release\DoAnQLPM.exe.config"
  File "..\bin\Release\EntityFramework.dll"
  File "..\bin\Release\EntityFramework.SqlServer.dll"
  File "..\bin\Release\EntityFramework.SqlServer.xml"
  File "..\bin\Release\EntityFramework.xml"
  File "..\bin\Release\MaterialDesignColors.dll"
  File "..\bin\Release\MaterialDesignColors.pdb"
  File "..\bin\Release\MaterialDesignThemes.Wpf.dll"
  File "..\bin\Release\MaterialDesignThemes.Wpf.pdb"
  File "..\bin\Release\MaterialDesignThemes.Wpf.xml"
  File "..\bin\Release\Microsoft.Expression.Interactions.dll"
  File "..\bin\Release\Microsoft.Expression.Interactions.xml"
  File "..\bin\Release\System.Windows.Interactivity.dll"
  File "..\bin\Release\System.Windows.Interactivity.xml"
  File "..\bin\Release\ScripData.sql"

; Note: my system has a config template, which should manually be edited. This is a nice trick to save your username/password somewhere,
; but you can entirely skip this by deleting the following line. 
;  File /oname=Ultraviewer.exe.config "App.config.template"

; It is pretty clear what following line does: just rename the file name to your project startup executable.
  SetOutPath "$INSTDIR"
  
  ;Create shortcuts
  ;SetOutPath "$SMPROGRAMS"
  CreateDirectory "$SMPROGRAMS\DoAnQLPM"
  CreateShortCut "$SMPROGRAMS\DoAnQLPM\DoAnQLPM.lnk" "$INSTDIR\DoAnQLPM.exe" "" "$INSTDIR\DoAnQLPM.exe"
  CreateShortCut "$SMPROGRAMS\DoAnQLPM\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd


Section -Post
  ;Following lines will make uninstaller work - do not change anything, unless you really want to.
  WriteUninstaller "$INSTDIR\uninst.exe"
  ;WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\UltraViewer" \
  ;               "DisplayName" "UltraView-IT008"
  ;WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\UltraViewer" \
  ;               "UninstallString" "$INSTDIR\uninst.exe"			 		 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "${PRODUCT_NAME}-SE104"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\DoAnQLPM.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  
  ; COOL STUFF: Following line will add a registry setting that will add the INSTDIR into the list of folders from where
  ; the assemblies are listed in the Add Reference in C# or Visual Studio.
  ; This is super-cool if your installation package contains assemblies that someone will use to build more applications - 
  ; and it doesn't hurt even if it is placed there, it will only make the VS a bit slower to find all assemblies when adding references.
  ;WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx\Ultraviewer" "" "$INSTDIR"
SectionEnd

; Replace the following strings to suite your needs
Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "Application was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove DoAnQLPM and all of its components?" IDYES +2
  Abort
FunctionEnd

Function LaunchLink
  ExecShell "" "$INSTDIR\DoAnQLPM.exe"
FunctionEnd

Function CreateDesktopShortcut
  CreateShortCut "$DESKTOP\${PRODUCT_NAME}.lnk" "$INSTDIR\DoAnQLPM.exe" "" "$INSTDIR\DoAnQLPM.exe"
FunctionEnd

; Remove any file that you have added above - removing uninstallation and folders last.
; Note: if there is any file changed or added to these folders, they will not be removed. Also, parent folder (which in my example 
; is company name ZWare) will not be removed if there is any other application installed in it.
Section Uninstall
  Delete "$INSTDIR\DoAnQLPM.exe.exe"
  Delete "$INSTDIR\DoAnQLPM.exe.config"
  Delete "$INSTDIR\EntityFramework.dll"
  Delete "$INSTDIR\EntityFramework.SqlServer.dll"
  Delete "$INSTDIR\EntityFramework.SqlServer.xml"
  Delete "$INSTDIR\EntityFramework.xml"
  Delete "$INSTDIR\MaterialDesignColors.dll "
  Delete "$INSTDIR\MaterialDesignColors.pdb"
  Delete "$INSTDIR\MaterialDesignThemes.Wpf.dll"
  Delete "$INSTDIR\MaterialDesignThemes.Wpf.pdb"
  Delete "$INSTDIR\MaterialDesignThemes.Wpf.xml"
  Delete "$INSTDIR\Microsoft.Expression.Interactions.dll"
  Delete "$INSTDIR\Microsoft.Expression.Interactions.xml"
  Delete "$INSTDIR\System.Windows.Interactivity.dll"
  Delete "$INSTDIR\System.Windows.Interactivity.xml"
  Delete "$INSTDIR\ScripData.sql"
  
  Delete "$DESKTOP\${PRODUCT_NAME}.lnk"
  
  ;SetOutPath $TEMP
  
  RMDir /r "$INSTDIR"
  ;Delete shortcuts
  Delete "$SMPROGRAMS\DoAnQLPM\DoAnQLPM.lnk"
  Delete "$SMPROGRAMS\DoAnQLPM\Uninstall.lnk"
  RMDir "$SMPROGRAMS\DoAnQLPM"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DoAnQLPM"
  SetAutoClose true
SectionEnd