; =============================================================================
;  HORIZON HOTEL & RESORT - Instalador y reinstalador (Inno Setup 6)
; -----------------------------------------------------------------------------
;  Una sola pantalla para elegir:
;    INSTALAR    Instalación desde cero de la versión 1 (primer cuatrimestre):
;                sistema base - usuarios, perfiles, bitácora de eventos,
;                backup/restore, dígitos verificadores e idioma.
;                Base de datos: Database\Deploy_TP_Ing_Soft_v1.sql
;    REINSTALAR  Actualiza una instalación existente a la versión 2 (negocio:
;                reservas, habitaciones, huéspedes, estadías, servicio a la
;                habitación, cargos, reportes, bitácora de cambios). Usa la misma
;                carpeta y la misma instancia de SQL Server, ofrece un backup
;                automático antes de tocar la base y NO pierde datos.
;                Base de datos: Database\Update_TP_Ing_Soft_v2.sql
;
;  Antes de compilar:
;    1. Compilar la solución en Release (GUI\bin\Release queda con la versión 2).
;    2. Que exista GUI\Config\Correo.config (no está en el repositorio; modelo en
;       Correo.config.template) si se quiere que el sistema instalado envíe correos.
;    3. Abrir este archivo con Inno Setup 6 y Compilar (o ejecutar Compilar.bat).
;  Resultado: Instalador\Salida\HorizonHotel_Setup.exe
;
;  La desinstalación NUNCA borra la base de datos.
; =============================================================================

#define AppName      "Horizon Hotel & Resort"
#define AppVersion   "2.0.0"
#define VersionV1    "1.0.0"
#define AppPublisher "TP Ingeniería de Software - Sanchez y Yapura"
#define AppExe       "GUI_08YS.exe"
#define AppGuid      "4B1400F0-180C-4682-8358-70532813D1FE"
#define RegKey       "Software\TPIngSoftSanchezYapura\HorizonHotelResort"
#define RegKeyEmpresa "Software\TPIngSoftSanchezYapura"
#define DbName       "TP_Ing_Soft"
#define V2Bin        "..\GUI\bin\Release"
; Instalación anterior hecha con el MSI de la versión 1 (primer cuatrimestre)
#define MsiV1Code    "{290683B0-A545-462C-B707-AE3BE5EB4043}"

