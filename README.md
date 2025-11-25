# MoneyPay
MoneyPay/
├─ MoneyPay.sln            ← Solution 檔案，不是資料夾
├─ MoneyPayBackend.Test/   ← 後端測試專案資料夾
├─ backend/                ← ASP.NET Core 後端專案資料夾
├─ frontend/               ← Vue / React 前端專案資料夾
├─ README.md
└─ docker-compose.yml



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

MoneyPay/MoneyPayBackend.Test
# 1️⃣ 單元測試框架
dotnet add package xunit
# 2️⃣ 讓 VSCode / Visual Studio 或 dotnet test 可以辨識和執行 xUnit 測試
dotnet add package xunit.runner.visualstudio
# 3️⃣ 模擬物件 (Mock)，用來替代 Repository 或其他依賴，不用真的連資料庫
dotnet add package Moq


frontend point
emit prop
compute onmount
接API
{} 要return

小技術
CSS: grid
.container {
    display:grid;
    grid-template-columns: repeat(3,100px) => .container 會被切成 3 個直欄，每個欄寬 100px。
    grid-template-columns: 100px 200px auto; → .container 有三個欄位：第一欄固定 100px、第二欄固定 200px、第三欄自動填滿剩下空間
}
rf 單位
grid-template-columns: 1fr 1fr 1fr; => 1/3
grid-template-columns: 2fr 1fr 1fr; => 1/4


