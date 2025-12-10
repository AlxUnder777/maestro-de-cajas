Write-Host "============================================" -ForegroundColor Cyan
Write-Host "        DEPLOY PRO MAX POWERSELL 👑         " -ForegroundColor Cyan
Write-Host "============================================"
Write-Host "GitHub detectado: AlxUnder777" -ForegroundColor Yellow
Write-Host "============================================"


# =============================
# 0) VERIFICAR DEPENDENCIAS
# =============================
Write-Host "Verificando dependencias..." -ForegroundColor White

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host "❌ ERROR: .NET SDK no está instalado o no está en PATH." -ForegroundColor Red
    exit 1
}

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Host "❌ ERROR: Git no está instalado o no está en PATH." -ForegroundColor Red
    exit 1
}

Write-Host "✔ Dependencias correctas." -ForegroundColor Green


# =============================
# 1) PUBLICAR PROYECTO
# =============================
Write-Host "--------------------------------------------"
Write-Host "Publicando proyecto .NET en Release..." -ForegroundColor White

$publishResult = dotnet publish -c Release -o publish

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERROR: Falló la publicación del proyecto." -ForegroundColor Red
    exit 1
}

Write-Host "✔ Proyecto publicado correctamente." -ForegroundColor Green


# =============================
# 2) DETECTAR DLL PRINCIPAL
# =============================
Write-Host "Buscando DLL principal..." -ForegroundColor White

$dll = Get-ChildItem -Path "publish" -Filter "*.dll" | Select-Object -First 1

if (-not $dll) {
    Write-Host "❌ ERROR: No se encontró ningún DLL en /publish" -ForegroundColor Red
    exit 1
}

$dllName = $dll.Name

Write-Host "✔ DLL detectado: $dllName" -ForegroundColor Green


# =============================
# 3) CREAR DOCKERFILE
# =============================
Write-Host "Generando Dockerfile..." -ForegroundColor White

@"
# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "$dllName"]
"@ | Set-Content -Path "Dockerfile"

Write-Host "✔ Dockerfile creado." -ForegroundColor Green


# =============================
# 4) CREAR render.yaml
# =============================
Write-Host "Generando render.yaml..." -ForegroundColor White

@"
services:
  - type: web
    name: maestro-de-cajas
    runtime: docker
    plan: free
    autoDeploy: true
    envVars:
      ASPNETCORE_ENVIRONMENT: "Production"
"@ | Set-Content -Path "render.yaml"

Write-Host "✔ render.yaml creado." -ForegroundColor Green


# =============================
# 5) GIT: INIT, ADD, COMMIT, PUSH
# =============================
Write-Host "Configurando Git..." -ForegroundColor White

if (!(Test-Path ".git")) {
    git init | Out-Null
    Write-Host "✔ Repo Git inicializado." -ForegroundColor Green
} else {
    Write-Host "✔ Repo Git ya existente." -ForegroundColor Yellow
}

git add . | Out-Null
git commit -m "Deploy PRO MAX PowerShell" | Out-Null
git branch -M main | Out-Null

# Setear remoto a fuerza
git remote remove origin 2>$null
git remote add origin "https://github.com/AlxUnder777/maestro-de-cajas.git"

Write-Host "Subiendo a GitHub..." -ForegroundColor White
git push -u origin main

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERROR al hacer push a GitHub. Revisa PAT/credenciales." -ForegroundColor Red
    exit 1
}

Write-Host "✔ Push correcto." -ForegroundColor Green


# =============================
# 6) ABRIR RENDER AUTOMÁTICAMENTE
# =============================
Write-Host "Abriendo Render para finalizar deploy..." -ForegroundColor White
Start-Process "https://dashboard.render.com/select-repo?type=web"

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "🚀 DEPLOY POWERSELL FULL KING COMPLETADO 👑" -ForegroundColor Cyan
Write-Host "Selecciona tu repo en Render y tu app quedará online." -ForegroundColor White
Write-Host "============================================"