[Setup]
AppId={{{#AppGuid}}
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName}
AppPublisher={#AppPublisher}
VersionInfoVersion={#AppVersion}.0
VersionInfoDescription=Instalador de {#AppName}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
DisableWelcomePage=no
DisableDirPage=no
DisableReadyPage=no
UsePreviousAppDir=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
WizardStyle=modern
WizardImageFile=Imagenes\WizardImage_164x314.bmp,Imagenes\WizardImage_192x386.bmp,Imagenes\WizardImage_246x459.bmp,Imagenes\WizardImage_273x556.bmp,Imagenes\WizardImage_328x604.bmp,Imagenes\WizardImage_355x700.bmp,Imagenes\WizardImage_410x797.bmp
WizardSmallImageFile=Imagenes\WizardSmallImage_55x55.bmp,Imagenes\WizardSmallImage_64x68.bmp,Imagenes\WizardSmallImage_83x80.bmp,Imagenes\WizardSmallImage_92x97.bmp,Imagenes\WizardSmallImage_110x106.bmp,Imagenes\WizardSmallImage_119x123.bmp,Imagenes\WizardSmallImage_138x140.bmp
SetupIconFile=..\GUI\AppIcon.ico
UninstallDisplayIcon={app}\{#AppExe}
UninstallDisplayName={#AppName}
InfoBeforeFile=Requisitos.txt
OutputDir=Salida
OutputBaseFilename=HorizonHotel_Setup
Compression=lzma2/ultra64
SolidCompression=yes
CloseApplications=yes
RestartApplications=no
ChangesAssociations=no
SetupLogging=yes

[Languages]
Name: "es"; MessagesFile: "compiler:Languages\Spanish.isl"

[Messages]
WelcomeLabel1=Bienvenido a {#AppName}
WelcomeLabel2=Este asistente instala o actualiza el Sistema de Gestión Hotelera Horizon.%n%nEn el paso siguiente podrá elegir:%n%n•  INSTALAR: instalación desde cero del sistema base.%n•  REINSTALAR: actualiza una instalación existente a la versión actual sin perder datos.%n%nAntes de continuar cierre el sistema si está abierto.
FinishedHeadingLabel=Listo
ClickFinish=Haga clic en Finalizar para salir.

[Tasks]
Name: "escritorio"; Description: "Crear un acceso directo en el escritorio"; GroupDescription: "Accesos directos:"

[InstallDelete]
; El script de la versión 1 que usaba el .exe viejo ya no hace falta en la versión 2
Type: files; Name: "{app}\script.sql"; Check: EsReinstalar

[Files]
; ---- Versión 1 (INSTALAR) -------------------------------------------------
Source: "Version1\*"; DestDir: "{app}"; Check: EsInstalar; Flags: ignoreversion
; El .exe de la versión 1 busca script.sql a su lado si alguna vez falta la base
Source: "Database\Deploy_TP_Ing_Soft_v1.sql"; DestDir: "{app}"; DestName: "script.sql"; Check: EsInstalar; Flags: ignoreversion
; ---- Versión 2 (REINSTALAR) -----------------------------------------------
Source: "{#V2Bin}\*"; DestDir: "{app}"; Excludes: "*.pdb,*.xml,*.log,*.bak,script.sql,Correo.config,*.vshost.*,\Database\*"; Check: EsReinstalar; Flags: ignoreversion recursesubdirs createallsubdirs
; Datos de la casilla de correo: se copian solo si no hay uno (se respeta el del cliente)
Source: "{#V2Bin}\Correo.config"; DestDir: "{app}"; Check: EsReinstalar; Flags: onlyifdoesntexist skipifsourcedoesntexist
; ---- Scripts de base de datos (se ejecutan en PrepareToInstall) -----------
Source: "Database\Deploy_TP_Ing_Soft_v1.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "Database\Update_TP_Ing_Soft_v2.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
; ---- Manual -----------------------------------------------------------------
Source: "Manual\Manual_de_instalacion.pdf"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

[Icons]
Name: "{group}\{#AppName}"; Filename: "{app}\{#AppExe}"; WorkingDir: "{app}"
Name: "{group}\Manual de instalación"; Filename: "{app}\Manual_de_instalacion.pdf"; Check: ExisteManual
Name: "{group}\Desinstalar {#AppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExe}"; WorkingDir: "{app}"; Tasks: escritorio

[Registry]
Root: HKLM; Subkey: "{#RegKeyEmpresa}"; Flags: uninsdeletekeyifempty
Root: HKLM; Subkey: "{#RegKey}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "{#RegKey}"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"
Root: HKLM; Subkey: "{#RegKey}"; ValueType: string; ValueName: "Version"; ValueData: "{code:VersionInstalada}"
Root: HKLM; Subkey: "{#RegKey}"; ValueType: string; ValueName: "SqlInstance"; ValueData: "{code:InstanciaElegida}"
Root: HKLM; Subkey: "{#RegKey}"; ValueType: string; ValueName: "Database"; ValueData: "{#DbName}"

[Run]
Filename: "{app}\{#AppExe}"; Description: "Abrir {#AppName}"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\instalador_db.log"
Type: dirifempty; Name: "{app}\Database"
Type: dirifempty; Name: "{app}"

[Code]
// -----------------------------------------------------------------------------
//  Colores de la marca (TColor = $00BBGGRR)
// -----------------------------------------------------------------------------
const
  COLOR_AZUL   = $280A05;   // RGB(5, 10, 40)   - azul noche de las pantallas del sistema
  COLOR_DORADO = $20A5DA;   // RGB(218, 165, 32) - Goldenrod
  COLOR_CLARO  = $F0E8E0;   // texto claro sobre azul
  MSI_V1_KEY   = 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{#MsiV1Code}';
  INNO_KEY     = 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{{#AppGuid}}_is1';

var
  // Instalación previa
  HayPrevia, PreviaEsMsi: Boolean;
  PreviaRuta, PreviaVersion, PreviaInstancia: String;

  // Página "Tipo de instalación"
  PaginaModo: TWizardPage;
  rbInstalar, rbReinstalar: TNewRadioButton;
  lblInstalar, lblReinstalar, lblPrevia: TNewStaticText;

  // Página "Servidor de base de datos"
  PaginaInstancia: TWizardPage;
  cbInstancia: TNewComboBox;
  btnProbar: TNewButton;
  lblEstado: TNewStaticText;
  InstanciaProbada, EstadoBase, VersionServidor: String;

  // Página "Copia de seguridad"
  PaginaBackup: TWizardPage;
  chkBackup, chkConfirmo: TNewCheckBox;
  lblAviso: TNewStaticText;

  // Resultado
  RutaSqlcmd, CarpetaLogs, ArchivoBackup, LogBase: String;
  ProgresoBD: TOutputProgressWizardPage;

// -----------------------------------------------------------------------------
//  Utilidades
// -----------------------------------------------------------------------------
function EsReinstalar(): Boolean;
begin
  Result := (rbReinstalar <> nil) and rbReinstalar.Checked;
end;

function EsInstalar(): Boolean;
begin
  Result := not EsReinstalar();
end;

function VersionInstalada(Param: String): String;
begin
  if EsReinstalar() then Result := '{#AppVersion}' else Result := '{#VersionV1}';
end;

function InstanciaElegida(Param: String): String;
begin
  Result := Trim(cbInstancia.Text);
end;

function ExisteManual(): Boolean;
begin
  Result := FileExists(ExpandConstant('{app}\Manual_de_instalacion.pdf'));
end;

function LeerArchivo(const Archivo: String): String;
var
  S: AnsiString;
begin
  if LoadStringFromFile(Archivo, S) then Result := String(S) else Result := '';
end;

// Devuelve lo que sigue al marcador hasta el fin de la línea ('' si no está)
function ValorMarcador(const Texto, Marcador: String): String;
var
  P, Q: Integer;
  Resto: String;
begin
  Result := '';
  P := Pos(Marcador, Texto);
  if P = 0 then Exit;
  Resto := Copy(Texto, P + Length(Marcador), Length(Texto));
  Q := Pos(#13, Resto);
  if Q = 0 then Q := Pos(#10, Resto);
  if Q > 0 then Resto := Copy(Resto, 1, Q - 1);
  Result := Trim(Resto);
end;

function FinDelLog(const Texto: String): String;
begin
  if Length(Texto) > 1500 then
    Result := '...' + Copy(Texto, Length(Texto) - 1500, 1501)
  else
    Result := Texto;
end;

function BuscarSqlcmd(): String;
var
  Candidatos: array of String;
  i: Integer;
begin
  Result := FileSearch('sqlcmd.exe', GetEnv('PATH'));
  if Result <> '' then Exit;
  SetArrayLength(Candidatos, 10);
  Candidatos[0] := ExpandConstant('{commonpf64}\Microsoft SQL Server\Client SDK\ODBC\180\Tools\Binn\SQLCMD.EXE');
  Candidatos[1] := ExpandConstant('{commonpf64}\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE');
  Candidatos[2] := ExpandConstant('{commonpf64}\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\SQLCMD.EXE');
  Candidatos[3] := ExpandConstant('{commonpf32}\Microsoft SQL Server\Client SDK\ODBC\180\Tools\Binn\SQLCMD.EXE');
  Candidatos[4] := ExpandConstant('{commonpf32}\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE');
  Candidatos[5] := ExpandConstant('{commonpf64}\Microsoft SQL Server\160\Tools\Binn\SQLCMD.EXE');
  Candidatos[6] := ExpandConstant('{commonpf64}\Microsoft SQL Server\150\Tools\Binn\SQLCMD.EXE');
  Candidatos[7] := ExpandConstant('{commonpf64}\Microsoft SQL Server\140\Tools\Binn\SQLCMD.EXE');
  Candidatos[8] := ExpandConstant('{commonpf64}\Microsoft SQL Server\130\Tools\Binn\SQLCMD.EXE');
  Candidatos[9] := ExpandConstant('{commonpf64}\SqlCmd\sqlcmd.exe');
  for i := 0 to GetArrayLength(Candidatos) - 1 do
    if FileExists(Candidatos[i]) then
    begin
      Result := Candidatos[i];
      Exit;
    end;
end;

// Ejecuta sqlcmd con autenticación de Windows. Entrada UTF-8 (los scripts tienen
// acentos) y salida en ANSI para poder mostrarla. Toda la salida va a Log.
// Con LocalDB se ejecuta con el usuario que abrió el instalador SIN elevar: si la instancia
// la arranca un proceso elevado (el instalador corre como administrador), después ni el
// sistema ni SSMS (que corren sin elevar) se pueden conectar ("SQL Server process failed
// to start", error 50). Con una instancia de servicio (SQLEXPRESS, etc.) se ejecuta elevado,
// porque el permiso sysadmin suele venir por el grupo Administradores.
function EjecutarSqlcmd(const Instancia, Script, ArchivoLog: String; var Codigo: Integer): Boolean;
var
  Params, Salida: String;
begin
  Log('sqlcmd ' + Script + ' en ' + Instancia);
  if Pos('(localdb)', Lowercase(Trim(Instancia))) = 1 then
  begin
    // La salida va primero a {tmp} (el usuario sin elevar puede escribir ahí) y después se copia al log
    Salida := ExpandConstant('{tmp}\sqlcmd_salida.log');
    DeleteFile(Salida);
    Params := '/C ""' + RutaSqlcmd + '" -S "' + Instancia + '" -E -C -I -b -l 30 -f i:65001,o:1252 -i "' +
              Script + '" > "' + Salida + '" 2>&1"';
    Result := ExecAsOriginalUser(ExpandConstant('{cmd}'), Params, '', SW_HIDE, ewWaitUntilTerminated, Codigo) and (Codigo = 0);
    if FileExists(Salida) then
      FileCopy(Salida, ArchivoLog, False);
  end
  else
  begin
    Params := '/C ""' + RutaSqlcmd + '" -S "' + Instancia + '" -E -C -I -b -l 30 -f i:65001,o:1252 -i "' +
              Script + '" > "' + ArchivoLog + '" 2>&1"';
    Result := Exec(ExpandConstant('{cmd}'), Params, '', SW_HIDE, ewWaitUntilTerminated, Codigo) and (Codigo = 0);
  end;
end;

// -----------------------------------------------------------------------------
//  Instalación anterior: registro propio o MSI de la versión 1
// -----------------------------------------------------------------------------
procedure DetectarInstalacionPrevia();
begin
  HayPrevia := False;
  PreviaEsMsi := False;
  if RegQueryStringValue(HKLM, '{#RegKey}', 'InstallPath', PreviaRuta) and DirExists(PreviaRuta) then
  begin
    HayPrevia := True;
    if not RegQueryStringValue(HKLM, '{#RegKey}', 'Version', PreviaVersion) then PreviaVersion := '?';
    if not RegQueryStringValue(HKLM, '{#RegKey}', 'SqlInstance', PreviaInstancia) then PreviaInstancia := '';
  end
  else if RegKeyExists(HKLM32, MSI_V1_KEY) or RegKeyExists(HKLM64, MSI_V1_KEY) then
  begin
    // Versión 1 instalada con el MSI del primer cuatrimestre: usaba LocalDB
    HayPrevia := True;
    PreviaEsMsi := True;
    PreviaVersion := '{#VersionV1} (instalador MSI)';
    PreviaInstancia := '(localdb)\MSSQLLocalDB';
    PreviaRuta := ExpandConstant('{autopf}\{#AppName}');
  end;
end;

// -----------------------------------------------------------------------------
//  Instancias de SQL Server instaladas en el equipo
// -----------------------------------------------------------------------------
procedure AgregarInstancia(const Nombre: String);
begin
  if (Nombre <> '') and (cbInstancia.Items.IndexOf(Nombre) < 0) then
    cbInstancia.Items.Add(Nombre);
end;

procedure AgregarInstanciasDe(Raiz: Integer);
var
  Nombres: TArrayOfString;
  i: Integer;
begin
  if RegGetValueNames(Raiz, 'SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL', Nombres) then
    for i := 0 to GetArrayLength(Nombres) - 1 do
      if CompareText(Nombres[i], 'MSSQLSERVER') = 0 then
        AgregarInstancia('.')
      else
        AgregarInstancia('.\' + Nombres[i]);
end;

procedure CargarInstancias();
begin
  if PreviaInstancia <> '' then AgregarInstancia(PreviaInstancia);
  AgregarInstanciasDe(HKLM64);
  AgregarInstanciasDe(HKLM32);
  if RegKeyExists(HKLM64, 'SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions') or
     RegKeyExists(HKLM32, 'SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions') then
    AgregarInstancia('(localdb)\MSSQLLocalDB');
  if cbInstancia.Items.Count > 0 then
    cbInstancia.ItemIndex := 0
  else
    cbInstancia.Text := '.\SQLEXPRESS';
end;

// -----------------------------------------------------------------------------
//  Prueba de conexión: versión del servidor y estado de la base
//  Estado: SIN_BASE | VACIA | V1 (sistema) | V2 (sistema + negocio)
// -----------------------------------------------------------------------------
function ProbarConexion(const Instancia: String; Silencioso: Boolean): Boolean;
var
  Script, ArchivoLog, Salida, Datos: String;
  Codigo, P, Mayor: Integer;
begin
  Result := False;
  EstadoBase := '';
  VersionServidor := '';
  Script := ExpandConstant('{tmp}\probar_conexion.sql');
  ArchivoLog := CarpetaLogs + '\probar_conexion.log';
  SaveStringToFile(Script,
    'SET NOCOUNT ON;' + #13#10 +
    'DECLARE @estado NVARCHAR(20) = CASE' + #13#10 +
    '  WHEN DB_ID(N''{#DbName}'') IS NULL THEN N''SIN_BASE''' + #13#10 +
    '  WHEN OBJECT_ID(N''{#DbName}.dbo.Reservas'', N''U'') IS NOT NULL THEN N''V2''' + #13#10 +
    '  WHEN OBJECT_ID(N''{#DbName}.dbo.Users'', N''U'') IS NOT NULL THEN N''V1''' + #13#10 +
    '  ELSE N''VACIA'' END;' + #13#10 +
    'PRINT N''CONEXION_OK;'' + CAST(SERVERPROPERTY(''ProductVersion'') AS NVARCHAR(50)) + N'';'' + @estado;' + #13#10,
    False);
  lblEstado.Font.Color := clWindowText;
  lblEstado.Caption := 'Probando la conexión con ' + Instancia + '...';
  WizardForm.Refresh();

  if EjecutarSqlcmd(Instancia, Script, ArchivoLog, Codigo) then
  begin
    Salida := LeerArchivo(ArchivoLog);
    Datos := ValorMarcador(Salida, 'CONEXION_OK;');
    P := Pos(';', Datos);
    if P > 0 then
    begin
      VersionServidor := Copy(Datos, 1, P - 1);
      EstadoBase := Copy(Datos, P + 1, Length(Datos));
      Mayor := StrToIntDef(Copy(VersionServidor, 1, Pos('.', VersionServidor) - 1), 0);
      if Mayor < 13 then
      begin
        lblEstado.Font.Color := clRed;
        lblEstado.Caption := 'SQL Server ' + VersionServidor + ' es muy antiguo: se necesita SQL Server 2016 SP1 o superior.';
        Exit;
      end;
      InstanciaProbada := Instancia;
      Result := True;
      lblEstado.Font.Color := $006400;
      if EstadoBase = 'SIN_BASE' then
        lblEstado.Caption := 'Conexión correcta (SQL Server ' + VersionServidor + '). La base {#DbName} no existe: se va a crear.'
      else if EstadoBase = 'V1' then
        lblEstado.Caption := 'Conexión correcta (SQL Server ' + VersionServidor + '). La base {#DbName} existe con el sistema versión 1.'
      else if EstadoBase = 'V2' then
        lblEstado.Caption := 'Conexión correcta (SQL Server ' + VersionServidor + '). La base {#DbName} existe con la versión 2.'
      else
        lblEstado.Caption := 'Conexión correcta (SQL Server ' + VersionServidor + '). La base {#DbName} existe pero está vacía.';
      Exit;
    end;
  end;

  lblEstado.Font.Color := clRed;
  lblEstado.Caption := 'No se pudo conectar con ' + Instancia + '. Verifique el nombre de la instancia y que el servicio de SQL Server esté iniciado.';
  if not Silencioso then
    MsgBox('No se pudo conectar con "' + Instancia + '".' + #13#10#13#10 + FinDelLog(LeerArchivo(ArchivoLog)), mbError, MB_OK);
end;

procedure BtnProbarClick(Sender: TObject);
begin
  ProbarConexion(Trim(cbInstancia.Text), False);
end;

procedure CbInstanciaChange(Sender: TObject);
begin
  InstanciaProbada := '';
  lblEstado.Font.Color := clGrayText;
  lblEstado.Caption := 'Pruebe la conexión antes de continuar.';
end;

// -----------------------------------------------------------------------------
//  Estética: encabezado y páginas de bienvenida/fin en azul y dorado
// -----------------------------------------------------------------------------
procedure AplicarEstetica();
begin
  WizardForm.MainPanel.Color := COLOR_AZUL;
  WizardForm.PageNameLabel.Font.Color := COLOR_DORADO;
  WizardForm.PageDescriptionLabel.Font.Color := COLOR_CLARO;

  WizardForm.WelcomePage.Color := COLOR_AZUL;
  WizardForm.WelcomeLabel1.Font.Color := COLOR_DORADO;
  WizardForm.WelcomeLabel2.Font.Color := COLOR_CLARO;

  WizardForm.FinishedPage.Color := COLOR_AZUL;
  WizardForm.FinishedHeadingLabel.Font.Color := COLOR_DORADO;
  WizardForm.FinishedLabel.Font.Color := COLOR_CLARO;
  WizardForm.RunList.Color := COLOR_AZUL;
  WizardForm.RunList.Font.Color := COLOR_CLARO;
end;

// -----------------------------------------------------------------------------
//  Páginas propias
// -----------------------------------------------------------------------------
function NuevoTexto(Pagina: TWizardPage; Top, Alto: Integer; const Texto: String): TNewStaticText;
begin
  Result := TNewStaticText.Create(Pagina);
  Result.Parent := Pagina.Surface;
  Result.AutoSize := False;
  Result.WordWrap := True;
  Result.Left := ScaleX(20);
  Result.Top := Top;
  Result.Width := Pagina.SurfaceWidth - ScaleX(20);
  Result.Height := Alto;
  Result.Caption := Texto;
end;

procedure CrearPaginaModo();
begin
  PaginaModo := CreateCustomPage(wpWelcome, 'Tipo de instalación',
    '¿Qué desea hacer?');

  rbInstalar := TNewRadioButton.Create(PaginaModo);
  rbInstalar.Parent := PaginaModo.Surface;
  rbInstalar.Left := 0;
  rbInstalar.Top := 0;
  rbInstalar.Width := PaginaModo.SurfaceWidth;
  rbInstalar.Height := ScaleY(20);
  rbInstalar.Caption := 'Instalar (instalación desde cero)';
  rbInstalar.Font.Style := [fsBold];

  lblInstalar := NuevoTexto(PaginaModo, ScaleY(22), ScaleY(48),
    'Instala el sistema base (versión {#VersionV1}): usuarios, perfiles (roles, familias y permisos), ' +
    'bitácora de eventos, backup y restore, dígitos verificadores e idioma. Crea la base de datos ' +
    '{#DbName} si no existe; si ya existe, no la modifica.');

  rbReinstalar := TNewRadioButton.Create(PaginaModo);
  rbReinstalar.Parent := PaginaModo.Surface;
  rbReinstalar.Left := 0;
  rbReinstalar.Top := ScaleY(82);
  rbReinstalar.Width := PaginaModo.SurfaceWidth;
  rbReinstalar.Height := ScaleY(20);
  rbReinstalar.Caption := 'Reinstalar (actualizar a la versión {#AppVersion})';
  rbReinstalar.Font.Style := [fsBold];

  lblReinstalar := NuevoTexto(PaginaModo, ScaleY(104), ScaleY(62),
    'Actualiza la instalación existente en la misma carpeta y la misma base de datos: agrega reservas, ' +
    'habitaciones, huéspedes, estadías, servicio a la habitación, cargos adicionales, reportes y bitácora ' +
    'de cambios. Conserva usuarios, contraseñas, roles y todos los datos. Antes de actualizar ofrece ' +
    'hacer una copia de seguridad de la base.');

  lblPrevia := NuevoTexto(PaginaModo, ScaleY(176), ScaleY(60), '');
  lblPrevia.Left := 0;
  lblPrevia.Width := PaginaModo.SurfaceWidth;

  if HayPrevia then
  begin
    lblPrevia.Font.Color := clNavy;
    lblPrevia.Caption := 'Instalación encontrada en este equipo:' + #13#10 +
      '   Carpeta: ' + PreviaRuta + #13#10 +
      '   Versión: ' + PreviaVersion + '      Instancia SQL: ' + PreviaInstancia;
    rbReinstalar.Checked := True;
  end
  else
  begin
    lblPrevia.Font.Color := clGrayText;
    lblPrevia.Caption := 'No se encontró una instalación anterior en este equipo: solo se puede Instalar.';
    rbReinstalar.Enabled := False;
    lblReinstalar.Enabled := False;
    rbInstalar.Checked := True;
  end;
end;

procedure CrearPaginaInstancia();
var
  lbl: TNewStaticText;
begin
  PaginaInstancia := CreateCustomPage(wpSelectDir, 'Servidor de base de datos',
    'Instancia de SQL Server donde está o se creará la base {#DbName}.');

  lbl := NuevoTexto(PaginaInstancia, 0, ScaleY(44),
    'Elija la instancia de la lista o escríbala (por ejemplo  .\SQLEXPRESS  o  (localdb)\MSSQLLocalDB). ' +
    'Se usa la autenticación de Windows del usuario actual.');
  lbl.Left := 0;
  lbl.Width := PaginaInstancia.SurfaceWidth;

  cbInstancia := TNewComboBox.Create(PaginaInstancia);
  cbInstancia.Parent := PaginaInstancia.Surface;
  cbInstancia.Style := csDropDown;
  cbInstancia.Left := 0;
  cbInstancia.Top := ScaleY(50);
  cbInstancia.Width := PaginaInstancia.SurfaceWidth - ScaleX(130);
  cbInstancia.OnChange := @CbInstanciaChange;

  btnProbar := TNewButton.Create(PaginaInstancia);
  btnProbar.Parent := PaginaInstancia.Surface;
  btnProbar.Left := PaginaInstancia.SurfaceWidth - ScaleX(120);
  btnProbar.Top := ScaleY(49);
  btnProbar.Width := ScaleX(120);
  btnProbar.Height := ScaleY(25);
  btnProbar.Caption := 'Probar conexión';
  btnProbar.OnClick := @BtnProbarClick;

  lblEstado := NuevoTexto(PaginaInstancia, ScaleY(86), ScaleY(48), 'Pruebe la conexión antes de continuar.');
  lblEstado.Left := 0;
  lblEstado.Width := PaginaInstancia.SurfaceWidth;
  lblEstado.Font.Color := clGrayText;

  lbl := NuevoTexto(PaginaInstancia, ScaleY(150), ScaleY(60),
    'Con LocalDB la base pertenece al usuario de Windows: instale y use el sistema con el mismo usuario. ' +
    'Al desinstalar, la base de datos NO se elimina.');
  lbl.Left := 0;
  lbl.Width := PaginaInstancia.SurfaceWidth;
  lbl.Font.Color := clGrayText;

  CargarInstancias();
end;

procedure CrearPaginaBackup();
var
  lblNota: TNewStaticText;
begin
  PaginaBackup := CreateCustomPage(PaginaInstancia.ID, 'Copia de seguridad',
    'Antes de reinstalar, resguarde la base de datos.');

  lblAviso := NuevoTexto(PaginaBackup, 0, ScaleY(70),
    'IMPORTANTE: la reinstalación modifica la base {#DbName} (agrega tablas, procedimientos, permisos y ' +
    'catálogos). Está preparada para no perder datos y, si algo falla, deshace todos los cambios; aun así ' +
    'se recomienda tener una copia de seguridad reciente antes de continuar.');
  lblAviso.Left := 0;
  lblAviso.Width := PaginaBackup.SurfaceWidth;
  lblAviso.Font.Style := [fsBold];

  chkBackup := TNewCheckBox.Create(PaginaBackup);
  chkBackup.Parent := PaginaBackup.Surface;
  chkBackup.Left := 0;
  chkBackup.Top := ScaleY(80);
  chkBackup.Width := PaginaBackup.SurfaceWidth;
  chkBackup.Height := ScaleY(20);
  chkBackup.Caption := 'Hacer ahora un backup completo de la base (recomendado)';
  chkBackup.Checked := True;

  lblNota := NuevoTexto(PaginaBackup, ScaleY(102), ScaleY(44),
    'Se guarda en la carpeta de backups de la instancia de SQL Server con el nombre ' +
    '{#DbName}_antes_v{#AppVersion}_<fecha>.bak. La ruta exacta se muestra al terminar.');
  lblNota.Font.Color := clGrayText;

  chkConfirmo := TNewCheckBox.Create(PaginaBackup);
  chkConfirmo.Parent := PaginaBackup.Surface;
  chkConfirmo.Left := 0;
  chkConfirmo.Top := ScaleY(156);
  chkConfirmo.Width := PaginaBackup.SurfaceWidth;
  chkConfirmo.Height := ScaleY(20);
  chkConfirmo.Caption := 'Ya tengo un backup reciente (desde el sistema: Respaldos > Backup)';
end;

// -----------------------------------------------------------------------------
//  Eventos del asistente
// -----------------------------------------------------------------------------
function InitializeSetup(): Boolean;
begin
  Result := False;
  if not IsDotNetInstalled(net48, 0) then
  begin
    MsgBox('{#AppName} necesita Microsoft .NET Framework 4.8.' + #13#10 +
           'Instálelo desde el sitio de Microsoft y vuelva a ejecutar este instalador.', mbCriticalError, MB_OK);
    Exit;
  end;
  RutaSqlcmd := BuscarSqlcmd();
  if RutaSqlcmd = '' then
  begin
    MsgBox('No se encontró la herramienta sqlcmd de SQL Server.' + #13#10#13#10 +
           'Instale SQL Server (Express o LocalDB) o las "Herramientas de línea de comandos de SQL Server" ' +
           'y vuelva a ejecutar este instalador.', mbCriticalError, MB_OK);
    Exit;
  end;
  CarpetaLogs := ExpandConstant('{commonappdata}\{#AppName}\Logs');
  ForceDirectories(CarpetaLogs);
  DetectarInstalacionPrevia();
  Result := True;
end;

procedure InitializeWizard();
begin
  AplicarEstetica();
  CrearPaginaModo();
  CrearPaginaInstancia();
  CrearPaginaBackup();
  ProgresoBD := CreateOutputProgressPage('Base de datos', 'Preparando la base de datos {#DbName}...');
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := False;
  // Reinstalar usa la carpeta de la instalación existente
  if (PageID = wpSelectDir) and EsReinstalar() then Result := True;
  if (PageID = PaginaBackup.ID) and EsInstalar() then Result := True;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
var
  Instancia: String;
begin
  Result := True;

  if CurPageID = PaginaModo.ID then
  begin
    if EsReinstalar() then
    begin
      WizardForm.DirEdit.Text := PreviaRuta;
      if PreviaInstancia <> '' then cbInstancia.Text := PreviaInstancia;
    end
    else if HayPrevia then
      Result := MsgBox('Ya hay una instalación en este equipo (versión ' + PreviaVersion + ').' + #13#10#13#10 +
                       'Instalar coloca la versión {#VersionV1} del sistema. La base de datos existente NO se ' +
                       'borra ni se modifica. Para pasar a la versión actual conservando los datos use Reinstalar.' + #13#10#13#10 +
                       '¿Continuar con Instalar?', mbConfirmation, MB_YESNO) = IDYES;
  end

  else if CurPageID = PaginaInstancia.ID then
  begin
    Instancia := Trim(cbInstancia.Text);
    if Instancia = '' then
    begin
      MsgBox('Indique la instancia de SQL Server.', mbError, MB_OK);
      Result := False;
      Exit;
    end;
    if InstanciaProbada <> Instancia then
      if not ProbarConexion(Instancia, False) then
      begin
        Result := False;
        Exit;
      end;

    if EsReinstalar() then
    begin
      if (EstadoBase = 'SIN_BASE') or (EstadoBase = 'VACIA') then
      begin
        MsgBox('En la instancia "' + Instancia + '" no está instalado el sistema, no hay nada que actualizar.' + #13#10 +
               'Verifique la instancia o vuelva atrás y elija Instalar.', mbError, MB_OK);
        Result := False;
      end;
    end
    else
    begin
      if EstadoBase = 'V2' then
        Result := MsgBox('La base {#DbName} de esta instancia ya tiene la versión {#AppVersion}.' + #13#10 +
                         'Instalar NO la modifica y coloca la aplicación versión {#VersionV1}, que no tiene las ' +
                         'pantallas de negocio. Lo habitual en este caso es Reinstalar.' + #13#10#13#10 +
                         '¿Continuar igual?', mbConfirmation, MB_YESNO) = IDYES
      else if EstadoBase = 'V1' then
        MsgBox('La base {#DbName} ya existe en esta instancia: se conserva tal como está (no se pisan datos).',
               mbInformation, MB_OK);
    end;
  end

  else if CurPageID = PaginaBackup.ID then
  begin
    if (not chkBackup.Checked) and (not chkConfirmo.Checked) then
    begin
      MsgBox('Antes de reinstalar haga una copia de seguridad: marque "Hacer ahora un backup" o confirme ' +
             'que ya tiene uno reciente.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo,
  MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
begin
  if EsReinstalar() then
  begin
    Result := 'Operación:' + NewLine + Space + 'REINSTALAR - actualizar a la versión {#AppVersion}' + NewLine + NewLine +
              'Carpeta (instalación existente):' + NewLine + Space + WizardDirValue() + NewLine + NewLine +
              'Base de datos:' + NewLine + Space + '{#DbName} en ' + Trim(cbInstancia.Text) + NewLine +
              Space + 'Se ejecuta Update_TP_Ing_Soft_v2.sql (una sola transacción).' + NewLine + NewLine +
              'Copia de seguridad:' + NewLine;
    if chkBackup.Checked then
      Result := Result + Space + 'Se hace un backup completo antes de actualizar.'
    else
      Result := Result + Space + 'No (el usuario confirmó que tiene un backup reciente).';
  end
  else
    Result := 'Operación:' + NewLine + Space + 'INSTALAR - sistema base, versión {#VersionV1}' + NewLine + NewLine +
              MemoDirInfo + NewLine + NewLine +
              'Base de datos:' + NewLine + Space + '{#DbName} en ' + Trim(cbInstancia.Text) + NewLine +
              Space + 'Se ejecuta Deploy_TP_Ing_Soft_v1.sql (si la base ya existe, no se modifica).';
  if MemoTasksInfo <> '' then
    Result := Result + NewLine + NewLine + MemoTasksInfo;
end;

// -----------------------------------------------------------------------------
//  Base de datos: backup y script. Se hace ANTES de copiar archivos, así un
//  error deja todo como estaba (los scripts son transaccionales).
// -----------------------------------------------------------------------------
function HacerBackup(const Instancia: String): String;
var
  Script, ArchivoLog, Nombre: String;
  Codigo: Integer;
begin
  Result := '';
  Nombre := '{#DbName}_antes_v{#AppVersion}_' + GetDateTimeString('yyyymmdd_hhnnss', #0, #0) + '.bak';
  Script := ExpandConstant('{tmp}\backup.sql');
  ArchivoLog := CarpetaLogs + '\backup.log';
  SaveStringToFile(Script,
    'SET NOCOUNT ON;' + #13#10 +
    'BACKUP DATABASE [{#DbName}] TO DISK = N''' + Nombre + ''' WITH COPY_ONLY, INIT, CHECKSUM, ' +
    'NAME = N''Horizon Hotel - antes de reinstalar'';' + #13#10 +
    'DECLARE @ruta NVARCHAR(260);' + #13#10 +
    'SELECT TOP (1) @ruta = f.physical_device_name FROM msdb.dbo.backupset s ' +
    'INNER JOIN msdb.dbo.backupmediafamily f ON f.media_set_id = s.media_set_id ' +
    'WHERE s.database_name = N''{#DbName}'' ORDER BY s.backup_set_id DESC;' + #13#10 +
    'PRINT N''BACKUP_OK;'' + ISNULL(@ruta, N''' + Nombre + ''');' + #13#10, False);

  ProgresoBD.SetText('Haciendo la copia de seguridad de la base {#DbName}...', Nombre);
  if EjecutarSqlcmd(Instancia, Script, ArchivoLog, Codigo) and (Pos('BACKUP_OK;', LeerArchivo(ArchivoLog)) > 0) then
    ArchivoBackup := ValorMarcador(LeerArchivo(ArchivoLog), 'BACKUP_OK;')
  else
    Result := 'No se pudo hacer la copia de seguridad, no se modificó nada.' + #13#10#13#10 +
              FinDelLog(LeerArchivo(ArchivoLog)) + #13#10#13#10 + 'Detalle en: ' + ArchivoLog;
end;

function EjecutarScriptBase(const Instancia: String): String;
var
  Nombre, Script, Salida: String;
  Codigo: Integer;
  Ok: Boolean;
begin
  Result := '';
  if EsReinstalar() then Nombre := 'Update_TP_Ing_Soft_v2.sql' else Nombre := 'Deploy_TP_Ing_Soft_v1.sql';
  ExtractTemporaryFile(Nombre);
  Script := ExpandConstant('{tmp}\' + Nombre);
  LogBase := CarpetaLogs + '\' + ChangeFileExt(Nombre, '') + '_' + GetDateTimeString('yyyymmdd_hhnnss', #0, #0) + '.log';

  ProgresoBD.SetText('Ejecutando ' + Nombre + '...', 'Instancia: ' + Instancia);
  Ok := EjecutarSqlcmd(Instancia, Script, LogBase, Codigo);
  Salida := LeerArchivo(LogBase);
  if EsReinstalar() then
    Ok := Ok and (Pos('UPDATE_OK', Salida) > 0)
  else
    Ok := Ok and ((Pos('DEPLOY_OK', Salida) > 0) or (Pos('DEPLOY_OMITIDO', Salida) > 0));

  if not Ok then
    Result := 'Falló la preparación de la base de datos (' + Nombre + '). ' +
              'Los cambios se deshicieron y no se copió ningún archivo.' + #13#10#13#10 +
              FinDelLog(Salida) + #13#10#13#10 + 'Detalle completo en: ' + LogBase;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  Instancia: String;
begin
  Result := '';
  Instancia := Trim(cbInstancia.Text);
  ProgresoBD.Show();
  try
    ProgresoBD.SetProgress(0, 3);
    if InstanciaProbada <> Instancia then
      if not ProbarConexion(Instancia, True) then
      begin
        Result := 'No se pudo conectar con la instancia de SQL Server "' + Instancia + '".';
        Exit;
      end;
    ProgresoBD.SetProgress(1, 3);

    if EsReinstalar() and chkBackup.Checked then
    begin
      Result := HacerBackup(Instancia);
      if Result <> '' then Exit;
    end;
    ProgresoBD.SetProgress(2, 3);

    Result := EjecutarScriptBase(Instancia);
    ProgresoBD.SetProgress(3, 3);
  finally
    ProgresoBD.Hide();
  end;
end;

// -----------------------------------------------------------------------------
//  Después de copiar: cadena de conexión, versión visible y limpieza del MSI
// -----------------------------------------------------------------------------
procedure ActualizarCadenaConexion(const Archivo, Instancia: String);
var
  S, Nuevo: AnsiString;
  P, Q: Integer;
begin
  if not LoadStringFromFile(Archivo, S) then
  begin
    Log('No se pudo leer ' + Archivo);
    Exit;
  end;
  P := Pos('Data Source=', S);
  if P = 0 then Exit;
  P := P + Length('Data Source=');
  Q := P;
  while (Q <= Length(S)) and (S[Q] <> ';') and (S[Q] <> '"') do Q := Q + 1;
  // Se trabaja sobre los bytes del archivo (UTF-8 con BOM): solo cambia el valor de Data Source
  Nuevo := Copy(S, 1, P - 1);
  Nuevo := Nuevo + AnsiString(Instancia);
  Nuevo := Nuevo + Copy(S, Q, Length(S) - Q + 1);
  if not SaveStringToFile(Archivo, Nuevo, False) then
    MsgBox('No se pudo actualizar ' + Archivo + '. Edite a mano la instancia (Data Source) de la cadena de conexión.',
           mbError, MB_OK);
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  Codigo: Integer;
begin
  if CurStep = ssPostInstall then
  begin
    ActualizarCadenaConexion(ExpandConstant('{app}\{#AppExe}.config'), Trim(cbInstancia.Text));

    // En Programas y características se ve la versión realmente instalada
    RegWriteStringValue(HKLM, INNO_KEY, 'DisplayVersion', VersionInstalada(''));

    // La versión 1 instalada con el MSI viejo queda reemplazada: se quita en silencio
    // (el MSI solo tenía los archivos del programa; la base de datos no se toca)
    if EsReinstalar() and PreviaEsMsi then
      Exec(ExpandConstant('{sys}\msiexec.exe'), '/x {#MsiV1Code} /qn /norestart', '', SW_HIDE,
           ewWaitUntilTerminated, Codigo);
  end;
end;

procedure CurPageChanged(CurPageID: Integer);
var
  Texto: String;
begin
  if CurPageID = wpFinished then
  begin
    if EsReinstalar() then
      Texto := 'Se actualizó {#AppName} a la versión {#AppVersion} conservando los datos.'
    else
      Texto := 'Se instaló {#AppName} (sistema base, versión {#VersionV1}).';
    Texto := Texto + #13#10#13#10 + 'Base de datos: {#DbName} en ' + Trim(cbInstancia.Text);
    if ArchivoBackup <> '' then
      Texto := Texto + #13#10#13#10 + 'Copia de seguridad previa:' + #13#10 + ArchivoBackup;
    if EsReinstalar() then
      Texto := Texto + #13#10#13#10 + 'Los dígitos verificadores se recalculan solos en la primera operación ' +
               'que modifique datos.';
    Texto := Texto + #13#10#13#10 + 'Registro de la base de datos: ' + LogBase;
    WizardForm.FinishedLabel.Caption := Texto;
  end;
end;

// -----------------------------------------------------------------------------
//  Desinstalación: nunca borra la base
// -----------------------------------------------------------------------------
var
  InstanciaDesinstalada: String;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usUninstall then
  begin
    if not RegQueryStringValue(HKLM, '{#RegKey}', 'SqlInstance', InstanciaDesinstalada) then
      InstanciaDesinstalada := '';
  end
  else if (CurUninstallStep = usPostUninstall) and not UninstallSilent() then
    MsgBox('{#AppName} se desinstaló.' + #13#10#13#10 +
           'La base de datos {#DbName} NO se eliminó' +
           ' (instancia: ' + InstanciaDesinstalada + '). Si ya no la necesita, puede borrarla desde ' +
           'SQL Server Management Studio después de hacer un backup.', mbInformation, MB_OK);
end;
