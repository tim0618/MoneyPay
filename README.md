# MoneyPay
MoneyPay/
├─ backend/      ← ASP.NET Core 後端
├─ frontend/     ← Vue / React / HTML 前端
├─ README.md


MoneyPay/
# 1️⃣ 建立 Solution
dotnet new sln -n MoneyPay

# 2️⃣ 建立後端專案
dotnet new webapi -n MoneyPayBackend

# 3️⃣ 把後端專案加入 Solution
dotnet sln add MoneyPayBackend/MoneyPayBackend.csproj

# 4️⃣ 建立測試專案
dotnet new xunit -n MoneyPayBackend.Test

# 5️⃣ 把測試專案加入 Solution
dotnet sln add MoneyPayBackend.Test/MoneyPayBackend.Test.csproj

# 6️⃣ 讓測試專案可以引用後端
dotnet add MoneyPayBackend.Test reference MoneyPayBackend/MoneyPayBackend.csproj

# 7️⃣ 執行測試 (確認測試環境 OK)
dotnet test


